Shader "Demo/UseGrabPass"
{
    Properties
    {
        _RelativeRefractionIndex("Relative Refraction Index", Range(0.0, 1.0)) = 0.67
        [PowerSlider(5)]_Distance("Distance", Range(0.0, 100.0)) = 10.0
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent" "RenderPipeline" = "UniversalRenderPipeline"
        }

        Pass
        {
            Tags
            {
                // Specify LightMode correctly.
                "LightMode" = "UseColorTexture"
            }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                half3 normal : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                half4 grabPos : TEXCOORD1;
                half2 samplingViewportPos : TEXCOORD2;
            };

            sampler2D _GrabbedTexture;
            float _RelativeRefractionIndex;
            float _Distance;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                float3 positionW = TransformObjectToWorld(IN.positionOS);
                half3 normalW = TransformObjectToWorldNormal(IN.normal);

                half3 viewDir = normalize(positionW - _WorldSpaceCameraPos.xyz);
                half3 refractDir = refract(viewDir, normalW, _RelativeRefractionIndex);
                half3 samplingPos = positionW + refractDir * _Distance;
                half4 samplingScreenPos = mul(UNITY_MATRIX_VP, half4(samplingPos, 1.0));
                OUT.samplingViewportPos = (samplingScreenPos.xy / samplingScreenPos.w) * 0.5 + 0.5;
                #if UNITY_UV_STARTS_AT_TOP
                OUT.samplingViewportPos.y = 1.0 - OUT.samplingViewportPos.y;
                #endif

                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                return tex2D(_GrabbedTexture, IN.samplingViewportPos);
            }
            ENDHLSL
        }
    }
}