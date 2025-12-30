# UGUI

Unity用户图像界面（Unity Graphical User Interface，UGUI）是Unity 4.6版本之后引入的Unity公司自研的一套界面显示系统。4.6版本以前Unity使用GUI、NGUI插件。

## 基本控件

### 矩形变换Rect Transform

所有UI元素都具有的变换组件，派生自`Transform`，表示可包含UI元素的矩形。如果矩形变换的父项也是矩形变换，则子矩形变换还可指定子矩形应该如何相对于父矩形进行定位和大小调整。

- Pos位置：矩形轴心点相对于锚点的位置。
- Width/Height矩形的宽度和高度。
- Left/Top/Right/Bottom矩形边缘相对于锚点的位置：可视为由锚点定义的矩形内的填充。当锚点分离时将取代Pos和Width/Height。
- Anchors锚点：矩形左下角（Min）和右上角（Max）的锚点，定义为父矩形大小的一个比例。(0,0)相当于锚定到父项的左下角，而(1,1)相当于锚定到父项的右上角。
- Pivot轴心点：矩形旋转围绕的轴心点的位置，定义为矩形本身大小的一个比例。(0,0)相当于左下角，而(1,1)相当于右上角。
- Rotation旋转（以度为单位）。
- Scale缩放。

### 画布Canvas

画布Canvas是绘制UI元素的载体，所有元素必须在Canvas之下。UI元素的绘制顺序依赖于层次面板中的顺序，同一画布中的元素在上的先渲染。

- Render Mode渲染方式：Screen Space-Overlay覆盖模式（画布原点与世界坐标系原点重合，UI将绘制在其他元素之前，且绘制过程独立于场景元素和摄像机设置，画布尺寸由屏幕大小和分辨率决定）；Screen Space-Camera相机模式（使用单独的相机渲染UI，绘制效果受摄像机参数影响）；World Space世界模式（画布渲染与3D空间，与场景元素性质相同）。
- Rixel Perfect完美像素：锐化屏幕显示效果。
- Sort Order渲染顺序：确定不同画布的渲染顺序，值越大越后渲染。

### 图片Image

- Source Image图片源
- Image Type图片类型：Simple简单；Sliced切割；Tiled平铺；Filled填充。
- Color颜色
- Material材质
- Raycast Target射线目标

### 文本Text

- Text文本。
- Font字体。
- Font Style字体样式。
- Font Size字体大小。
- Line Spacing行间距。
- Rich Text是否使用富文本样式。
- Alignment对齐。
- Align By Geometr
- Horizontal/Vertical Overflow水平/垂直溢出。
- Best Fit大小自适应。
- Color颜色。
- Material材质。
- Raycast Target射线目标。

### 按钮Button

- Interactable是否启用交互。
- Transition过渡方式：
  - Color Tint颜色过渡：
    - Target Graphic目标图形。
    - Normal Color正常颜色。
    - Highlighted Color高亮颜色：鼠标悬浮在按钮上时的颜色。
    - Pressed Color点击时颜色。
    - Disable Color禁用时的颜色。
    - Color Multiplier颜色倍数。
    - Fade Duration变化时间。
  - Sprite Swap精灵过渡。
  - Animation动画过渡。
- On Click点击事件：先选择游戏对象，再选择该对象上脚本，最好选择点击要执行的方法。只能绑定零或一个参数的方法。

### 开关Toggle

- Is On复选框的选中状态。
- Toggle Transition状态改变时是否启用过渡。
- Graphic切换的背景图片。
- Group单选组。
- On Value Changed值改变事件。

### 滑块Slider

- File Rect填充矩形区域。
- Handle Rect手柄矩形区域。
- Direction方向。
- Min/Max Value最小/大值。
- Whole Numbers整数数值。
- Value数值。
- On Value Changed值改变事件。

### 滚动条ScrollBar

- Handle Rect手柄矩形区域。
- Direction方向。
- Value数值。
- Size手柄尺寸。
- Number of Steps从开始滑到末尾的步骤。
- On Value Changed值改变事件。

### 下拉菜单DropDown

- Template模板。
- Caption Text标题文本。
- Caption Image标题图片。
- Item Text选项文本。
- Item Image选项图片。
- Value选项的值。
- Options选项组。
- On Value Changed值改变事件。

### 输入框InputFiled

- Text Component文本组件。
- Text文本。
- Character Limit字符数量限制。
- Content Type内容类型：Standard标准；Autocorrected自动验证；Integer Number数字；Decimal Number小数；Alphanumeric字母数字；Name姓名；Email Address邮件地址；Password密码；Pin仅输入整数，用*隐藏字符；Custom自定义。
- Line Type行类型：Single Line单行；Multi Line Submit多行文本，回车提交；Multi Line NewLine多行文本，回车换行。
- Caret Blink Rate光标闪烁速度。
- Caret Width光标宽度。
- Custom Caret Color是否自定义光标颜色。
- Selection Color选择的字符颜色。
- Hide Mobile Input是否隐藏移动输入。
- Read Only是否只读。
- On Value Changed值改变事件。
- On End Editor编辑结束事件。

### 自动布局组Grid Layout Group

网格布局组，将子元素以表格形式自动排列。

- Padding间距：Left左边距；Right右边距；Top上边距；Bottom下边距。
- Cell Size元素尺寸。
- Spacing元素之间的空白。
- Start Corner开始位置：Upper Left左上角；Upper Right右上角；Lower Left左下角；Lower Right右下角。
- Start Axis开始方向：Horizontal水平；Vertical垂直。
- Child Alignment元素对齐方式。
- Constraint约束：Flexible灵活的；Fixed Column Count固定列数；Fixed Row Count固定行数。

### 水平布局组Horizontal Layout Group

将子元素按水平方向自动排列。

- Child Force Expand元素展开。

### 垂直布局组Vertical Layout Group

将子元素按垂直方向自动排列。

### 布局元素Layout Element

可以为自动布局组中的子元素指定大小。

- Ignore Layout忽略布局。
- Min Width/Height最小宽高。
- Preferred Width/Height优先宽高（不会超过父物体）。
- Flexible Width/Height弹性宽高比例。

### 内容大小适配器Content Size Fitter

根据子元素Layout Element组件自动调整父容器大小。

- Horizontal/Vertical Fit水平适配：Unconstrained无约束；Min Size最小尺寸；Perferred Size优先尺寸。

## Draw Call优化

### 渲染

渲染是图形数据在GPU上经过运算处理，最后输出到屏幕的过程。

渲染管线：游戏中物体经过CPU计算，然后调用PU进行顶点处理、图元装配、光栅化、像素处理、缓存。

帧缓存：存储每个像素的色彩即渲染后的图像。帧缓存常在显存中，显卡不断读取并输出到屏幕。

深度缓存z-buffer：存储像素的深度信息即物体到摄像机的距离。光栅化时便计算各像素的深度，如果新的深度值比现有值更近，则像素颜色被写入帧缓存，并替换深度缓存。

绘制调用Draw Call：每次引擎准备数据并通知图形学的过程，即每帧调用显卡渲染物体的次数。

即时遮挡剔除Instant Occlusion Culling：当物体被送进渲染管线之前，将摄像机视角内看不到的物体进行剔除，从而减少每帧渲染的数据量，提高渲染性能。虽然可以降低GPU工作量，但会提高CPU的工作量。

多细节层次渲染Levels of Details（LOD）：根据物体模型的节点在显示环境中的所处位置和重要度，决定物体渲染的资源分配，降低非重要物体的面数和细节度，从而获得高效率的渲染运算。虽然可以提高渲染效率，但占用内存量即空间换时间。

### 优化

在界面中默认一个图片一个Draw Call，一个图片显示多次仍仅一个Drall Call。

精灵打包：做界面时使用小图，而在项目发布引擎会根据Sprite的Packing Tag自动将小图合并在一张大图中，从而减少Draw Calls。需要在在Project Setting的Editor设置Sprite Packer模式。

使用Sprite Editor切割图集。

## 事件

### 通过编辑器绑定事件

在UI元素的监视面板中给UI元素添加事件。

### AddListenser

先获取对应组件，在对应组件的事件参数上调用`AddListener`方法添加事件。

``` C#
Button btn = transform.Find("Button").GetComponent<Button>();
btn.OnClick.AddListener(Method);
```

### 实现接口

使用接口实现事件只需要实现相应的接口，编写需要执行的方法即可。事件的注册和调用不要自己做。

鼠标指针类的接口：`IPointerEnterHandler`、`IPointerExitHandler`、`IPointerDownHandler`、`IPointerUpHandler`、`IPointerClickHandler`。

拖拽类的接口：`IBeginDragHandler`、`IDragHandler`、`IEndDragHandler`、`IDropHandler`。

点选类的接口：`IUpdateSelectedHandler`、`ISelectHandler`、`IDeselectHandler`。

输入类的接口：`IScrollHandler`、`IMoveHandler`、`ISubmitHandler`、`ICancelHandler`。

``` C#
class OnClickTest : IPointerClickHandler
{
    public void OnPointClick (PointerEventData eventData)
    {
        print("点击了{0}次", eventData.clickCount);
    }
}
```
