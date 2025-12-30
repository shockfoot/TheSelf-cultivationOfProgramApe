// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyCustom/Chapter7/WorldSpaceNormalMap"
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
                float4 uv : TEXCOORD0;
                float4 TtWX : TEXCOORD1;
                float4 TtWY : TEXCOORD2;
                float4 TtWZ : TEXCOORD3;
            };

            v2f vert(a2v i)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(i.vertex);

                o.uv.xy = i.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                o.uv.zw = i.texcoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;

                float3 worldPos = mul(unity_ObjectToWorld, i.vertex).xyz;
                fixed3 worldNormal = UnityObjectToWorldNormal(i.normal);
                fixed3 worldTangent = UnityObjectToWorldDir(i.tangent.xyz);
                fixed3 worldBinormal = cross(worldNormal, worldTangent) * i.tangent.w;

                o.TtWX = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
                o.TtWY = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
                o.TtWZ = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 worldPos = float3(i.TtWX.w, i.TtWY.w, i.TtWZ.w);

                fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(worldPos));
                fixed3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));

                fixed3 bump = UnpackNormal(tex2D(_BumpMap, i.uv.zw));
                bump.xy *= _BumpScale;
                bump.z = sqrt(1.0 - saturate(dot(bump.xy, bump.xy)));
                bump = normalize(half3(dot(i.TtWX.xyz, bump), dot(i.TtWY.xyz, bump), dot(i.TtWZ.xyz, bump)));

                // 纹理反射率
                fixed3 albedo = tex2D(_MainTex, i.uv.xy).rgb * _Color.rgb;

                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;
                fixed3 diffuse = _LightColor0.rgb * albedo * saturate(dot(bump, worldLightDir));

                fixed3 halfDir = normalize(worldLightDir + worldViewDir);
                fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(saturate(dot(bump, halfDir)), _Gloss);

                fixed3 color = ambient + diffuse + specular;
                return fixed4(color.r, color.g, color.b, 1.0);

            }

            ENDCG
        }
     }

     FallBack "Specular"
}
