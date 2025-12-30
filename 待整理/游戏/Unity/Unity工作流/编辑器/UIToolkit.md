# 简介

UI Toolkit是用于开发用户界面（UI）的工具集合，可以用于扩展编辑器、开发运行时UI。UI Toolkit是基于Web开发的，支持样式表、动态布局和上下文事件处理。

UI Toolkit的UI系统主要包括：

- 可视树（Visual Tree）：由轻量级节点组成的对象图，包含窗口和所有控件元素，定义了使用UI Toolkit构建的每个UI。
- 控件（Controls）：一个标准UI控件库，如按钮、弹出窗口、列表视图和颜色选择器，可以按原样使用或修改控件，可以创建自己的控件。
- 数据绑定系统（Data Binding System）：链接数据和控件。
- 布局引擎（Layout Engine）：基于CSS Flexbox模型的布局系统，根据布局和样式属性定位元素。
- 事件系统（Event System）：绑定控件和用户输入，包括一个调度程序、一个处理程序、一个合成器和一个事件类型库。
- UI渲染器（UI Renderer）：基于Unity图形设备层的渲染系统。
- 编辑器支持（Editor UI Support）：用于创建编辑器UI的一组控件。
- 运行时支持（Runtime UI Support）：用于运行时UI的一组控件。

优点：

- 标准的UI开发工作流，配套的UI工具。
- 使用Flexbox模型，容易实现UI适配，易跨平台。
- 可使用样式表自定义任何元素外观样式，复用性、灵活性好，自由度高。
- 高性能。仅仅读取UXML和USS文件信息，一个Draw Call绘制所有UI，且不依赖于`GameObject`，不参与游戏循环。
- 同时支持Editor和Runtime。

缺点：

- 不依赖`GameObject`，难以制作3D世界中的可交互UI。
- 不支持Shader，难以制作特效。
- 不支持`Animatpr`，无法制作实时循环动画。
- 表现层面的特性支持较少，不适合制作表现力较好的UI和运行时UI。

# Inspector扩展

可以使用UI Toolkit为`MonoBehaviour`和`ScriptableObject`派生类自定义Inspector窗口显示。

- 创建用户友好型界面。
- 更好的组织属性。
- 根据设置显示或隐藏指定属性。
- 提供有关各个设置和属性的含义。

在自定义时，需要**创建一个从`Editor`派生的类，并使用`CustomEdirtor`属性指定为哪个类自定义Inspector窗口显示**。

> 需要注意的是，自定义Inspector窗口显示的类文件必须位于Editor文件夹或仅用于Editor的程序集中，否则将由于无法引用`UnityEditor`类而无法创建。

重写`Editor`的`CreateInspectorGUI()`方法来自定义Inspector窗口显示。`CreateInspectorGUI()`方法为Inspector窗口显示构建一个可视树，返回包含UI的`VisualElement`对象。

> 在IMGUI中，`Editor`类还有可以通过`OnInspectorGUI()`方法来自定义Inspector窗口显示。`CreateInspectorGUI()`方法的优先级高于`OnInspectorGUI()`方法，只有当`CreateInspectorGUI()`方法返回`null`时，`OnInspectorGUI()`方法才生效。

在`CreateInspectorGUI()`方法中可以使用UXML文件，通常做法是加载UXML文件，然后克隆并将其添加到可视树中。使用`AssetDatabase`类加载UXML文件需要硬编码该文件路径，因此Unity支持通过GUID为自定义Inspector窗口显示的类引用UXML文件，即定义公共`VisualTreeAsset`字段并在类的Inspector窗口中赋值。

自定义Inspector窗口中的控件可以在`CreateInspectorGUI()`方法中手动绑定到目标的各个属性并添加事件。另外，UI Toolkit支持使用序列化对象数据绑定将序列化字段绑定到控件上，只需要将属性分配到对应控件的Binding Path即可（类型必须匹配）。其中，PropertyField控件适用于大多数类型的属性，会为不同类型的属性生成默认的控件，但因为不知道控件类型而无法在UI Builder中预览。

PropertyField控件通常用于自定义可序列化数据和数组。此时，可以为每个元素自定义Inspector显示，但该类需要继承`PropertyDrawer`类而不是`Editor`类，然后重写`CreatePropertyGUI()`方法。