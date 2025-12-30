// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyCustom/Chapter5/SimpleVertFragShader2"
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

            // 使用结构体定义顶点函数输出以传给片元着色器
            struct v2f
            {
                float4 pos : SV_POSITION; // 顶点坐标
                fixed3 color : COLOR0; // 颜色
            };

            v2f vert (a2v v)
            {
                // 声明输出结构
                v2f rel;
                rel.pos = UnityObjectToClipPos (v.vertex);
                // v.normal法线方向范围在[-1,1]，将分量范围映射到[0,1]之间并存储到rel.color中传给片元着色器
                rel.color = v.normal * 0.5 + fixed3(0.5, 0.5, 0.5);
                return rel;
            }

            fixed4 frag (v2f f) : SV_Target
            {
                // 将插值后的颜色显示在屏幕上
                return fixed4(f.color, 1.0);
            }
            ENDCG
        }
    }
}
