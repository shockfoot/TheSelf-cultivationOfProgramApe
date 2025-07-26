Shader "MyCustom/Chapter6/SpecularPixelLevel"
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
                float3 worldNormal : TEXCOORD0;
                float3 worldPos : TEXCOORD1; 
            };

            v2f vert(a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 环境光
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

                fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz);
                fixed3 worldNormal = normalize(i.worldNormal);
                // 计算漫反射
                fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal, worldLight));
                
                fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos);
                fixed3 reflectDir = normalize(reflect(-worldLight, worldNormal));
                // 计算高光反射
                fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(saturate(dot(viewDir, reflectDir)), _Gloss);

                fixed3 color = ambient + diffuse + specular;

                return fixed4(color.r, color.g, color.b, 1.0);
            }
            ENDCG
        }
    }

    FallBack "Specular"
}
