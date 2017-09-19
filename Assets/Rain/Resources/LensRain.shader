Shader "Hidden/PostProcessing/Extensions/LensRain"
{
    HLSLINCLUDE
        
        #include "/Assets/PostProcessing/PostProcessing/Shaders/StdLib.hlsl"

        TEXTURE2D(_MainTex);
        SAMPLER2D(sampler_MainTex);

        float _Strength;
        sampler2D _DropletNormal;

        float4 Frag(VaryingsDefault i) : SV_Target
        {
            float tex = SAMPLE_TEXTURE2D(_DropletNormal, sampler_MainTex, i.texcoord).x;
			float n = SAMPLE_TEXTURE2D(_DropletNormal, sampler_MainTex, float2(i.texcoord.x, i.texcoord.y + 1.0 / _ScreenParams.y)).x;
			float s = SAMPLE_TEXTURE2D(_DropletNormal, sampler_MainTex, float2(i.texcoord.x, i.texcoord.y - 1.0 / _ScreenParams.y)).x;
			float e = SAMPLE_TEXTURE2D(_DropletNormal, sampler_MainTex, float2(i.texcoord.x + 1.0 / _ScreenParams.x, i.texcoord.y)).x;
			float w = SAMPLE_TEXTURE2D(_DropletNormal, sampler_MainTex, float2(i.texcoord.x - 1.0 / _ScreenParams.x, i.texcoord.y)).x;

            float3 normal;
            normal.x		= s - n;
            normal.z		= w - e;
            normal.y		= 0.05;
            
            float2 uv		= float2(normal.x * _ScreenParams.z * _Strength + i.texcoord.x,
                                     normal.z * _ScreenParams.z * _Strength + i.texcoord.y);

            return tex2D(_MainTex, uv);
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