Shader "Liam/Fresnel"
{
    Properties
    {
        [MainColor] _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        [MainTexture] _BaseMap("Base Map", 2D) = "white" {}
        
        
        _RimColor ("Rim Color", Color) = (1,1,1,1)
        _RimPower ("Rim Power", Range(0.5, 8)) = 3
        _RimStrength ("Rim Strength", Range(0,1)) = 0.5
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            Name "Unlit"
            Tags {"LightMode" = "UniversalForward"}
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            //shadow stuff
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
            #pragma shader_feature_local _UVSET_UV0 _UVSET_UV1
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                
                float3 positionWS : TEXCOORD1;
                float3 normalWS : TEXCOORD2;
                
                float4 shadowCoord : TEXCOORD3;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
                float4 _BaseMap_ST;
                float4 _RimColor;
                float _RimPower;
                float _RimStrength;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                
                float3 posWS = TransformObjectToWorld(IN.positionOS.xyz);
                float3 nrmWS = TransformObjectToWorldNormal(IN.normalOS);
                
                OUT.positionWS = posWS;
                OUT.normalWS = nrmWS;
                OUT.positionHCS = TransformWorldToHClip(posWS);
                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
                OUT.shadowCoord = TransformWorldToShadowCoord(TransformObjectToWorld(IN.positionOS));
                
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half4 baseTex  = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv);
                half3 color = baseTex.rgb * _BaseColor.rgb;
                
                
                float3 viewDirWS = GetWorldSpaceViewDir(IN.positionWS);
                viewDirWS = SafeNormalize(viewDirWS);
                
                float3 n = SafeNormalize(IN.normalWS);
                float ndotv = saturate(dot(n, viewDirWS));
                float fres = pow(1.0 - ndotv, _RimPower);
                
                //shadow stuff
                Light mainLight = GetMainLight(IN.shadowCoord);
                float NdotL = saturate(dot(n, mainLight.direction));
                float3 diffuse = NdotL * mainLight.shadowAttenuation;
                float3 ambient = SampleSH(n) * _BaseColor.rgb;
                
                color *= diffuse + ambient;
                
                color += (_RimColor.rgb * fres) * _RimStrength;
                
                return half4(color, 1.0);
            }
            ENDHLSL
        }
    }
}
