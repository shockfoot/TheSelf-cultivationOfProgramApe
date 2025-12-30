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