对于Vertex Shader和Fragment Shader，在SubShader块中至少包含一个Pass通道。在Pass通道中，CG代码必须被CGPROGRAM-ENDCG包裹。

C语言需要一个入口Main函数，Vertex Shader和Fragment Shader也需要固定的入口函数，需要使用#pragma编译指令指定Vertex Shader的vertex函数和Fragment Shader的fragment函数（如同使用#pragma指定Surface Shader的surface函数一样），语法如下：

> 注意：在Vertex Shader和Fragment Shader中要求vertex函数和fragment函数必须同时编写。

``` CG
#pragma vertex vert
#pragma fragment frag
```

# vertex函数和fragment函数

vertex函数需要输入一个位置，将其处理后输出一个语义为POSITION的4阶向量的位置。

参数后需要使用":"来指明参数的语义。

``` CG
void vert(in float2 objPos:POSITION, out float4 pos:POSITION)
{
    pos = float4(objPos, 0, 1);
}
```

fragment函数需要输出一个语义为COLOR0或COLOR的4阶向量的颜色。vertex函数中的输出POSITION不需要再fragment中显示传入，它会被图形硬件自行处理，fragment只需要关注颜色即可。

``` CG
void frag (out float4 col:COLOR)
{
    col = float4(1, 0, 0, 1);
}
```

fragment函数中的输出可以从vertex函数中传入，使用inout关键字即可。

``` CG
void vert(in float2 objPos:POSITION, out float4 pos:POSITION, out float4 col:COLOR)
{
    pos = float4(objPos, 0, 1);
    col = float4(0, 0, 1, 1);
}

void frag (inout float4 col:COLOR) { }
```

**CG语言可以使用in关键字表示输入参数，使用out关键字表示输出参数，使用inout关键字表示前一阶段输入、此阶段输出参数。**

# 数据类型

基本数据类型：

- float：32精度浮点数，尾缀f，默认数字类型为float。
- half：16精度的浮点数，尾缀h。
- fixed：9位带符号定点数，通常用于分配颜色（刚好满足256），尾缀x。
- bool：布尔类型，值为true或false。
- int：32位整数

高级数据类型：

- 向量`[基本数据类型][阶数]`：如float1（等同于float）、float2、float3、float4。注意，**CG语言中最高阶数为4**，不存在5阶向量。使用`[基本数据类型][阶数](阶数数个值)`为向量赋值。
- 矩阵`[基本数据类型][行]x[列]`：使用花括号赋值，如`float2x4 my2x4 = {1,0,1,1, 0,1,1,1}`或者`float2x4 my2x4 = {{1,0,1,1}, {0,1,1,1}}`，也支持使用相同纬度的向量赋值某行。取值时使用"[行]"访问某行数据，"[行][列]"访问某元素。
- 数组`数据类型 变量名[维度]`：使用花括号赋值，如`float arr[4] = {1, 1, 0, 1}`。取值时使用"[索引]"访问元素。
- 结构体`struct 结构体名称{...}`。使用"."访问结构体元素。

CG支持大多数C语言的运算。高精度转换为低精度时强制截取。

# 语法糖

高阶向量可以使用低阶向量赋值，如：

- `float4 f4 = float4(f2, 0, 1)`，f2的位置可变。
- `float4 f4 = float4(f2.xy, 0, 1)`，分量顺序可变。
- `float4 f4 = float4(f2.xyyz)`，可以重复取分量。
- `float4 f4 = float4(f41.rgba)`，可以取颜色分量，颜色分量和坐标分量不能同时取（即f41.rbyz）。
- `float4 f4 = float4(f21.yz, f22.gb)`。

CG可以像C语言一样使用typedef关键字为类型指定别名，例如`typedef float4 Vector4`。

CG可以像C语言一样使用#define定义宏，例如`#define MYCOLOR float4(1, 0, 0, 1);`。**注意：在宏定义处如果有分号，则在使用宏时不需要再加分号。**

# 流程控制

CG支持if-else、switch-case、while、do-while、for等语句。

> 注意：分支嵌套不能过深、循环次数有上限限制。

# 自定义函数

函数需要返回类型，如果没有则用void关键字代替。

**自定义函数需要在被使用之前定义**，或者使用前向声明（在程序开头声明函数标签，不带函数体，以";"结尾）。

CG中没有指针，不支持引用类型，因此函数之间的参数传递都是值类型，即拷贝传递。可以使用out关键字表示形参的修改直接反映在实参。

函数中形参数组必须指定长度。

函数重载区别于函数的传参个数、顺序、类型，不取决于返回值类型。

# 引用文件

可以将自定义函数放在cginc文件中，使用`#include "路径/文件名（带拓展名）"`指令引用，此时可以不需要使用前向声明。

# 内建函数

CG内建函数可分为数学函数、几何函数（距离判断、反射、折射等）、纹理函数（采样等）、导数函数（偏导等）、调试函数。

函数：saturate(x) 
参数：x为用于操作的标量或矢量，可以是`float`、`loat2`、`float3`等类型。
描述：截取在[O,1)范围内，如果是一个矢量，那么会对每一个分量进行这样的操作。