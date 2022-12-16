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

        private readonly RTHandle _grabbedTextureHandle;
        private readonly string _grabbedTextureName;
        private readonly int _grabbedTexturePropertyId;

        private ScriptableRenderer _renderer;

        public GrabColorTexturePass(GrabTiming timing, string grabbedTextureName)
        {
            renderPassEvent = timing.ToRenderPassEvent();
            _grabbedTextureName = grabbedTextureName;
            _grabbedTextureHandle = RTHandles.Alloc(_grabbedTextureName, _grabbedTextureName);
            _grabbedTexturePropertyId = Shader.PropertyToID(_grabbedTextureName);
        }

        public void BeforeEnqueue(ScriptableRenderer renderer)
        {
            _renderer = renderer;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            cmd.GetTemporaryRT(_grabbedTexturePropertyId, cameraTextureDescriptor);
            cmd.SetGlobalTexture(_grabbedTextureName, _grabbedTextureHandle.nameID);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var cmd = CommandBufferPool.Get(nameof(GrabColorTexturePass));
            cmd.Clear();
            Blit(cmd, _renderer.cameraColorTarget, _grabbedTextureHandle.nameID);
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(_grabbedTexturePropertyId);
        }
    }
}
