Unity中的渲染管线渲染场景主要分为三个阶段：

1. **剔除**（Culling）：剔除摄像机不可见对象（**视锥体剔除**Frustum Culling）和被遮挡对象（**遮挡剔除**Occlusion Culling）；
2. **渲染**（Rendering）：将可见对象经过一系列计算（如光照计算）后绘制到像素缓冲区中；
3. **后期处理**（Post-Processing）：对像素缓冲区上的颜色执行后期处理操作（如景深），生成最终的输出帧；

Unity提供四种渲染管线，分别为**内置渲染管线**（Built-in Render Pipeline）、**通用渲染管线**（Universal Render Pipeline，URP）、**高清渲染管线**（High Definition Render Pipeline，HDRP）、**可编程渲染管线**（Scriptable Render Pipeline，SRP）。不同的管线具有不同的特性和限制。

内置渲染管线提供了两种渲染路径可供选择，分别是**前向渲染路径**（Forward Rendering Paths）和**延迟渲染路径**（Forward Rendering Paths）。

- 在使用（多通道）前向渲染路径时，场景中的所有对象都是按顺序渲染的。每个对象根据受到的光源数量可能在多个通道中渲染，因此当光源较多时，渲染成本会急剧上升。这种类型的渲染通常提供了各种各样的着色器，并且可以轻松地处理透明度。
- 在使用延迟渲染路径时，所有不透明几何体先渲染到缓冲区。然后在延迟通道中，每个像素按顺序着色。渲染顺序主要取决于像素受到的光源数量。对于透明物体以及某些使用了复杂着色器的对象，仍然需要额外的前向渲染通道。这种类型的渲染通常用于处理包含许多动态光源的场景。

HDRP是一种混合了前向和延迟渲染的瓦片/簇渲染管线。HDRP提供了先进的渲染和着色功能，是专为要求逼真视觉的PC和高端游戏主机项目设计的。

> 瓦片是帧中的一个小型二维方形像素几何，而簇则是摄像机视锥体中的一个三维几何体。不管是瓦片还是簇，它们的渲染技术都依赖于一个光源列表，可以在一个单独的通道中根据这个列表进行光照计算。
> 不透明对象多使用瓦片系统进行着色，而透明对象的着色则多使用簇系统。
> 与内置渲染管线相比，HDRP的光照处理更快，带宽消耗更少。

URP是一种快速的单通道前向渲染管线，被设计用于不知此计算着色技术的低端设备。URP可以为中端设备提供更高质量的图形性能，有时性能消耗甚至低于内置渲染管线。URP根据每个对象来剔除光线，并允许在单个通道中计算光照，这会降低Draw Call。此外，URP也支持2D渲染和延迟渲染。

可以在Edit > Project Settings下的图形设置Graphics和质量Quality设置中指定要使用的渲染管线。

如果当前质量等级指定了要使用的渲染管线，则使用该渲染管线，否则使用图形设置中指定的默认渲染管线。如果两个设置都没有指定具体渲染管线，则使用内置渲染管线。

在C#脚本中也可以获取或设置渲染管线：

- 使用`GraphicsSetting.currentRenderPipeline`获取当前活动渲染管线的资产。
- 使用`GraphicsSetting.defaultRenderPipeline`和`QualitySetting.renderPipeline`判断当前活动渲染管线是否是默认渲染管线或设置当前活动渲染管线。
- 使用`RenderPipelineManager.currentRenderPipeline`获取当前活动渲染管线的实例。
- 通过`RenderPipelineManager.activeRenderPipelineTypeChanged`来注册活动渲染管线改变事件。