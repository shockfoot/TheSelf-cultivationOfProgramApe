// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "MyCustom/Chapter6/DiffusePixelLevel"
{
    Properties
    {
        // 材质的漫反射颜色
        _Diffuse ("Diffuse", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        pass
        {
            // 指明光照模式
            Tags { "LightMode" = "ForwardBase" }

            CGPROGRAM
            // 指定顶点着色器和片元着色器函数
            #pragma vertex vert
            #pragma fragment frag
            // 引用公共文件以使用光源相关变量
            #include "Lighting.cginc"
            // 声明使用属性的变量
            fixed4 _Diffuse;

            // 定义顶点函数输入和输出结构体
            struct a2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL; // 顶点法线
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0; // 传递顶点世界空间下的法线
            };

            v2f vert(a2v v)
            {
                // 顶点着色器输出
                v2f o;
                // 将顶点坐标从模型空间转换到裁剪空间
                o.pos = UnityObjectToClipPos(v.vertex);
                // 顶点法线从模型空间转换到世界空间
                o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
                
                return o;


            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 环境光
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
                // 顶点法线
                fixed3 worldNormal = normalize(i.worldNormal);
                // 场景中只有一个平行光的光源的世界方向
                fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz);
                // 计算漫反射：使用_LightColor0访问该通道处理的光源的颜色和强度（想要得到正确的值需要定义合适的LightMode标签）
                fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal, worldLight));

                fixed3 color = ambient + diffuse;

                return fixed4(color.r, color.g, color.b, 1.0);
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
