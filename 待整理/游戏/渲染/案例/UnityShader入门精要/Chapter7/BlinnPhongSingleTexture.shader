Shader "MyCustom/Chapter7/BlinnPhongSingleTexture"
{
    Properties
    {
        // 基础色调
        _Color ("Color Tint", Color) = (1, 1, 1, 1)
        // 2D纹理
        _MainTex ("Main Texture", 2D) = "white" {}
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
            fixed4 _Color;

            sampler2D _MainTex;
            float4 _MainTex_ST; // 得到_MainTex的缩放（Scale）和平移（Translation）属性，其中xy是缩放值，zw是偏移值

            fixed4 _Specular;
            float _Gloss;

            // 定义顶点函数输入和输出结构体
            struct a2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texcoord : TEXCOORD0; // 第一组纹理坐标
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float3 worldPos : TEXCOORD1; 
                float2 uv : TEXCOORD2;
            };

            v2f vert(a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.uv = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                // 等价于
                // o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed3 worldLight = normalize(UnityWorldSpaceLightDir(i.worldPos));
                fixed3 worldNormal = normalize(i.worldNormal);
                // 纹理反射率
                fixed3 aldedo = tex2D(_MainTex, i.uv).rgb * _Color.rgb;
                
                // 环境光
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * aldedo;
                // 漫反射
                fixed3 diffuse = _LightColor0.rgb * aldedo * saturate(dot(worldNormal, worldLight));
                
                fixed3 viewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
                fixed3 helfDir = normalize(worldLight + viewDir);
                // 计算高光反射
                fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(saturate(dot(worldNormal, helfDir)), _Gloss);

                fixed3 color = ambient + diffuse + specular;

                return fixed4(color.r, color.g, color.b, 1.0);
            }
            ENDCG
        }
    }

    FallBack "Specular"
}
