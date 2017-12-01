Shader "Hidden/PostProcessing/Extensions/LensRain"
{
    HLSLINCLUDE
        
        #include "/Assets/PostProcessing/PostProcessing/Shaders/StdLib.hlsl" // Post-processing standard library

        TEXTURE2D(_MainTex); // Texture asset
        SAMPLER2D(sampler_MainTex); // Sampler state

        sampler2D _RainTex; // Rain texture from rain camera target
        float _Strength; // Strength from effect UI
        float _Debug; // Debug from effect UI

        float4 Frag(VaryingsDefault i) : SV_Target
        {
            float tex = SAMPLE_TEXTURE2D(_RainTex, sampler_MainTex, i.texcoord).x; // Sample texture
			float n = SAMPLE_TEXTURE2D(_RainTex, sampler_MainTex, float2(i.texcoord.x, i.texcoord.y + 1.0 / _ScreenParams.y)).x; // Sample with north offset
			float s = SAMPLE_TEXTURE2D(_RainTex, sampler_MainTex, float2(i.texcoord.x, i.texcoord.y - 1.0 / _ScreenParams.y)).x; // Sample with south offset
			float e = SAMPLE_TEXTURE2D(_RainTex, sampler_MainTex, float2(i.texcoord.x + 1.0 / _ScreenParams.x, i.texcoord.y)).x; // Sample with east offset
			float w = SAMPLE_TEXTURE2D(_RainTex, sampler_MainTex, float2(i.texcoord.x - 1.0 / _ScreenParams.x, i.texcoord.y)).x; // Sample with west offset

if(_Debug == 1) // If debug is enabled
            return tex * _Strength; // Return

            float3 normal; // Declare normals vector
            normal.x		= s - n; // Get X
            normal.y		= w - e; // Get Y
            normal.z		= 0.05; // Get Z
            
            // Generate new UVs with distortion
            float2 uv		= float2(normal.x * _ScreenParams.z * _Strength + i.texcoord.x,
                                     normal.z * _ScreenParams.z * _Strength + i.texcoord.y);
            return tex2D(_MainTex, uv); // Return screen with distortion
        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }
    }
}