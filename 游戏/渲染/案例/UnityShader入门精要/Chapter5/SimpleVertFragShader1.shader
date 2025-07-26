// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyCustom/Chapter5/SimpleVertFragShader1"
{
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // 使用结构体定义顶点函数输入以获取更多模型数据
            struct a2v
            {
                float4 vertex : POSITION; // 顶点坐标
                float3 normal : NORMAL; // 法线方向
                float4 texcoord : TEXCOORD0; // 第一套纹理坐标
            };

            float4 vert (a2v v) : SV_POSITION
            {
                return UnityObjectToClipPos (v.vertex);
            }

            fixed4 frag () : SV_Target
            {
                return fixed4(1.0, 1.0, 1.0, 1.0);
            }
            ENDCG
        }
    }
}
