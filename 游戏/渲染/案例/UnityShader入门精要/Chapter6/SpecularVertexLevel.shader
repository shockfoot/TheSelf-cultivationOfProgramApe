// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "MyCustom/Chapter6/SpecularVertexLevel"
{
    Properties
    {
        // 材质的漫反射颜色
        _Diffuse ("Diffuse", Color) = (1, 1, 1, 1)
        // 材质的高光反射颜色
        _Specular ("Specular", Color) = (1, 1, 1, 1)
        // 材质的光泽度
        _Gloss ("Gloss", Range(8.0, 256)) = 20
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
            fixed4 _Specular;
            float _Gloss;

            // 定义顶点函数输入和输出结构体
            struct a2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                fixed3 color : COLOR;
            };

            v2f vert(a2v v)
            {
                // 顶点着色器输出
                v2f o;
                // 将顶点坐标从模型空间转换到裁剪空间
                o.pos = UnityObjectToClipPos(v.vertex);

                // 环境光
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

                // 顶点法线从模型空间转换到世界空间
                fixed3 worldNormal = normalize(mul(v.normal, (float3x3)unity_WorldToObject));
                // 场景中只有一个平行光的光源的世界方向
                fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz);
                
                // 计算漫反射
                fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal, worldLight));
                
                // 反射方向
                fixed3 reflectDir = normalize(reflect(-worldLight, worldNormal));
                // 视觉方向
                fixed3 view = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz);

                // 计算高光反射
                fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(saturate(dot(view, reflectDir)), _Gloss);

                o.color = ambient + diffuse + specular;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return fixed4(i.color.r, i.color.g, i.color.b, 1.0);
            }
            ENDCG
        }
    }

    FallBack "Specular"
}
