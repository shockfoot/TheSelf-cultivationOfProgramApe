为了方便开发者的编码过程，Unity提供了很多内置文件，这些文件包含了很多提前定义的函数、变量和宏等。

# 内置包含文件

**包含文件** (Include file)是类似于C++中头文件的一种文件。在Unity中，其的文件后缀为.cginc。在编写Shader时，可以使用`#include`指令把这些文件包含进来，这样就可以使用Unity提供的一些非常有用的变量和帮助函数。

``` CG
CGPROGRAM
// ...
#include"UnityCG.cginc"
// ...
ENDCG
```

内置的着色器文件可以在编辑器程序目录的Data\CGIncludes下查看，包括：

- HLSLSupport.cginc：声明了用于跨平台着色器编译的宏和定义。在编译Unity Shader时会自动包含。
- UnityShaderVariables.cginc：声明了常用的全局变量。在编译Unity Shader时会自动包含。
- UnityCG.cginc：声明了常用的帮助函数、宏和结构体等。
- AutoLight.cginc：提供了常用的光照和阴影功能。Surface Shader自动包含。
- Lighting.cginc：提供了内置光照模型。在编译Surface Shader时会自动包含。

UnityCG.cginc常用的结构体信息：

| 名称 | 描述 | 包含变量 |
| --- | --- | --- |
| appdata_img | 可用于顶点着色器的输入 | 顶点位置、第一组纹理坐标 |
| appdata_base | 可用于顶点着色器的输入 | 顶点位置、顶点法线、第一组纹理坐标 |
| appdata_tan | 可用于顶点着色器的输入 | 顶点位置、顶点切线、顶点法线、第一组纹理坐标 |
| appdata_full | 可用于顶点着色器的输入 | 顶点位置、顶点切线、顶点法线、顶点颜色、四组组纹理坐标 |
| v2f_img | 可用于顶点着色器的输出 | 裁剪空间中的位置、纹理坐标 |

除了结构体外，UnityCG.cginc也提供了一些常用的帮助函数：

| 函数名 | 描述 |
| --- | --- |
| float3 WorldSpaceViewDir (float4 v) | 输入一个模型空间中的顶点位置，返回世界空间中从该点到摄像机的观察方向 |
| float3 ObjSpaceViewDir (float4 v) | 输入一个模型空间中的顶点位置，返回模型空间中从该点到摄像机的观察方向 |
| float3 WorldSpaceLightDir (float4 v) | 仅可用于前向渲染中。输入一个模型空间中的顶点位置，返回世界空间中从该点到光源的光照方向（没有被归一化） |
| float3 ObjSpaceLightDir (float4 v) | 仅可用于前向渲染中。输入一个模型空间中的顶点位置，返回模型空间中从该点到光源的光照方向（没有被归一化） |
| float3 UnityObjectToWorldNormal (float3 norm) | 把法线方向从模型空间转换到世界空间中 |
| float3 UnityObjectToWorldDir (float3 dir) | 把方向矢量从模型空间变换到世界空间中 |
| float3 UnityWorldToObjectDir (float3 dir) | 把方向矢量从世界空间变换到模型空间中 |

# 内置变量

Unity的内置文件包含着色器的全局变量：当前对象的变换矩阵、光源参数、当前时间等等。就像任何其他变量一样，可在着色器程序中使用这些变量，但如果已经包含相关的文件，则不必声明这些变量。

所有变换矩阵都是`float4x4`类型，并且是列主序的。

| 名称 | 值 |
| --- | --- |
| UNITY_MATRIX_MVP | 当前模型-观察-投影矩阵。 |
| UNITY_MATRIX_MV | 当前模型-观察矩阵。 |
| UNITY_MATRIX_V | 当前观察矩阵。 |
| UNITY_MATRIX_P | 当前投影矩阵。 |
| UNITY_MATRIX_VP | 当前观察-投影矩阵。 |
| UNITY_MATRIX_T_MV | UNITY_MATRIX_MV的转置矩阵。 |
| UNITY_MATRIX_IT_MV | UNITY_MATRIX_MV的逆转置矩阵，用于将法线从模型空间变换到观察空间。 |
| unity_ObjectToWorld | 当前模型矩阵，用于将点/方向矢量从模型空间变换到世界空间。 |
| unity_WorldToObject | 当前世界矩阵的逆矩阵，用于将点/方向矢量从世界空间变换到模型空间。 |

摄像机和屏幕参数用于访问正在渲染的摄像机的参数信息。这些参数对应了摄像机Camera组件中的属性值。

| 名称 | 类型 | 值 |
| --- | --- | --- |
| _WorldSpaceCameraPos | float3 | 摄像机的世界空间位置。 |
| _ProjectionParams | float4 | x=1.0（如果当前使用翻转的投影矩阵渲染，则为–1.0），y=Near，z=Far，w=1.0+1.0/Far。Near和Far是摄像机近/远裁剪平面与摄像机的距离。 |
| _ScreenParams | float4 | x=width，y=height，z=1.0+1.0/width，w=1.0+1.0/height。width和height是该摄像机的渲染目标（Render Target）的像素宽高。 |
| _ZBufferParams | float4 | x=1-Far/Near，y=Far/Near，z=x/Far，w=y/Far。用于线性化Z缓冲区的深度值。 |
| unity_OrthoParams | float4 | x=width，y=height，z未定义，w=1.0（正交摄像机）或0.0（透视摄像机）。width和height是正交摄像机的宽高。 |
| unity_CameraProjection | float4x4 | 摄像机的投影矩阵。 |
| unity_CameraInvProjection | float4x4 | 摄像机投影矩阵的逆矩阵。 |
| unity_CameraWorldClipPlanes[6] | float4 | 摄像机视锥体6个裁剪平面在世界空间下的表示，顺序为：左、右、下、上、近、远。 |

时间以秒为单位，并由项目Time设置中的时间乘数（Time Multiplier）进行缩放。没有内置变量可用于访问未缩放的时间。

| 名称 | 类型 | 值 |
| --- | --- | --- |
| _Time | float4 | 自关卡加载以来的时间(t/20,t,t*2,t*3)，用于将着色器中的内容动画化。 |
| _SinTime | float4 | 时间正弦：(t/8,t/4,t/2,t)。 |
| _CosTime | float4 | 时间余弦：(t/8,t/4,t/2,t)。 |
| unity_DeltaTime | float4 | 增量时间：(dt,1/dt,smoothDt,1/smoothDt)。 |

光源参数以不同的方式传递给着色器，具体取决于使用哪个渲染路径， 以及着色器中使用哪种光源模式通道标签。

前向渲染（ForwardBase和ForwardAdd通道类型）：

| 名称 | 类型 | 值 |
| --- | --- | --- |
| _LightColor0（UnityLightingCommon.cginc） | fixed4 | 光源颜色。 |
| _WorldSpaceLightPos0 | float4 | 方向光：(世界空间方向,0)。其他光源：(世界空间位置,1)。 |
| unity_WorldToLight（AutoLight.cginc） | float4x4 | 世界/光源矩阵。用于对剪影和衰减纹理进行采样。 |
| unity_4LightPosX0、unity_4LightPosY0、unity_4LightPosZ0 | float4 | 前四个非重要点光源的世界空间位置。仅限ForwardBase通道。 |
| unity_4LightAtten0 | float4 | 前四个非重要点光源的衰减因子。仅限ForwardBase通道。 |
| unity_LightColor | half4[4] | 前四个非重要点光源的颜色。仅限ForwardBase通道。 |
| unity_WorldToShadow | float4x4[4] | 世界空间到阴影空间的变换矩阵。一个矩阵用于聚光灯，最多四个矩阵用于定向光级联。 |

延迟着色和延迟光照，在光照通道着色器中使用（全部在 UnityDeferredLibrary.cginc中声明）：

| 名称 | 类型 | 值 |
| --- | --- | --- |
| _LightColor | float4 | 光源颜色。 |
| unity_WorldToLight | float4x4 | 世界/光源矩阵。用于对剪影和衰减纹理进行采样。 |
| unity_WorldToShadow | float4x4[4] | 世界空间到阴影空间的变换矩阵。一个矩阵用于聚光灯，最多四个矩阵用于定向光级联。 |

顶点光照渲染（Vertex通道类型）：

> 最多可为Vertex通道类型设置8个光源；始终从最亮的光源开始排序。因此，如果希望一次渲染受两个光源影响的对象，可直接采用数组中前两个条目。如果影响对象的光源数量少于8，则其余光源的颜色将设置为黑色。

| 名称 | 类型 | 值 |
| --- | --- | --- |
| unity_LightColor | half4[8] | 光源颜色。 |
| unity_LightPosition | float4[8] | 视图空间光源位置。方向光为(-direction,0)，点光源为(position,1)。 |
| unity_LightAtten | half4[8] | 光源衰减因子。x=cos(点光源角度/2)（点光源）或-1（非点光源），y=1/cos(点光源角度/4)（点光源）或1（非点光源），y=1/cos(点光源角度/4)（点光源）或1（非点光源），z是衰减因子的平方，w是光源范围的平方根。 |
| unity_SpotDirection | float4[8] | 观察空间下的点光源位置。非点光源为(0,0,1,0)。

光照贴图

| 名称 | 类型 | 值 |
| --- | --- | --- |
| unity_Lightmap | Texture2D | 包含光照贴图信息。 |
| unity_LightmapST | float4[8] | 缩放UV信息并转换到正确的范围以对光照贴图纹理进行采样。 |

雾效和环境光

| 名称 | 类型 | 值 |
| --- | --- | --- |
| unity_AmbientSky | fixed4 | 梯度环境光照情况下的天空环境光照颜色。 |
| unity_AmbientEquator | fixed4 | 梯度环境光照情况下的赤道环境光照颜色。 |
| unity_AmbientGround | fixed4 | 梯度环境光照情况下的地面环境光照颜色。 |
| UNITY_LIGHTMODEL_AMBIENT | fixed4 | 环境光照颜色（梯度环境情况下的天空颜色）。旧版变量。 |
| unity_FogColor | fixed4 | 雾效颜色。 |
| unity_FogParams | float4 | 用于雾效计算的参数。 |

其他

| 名称 | 类型 | 值 |
| --- | --- | --- |
| unity_LODFade | float4 | 使用LODGroup时的细节级别淡入淡出。x为淡入淡出（0~1），y为量化为16级的淡入淡出，z和w未定义。 |
| _TextureSampleAdd | float4 | 根据所使用的纹理是Alpha8格式（值为(1,1,1,0)）还是其他格式（值为(0,0,0,0)）。由Unity仅针对UI自动设置。 |