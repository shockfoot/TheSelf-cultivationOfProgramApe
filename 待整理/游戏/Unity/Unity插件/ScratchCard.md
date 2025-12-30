# 简介

《Scratch Card》是一个易于使用的插件，只需要使用其提供的预制体并设置一些参数，就可以在`MeshRender`、`SpriteRender`和`Image`组件上实现刮刮乐效果。

该插件无需添加任何碰撞器，支持标准、URP、HDRP，兼容Input Manager和Input System Package。当然，请确保Unity版本不低于2019.4。

使用时，只需要从`Assets/ScratchCard/Prefabs`下添加一个ScratchCard预制体到场景或者在Hierarchy面板右键创建一个ScratchCard预制体。选择渲染类型、设置主摄像机和涂层表面精灵即可。

ScratchCard预制体挂载了`ScratchCardManager`组件以实现刮擦逻辑和管理刮擦进度，其包含以下参数：

- Scratch Card：`ScratchCard`组件。
- Erase Progress：`EraseProgress`组件。
- Main Camera：摄像机组件。如果为空，则使用主摄像机。
- Render Type：涂层表面的渲染类型，值可为`MeshRenderer`、`SpriteRenderer`或`Image`。选择不填的渲染类型会绑定不同的组件。
- - Mesh Card：`MeshRenderer`组件。
- - Sprite Card：`SpriteRenderer`组件。
- - Image Card：`Image`组件。
- Sprite：涂层表面精灵。
- Input Enable：是否启用输入。
- Use Pressure：是否检测输入时的压力。
- Check Canvas Raycasts：是否检测画布射线碰撞。如果启用，如果画布上方有组件阻挡画布射线检测，则不会进行刮擦。
- Canvases For Raycasts Blocking：忽略画布射线检测的组件列表。
- Brush Texture：笔刷纹理。
- Brush Opacity：笔刷纹理透明度。
- Brush Size：笔刷尺寸。
- Scratch Mode：刮擦模式。
- - Erase：擦除模式。
- - Restore：恢复模式。

其中，`ScratchCard`组件创建并配置RenderTexture，从InputController获取获取用户的输入，然后渲染。`EraseProgress`组件通过着色器计算该RenderTexture的红色通道以获取刮擦进度。