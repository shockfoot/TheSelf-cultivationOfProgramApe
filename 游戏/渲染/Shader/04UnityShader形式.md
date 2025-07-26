# 表面着色器

表面着色器是对顶点/片元着色器的抽象和封装，需要的代码量少，但渲染代价较大，其本质与顶点/片元着色器一样。

表面着色器存在的价值在于Unity封装了复杂操作如光照计算等，让开发者不去关注顶点程序和片元程序实现的细节。

## 表面着色器基本代码

Unity创建的着色器默认为表面着色器，包含4个材质属性（颜色、贴图、高光、金属度）、1个不含通道的SubShader块以及一个Fallback块。

``` ShaderLab
Shader "MyCustom/StandardSurfaceShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }

    FallBack "Diffuse"
}
```

表面着色器是被定义在SubShader块（而非Pass块）中的着色器代码块，因为其就是对顶点/片元着色器的封装，不需要开发者关心使用多少个通道、每个通道如何渲染，它会在编译时生成数个通道。

在SubShader块中有：

- RenderType值为Opaque的标签，指示渲染物体未不透明。
- LOD值为200。
- CGPROGRAM-ENDCG代码块，指示该代码块中使用了CG语法。
- 使用`#pragma surface <表面函数名> <光照模型> <可选参数>`编译指令指示这是一个表面着色器，并告诉编译器使用哪个函数作为表面函数，并将数据传递给该函数。
- - 表面函数名指示哪个CG函数有表面着色器代码。该函数应该具有`void surf (Input IN, inout SurfaceOutput o)`的形式。输入应该包含表面函数所需的任何纹理坐标和额外的自动变量。
- - 光照模型是要使用的照明模型。内置的有基于物理的Standard、StandardSpecular以及简单的非物理的Lambert（漫反射）和BlinnPhong（高光）。**自定义的光照模型函数必须以"Lighting函数名"为名，使用时不需要带Lighting。**
- - 可选参数见下表。
- #pragma target指示使用的Shader模型版本，不指示时默认使用2.0。
- `sampler2D _MainTex;`四句是对材质属性变量的声明。
- `struct Input { float2 uv_MainTex; };`描述输入的UV纹理坐标的结构体。输入结构通常有着色器需要的任何纹理坐标。**纹理坐标必须命名为“uv”，后跟纹理名称（或以“uv2”开头，以使用第二套纹理坐标）。**
- surf函数总是无返回值的，将需要的任何UV或数据作为输入，通过形参返回输出结构SurfaceOutput的结果。SurfaceOutput描述了表面的属性（反射颜色、法线、散射、高光等）。然后，表面着色器编译器会计算出需要哪些输入，填充哪些输出等等，并生成实际的顶点和像素着色器，以及处理向前和延迟渲染的渲染通道。在该函数中，使用UV坐标在纹理上进行采样，获取到颜色，并将该值赋给输出结果，同时赋值金属度、光滑度等结果。

``` CG
// 表面着色器的标准输出结构
struct SurfaceOutput
{
    fixed3 Albedo; // 漫反射颜色
    fixed3 Normal; // 切线空间的法线
    fixed3 Emission; // 自发光
    half Specular; // 0-1范围内的高光功率
    fixed Gloss; // 高光强度
    fixed Alpha; // 透明度
};

// 在Unity 5中，表面着色器也可以使用基于物理的光照模型。
// 内置标准输出结构
struct SurfaceOutputStandard
{
    fixed3 Albedo; // 基本的漫反射或镜面高光颜色
    fixed3 Normal; // 切线空间的法线
    half3 Emission; // 自发光
    half Metallic; // 0-1范围内的金属度
    half Smoothness; // 0-1范围内的光滑度
    half Occlusion; // 遮挡剔除（默认为1）
    fixed Alpha; // 透明度
};
// 内置标准高光输出结构
struct SurfaceOutputStandardSpecular
{
    fixed3 Albedo; // 漫反射颜色
    fixed3 Specular; // 镜面高光颜色
    fixed3 Normal; // 切线空间的法线
    half3 Emission; // 自发光
    half Smoothness; // 0-1范围内的光滑度
    half Occlusion; // 遮挡剔除（默认为1）
    fixed Alpha; // 透明度
};
```

# 顶点/片元着色器

相比于表面着色器，顶点/片元着色器更复杂，但灵活性也更高。

``` ShaderLab
Shader "MyCustom/VertexFragmentShader"
{
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            float4 vert(float4 v : POSITION) : SV_POSITION
            {
                return mul (UNITY_MATRIX_MVP, v);
            }

            fixed4 frag() : SV_Target
            {
                return fixed4(1.0, 0.0, 0.0, 1.0);
            }
            ENDCG
        }
    }
}
```

顶点/片元着色器是被定义在Pass中的着色器代码块，开发者需要自己定义每个通道使用的代码，灵活性更高，并且可以控制渲染的细节。

# 固定功能着色器

固定功能着色器是针对硬件能够执行的命令编写的着色器，主要用于不支持可编程管线的设备，功能有限但速度快、支持性好。

固定功能着色器

``` ShaderLab
Shader "MyCustom/FixedFunctionShader"
{
    Properties {
        _Color ("Main Color", Color) = (1, 1, 1, 1)
    }
    
    SubShader
    {
        Pass
        {
            Material
            {
                Diffuse[_Color]
            }
        }
    }
}
```

固定功能着色器必须在Pass块中编写，通常是对通道的一些渲染设置，只能使用ShaderLab的渲染设置命令编写。

> 在Unity 5.2中，固定功能着色器都会被Unity编译成对应的顶点/片元着色器，因此真正意义上的固定功能着色器已经不存在了。