using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace URPGrabPass.Runtime
{
    /// <summary>
    ///     Path that grabs the color texture of the camera.
    /// </summary>
    public class GrabColorTexturePass : ScriptableRenderPass
    {
        private readonly string _grabbedTextureName;

        private RenderTargetIdentifier _cameraColorTarget;
        private RenderTargetHandle _grabbedTextureHandle;

        public GrabColorTexturePass(GrabTiming timing, string grabbedTextureName)
        {
            renderPassEvent = timing.ToRenderPassEvent();
            _grabbedTextureName = grabbedTextureName;
            _grabbedTextureHandle.Init("_GrabbedTexture");
        }

        public void BeforeEnqueue(RenderTargetIdentifier cameraColorTarget)
        {
            _cameraColorTarget = cameraColorTarget;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            cmd.GetTemporaryRT(_grabbedTextureHandle.id, cameraTextureDescriptor);
            cmd.SetGlobalTexture(_grabbedTextureName, _grabbedTextureHandle.Identifier());
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var cmd = CommandBufferPool.Get(nameof(GrabColorTexturePass));
            cmd.Clear();
            Blit(cmd, _cameraColorTarget, _grabbedTextureHandle.Identifier());
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(_grabbedTextureHandle.id);
        }
    }
}