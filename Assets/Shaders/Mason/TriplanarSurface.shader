// This shader fills the mesh shape with a color predefined in the code.
Shader "Custom/TriplanarSurface"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
        }

        Cull Off
        ZWrite On
        Pass
        {
            // The LightMode tag matches the ShaderPassName set in UniversalRenderPipeline.cs.
            // The SRPDefaultUnlit pass and passes without the LightMode tag are also rendered by URP
            Name "ForwardLit"
            Tags
            {
                "LightMode" = "UniversalForward"
            }

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            // This multi_compile declaration is required for the Forward rendering path
            #pragma multi_compile _ _ADDITIONAL_LIGHTS

            // This multi_compile declaration is required for the Forward+ rendering path
            #pragma multi_compile _ _FORWARD_PLUS

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RealtimeLights.hlsl"

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float3 normalOS     : NORMAL;
                float2 uv           : TEXCOORD;
            };

            struct Varyings
            {
                float4 positionCS  : SV_POSITION;
                float2 uv          : TEXCOORD;
                float3 positionWS  : TEXCOORD1;
                float3 normalWS    : TEXCOORD2;
            };

            sampler2D _MainTex;

            //
            // Mason: ported from https://www.youtube.com/watch?v=YwnVl2YHXBc
            //

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                OUT.positionWS = TransformObjectToWorld(IN.positionOS.xyz);
                OUT.positionCS = TransformWorldToHClip(OUT.positionWS);
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);

                float3 vertexWorldPos = mul(unity_ObjectToWorld, IN.positionOS).xyz;
                float3 anormal = abs(normalize(mul(unity_ObjectToWorld, IN.normalOS)));

                OUT.uv = lerp(vertexWorldPos.xy, vertexWorldPos.zy, anormal.x);
                OUT.uv = lerp(OUT.uv, vertexWorldPos.xz, anormal.y);

                return OUT;
            }

            float3 MyLightingFunction(float3 normalWS, Light light)
            {
                float NdotL = dot(normalWS, normalize(light.direction));
                NdotL = (NdotL + 1) * 0.5;
                return saturate(NdotL) * light.color * light.distanceAttenuation * light.shadowAttenuation;
            }

            // This function loops through the lights in the scene
            float3 MyLightLoop(float3 color, InputData inputData)
            {
                float3 lighting = 0;

                // Get the main light
                Light mainLight = GetMainLight();
                lighting += MyLightingFunction(inputData.normalWS, mainLight);

                // Get additional lights
                #if defined(_ADDITIONAL_LIGHTS)

                // Additional light loop including directional lights. This block is specific to Forward+.
                #if USE_FORWARD_PLUS
                UNITY_LOOP for (uint lightIndex = 0; lightIndex < min(URP_FP_DIRECTIONAL_LIGHTS_COUNT, MAX_VISIBLE_LIGHTS); lightIndex++)
                {
                    Light additionalLight = GetAdditionalLight(lightIndex, inputData.positionWS, half4(1,1,1,1));
                    lighting += MyLightingFunction(inputData.normalWS, additionalLight);
                }
                #endif

                // Additional light loop. The GetAdditionalLightsCount method always returns 0 in Forward+.
                uint pixelLightCount = GetAdditionalLightsCount();
                LIGHT_LOOP_BEGIN(pixelLightCount)
                    Light additionalLight = GetAdditionalLight(lightIndex, inputData.positionWS, half4(1,1,1,1));
                    lighting += MyLightingFunction(inputData.normalWS, additionalLight);
                LIGHT_LOOP_END

                #endif

                return color * lighting;
            }

            half4 frag(Varyings input) : SV_Target0
            {
                // The Forward+ light loop (LIGHT_LOOP_BEGIN) requires the InputData struct to be in its scope.
                InputData inputData = (InputData)0;
                inputData.positionWS = input.positionWS;
                inputData.normalWS = input.normalWS;
                inputData.viewDirectionWS = GetWorldSpaceNormalizeViewDir(input.positionWS);
                inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(input.positionCS);

                float3 surfaceColor = float3(1, 1, 1);
                float3 lighting = MyLightLoop(surfaceColor, inputData);

                half4 finalColor = tex2D(_MainTex, input.uv);
                finalColor *= half4(lighting, 1);

                return finalColor;
            }

            ENDHLSL
        }
    }
}