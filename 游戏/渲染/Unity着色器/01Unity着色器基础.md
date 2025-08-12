# Unity着色器基础

## Unity中的着色器

在Unity中，*着色器*（Shader）需要搭配*材质*（Material）才能实现需要的效果。着色器定义了渲染所需的代码（如顶点着色器和片元着色器）、属性（如纹理）和指令（如标签），材质则打包存储了输入的贴图或颜色、着色器以及对着色器的特定参数的设置，允许开发者在Inspector面板修改着色器属性，并将材质应用到指定对象。

Unity Shader与渲染管线中的Shader有很大不同。

在Unity 2021.3.5f1中，Unity提供了5种Shader模板创建Shader资产：

- Standard Surface Shader会生成一个包含了标准光照模型的表面着色器模板。
- Unlit Shader会生成一个不包含光照但包含雾效的基本的顶点/片元着色器。
- Image Effect Shader生成实现各种屏幕后处理效果的基本模板。
- Compute Shader生成一个利用GPU的并行性进行与常规渲染无关的通用性计算的模板。
- Ray Tracing Shader则生成用于执行与光线追踪计算相关的模板。

Unity Shader是一个扩展名为.shader的文本文件。选中某个Shader后，Inspector面板会显示该Shader的信息，查看和编辑与该资产本身相关的设置以及着色器编译器对它的处理方式。

- Default Maps用于指定默认纹理。当任何材质第一次使用该Shader时，这些纹理会自动赋予到相应属性上。
- Surface Shader/Fixed Function指示是否为对应类型的Shader。单机Show generated code可以显示其代码。
- Compiled Code可以让开发者选择编译选项和检查该Shader针对不同图形API最终编译成的代码，可利用这些代码分析和优化Shader。
- 一些标签设置，如是否会投射阴影、渲染队列、LOD、是否关闭批处理等。
- 关键字。
- 属性。

## ShaderLab

Unity Shader是Unity为开发者提供的高层级的渲染抽象层，ShaderLab是Unity提供的编写Unity Shader的一种说明性语言。在Unity中，所有Shader都使用ShaderLab编写。Unity在背后会根据使用的平台来把这些Shader编译成真正的代码和Shader文件，而开发者只需要和Unity Shader打交道即可。

ShaderLab使用了一些嵌套在花括号内部的语义（Syntax）描述Unity Shader文件的结构。

``` ShaderLab
Shader "<菜单路径/名称>"  // Shader块
{
    Properties  // Properties块，可选
    {
        // 属性声明
        [<特性>] <Name> ("<Display Name>", <Property Type>) = <Default Value>
    }

    SubShader  // SubShader块
    {
        LOD <数值>  // LOD，可选

        Tags { "Tag1" = "Value" "Tag2" = "Value" }  // Tags块，可选
        
        [RenderSetup]  // 状态设置，可选

        Pass  // Pass块
        {
            Name "<Pass Name>"  // 可选

            Tags { "Tag1" = "Value" "Tag2" = "Value" }  // Tags块，可选
            
            [RenderSetup]  // 状态设置，可选

            [Code]  // 着色器代码，可选 
        }
    }

    Fallback "<Other Shader>"  // 后备着色器，可选
    
    CustomEditor "<Editor Class>"  // 自定义编辑器，可选
    CustomEditorForRenderPipeline "<Editor Class>" "<Pipeline Name>"  // 自定义编辑器，可选
}
```

### Shader块

`Shader { }`块定义了一个Unity Shader对象。`Shader`关键字后使用*菜单路径*的方式定义该着色器对象的名称，用于在Inspector面板为材质设置Shader。

在Shader块中，可以：

- 使用Properties块定义材质属性，可选。
- 在Fallback块分配后备着色器对象，可选。
- 使用CustonmEditor块或CustomEditorForRenderPipeline块分配自定义编辑器，可选。
- 在SubShader块中定义一或多个子着色器，必须。

### Properties块

在Shader块中使用`Properties { }`块定义属性，可选。这些属性会出现在材质的Inspector面板中以便更改和持久化。Properties块中可声明任意数量的属性。

Name是属性名，用于在Shader中访问，通常由下划线开始。Display Name是出现在材质面板上的名称。每个属性都需要指定Property Type和Default Value，第一次把Shader赋给某个材质时，材质使用的就是Default Value。

常见属性类型如下：

| 类型 | 语法用例 | 说明 |
| --- | --- | --- |
| Integer | `_Name ("DisplayName", Integer) = 1` | 基于实整数的整型。 |
| Int（不支持） | `_Name ("DisplayName", Int) = 1` | 基于浮点数的整型。 |
| Float | `_Name ("DisplayName", Float) = 0.5` | 没有范围的浮点数。 |
| Float | `_Name ("DisplayName", Range(0.0, 1.0)) = 0.5` | 可以取到范围边界值。 |
| Texture2D | `_Name ("DisplayName", 2D) = "red" {}` | 可以指定"white"、"black"、"gray"和"red"等Unity内置纹理。如果使用了非法或空字符串，默认为"gray"。这些内置纹理在Inspector面板不可见。 |
| Texture2DArray | `_Name ("DisplayName", 2DArray) = "" {}` | 2D纹理数组，是相同大小/格式/标志的2D纹理的集合。2D纹理数组的元素也被称为切片或层。 |
| Texture3D | `_Name ("DisplayName", 3D) = "" {}` | 默认为"gray"。 |
| Cubemap | `_Name ("DisplayName", Cube) = "" {}` | 一个由6个正方形纹理组成的立方体贴图，可以代表环境中的反射（可用于天空盒）。这六个纹理构成了一个假想的立方体，这个立方体包围着一个物体；每个面代表沿世界轴方向(上、下、左、右、前、后)的视图。默认为"gray"。 |
| CubemapArray | `_Name ("DisplayName", CubeArray) = "" {}` | 大小和格式相同的立方体贴图的数组，GPU可以将其作为单个纹理资源访问。立方体映射阵列常用于实现高效的反射探测、照明和阴影系统。 |
| Color | `_Name ("DisplayName", Color) = (1, 1, 1, 1)` | 映射到一个4浮点数的结构体，表示RGBA颜色，在Inpsectot面板显示为一个颜色选择器。如果希望将值显示为四个独立的浮点数，可使用Vector类型。 |
| Vector | `_Name ("DisplayName", Vector) = (1, 1, 1, 1)` | 映射到一个4浮点数的结构体，在Inpsectot面板显示为四个独立的浮动字段。如果希望显示为颜色选择器，可使用Color类型。 |

**为了在Shader中可以访问到这些属性，需要在CG代码片中定义和这些属性类型相匹配的变量。** 即使不在Properties块中声明这些属性，也可以直接在CG代码片中定义变量。此时，可以通过脚本（属性在C#代码中由`MaterialProperty`类表示，`Material`类的API如`GetFloat`、`SetFloat`等可以读取或设置着色器中的变量）向Shader中传递这些属性。因此，Properties块的作用仅仅是为了让这些属性可以出现在材质面板中。

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
| `[KeywordEnum(枚举1, 枚举2,...)]` | Keyword枚举，切换该变量所代表的Keyword。Keyword命名规则为“变量名的大写形式_枚举的大写形式”。这些Keyword无法被Enum使用。 |

除了这里列出的特性，还可以使用相同的语法用`MaterialPropertyDrawer`可以控制材质属性在Inpsectot面板中的显示方式。对于更复杂的需求，可以使用`MaterialEditor`、`MaterialProperty`和`ShaderGUI`类实现。

### SubShader块

`SubShader { }`块定义了子着色器。Shader块中可以包含多个SubShader块，但至少需定义一个SubShader块。

> 当Unity需要加载这个Shader时，Unity会扫描所有的SubShader块，然后选择第一个能够在目标平台上运行的SubShader块。如果都不支持的话，Unity就会使用Fallback块指定的Unity Shader。这样可以为不同的硬件定义不同的GPU设置和着色程序、渲染管道以及运行时设置。

SubShader块中定义了一系列Pass块以及可选的状态和标签设置。SubShader块的状态和标签设置会应用于所有的Pass块。

#### LOD指令

可在SubShader块中使用`LOD`指令定义子着色器的LOD值，表示对计算的要求，可选。

在运行时，可以设置单个着色器或用于所有着色器的LOD值。Unity然后优先考虑具有较低LOD值的子着色器。着色器LOD与相机的距离无关，Unity不会自动计算着色器LOD。必须手动设置最大着色器LOD。

子着色器必须按LOD降序排列，因为Unity选择找到的第一个有效子着色器，所以如果Unity首先找到一个LOD较低的子着色器，Unity将始终使用它。

使用这种技术可以在不同的硬件上微调着色器的性能。当用户的硬件理论上支持某子着色器，但硬件不能很好地运行它时，这是有用的。

要为给定的着色器对象设置着色器LOD，可以使用`Shader.maximumLOD`。要为所有着色器对象设置着色器LOD，可以使用`Shader.globalMaximumLOD`。缺省情况下，没有最大LOD。

Unity内置渲染管线中，Unity内置着色器的LOD值有100（有Unlit/Texture、Unlit/Color、Unlit/Transparent、Unlit/Transparent Cutout）、300（有Standard、Standard (Specular Setup)、Autodesk Interactive）两个级别，内置不支持（Legacy）的有100、150、200、250、300、400、500、600等级别。

### Pass块

`Pass { }`块定义*通道*（Pass）。SubShader块中可以包含多个Pass块，但至少需定义一个Pass块。通道包含设置GPU状态的指令以及在GPU上运行的着色器程序。每个通道定义了一次完整的渲染流程。不同的通道定义了不同的工作方式，可实现不同的效果。**当通道数量过多时会造成渲染性能的下降，因此应尽量使用最小数量的通道。**

可以使用`Name`指令为通道命名。通过这个名称，可以使用ShaderLab的`UsePass`命令或在脚本中通过API按名称直接使用其他Unity Shader中的通道，提高代码复用性。由于Unity内部会把所有通道的名称转换成大写字母的表示，因此`UsePass`命令中必须使用大写形式的名字。如果一个子着色器中具有多个同名通道，则使用第一个。

Pass块中主要包含着色器代码（可选），可以设置渲染状态（可选）和标签（可选），还可以使用固定管线的着色器命令（可选）和指定的包（可选）。

除了普通的通道，Unity Shader还支持一些特殊的通道, 以便进行代码复用或实现更复杂的效果。

- UsePass：可以使用该命令来复用其他Unity Shader中的通道。
- GrabPass：该通道负责抓取屏幕并将结果存储在一张纹理中，以用于后续的通道处理。

### 状态设置

ShaderLab提供了一系列渲染状态的设置指令，可设置显卡的各种状态。SubShader块中的状态将会应用于其下的所有Pass块。

常见的渲染状态设置选项有：

| 状态 | 设置指令 | 说明 |
| --- | --- | --- |
| Cull | Cull Back/Front/Off | 设置剔除模式：剔除背面/正面/关闭剔除 |
| ZTest | ZTest Less/Greater/LEqual/GEqual/Equal/NotEqual/Always | 设置深度测试时使用的函数 |
| ZWrite | ZWrite On/Off | 开启/关闭深度写入 |
| Blend | Blend SrcFactor/DstFactor | 开启并设置混合模式 |

### Tags块

`Tags { }`块定义标签设置，数量任意。**标签是一组键和值都是字符串的键值对。** Unity使用预定义的键和值来确定如何以及何时使用子着色器或通道，或者可以使用自定义值创建自定义标签并从C#代码中访问它们（`Material.GetTag`API）。

#### SubShader块标签

**Queue标签**用于告诉Unity渲染物体时使用哪个渲染队列，**值越小越先渲染**。渲染队列是决定Unity渲染几何图形顺序的因素之一。通过这种方式可以保证所有的透明物体在不透明物体之后渲染。

有两种方式使用Queue标签：使用内置的已命名渲染队列，或者在指定队列的给定偏移量（偏移量为整数）处的未命名队列（自定义队列）。内置队列有：

- Background：背景。
- Geometry：几何。
- AlphaTest：Alpha测试。
- Transparent：透明。
- Overlay：叠加。

**RenderType标签**用于对SubShader块进行分类，以便在其他地方标识该着色器，用于被覆盖。

**DisableBatching标签**用于指明此物体是否应用批处理，值可为True、False（默认）或LODFading。

**ForceNoShadowCasting标签**用于控制物体是否投射（有时接收）阴影，值可为True或False（默认）。

**IgnoreProjector标签**用于内置渲染管道，告诉Unity此物体是否忽视Projector组件的影响，值可为True或False（默认）。这对于排除半透明物体非常有用，因为Projector组件与半透明物体不兼容。其他渲染管道中不起作用。

**CanUseSpriteAtlas标签**用于在使用Legacy Sprite Packer的项目中指示该着色器是否依赖于原始纹理坐标，因此是否应该将其纹理打包到图集中，值可为True（默认）或False。

**PreviewType标签**用于告诉Unity编辑器如何在Inpsector面板中预览使用此着色器的材质。值可为Sphere（默认）、Plane或Skybox。

**RenderPipeline标签**用于告诉Unity子着色器是否与通用渲染管道（URP）或高清晰度渲染管道（HDRP）兼容，其值可为UniversalRenderPipeline（兼容URP）、HighDefinitionRenderPipeline（兼容HDRP）、其他任何值或未设置（不与URP或HDRP兼容。）

可以使用`Shader.renderQueue`读取Shader对象中正在使用的子着色器的Queue标签。

#### Pass块标签

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

**RequireOptions标签**用于启用或禁用一个通道，值只能为"SoftVegetation"，仅当项目的质量设置QualitySettings中的SoftVegetation被启用时才渲染此通道。

**PassFlags标签**用于指定Unity提供给通道的数据，值只能为"OnlyDirectional"，并且只能在内置渲染管线且渲染路径为"Forward"、LightMode标签为"ForwardBase"的通道中有效，此时Unity只为该通道提供主方向光和环境光/光照探针数据。这意味着非重要光源的数据将不会传递到顶点光源或球谐函数着色器变量。

### Fallback块

`Fallback`指令可定义后备着色器，用于指定找不到兼容的着色器时要使用的后备着色器名或指定Off以禁用后备着色器，可选。

常用内置着色器：

- Unlit不发光：只是一个纹理，不被任何光照影响，常用于UI。
- VertexLit顶点光照。
- Diffuse漫反射。
- Normal Mapped法线贴图：增加了一个或更多纹理（法线贴图）和几个着色器结构。
- Specular高光：增加了特殊的高光计算。
- Normal Mapped Specular高光法线贴图。
- Parallax Normal Mapped视差法线贴图：增加了视差法线贴图计算。
- Parallax Normal Mapped Specular视差高光法线贴图：增加了视差法线贴图和高光计算。

### CustomEditor块

在Shader块中可以使用`CustomEditor`或`CustomEditorForRenderPipeline`指令指定自定义编辑器，用于在Inspector面板中改变着色器的显示或自定义控件和数据验证，可选。

`CustomEditor`指令用于使用指定的自定义编辑器，而`CustomEditorForRenderPipeline`指令用于在指定渲染管线下使用指定的自定义编辑器。`CustomEditorForRenderPipeline`指令优先于`CustomEditor`指令。

需要注意的时，指定的自定义编辑器类必须要继承`ShaderGUI`，且位于Assets下的Editor文件夹中。



在Unity中，着色器分为三大类：

- 渲染管线中的着色器：用来计算图像。
- 通用计算着色器：独立于渲染管线，仅在GPU上执行通用计算。
- 光线追踪着色器：用于执行与光线追踪相关的计算。

渲染管线中的着色器可分为：

- 顶点/片元着色器。
- **表面着色器**（Surface Shaders）：Unity推荐和鼓励使用的着色器，是对顶点/片元着色器的封装，最终会被编译成能被硬件识别的顶点着色器和片元着色器。
- **固定函数着色器**（Fixed Function Shaders）：固定管线中的固定功能的着色器，是针对硬件能够执行的命令编写的着色器，功能有限但速度快、支持性好，只能使用ShaderLab编写。

## 着色器编译

每次构建项目时，Unity会编译构建所需的所有着色器，即针对每个所需的图形API编译每个所需的着色器。在构建时，Unity可以检测到一些未被游戏使用着色器变量，并从构建数据中剥离它们。

在Unity编辑器中时，Unity不会提前编译所有内容，这是因为针对每个所需的图形API编译每个所需的着色器可能需要很长时间。Unity编辑器通常会：

- 在导入着色器时执行最小的处理，如生成表面着色器等。
- 当需要显示着色器时检查Library/ShaderCache中是否存在事先编译好的着色器。如果有，则使用之；如果没有，则编译所需的着色器并将结果保存到缓存中。

着色器编译使用名为UnityShaderCompiler的进程。可启动多个UnityShaderCompiler编译器进程（通常每个CPU核心对应一个），这样在Unity编辑器下运行时就可以并行完成着色器编译。当Unity编辑器不编译着色器时，编译器进程不执行任何操作，也不消耗计算机资源。

如果有许多经常更改的着色器，那么着色器缓存文件夹可能会变得非常大。删除着色器缓存文件夹是安全的；只会导致Unity重新编译着色器。

在Unity编辑器下运行时，所有“尚未编译”的着色器都将被编译，因此即使编辑器不会使用这些着色器，它们也会存在于游戏数据中。

不同平台使用不同的编译器编译着色器：

- DirectX使用Microsoft的FXC HLSL编译器。
- OpenGL（Core或ES版本）先使用Microsoft的FXC HLSL编译器，然后使用HLSLcc将字节代码转换为GLSL。
- Metal先使用Microsoft的FXC HLSL编译器，然后使用HLSLcc将字节代码转换为Metal。
- Vulkan先使用Microsoft的FXC HLSL编译器，然后使用HLSLcc将字节代码转换为SPIR-V。
- 其他平台（如游戏主机平台）使用其各自的编译器。
- 表面着色器使用HLSL和MojoShader来完成代码生成分析步骤。

着色器编译分为几个步骤：

- 预处理：使用预处理器为编译器准备着色器源代码。在以前，Unity使用平台编译器提供的预处理器。现在Unity使用自己的预处理器，也叫缓存着色器预处理（Caching Shader Preprocessor）。

## 着色器加载

Unity通过以下方式从构建的应用程序中加载编译过的着色器：

1. 当Unity加载场景或资源时，**加载该场景或资源需要的所有已编译的着色器到CPU内存**。默认情况下，Unity将所有着色器解压到CPU内存的其他区域，可以控制在不同平台上着色器使用的内存大小；
2. Unity第一次使用着色器渲染时，会将着色器及其数据传递给图形API和图形驱动程序；
3. 图形驱动程序创建GPU可用的着色器并上传到显存中。

这种方法确保Unity和图形驱动程序在使用着色器之前避免加载、处理和存储着色器，但当图形驱动程序第一次创建GPU可用的着色器时可能会出现明显的停滞。

Unity会**缓存每个GPU可用的着色器**，以避免重复创建。

当不再有任何对象引用着色器时，Unity将着色器完全从CPU和GPU内存中移除。

如果着色器中包含多个子着色器，当Unity首次使用着色器对象渲染以及更改活动渲染管线、着色器的LOD值时，Unity会尝试选择并使用**与平台硬件兼容、等于或低于当前着色器LOD值、与渲染管线兼容的子着色器**。如果可以运行的子着色器有多个，则选择第一个；如果Unity找不到需要的着色器，则会尝试选择一个类似的着色器；如果找不到类似的着色器，则会使用洋红色显示渲染的对象。可以启用严格的着色器匹配选项来阻止Unity尝试选择类似的着色器。

Unity可以识别使用相同着色器变体的几何体并将其合批以实现更高效的渲染。对于合批的几何体：

1. Unity确定其应该渲染运行的子着色器中的哪些通道；
2. 对于其渲染的每个通道，如果当前渲染状态和通道中定义的渲染状态不匹配，Unity会根据通道中的定义设置渲染状态，然后GPU使用相应的着色器变体渲染几何体。