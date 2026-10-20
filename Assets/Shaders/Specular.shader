Shader "Custom/Specular"
{
    Properties
    {
        [MainColor] _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        [MainTexture] _BaseMap("Base Map", 2D) = "white" {}
        _DiffuseMultiplier("Diffuse Multiplier", Float) = 1
        _SpecColor ("Specular Color", Color) = (1,1,1,1)
        _SpecMultiplier("Specular Multiplier", Float) = 1
        _Metallic("Metallic", Range(0,1)) = 0
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 normalWS : TEXCOORD1;
                float2 uv : TEXCOORD0;
                float4 shadowCoord :TEXCOORD2;
                float3 viewDirWS : TEXCOORD3;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
                float4 _BaseMap_ST;
                float _DiffuseMultiplier;
                float _Metallic;
                float _SpecMultiplier;
                half4 _SpecColor;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.shadowCoord = TransformWorldToShadowCoord(TransformObjectToWorld(IN.positionOS));
                float3 worldPosWS = TransformObjectToWorld(IN.positionOS.xyz);
                OUT.viewDirWS = normalize(GetCameraPositionWS() - worldPosWS);
                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                Light mainLight = GetMainLight(IN.shadowCoord);
                float3 normalWS = normalize(IN.normalWS);
                float NdotL = saturate(dot(normalWS, mainLight.direction));
                
                float3 diffuse = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv) * _BaseColor * NdotL * _DiffuseMultiplier * mainLight.shadowAttenuation;
                float3 ambient = SampleSH(normalWS) * _BaseColor.rgb;
                
                //specular stuff
                half3 reflectDir = reflect(-mainLight.direction, normalWS);
                half3 viewDir = normalize(IN.viewDirWS);
                half specFactor = pow(saturate(dot(reflectDir, viewDir)), _SpecMultiplier);
                half3 specular = _SpecColor.rgb * specFactor;
                
                
                half3 color = diffuse + specular + ambient;
                return half4(color,1);
            }
            ENDHLSL
        }
    }
}
