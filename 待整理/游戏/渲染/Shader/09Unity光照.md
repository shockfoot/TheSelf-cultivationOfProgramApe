渲染总是围绕着一个基础问题：如何决定一个像素的颜色？从宏观上来说，渲染包含了两大部分：决定一个像素的可见性，决定这个像素上的光照计算。而光照模型就是用于决定在一个像素上进行怎样的光照计算。

# 光

光由光源（Light Source）发射出来。在实时渲染中，通常把光源当成一个没有体积的点，用*l*来表示其方向。在光学里，使用**辐照度**（Irradiance）量化光。**对于平行光来说，它的辐照度可通过计算在垂直于l的单位面积上单位时间内穿过的能量来得到。**

在计算光照模型时，需要知道一个物体表面的辐照度，而物体表面往往是和*l*不垂直，此时可以使用光源方向*l*和表面法线*n*之间的夹角的余弦值来得到。需要注意的是，这里默认方向矢量的模都为1。

对于垂直照射到物体表面的平行光，光线之间的距离保持不变。对于倾斜照射到物体表面的平行光，光线之间的距离为*d/cosθ*，因此单位面积上接收到的光线数目变少了。

**辐照度和照射到物体表面的光线之间的距离*d/cosθ*成反比，因此辐照度与*cosθ*成正比。*cosθ*可以使用光源方向*l*和表面法线*n*的点积得到，此即使用点积计算辐照度的由来。**

光线由光源发射出来后，就会与一些物体相交。通常，相交的结果有两个：**散射**（Scattering）和**吸收**（Absorption）。散射只改变光线的方向，但不改变光线的密度和颜色。而吸收只改变光线的密度和颜色，但不改变光线的方向。

光线在物体表面经过散射后，有两种方向：一种将会散射到物体内部，即**折射**（Refraction）或**透射**（Transmission），另一种将会散射到外部，即**反射**（Rreflection）。对于不透明物体，折射进入物体内部的光线还会继续与内部的颗粒进行相交，其中一些光线最后会重新发射出物体表面，而另一些则被物体吸收。那些从物体表面重新发射出的光线将具有和入射光线不同的方向分布和颜色。

为了区分两种不同的散射方向，在光照模型中使用了不同的部分来计算它们：**高光反射**（Specular）部分表示物体表面是如何反射光线的，而**漫反射**（Diffuse）部分则表示有多少光线会被折射、吸收和散射出表面。根据入射光线的数量和方向，可以计算出射光线的数量和方向，通常使用**出射度**（Exitaoce）来描述它。辐照度和出射度之间是满足线性关系的，而它们之间的比值就是材质的漫反射和高光反射属性。

**着色**（Shading）指根据材质属性（如漫反射属性等）、光源信息（如光源方向、辐照度等），使用一个等式去计算沿某个观察方向的出射度的过程。这个等式即称为**光照模型**（Lighting Model）。不同的光照模型有不同的目的。

当已知光源位置和方向、视角方向时，就需要知道一个表面是如何和光照进行交互的。例如，当光线从某个方向照射到一个表面时，有多少光线被反射？反射的方向有哪些？而**双向反射分布函数**（Bidirectional Reflectance Distribution Function，BRDF）就是用来回答这些问题的。当给定模型表面上的一个点时，BRDF包含了对该点外观的完整的描述。在图形学中，BRDF大多使用一个数学公式来表示，并且提供了一些参数来调整材质属性。通俗来讲，当给定入射光线的方向和辐照度后，BRDF可以给出在某个出射方向上的光照能量分布。大多BRDF都是对真实场景进行理想化和简化后的模型，即它们并不能真实地反映物体和光线之间的交互，这些光照模型被称为是经验模型。尽管如此，这些经验模型仍然在实时渲染领域被应用了多年。

# 标准光照模型

在1975年，著名学者裴祥风（Bui Tuong Phong）提出了标准光照模型背后的基本理念。**标准光照模型只关心直接光照（Direct Light），即直接从光源发射出来照射到物体表面后经过一次反射直接进入摄像机的光线。** 其基本方法是把进入到摄像机内的光线分为4个部分，每部分使用一种方法来计算贡献度。这4个部分为：

- 自发光（Emissive）：记c~emissive~，用于描述当给定一个方向时，一个表面本身会向该方向发射多少辐射量。需要注意的是，如果没有使用全局光照（Global Illumination）技术，自发光的表面并不会真的照亮周围的物体，而是使本身看起来更亮。
- 高光反射（Specular）：记c~specular~，用于描述当光线从光源照射到模型表面时，该表面会在完全镜面反射方向散射多少辐射量。
- 漫反射（Diffuse）：记c~diffuse~，用于描述当光线从光源照射
到模型表面时，该表面会向每个方向散射多少辐射量。
- 环境光（Ambient）：记c~ambient~，用于描述其他所有的间接光照。

## 环境光

虽然标准光照模型的重点在于描述直接光照，但在真实的世界中，物体也可以被**间接光照**（Indirect Light）所照亮。间接光照指光线通常会在多个物体之间反射，最后进入摄像机，即在光线进入摄像机之前，经过了不止一次的物体反射。

标准光照模型使用了一种被称为环境光的部分来近似模拟间接光照。环境光的计算非常简单，它通常是一个全局变量，即场景中的所有物体都使用这个环境光。

> c~ambient~ = g~ambient~

## 自发光

光线也可以直接由光源发射进入摄像机，而不需要经过任何物体的反射。标准光照模型使用自发光来计算这个部分的贡献度。它的计算也很简单，就是直接使用了该材质的自发光颜色。

通常在实时渲染中，自发光的表面往往并不会照亮周围的表面，即该物体并不会被当成一个光源。

> c~emissive~ = m~emissive~

## 漫反射

漫反射光照是用于对那些被物体表面随机散射到各个方向的辐射度进行建模的。在漫反射中，视角的位置是不重要的，因为反射是完全随机的，因此可以认为**在任何反射方向上的分布都是一样的**。但是，入射光线的角度很重要。

漫反射光照符合**兰伯特定律**（Lambert's Law）：**反射光线的强度与表面法线和光源方向之间角的余弦值。** 因此，漫反射部分的计算如下：

> c~diffuse~ = c~light~·m~diffuse~Max(0, *n*·*l*)

其中，*n*是表面法线，*l*指光源的单位矢量，m~diffuse~是材质的漫反射颜色，c~light~是光源颜色。**需要注意的是，需要防止法线和光源方向点乘的结果为负值，为此，使用取最大值的函数来将其截取到0，这可以防止物体被从后面来的光源照亮。**

## 高光反射

此处的高光反射是一种经验模型，即它并不完全符合真实世界中的高光反射现象。它可用于计算那些沿着完全镜面反射方向被反射的光线，这可以让物体看起来是有光泽的，例如金属材质。

计算高光反射需要知道的信息比较多，如表面法线*n*、视角方向*v*、光源方向*l*、反射方向*r*等。在此假设这些矢量都是单位矢量。

上述四个矢量中，只需要知道其中前三个矢量即可，即可求得第四个矢量——反射方向：

> *r* = 2(*n*·*l*)*n*-*l*

其中，*l*指光源的单位矢量。这样，可以利用**Phong模型**计算高光反射部分：

> c~specular~ = c~light~·m~specular~Max(0, *v*·*r*)^mgloss^

其中，m~gloss~是材质的光泽度（Gloss），即反光度（Shininess），用于控制高光区域的“亮点”有多宽。m~gloss~越大，亮点越小。m~specular~是材质的高光反射颜色，用于控制高光反射的强度和颜色。c~light~是光源颜色和强度。同样，也需要防止*v*·*r*的结果为负数。

Blinn模型的基本思想是，避免计算反射方向*r*。为此，Blinn模型引入了一个新的矢量*h*，它是通过对*v*和*l*的取平均后再归一化得到的。即

> *h* = (*v*+*l*)/(|*v*+*l*|)

然后，使用*n*和*h*之间的夹角进行计算，而非*v*和*r*之间的夹角。因此，**Blinn模型**公式：

> c~specular~ = c~light~·m~specular~Max(0, *n*·*h*)^mgloss^

在硬件实现时，如果摄像机和光源距离模型足够远的话，Blinn模型会快于Phong模型，这是因为，此时可以认为*v*和*l*都是定值，因此*h*将是个常量。但是，当*v*或*l*不是定值时，Phong模型可能反而更快些。需要注意的是，这两种光照模型都是经验模型。

## 逐像素和逐顶点光照

在片元着色器中计算的称为**逐像素光照**（Per-pixel Lighting，在顶点着色器中计算的称为**逐顶点光照**（Per-vertex Lighting）。

在逐像素光照中，会以每个像素为基础，得到它的法线（可以是对顶点法线插值得到的也可以是从法线纹理中采样得到的），然后进行光照模型的计算。这种在面片之间对顶点法线进行插值的技术被称为**Phong着色** (Phong shading), 也被称为Phong插值或法线插值着色技术（不同于Phong光照模型）。

与之相对的是逐顶点光照，也被称为**高洛德着色**（Gouraud Shading）。在逐顶点光照中，在每个顶点上计算光照，然后会在渲染图元内部进行线性插值最后输出成像素颜色。由于顶点数目往往远小于像素数目，因此逐顶点光照的计算量往往要小于逐像素光照。但是，由于逐顶点光照依赖于线性插值来得到像素光照，因此，当光照模型中有非线性的计算（例如计算高光反射时）时，逐顶点光照就会出问题。而且，由于逐顶点光照会在渲染图元内部对顶点颜色进行插值，这会导致渲染图元内部的颜色总是暗于顶点处的最高颜色值，这在某些情况下会产生明显的棱角现象。

## 总结

标准光照模型（即Blinn-Phong模型）虽然是一个经验模型，并不完全符合真实世界中的光照现象，但由于易用性、计算速度和得到的效果都比较好，因此仍然被广泛使用。但这种模型有很多局限性。首先，有很多重要的物理现象无法用Blinn-Phon模型表现出来，例如菲涅耳反射（Fresnel Reflection）。其次，Blinn-Phong模型是各项同性（Isotropic）的，即当我们固定视角和光源方向旋转这个表面时，反射不会发生任何改变。但有些表面是具有各向异性（Anisotropic）反射性质的，例如拉丝金屈、毛发等。

# 实践

## 环境光和自发光

在标准光照模型中，环境光和自发光的计算是最简单的。

Unity中，场景中的环境光可以在Window->Lighting->Ambient Source/Ambient Color/Ambient Intensity中控制。在Shader中，只需要通过Unity的内置变量**UNITY_LIGHTMODEL_AMBIENT**就可以得到环境光的颜色和强度信息。

大多数物体是没有自发光特性的，因此在绝大部分的Shader中都没有计算自发光部分。如果要计算自发光也非常简单，只需要在片元着色器输出最后的颜色之前，把材质的自发光颜色添加到输出颜色上即可。

## 实现漫反射光照模型

### 逐顶点光照

（1）声明需要使用的属性：漫反射颜色。

``` CG
Properties
{
    _Diffuse ("Diffuse", Color) = (1, 1, 1, 1)
}
```

（2）设置Pass光照模式。

``` CG
Tags { "LightMode" = "ForwardBase" }
```

（3）在CG代码块中指明顶点着色器和片元着色器，引用内置文件。

``` CG
#pragma vertex vert
#pragma fragment frag

#include "Lighting.cginc"
```

（4）声明与属性相匹配的变量。因为颜色属性的范围在0-1之间，可以使用`fixed`精度变量存储。

``` CG
fixed4 _Diffuse;
```

（5）定义顶点着色器输入：计算漫反射需要法线，因此顶点着色器输入中除了位置还需要法线信息。

``` CG
struct a2v
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
};
```

（6）定义顶点着色器输出：为了将顶点着色器计算得到的颜色传递给片元着色器，因此顶点着色器输出中除了裁剪空间的位置还需要一个颜色信息。

``` CG
struct v2f
{
    float4 vertex : SV_POSITION;
    fixed3 color : COLOR;
};
```

（7）顶点着色器：除了将顶点坐标从模型空间转换到裁剪空间外，光照部分需要环境光和漫反射。环境光可以通过内置变量`UNITY_LIGHTMODEL_AMBIENT`获得。漫反射需要先获得表面法线（模型空间）和光源方向（世界空间），因此选择世界空间进行计算。表面法线由顶点法线从模型空间转换到世界空间得到（普通计算是变换矩阵左乘，等价于逆转置矩阵右乘），由于法线是三维向量，需要截取变换矩阵的前三行三列。对于**单一平行光光源**，可以通过内置变量`_WorldSpaceLightPos0`获得光源位置。计算漫反射时使用`_LightColor0`访问该通道处理的光源的颜色和强度（想要得到正确的值需要定义合适的LightMode标签）。使用CG内置`saturate`函数将两向量点积结果截取到[0,1]范围内。将环境光与漫反射相加，得到最终光照结果。

``` CG
v2f vert(a2v v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);

    fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

    fixed3 worldNormal = normalize(mul(v.normal, (float3x3)unity_WorldToObject));
    fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz);
    fixed diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal, worldLight));

    o.color = ambient + diffuse;
    return o;
}
```

（8）片元着色器直接输出颜色即可。

```CG
fixed4 frag(v2f i) : SV_Target
{
    return fixed4(i.color.r, i.color.g, i.color.b, 1.0);
}
```

（9）设置FallBack

```CG
FallBack "Diffuse"
```

### 逐像素光照

逐像素漫反射与逐顶点漫反射基本相同，区别是将漫反射计算放在了片元着色器，因此在顶点着色器中只需要计算顶点法线并传递给片元着色器。

（1）定义顶点着色器输出：顶点着色器只变换顶点法线到世界空间，因此顶点着色器输出中除了裁剪空间的位置还需要传递法线。

``` CG
struct v2f
{
    float4 pos : SV_POSITION;
    float3 worldNormal : TEXCOORD0;
};
```

（2）顶点着色器：除了将顶点坐标从模型空间转换到裁剪空间外，将顶点法线从模型空间转换到世界空间即可。

``` CG
v2f vert(a2v v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);

    return o;
}
```

（3）片元着色器需要计算漫反射。

```CG
fixed4 frag(v2f i) : SV_Target
{
    fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
    fixed3 worldNormal = normalize(i.worldNormal);
    fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz);
    fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal, worldLight));

    fixed3 color = ambient + diffuse;

    return fixed4(color.r, color.g, color.b, 1.0);
}
```

逐像素光照可以得到更加平滑的光照效果。但是，即便使用了逐像素漫反射光照，在光照无法到达的区域，模型的外观通常是全黑的，没有任何明暗变化，这会使模型的背光区域看起来就像一个平面一样，失去了细节表现。实际上可以通过添加环境光来得到非全黑的效果，但即便这样仍然无法解决背光面明暗一样的缺点。

### 半兰伯特模型

上述使用的漫反射光照模型也被称为兰伯特光照模型，因为它符合兰伯特定律——在平面某点漫反射光的光强与该反射点的法向量和入射光角度的余弦值成正比。为了解决背光面明暗一样的问题，Valve公司在开发游戏《半条命》时提出了一种技术，由于该技术是在原兰伯特光照模型的基础上进行了一个简单的修改，因此被称为**半兰伯特光照模型**。

广义的半兰伯特光照模型公式如下：

> c~diffuse~ = c~light~·m~diffuse~(α(*n*·*l*)+β)

与原兰伯特光照模型相比，半兰伯特光照模型没有使用Max操作防止*n*·*l*结果为负，而是对该结果进行了α倍缩放再加上一个β大小的偏移。绝大多数下，α和β均为0.5，这样可以把*n*·*l*的结果范围从[-1,1]映射到[0,1]。也就是说，对于模型的背光面，原兰伯特光照模型将点积结果都映射到0，而半兰伯特光照模型中不同的点积结果会映射到不同的值。

仅修改片元着色器中使用的光照模型即可，其他部分与逐像素漫反射相同。

``` CG
fixed4 frag(v2f i) : SV_Target
{
    ...
    fixed halfLambert = dot(worldNormal, worldLight) * 0.5 + 0.5;
    fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * halfLambert;
    ...
}
```

## 实现高光反射光照模型

### 逐顶点光照

（1）声明需要使用的属性：漫反射颜色属性、高光反射颜色和光泽度。

``` CG
Properties
{
    _Diffuse ("Diffuse", Color) = (1, 1, 1, 1)
    _Specular ("Specular", Color) = (1, 1, 1, 1)
    _Gloss ("Gloss", Range(8.0, 256)) = 20
}
```

（2）设置Pass光照模式。

``` CG
Tags { "LightMode" = "ForwardBase" }
```

（3）在CG代码块中指明顶点着色器和片元着色器，引用内置文件。

``` CG
#pragma vertex vert
#pragma fragment frag

#include "Lighting.cginc"
```

（4）声明与属性相匹配的变量。

``` CG
fixed4 _Diffuse;
fixed4 _Specular;
float _Gloss;
```

（5）定义顶点着色器输入：计算漫反射和高光反射都需要法线，因此顶点着色器输入中除了位置还需要法线信息。

``` CG
struct a2v
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
};
```

（6）定义顶点着色器输出：为了将顶点着色器计算得到的颜色传递给片元着色器，因此顶点着色器输出中除了裁剪空间的位置还需要一个颜色信息。

``` CG
struct v2f
{
    float4 pos : SV_POSITION;
    fixed3 color : COLOR;
};
```

（7）顶点着色器：除了将顶点坐标从模型空间转换到裁剪空间外，光照部分需要环境光、漫反射和高光反射。环境光和漫反射见前述。高光反射按Phong光照模型先求反射方向（可以使用CG内置函数`reflect`）和视觉方向（通过相机位置`_WorldSpaceCameraPos`与顶点位置），然后计算高光反射。最后环境光、漫反射、高光反射相加，得到最终光照结果。

``` CG
v2f vert(a2v v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);

    fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

    fixed3 worldNormal = normalize(mul(v.normal, (float3x3)unity_WorldToObject));
    fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz);
    fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal, worldLight));

    fixed3 view = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz);
    fixed3 reflectDir = normalize(reflect(-worldLight, worldNormal));
    fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(saturate(dot(view, reflectDir)), _Gloss);

    o.color = ambient + diffuse + specular;
    return o;
}
```

（8）片元着色器直接输出颜色即可。

```CG
fixed4 frag(v2f i) : SV_Target
{
    return fixed4(i.color.r, i.color.g, i.color.b, 1.0);
}
```

（9）设置FallBack

```CG
FallBack "Specular"
```

### 逐像素光照

逐像素与逐顶点基本相同，区别是将顶点着色器中的光照计算放在片元着色器中，因此顶点着色器中需要传递世界空间下的顶点位置（计算视觉方向）和顶点法线。

（1）定义顶点着色器输出：需要额外传递世界空间下的顶点位置和顶点法线。

``` CG
struct v2f
{
    float4 pos : SV_POSITION;
    float3 worldNormal : TEXCOORD0;
    float3 worldPos : TEXCOORD1; 
};
```

（2）顶点着色器：除了将顶点坐标从模型空间转换到裁剪空间外，需要计算世界空间下的顶点位置和顶点法线。

``` CG
v2f vert(a2v v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
    o.worldPos = mul(unity_ObjectToWorld, v.vertex);

    return o;
}
```

（3）片元着色器进行光照计算。

```CG
fixed4 frag(v2f i) : SV_Target
{
    fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

    fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz);
    fixed3 worldNormal = normalize(i.worldNormal);
    fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal, worldLight));

    fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos);
    fixed3 reflectDir = normalize(reflect(-worldLight, worldNormal));
    fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(saturate(dot(viewDir, reflectDir)), _Gloss);

    fixed3 color = ambient + diffuse + specular;

    return fixed4(color.r, color.g, color.b, 1.0);
}
```

逐像素光照效果更加平滑。

### Blinn-Phong光照模型

基本与逐像素相同，只不过片元着色器中计算反射方向改为计算*h*。

``` CG
fixed4 frag(v2f i) : SV_Target
{
    ...
    fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos);
    fixed3 helfDir = normalize(worldLight + viewDir);
    fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(saturate(dot(worldNormal, helfDir)), _Gloss);
    ...
}
```