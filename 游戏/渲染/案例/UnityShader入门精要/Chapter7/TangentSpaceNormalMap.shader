// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyCustom/Chapter7/TangentSpaceNormalMap"
{
    Properties
    {
        _Color ("Color Tint", Color) = (1, 1, 1 ) // 主色调
        _MainTex ("Main Texture", 2D) = "white" { } // 主纹理
        _BumpMap ("Normal Map", 2D) = "bump" { } // 法线纹理
        _BumpScale ("Bump Scale", Range(-1.0, 1.0)) = 1.0 // 凹凸程度
        _Specular ("Specular", Color) = (1, 1, 1 ) // 高光反射颜色
        _Gloss ("Gloss", Range(8.0, 256)) = 20 // 光泽度
     }

     SubShader
     {
        Pass
        {
            Tags { "LightMode" = "ForwardBase" }

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Lighting.cginc"

            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _BumpMap;
            float4 _BumpMap_ST;
            float _BumpScale;
            fixed4 _Specular;
            float _Gloss;

            struct a2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float4 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 uv : TEXCOORD0; // 存储两张纹理的UV坐标
                float3 lightDir : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
            };

            v2f vert(a2v i)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(i.vertex);

                o.uv.xy = i.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                o.uv.zw = i.texcoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;

                // 副法线
                float3 binormal = cross(normalize(i.normal), normalize(i.tangent)) * i.tangent.w;
                float3x3 rotation = float3x3(i.tangent.xyz, binormal, i.normal);
                // 等价于TANGENT_SPACE_ROTATION;

                o.lightDir = mul(rotation, ObjSpaceLightDir(i.vertex)).xyz;
                o.viewDir = mul(rotation, ObjSpaceViewDir(i.vertex)).xyz;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed3 tangentLightDir = normalize(i.lightDir);
                fixed3 tangentViewDir = normalize(i.viewDir);

                // 采样法线纹理
                fixed4 packedNormal = tex2D(_BumpMap, i.uv.zw);
                fixed3 tangentNormal;

                // 如果纹理不是法线纹理类型需要手动映射
                // tangentNormal.xy = (packedNormal.xy * 2 - 1) * _BumpScale;
                // tangentNormal.z = sqrt(1.0 - saturate(dot(tangentNormal.xy, tangentNormal.xy)));

                // 如果是法线纹理需要Unity映射
                tangentNormal = UnpackNormal(packedNormal);
                tangentNormal.xy *= _BumpScale;
                tangentNormal.z = sqrt(1.0 - saturate(dot(tangentNormal.xy, tangentNormal.xy)));

                // 纹理反射率
                fixed3 albedo = tex2D(_MainTex, i.uv.xy).rgb * _Color.rgb;

                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;
                fixed3 diffuse = _LightColor0.rgb * albedo * saturate(dot(tangentNormal, tangentLightDir));

                fixed3 halfDir = normalize(tangentLightDir + tangentViewDir);
                fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(saturate(dot(tangentNormal, halfDir)), _Gloss);

                fixed3 color = ambient + diffuse + specular;
                return fixed4(color.r, color.g, color.b, 1.0);

            }

            ENDCG
        }
     }

     FallBack "Specular"
}
