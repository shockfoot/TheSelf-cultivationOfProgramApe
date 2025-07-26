Unity中的着色器总是由ShaderLab的语法编写。ShaderLab是Unity定制的专门用于编写着色器的语法方案，是一种在着色器源文件中使用的声明性语言，使用大括号语法描述着色器对象。

在ShaderLab中可以定义很多内容，常见的有：

- 着色器对象的整体结构：着色器Shader块、属性Properties块、子着色器SubShader块、通道Pass块、后备着色器Fallback等。
- 在代码块中添加使用CG或HLSL编写的着色器程序。
- 在执行着色器程序或执行涉及另一个通道的操作之前，使用命令设置GPU的渲染状态。

# Shader块

使用`Shader`关键字定义一个着色器对象。`Shader`关键字后使用*菜单路径*的方式定义该着色器对象的名称，用于在Inspector面板为材质设置Shader。

在Shader块中，可以：

- 使用Properties块定义材质属性，可选。
- 在Fallback块分配后备着色器对象，可选。
- 使用CustonmEditor块或CustomEditorForRenderPipeline块分配自定义编辑器，可选。
- 在SubShader块中定义一或多个子着色器，必须。

``` ShaderLab
Shader "<菜单路径/名称>"
{
    // Shader块
}
```

# Properties块

在Shader块中使用`Properties`关键字定义材质属性。材质属性是Unity中存储的材质资产的一部分，可使开发者创建、编辑、共享不同配置的材质。**Properties块是可选的。** 

材质属性不仅可以通过调用函数获取或设置，还可以在Inspector面板进行查看和编辑，并且支持持久化。对于着色器对象中的非材质属性的变量，仍可以通过调用函数获取或设置，但这些变量不支持可视化编辑和持久化。

``` ShaderLab
Shader "MyShader/ShaderStruct"
{
    Properties
    {
        // 属性声明
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
}
```

在Properties块中可以声明任意数量的材质属性。在Shader代码中，材质属性名通常以'_'开头。所有的材质属性声明都遵循格式：

`[可选的: 特性] 材质属性名("Inspector显示名称", 类型) = 默认值`

**常用材质属性类型**如下：

| 类型 | 语法用例 | 说明 |
| --- | --- | --- |
| Integer | `_ExampleName ("IntegerName", Integer) = 1` | 基于实整数的整型。 |
| Int（不支持） | `_ExampleName ("IntName", Int) = 1` | 基于浮点数的整型。 |
| Float | `_ExampleName ("FloatName", Float) = 0.5` | 没有范围的浮点数。 |
| Float | `_ExampleName ("FloatName", Range(0.0, 1.0)) = 0.5` | 可以取到范围边界值。 |
| Texture2D | `_ExampleName ("Texture2DName", 2D) = "red" {}` | 可以指定"white"、"black"、"gray"和"red"等Unity内置纹理。如果使用了非法或空字符串，默认为"gray"。这些内置纹理在Inspector面板不可见。 |
| Texture2DArray | `_ExampleName ("Texture2DArrayName", 2DArray) = "" {}` | 2D纹理数组，是相同大小/格式/标志的2D纹理的集合。2D纹理数组的元素也被称为切片或层。 |
| Texture3D | `_ExampleName ("Texture3DName", 3D) = "" {}` | 默认为"gray"。 |
| Cubemap | `_ExampleName ("CubemapName", Cube) = "" {}` | 一个由6个正方形纹理组成的立方体贴图，可以代表环境中的反射（可用于天空盒）。这六个纹理构成了一个假想的立方体，这个立方体包围着一个物体；每个面代表沿世界轴方向(上、下、左、右、前、后)的视图。默认为"gray"。 |
| CubemapArray | `_ExampleName ("CubemapArrayName", CubeArray) = "" {}` | 大小和格式相同的立方体贴图的数组，GPU可以将其作为单个纹理资源访问。立方体映射阵列常用于实现高效的反射探测、照明和阴影系统。 |
| Color | `_ExampleName ("ColorName", Color) = (1, 1, 1, 1)` | 映射到一个4浮点数的结构体，表示RGBA颜色，在Inpsectot面板显示为一个颜色选择器。如果希望将值显示为四个独立的浮点数，可使用Vector类型。 |
| Vector | `_ExampleName ("VectorName", Vector) = (1, 1, 1, 1)` | 映射到一个4浮点数的结构体，在Inpsectot面板显示为四个独立的浮动字段。如果希望显示为颜色选择器，可使用Color类型。 |

Unity为一些材质属性保留了名称。当有属性使用这些名称时，Unity会执行预定义的操作。除非打算使用这些功能，否则不要使用保留名称。

| 名称 | 语法用例 | 说明 |
| --- | --- | --- |
| _TransparencyLM | `_TransparencyLM ("Transmissive Texture", 2D) = "white" {}` | 在光照映射期间启用自定义RGB透明度。 |

材质属性声明可以应用**特性**，以告诉Unity如何处理它们。

| 特性 | 说明 |
| --- | --- |
| `[Gamma]` | 指示浮动或矢量属性使用sRGB值，这意味着如果项目中的色彩空间需要这样做，则必须将其与其他sRGB值一起转换。 |
| `[HDR]` | 指示纹理或颜色属性使用高动态范围（HDR）值。 |
| `[HideInInspector]` | 在Inpsectot面板中隐藏这个属性。 |
| `[MainTexture]` | 设置材质的主纹理，可以使用`Material.maintexture`访问。默认情况下，Unity会将属性名为_MainTex的纹理作为主纹理。如果你的纹理有不同的属性名但希望Unity将其视为主纹理，则使用此特性。多次使用此属性，Unity会使用第一个并忽略后面的。当使用此特性设置主纹理时，在使用纹理流调试视图模式或自定义调试工具时，纹理在游戏视图中不可见。 |
| `[MainColor]` | 设置材质的主颜色，可以使用`Material.color`来访问。相似地，Unity将属性名为_Color的颜色视为主颜色。如果你的颜色有不同的属性名但想让Unity将其视为主颜色，则使用此特性。多次使用此属性，Unity会使用第一个并忽略后面的。 |
| `[NoScaleOffset]` | 告诉Unity编辑器隐藏这个纹理属性的平铺和偏移字段。 |
| `[Normal]` | 指示纹理属性需要法线映射。如果分配了一个不兼容的纹理，Unity编辑器会显示一个警告。 |
| `[PerRendererData]` | 表明纹理属性将以MaterialPropertyBlock的形式来自per-renderer数据。Inpsectot面板将这些属性显示为只读。 |
| `[Header(标题文本)]` | 标题。 |
| `[Space(间距)]` | 设置此属性之前的间距。 |
| `[IngRange]` | 整数滑块。 |
| `[PowerSlider]` | 非线性增长的滑块。 |
| `[Toggle]` | 是否启用指定的Keyword，Keyword命名规则为变量名的大写形式_ON。 |
| `[Toggle(Keyword)]` | 是否启用指定的Keyword，需要填写代码中声明的Keyword。 |
| `[Enum(枚举1, 值1, 枚举2, 值2,...)]` | 枚举，根据枚举设定变量值。 |
| `[KeywordEnum(枚举1, 枚举2,...)]` | Keyword枚举，切换该变量所代表的Keyword。Keyword命名规则为变量名的大写形式_枚举的大写形式。这些Keyword无法被Enum使用。 |

除了这里列出的特性，还可以使用相同的语法用`MaterialPropertyDrawer`可以控制材质属性在Inpsectot面板中的显示方式。对于更复杂的需求，可以使用`MaterialEditor`、`MaterialProperty`和`ShaderGUI`类实现。

材质属性在C#代码中由`MaterialProperty`类表示。可以通过`Material`类的API如`GetFloat`、`SetFloat`等读取着色器中的变量而不管其是不是材质属性。

# Fallback块

在Shader块中使用`Fallback`关键字定义后备着色器对象，用于指定找不到兼容的着色器时要使用的后备着色器对象名或指定Off以禁用后备着色器对象。**Fallback块是可选的。**

``` ShaderLab
Shader "MyShader/ShaderStruct"
{
    Fallback "ExampleOtherShader"或Off
}
```

> 常用内置着色器
> - Unlit不发光：只是一个纹理，不被任何光照影响，常用于UI。
> - VertexLit顶点光照。
> - Diffuse漫反射。
> - Normal Mapped法线贴图：增加了一个或更多纹理（法线贴图）和几个着色器结构。
> - Specular高光：增加了特殊的高光计算。
> - Normal Mapped Specular高光法线贴图。
> - Parallax Normal Mapped视差法线贴图：增加了视差法线贴图计算。
> - Parallax Normal Mapped Specular视差高光法线贴图：增加了视差法线贴图和高光计算。

# CustomEditor块

在Shader块中可以使用`CustomEditor`或`CustomEditorForRenderPipeline`关键字指定自定义编辑器，用于在Inspector面板中改变着色器对象的显示或自定义控件和数据验证。**自定义编辑器是可选的。**

`CustomEditor `关键字用于使用指定的自定义编辑器，而`CustomEditorForRenderPipeline`关键字用于在指定渲染管线下使用指定的自定义编辑器。`CustomEditorForRenderPipeline`关键字优先于`CustomEditor `关键字。

``` ShaderLab
Shader "MyShader/ShaderStruct"
{
    CustomEditor "自定义编辑器类名"
    CustomEditorForRenderPipeline "自定义编辑器类名" "渲染管线名"
}
```

需要注意的时，指定的自定义编辑器类必须要继承`ShaderGUI`，且位于Assets下的Editor文件夹中。

# SubShader块

在Shader块中使用`SubShader`关键字定义子着色器。**子着色器是必须的，着色器对象中至少需定义一个子着色器。**

着色器可以包含多个子着色器。多个子着色器可以为不同的硬件定义不同的GPU设置和着色程序、渲染管道以及运行时设置。

在SubShader块中，可以：

- 在Tags块中使用键值对设置标签，可选。
- 在LOD块中设置LOD（Level of Detail）值，可选。
- 定义一个或多个Pass通道，必须。
- 使用ShaderLab命令添加GPU指令或着色器代码，可选。
- 使用PackagerRequirements块指定必须的包，可选。

``` ShaderLab
Shader "MyShader/ShaderStruct"
{
    SubShader
    {
        // 子着色器
    }
}
```

## Tags块

**标签是一组键值对。** Unity使用预定义的键和值来确定如何以及何时使用子着色器，或者可以使用自定义值创建自定义标签并从C#代码中访问它们（`Material.GetTag`API）。

在SubShader块中使用`Tags`关键字定义子着色器标签。**Tags块是可选的，标签数量任意。**

``` ShaderLab
Shader "MyShader/ShaderStruct"
{
    SubShader
    {
        Tags { "RenderPipeline" = "UniversalRenderPipeline", "Queue" = "Transparent" }
    }
}
```

**RenderPipeline标签**用于告诉Unity子着色器是否与通用渲染管道（URP）或高清晰度渲染管道（HDRP）兼容，其值可为：

- UniversalRenderPipeline：兼容URP。
- HighDefinitionRenderPipeline：兼容HDRP。
- 其他任何值或未设置：不与URP或HDRP兼容。

**Queue标签**用于告诉Unity渲染几何体时使用哪个渲染队列，**值越小越先渲染**。渲染队列是决定Unity渲染几何图形顺序的因素之一。有两种方式使用Queue标签：使用一个命名的渲染队列，或者在指定队列的给定偏移量（偏移量为整数）处的未命名队列。内置队列名有：

- Background：背景。
- Geometry：几何。
- AlphaTest：Alpha测试。
- Transparent：透明。
- Overlay：叠加。

**RenderType标签**用于指定当前子着色器的RenderType值，以便在其他地方标识该着色器，用于被覆盖。

**ForceNoShadowCasting标签**用于阻止几何体投射（有时接收）阴影。值可为True或False（默认）。

**DisableBatching标签**用于阻止Unity应用动态批处理到使用这个子着色器的几何体。值可为True、False（默认）或LODFading。

**IgnoreProjector标签**用于内置渲染管道，告诉Unity几何体是否忽视Projector组件的影响。这对于排除半透明几何体非常有用，因为Projector组件与半透明几何体不兼容。其他渲染管道中不起作用。值可为True或False（默认）。

**PreviewType标签**用于告诉Unity编辑器如何在Inpsector面板中显示使用此着色器的材质。值可为Sphere（默认）、Plane或Skybox。

**CanUseSpriteAtlas标签**用于在使用Legacy Sprite Packer的项目中指示该着色器是否依赖于原始纹理坐标，因此是否应该将其纹理打包到图集中。值可为True（默认）、False。

可以使用`Material.GetTag`的API从C#脚本中获取子着色器标签。可以使用`Shader.renderQueue`读取Shader对象中正在使用的子着色器的Queue标签。

## LOD块

在SubShader块中使用`LOD`关键字定义子着色器LOD值，表示对计算的要求。**LOD块是可选的。**

``` ShaderLab
Shader "MyShader/ShaderStruct"
{
    SubShader
    {
        LOD 100
    }
}
```

在运行时，可以设置单个着色器或用于所有着色器的LOD值。Unity然后优先考虑具有较低LOD值的子着色器。着色器LOD与相机的距离无关，Unity不会自动计算着色器LOD。必须手动设置最大着色器LOD。

> **注意**：在Shader块中，子着色器必须按LOD降序排列.这是因为Unity选择它找到的第一个有效子着色器，所以如果它首先找到一个LOD较低的子着色器，它将始终使用之。

使用这种技术可以在不同的硬件上微调着色器的性能。当用户的硬件理论上支持某子着色器，但硬件不能很好地运行它时，这是有用的。

要为给定的着色器对象设置着色器LOD，可以使用`Shader.maximumLOD`。要为所有着色器对象设置着色器LOD，可以使用`Shader.globalMaximumLOD`。缺省情况下，没有最大LOD。

Unity内置渲染管线中，Unity内置着色器的LOD值有100（有Unlit/Texture、Unlit/Color、Unlit/Transparent、Unlit/Transparent Cutout）、300（有Standard、Standard (Specular Setup)、Autodesk Interactive）两个级别，内置不支持（Legacy）的有100、150、200、250、300、400、500、600等级别。

# Pass块

在SubShader块中使用`Pass`关键字定义通道。为着色器对象不同部分定义单独的通道可实现不同的工作方式。**通道是必须的，子着色器中至少需定义一个通道。**

通道是着色器对象的基本元素，包含设置GPU状态的指令以及在GPU上运行的着色器程序。每个通道定义了一次完整的渲染流程。**当通道数量过多时会造成渲染性能的下降，因此应尽量使用最小数量的通道。**

在Pass块中，可以：

- 使用Name块指定通道名称，可选。
- 在Tags块中使用键值对设置标签，可选。
- 使用ShaderLab命令执行操作，可选。
- 使用着色器代码块添加着色器代码，可选。
- 使用PackagerRequirements块指定必须的包，可选。

``` ShaderLab
Shader "MyShader/ShaderStruct"
{
    SubShader
    {
        Pass
        {
            // 通道
        }
    }
}
```

## Name块

在Pass块中使用`Name`关键字定义通道名称。**通道名称是可选。**

``` ShaderLab
Shader "MyShader/ShaderStruct"
{
    SubShader
    {
        Pass
        {
            Name "NewPass"
        }
    }
}
```

通道名称通常用于`UsePass`命令或在脚本中通过API按名称引用通道。**在内部，Unity将通道名称转换为大写，因此在ShaderLab代码中引用通道名称时必须使用大写变体。** 如果一个子着色器中具有多个同名通道，则使用第一个。

## Tags块

在Pass块中也可以使用`Tags`关键字定义通道标签。**Tags块是可选的，标签数量任意。** 同样的，Unity使用预定义的键和值来确定如何以及何时渲染给定的通道，且可以使用自定义值创建自定义标签并从C#代码中访问它们。

``` ShaderLab
Shader "MyShader/ShaderStruct"
{
    SubShader
    {
        Pass
        {
            Tags { "LightMode" = "Always" }
        }
    }
}
```

**LightMode标签**用于确定是否在给定帧期间执行该通道、在该帧期间何时执行该通道，以及输出执行哪些操作。**此标签的有效值取决于渲染管线。**

内置渲染管线中的有效值有：

- Aways：始终渲染，不应用任何光照（默认）；
- ForwardBase：在前向渲染中使用，应用环境光、主方向光、顶点/SH光源和光照贴图；
- ForwardAdd：在前向渲染中使用，应用附加的每像素光源（每个光源有一个通道）；
- Deferred：在延迟渲染中使用，渲染G缓冲区；
- ShadowCaster：将对象深度渲染到阴影贴图或深度纹理中；
- MotionVectors：用于计算每个对象的运动矢量；
- Vertex：用于旧版顶点光照渲染（当对象不进行光照贴图时），应用所有顶点光源；
- VertexLMRGBM：用于旧版顶点光照渲染（当对象不进行光照贴图时），以及光照贴图为RGBM编码的平台（PC和游戏主机）；
- VertexLM：用于旧版顶点光照渲染（当对象不进行光照贴图时），以及光照贴图为LDR编码的平台（移动平台）；
- Meta：在常规渲染期间不使用，仅用于光照贴图烘焙或光照实时全局照明。

**PassFlags标签**用于指定Unity提供给通道的数据，值只能为"OnlyDirectional"，并且只能在内置渲染管线且渲染路径为"Forward"、LightMode标签为"ForwardBase"的通道中有效，此时Unity只为该通道提供主方向光和环境光/光照探针数据。这意味着非重要光源的数据将不会传递到顶点光源或球谐函数着色器变量。

**RequireOptions标签**用于启用或禁用一个通道，值只能为"SoftVegetation"，仅当项目的质量设置QualitySettings中的SoftVegetation被启用时才渲染此通道。

# 着色器代码块

针对不同的着色器编程语言，在SubShader块（表面着色器）或Pass块（顶点/片元着色器和固定功能着色器）中使用不同的关键字定义着色器代码块，不同关键字及其兼容性如下：

| 关键字 | 内置渲染管线 | URP | HDRO | SRP |
| --- | --- | --- | --- | --- |
| HLSLPROGRAM | 是 | 是 | 是 | 是 |
| HLSLINCLUDE | 是 | 是 | 是 | 是 |
| CGPROGRAM | 是 | 否 | 否 | 是（与使用SPR Core包的自定义渲染管线不兼容） |
| CGINCLUDE | 是 | 否 | 否 | 是（与使用SPR Core包的自定义渲染管线不兼容） |

``` ShaderLab
Shader "MyShader/ShaderStruct"
{
    SubShader
    {
        Pass
        {
            CGPROGRAM
                // CG代码
            ENDCG

            CGINCLUDE
                // 要共享的CG代码
            ENDCG
        }
    }
}
```

- CG代码块是旧版本使用的，使用ENDCG闭合，默认情况下引用了Unity内置的代码库，这些代码库仅兼容内置渲染管线，使用很方便。
- HLSL代码块是更新的支持，使用ENDHLSL闭合，默认情况下没有引用Unity内置的着色器文件，因此必须手动引用需要使用的代码库，与任何渲染管线兼容。

- PROGRAM代码块是着色器代码块，可以在其中编写对应着色器编程语言的代码。PROGRAM代码块可以放在Pass块、SubShader块中。
- INCLUDE代码块是着色器代码库引用块，该块中的代码可以在同一源文件中共享，其原理与在代码块中使用Include的方式类似。INCLUDE代码块可以放在Pass块、SubShader块、Shader块中。

# PackageRequirements块

某些着色器需要同时支持多个渲染管线，使用PackageRequirement块可以避免在着色器中使用未安装的包而出现的编译错误。

可以在SubShader块或Pass块中使用`PackageRequirements`关键字声明包要求，**每个SubShader块或Pass块中最多只能包含一个PackageRequirements块，并且必须位于其他块之前**，但每个PackageRequirements块中可以有多个要求声明。

``` ShaderLab
Shader "MyShader/ShaderStruct"
{
    SubShader
    {
        PackageRequirements
        {
            // 要求声明
        }
    }
}
```

要求声明有多种语法形式：

- `"包名"`：不限版本的包。
- `"包名": "版本"`：指定版本的包。
- `"包名": "unity=版本"`：指定Unity版本的包。
- `"unity": "版本"`：指定Unity版本。

如果有任何声明没有满足版本要求，则该子着色器或通道将会被剔除。其中，版本可以是指定版本，也可以是范围。

一个具体版本通常是`大版本.小版本`或`大版本.小版本.补丁版本`形式的。如果使用`大版本.小版本`形式，则补丁版本默认为0。版本形式也可以包含`-preview`或`-preview.n`后缀，其中`-preview`等价于`-preview.0`。预览版本的顺序应该在非预览版本之前。

有多种方式指定版本范围：

- `具体版本`：包含指定版本及之后的版本。
- `[具体版本]`：特指指定的具体版本。
- `[/(具体版本1,具体版本2]/)`：指定版本1和版本2之间的版本。方括号表示包括边界，圆括号表示不包括边界。
- 使用`;`间隔上述版本范围，表示范围集合。

主要注意，如果指定版本范围的语法不符合要求，则范围限制无效，其中：

- 版本、版本范围、版本范围集合不能包含任何额外字符。
- 版本范围不能为空。
- 版本范围集合不能有交集。
- 对空格敏感。
- 包名为空。
- 同一个包声明了多个要求。
- 两个要求之间冲突。

# 命令

命令可以给GPU设置渲染状态（在SubShader块中为该子着色器及其下所有通道设置渲染状态，在Pass块中则为该通道设置渲染状态）、创建特定用途的通道。

可以使用`Category`关键字为用于设置渲染状态的命令分组，以便“继承”该代码块内的分组渲染状态。例如，在着色器中为多个子着色器同时设置混合为加法，则可以使用如下代码块：

``` ShaderLab
Shader "MyShader/ShaderStruct"
{
    Category
    {
        Blend One One

        SubShader { }

        SubShader { }
    }
}
```

Category块对着色器性能没有影响，其本质是复制黏贴代码。

## AlphaToMask命令

**AlphaToMask命令**用于启/禁用GPU的Alpha to Coverage模式，有效值为On或Off。此命令旨在与MSAA一起使用。如果在不使用MSAA时启用此命令，则无法预测结果；不同的图形API和GPU对此有不同的处理方式。

> Alpha to Coverage是一种多重采样计算机图形技术，可以减少在使用了Alpha测试的着色器（如植被着色器）在使用多重采样抗锯齿（Multisample Anti-aliasing，MSAA）时出现过度走样，为此，它以片元着色器输出的Alpha值按比例修改多重采样覆盖遮罩取代了Alhpa混合，在使用抗锯齿或半透明纹理时实现了与顺序无关的透明度，在视频游戏中渲染茂密的树叶或草地时很有用。

## Blend命令

**Blend命令**用于确定GPU如何将片元着色器的输出与渲染目标进行混合。启用混合会禁用GPU的一些优化操作（如隐藏表面移除或Early-Z等），这会增加GPU渲染时间。

如果启用了混合：

- 如果使用BlendOp命令，则混合操作将设置为该值，否则混合操作默认为`Add`。
- 如果混合操作是`Add`、`Sub`、`RevSub`、`Min`或`Max`，GPU会将片元着色器的输出值乘以源系数（Source Factor）。
- 如果混合操作是`Add`、`Sub`、`RevSub`、`Min`或`Max`，GPU会将渲染目标中现有的值乘以目标系数（Destination Factor）。
- GPU对结果值执行混合操作。

混合公式为：**最终值 = 源系数 * 片元着色器输出 操作 目标系数 * 目标缓冲区现有值**。其中，最终值是GPU写入目标缓冲区的值，源系数和目标系数在Blend命令中给出，操作是要执行的混合操作。

语法：

- `Blend Off`：禁用混合，默认值。
- `Blend 渲染目标 Off`：对指定的渲染目标禁用混合。
- `Blend 源系数 目标系数`：以给定参数启用混合，其中系数是RGBA混合系数。
- `Blend 渲染目标 源系数 目标系数`：对指定的渲染目标以给定参数启用混合，其中系数是RGBA混合系数。
- `Blend RGB源系数 Alpha源系数 RGB目标系数 Alpha目标系数`：以给定参数启用混合，其中系数分别为RGB和Aplha系数。
- `Blend 渲染目标 RGB源系数 Alpha源系数 RGB目标系数 Alpha目标系数`：对指定的渲染目标以给定参数启用混合，其中系数分别为RGB和Aplha系数。

> 注意：
> - 任何指定渲染目标的命令都需要OpenGL 4.0+、GL_ARB_draw_buffers_blend或OpenGL ES 3.2。
> - 分开设置RBG和Alpha系数的混合与高级OpenGL混合不兼容。

渲染目标是整数，范围0-7，表示渲染目标的索引。

系数的有效值有：

- `One`：输入1，表示使用该值。
- `Zero`：输入0，表示不使用该值。
- `SrcColor`：将输入值乘以片元着色器输出的颜色。
- `SrcAlpha`：将输入值乘以片元着色器输出的Alpha。
- `SrcAlphaSaturate`：将输入值乘以片元着色器输出的Alpha与1-目标缓冲区的Alpha中的较小值。
- `DstColor`：将输入值乘以目标缓冲区的颜色。
- `DstAlpha`：将输入值乘以目标缓冲区的Alpha。
- `OneMinusSrcColor`：将输入值乘以1-片元着色器输出的颜色。
- `OneMinusSrcAlpha`：将输入值乘以1-片元着色器输出的Alpha。
- `OneMinusDstColor`：将输入值乘以1-目标缓冲区的颜色。
- `OneMinusDstAlpha`：将输入值乘以1-目标缓冲区的Alpha。

## BlendOp命令

**BlendOp命令**用于指定Blend命令执行的混合操作。要使此命令生效，需要在同一代码块中指定Blend命令。

> 并非所有设置都支持所有混合操作，对于不支持的混合操作，不同设备有不同处理方式：GL跳过不支持的操作，Vulkan和Metal则执行Add操作。

有效值：

- `Add`：将片元着色器输出加上目标缓冲区。
- `Sub`：将片元着色器输出减去目标缓冲区。
- `RevSub`：将目标缓冲区减去片元着色器输出。
- `Min`：片元着色器输出与目标缓冲区中较小的。
- `Max`：片元着色器输出与目标缓冲区中较大的。
- `LogicalClear`：逻辑操作（`Clear (0)`）。
- `LogicalSet`：逻辑操作（`Set (1)`）。
- `LogicalCopy`：逻辑操作（`Copy (片元着色器输出)`）。
- `LogicalCopyInverted`：逻辑操作（`Copy Inverted (!片元着色器输出)`）。
- `LogicNoop`：逻辑操作（`Noop (目标缓冲区)`）。
- `LogicalInvert`：逻辑操作（`Invert (!目标缓冲区)`）。
- `LogicalAnd`：逻辑操作（`And (片元着色器输出 & 目标缓冲区)`）。
- `LogicalNand`：逻辑操作（`Nand !(片元着色器输出 & 目标缓冲区)`）。
- `LogicAndReverse`：逻辑操作（`Reverse And (片元着色器输出 & !目标缓冲区)`）。
- `LogicalAndInverted`：逻辑操作（`Inverted And (!片元着色器输出 & 目标缓冲区)`）。
- `LogicOr`：逻辑操作（`Or (片元着色器输出 | 目标缓冲区)`）。
- `LogicalNor`：逻辑操作（`Nor !(片元着色器输出 | 目标缓冲区)`）。
- `LogicalXor`：逻辑操作（`Xor (片元着色器输出 ^ 目标缓冲区)`）。
- `LogicalEquiv`：逻辑操作（`Equivalence !(片元着色器输出 ^ 目标缓冲区)`）。
- `LogicalOrReverse`：逻辑操作（`Reverse Or (片元着色器输出 | !目标缓冲区)`）。
- `LogicalOrInverted`：逻辑操作（`Inverted Or (!片元着色器输出 | 目标缓冲区)`）。
- `Multiply`：OpenGL高级混合操作。
- `Screen`：OpenGL高级混合操作。
- `Overlay`：OpenGL高级混合操作。
- `Darken`：OpenGL高级混合操作。
- `Lighten`：OpenGL高级混合操作。
- `ColorDodge`：OpenGL高级混合操作。
- `ColorBurn`：OpenGL高级混合操作。
- `HardLight`：OpenGL高级混合操作。
- `SoftLight`：OpenGL高级混合操作。
- `Difference`：OpenGL高级混合操作。
- `Exclusion`：OpenGL高级混合操作。
- `HSLHue`：OpenGL高级混合操作。
- `HSLSaturation`：OpenGL高级混合操作。
- `HSLColor`：OpenGL高级混合操作。
- `HSLLuminosity`：OpenGL高级混合操作。

## ColorMask命令

**ColorMask命令**用于设置颜色通道写入掩码，以防止GPU写入到渲染目标中的相应通道。默认情况下，GPU会写入所有通道（RGBA）。

语法：

- `ColorMask 通道`：允许GPU写入到指定通道。
- `ColorMask 通道 渲染目标`：允许GPU写入到渲染目标的指定通道。

渲染目标是整数，范围0-7，表示渲染目标的索引。

通道的有效值：

- `0`：启用所有通道的写入。
- `R`：启用R通道的写入。
- `G`：启用G通道的写入。
- `B`：启用B通道的写入。
- `A`：启用Aplha通道的写入。
- `RGBA任意的无空格组合`：启用指定通道的写入。

## Conservative命令



| 命令 | 语法 | 说明 |

透明度和Alpha测试由`alpha`和`alphatest`指令控制。透明度通常有两种：传统的Alpha混合（用于淡化物体）或物理上更合理的“左乘法混合”（允许半透明表面保留适当的镜面反射）。启用半透明使生成的表面着色器代码包含混合命令，而启用Alpha裁剪将在生成的像素中基于给定的变量丢弃片元。

| 值 | 说明 |
| --- | --- |
| `alpha`或`alpha:auto` | 将简单的照明功能设置为渐变透明度（与`alpha:fade`相同），将基于物理的照明功能设置为左乘透明度（与`alpha:premul`相同）。 |
| `alpha:blend` | 启用Alpha混合。 |
| `alpha:fade` | 启用传统的渐变透明度。 |
| `alpha:premul` | 启用左乘透明度。 |
| `alpha:VariableName` | 启用Alpha裁剪透明度。裁剪值在带有VariableName的float变量中。你可能还想使用`addshadow`指令来生成适当的阴影投射通道。 |
| `keepalpha` | 默认情况下，无论输出结构体的Alpha输出或照明函数返回的是什么，不透明表面着色器都将1.0（白色）写入alpha通道。使用这个选项可以保持光照函数的Alpha值。 |
| `decal:add` | 添加印花着色器，即放置在其他表面上的物体，并使用添加混合。 |
| `decal:blend` | 半透明印花着色器。这意味着物体位于其他表面的顶部，并使用Alpha混合。 |

自定义修饰函数可用于更改或计算传入顶点数据，或更改最终计算的片段颜色。

| 值 | 说明 |
| --- | --- |
| `vertex:VertexFunction` | 自定义顶点修改函数。这个函数在生成顶点着色器时被调用，并且可以修改或计算每个顶点的数据。 |
| `finalcolor:ColorFunction` | 自定义最终颜色修改函数。 |
| `finalgbuffer:ColorFunction` | 用于修改G缓冲区内容的自定义延迟路径。 |
| `finalprepass:ColorFunction` | 自定义预传基础路径。 |

阴影和细分可以给出额外的指令来控制如何处理阴影和细节。**当SubShader中没有阴影投射通道时，会使用Fallback指定的Shader对象的阴影投射通道。**

| 值 | 说明 |
| --- | --- |
| `addshadow` | 生成一个阴影投射通道。通常用于自定义顶点修改，使阴影投射也能应用顶点动画。通常着色器不需要任何特殊的阴影处理，因为可以通过阴影投射通道回退。 |
| `fullforwardshadows` | 在正向渲染路径中支持所有阴影类型。默认情况下，在正向渲染中，着色器只支持来自一个方向光的阴影以节省内部着色器变量计数。如果在前向渲染中需要点或聚光灯阴影则使用此指令。 |
| `tessellate:TessFunction` | 使用DX11 GPU细分；该函数计算细分因子。 |

默认情况下生成的表面着色器代码尝试处理所有可能的照明/阴影/光贴图场景。通过代码生成选项可以调整生成的着色器代码以删除不必要的操作，可以生成更小、更快的着色器。

| 值 | 说明 |
| --- | --- |
| `exclude_path:deferred`、`exclude_path:forward`、`exclude_path:prepass` | 不为给定的渲染路径（分别为Deferred Shading、Forward、Legacy Deferred）生成通道。 |
| `noshadow` | 禁用所有阴影接收。 |
| `noambient` | 不使用任何环境照明或光探针。 |
| `novertexlights` | 不要在正向渲染中应用任何光探针或顶点光照。 |
| `nolightmap` | 禁用所有光照映射。 |
| `nodynlightmap` | 禁用运行时动态全局光照。 |
| `nodirlightmap` | 禁用方向光贴图。 |
| `nofog` | 禁用所有内置烟雾。 |
| `nometa` | 不生成“元”通道（用于光照映射和动态全局照明来提取表面信息）。 |
| `noforwardadd` | 禁用正向渲染叠加通道。 |
| `nolppv` | 禁用Light Probe Proxy Volume。 |
| `noshadowmask` | 禁用阴影遮罩。 |

其他选项

| 值 | 说明 |
| --- | --- |
| `softvegetation` | 使表面着色器只在软植被Soft Vegetation状态下渲染。 |
| `interpolateview` | 在顶点着色器中计算视图方向并进行插值，而不是在像素着色器中计算它。这可以使像素着色器更快，但会使用更多的纹理插值器。 |
| `halfasview` | 将半方向矢量传递到照明函数中，而不是视图方向。每个顶点将计算和规范化半方向。这样更快，但不完全正确。 |
| `dualforward` | 在正向渲染路径中使用双重光照贴图。 |
| `dithercrossfade` | 使表面着色器支持抖动效果。 |

基本命令：

- Color(RGBA颜色值)或Color[变量名]（"()"包裹固定值，"[]"包裹参数值）：直接着色。
- Material {} 块中可以编写材质相关的固定功能：
- - Deffuse(RGBA颜色值)或Deffuse[变量名]：漫反射，需要启用顶点光照时才生效。
- - Ambient(RGBA颜色值)或Ambient[变量名]：环境光。
- - Specular(RGBA颜色值)或Specular[变量名]：高光，需要启用独立镜面高光才生效。
- - Shininess(浮点值)或Shininess[变量名]：高光区域，越大高光越集中。
- - Emission(RGBA颜色值)或Emission[变量名]：自发光。
- Lighting顶点光照：可以设置为启用on或关闭off，默认关闭。
- SeparateSpecular独立镜面高光：可以设置为启用on或关闭off，默认关闭。
- SetTexture[贴图变量] {} 块：设置贴图。**当需要混合多张贴图（不能超过显卡最大混合数量）时，需要编写多个settexture块。**
- - Combine合并，其中texture关键字为当前传入的贴图变量，primary关键字为前面所有已计算的顶点光照的值，previous关键字为当前settexture块之前所有计算和采样过后的结果，DOUBLE和QUAD关键字表示前述变量的值乘以二或四。Combine命令可以通过","再跟颜色值变量，此时仅取该变量的Alpha通道，之前颜色变量中的Alpha值失效。
- - ConstantColor(RGBA颜色值)或ConstantColor[变量名]：之后可以使用constant关键字获取该值。