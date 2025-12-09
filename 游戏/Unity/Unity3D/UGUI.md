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

## 裁切

UGUI的裁切为Mask（IMaskable）和RectMask2D（IClippable）两种。

Mask会赋予Image一个特殊的材质，这个材质会给Image的每个像素点进行标记，将标记结果存放在一个缓存内（这个缓存叫做**Stencil Buffer**）当子级UI进行渲染的时候会去检查这个Stencil Buffer内的标记，如果当前覆盖的区域存在标记（即该区域在Image的覆盖范围内），进行渲染，否则不渲染。Mask 组件调用了模板材质球构建了一个自己的材质球，因此它使用了实时渲染中的模板方法来裁切不需要显示的部分，所有在 Mask 组件的子节点都会进行裁切。**Mask是在GPU中做的裁切，使用的方法是着色器中的模板方法**。

GPU为每个像素点分配一个称之为Stencil Buffer的1字节大小的内存区域，这个区域可以实现保存或丢弃像素的目的。Stencil Buffer是为了实现多个“绘画者”之间互相通信而存在的。由于GPU是流水线作业，它们之间无法直接通信，所以通过这种**共享数据区**的方式来传递消息，实现特殊效果。

Mask实现的具体原理是一个DC来创建Stencil mask(来做像素剔除)，然后画所有子UI，再在最后一个DC移掉Stencil mask。这头尾两个DC无法跟其他UI操作进行Batch，所以表面上看加个Mask就会多2个DC，但是，因为Mask这种类似“汉堡包式”的渲染顺序，所有Mask的子节点与其他UI其实已经处在两个世界了，上面提到的层级合并规则只能分别作用于这两个世界了，所以很多原本可以合并的UI就无法合并了。

RectMask2D工作流程：

1. C#层：找出父物体中所有RectMask2D覆盖区域的交集（**FindCullAndClipWorldRect**）
2. C#层：所有继承MaskGraphic的子物体组件调用方法设置剪裁区域（**SetClipRect**）传递给Shader
3. Shader层：接收到矩形区域_ClipRect，片元着色器中判断像素是否在矩形区域内，不在则透明度设置为0（**UnityGet2DClipping** ）
4. Shader层：丢弃掉alpha小于0.001的元素（**clip (color.a - 0.001)**）

Mask组件需要依赖一个Image组件，裁剪区域就是Image的大小。

- **Mask会在首尾（首=Mask节点，尾=Mask节点下的孩子遍历完后）多出两个drawcall，多个Mask间如果符合合批条件这两个drawcall可以对应合批（mask1 的首 和 mask2 的首合；mask1 的尾 和 mask2 的尾合。首尾不能合）**。
- **计算depth的时候，当遍历到一个Mask的首，把它当做一个不可合批的UI节点看待，但注意可以作为其孩子UI节点的bottomUI。**
- **Mask内的UI节点和非Mask外的UI节点不能合批，但多个Mask内的UI节点间如果符合合批条件，可以合批。**

RectMask2D不需要依赖一个Image组件，其裁剪区域就是它的RectTransform的rect大小。

- **RectMask2D节点下的所有孩子都不能与外界UI节点合批且多个RectMask2D之间不能合批。**
- **计算depth的时候，所有的RectMask2D都按一般UI节点看待，只是它没有CanvasRenderer组件，不能看做任何UI控件的bottomUI。**

所以：

- 当一个界面只有一个mask，那么，RectMask2D优于Mask。
- 当有两个mask，那么，两者差不多。
- 当大于两个mask，那么，Mask优于RectMask2D。
- 如果只是矩形裁切，RectMask2D不需要重新创建了材质，每帧都使用新材质再次渲染，所以**RectMask2D的效率会比Mask要高**。

## 层级管理

影响层级的因素：在Hierarchy中的顺序、Camera的Clear Flags和Canvas的Renderer Mode。

在Overlay渲染模式下，Sort Order大的Canvas渲染再前。在Camera模式下，越下面的Sorting Layer的显示优先级越高；同一Sorting Layer下，Order in Layer越大的越先显示。

## 性能优化

### 渲染

渲染是图形数据在GPU上经过运算处理，最后输出到屏幕的过程。

渲染管线：游戏中物体经过CPU计算，然后调用GPU进行顶点处理、图元装配、光栅化、像素处理、缓存。

帧缓存：存储每个像素的色彩即渲染后的图像。帧缓存常在显存中，显卡不断读取并输出到屏幕。

深度缓存z-buffer：存储像素的深度信息即物体到摄像机的距离。光栅化时便计算各像素的深度，如果新的深度值比现有值更近，则像素颜色被写入帧缓存，并替换深度缓存。

绘制调用Draw Call：CPU准备数据并对底层图形程序接口进行调用，即每帧调用显卡渲染物体的次数。

即时遮挡剔除Instant Occlusion Culling：当物体被送进渲染管线之前，将摄像机视角内看不到的物体进行剔除，从而减少每帧渲染的数据量，提高渲染性能。虽然可以降低GPU工作量，但会提高CPU的工作量。

多细节层次渲染Levels of Details（LOD）：根据物体模型的节点在显示环境中的所处位置和重要度，决定物体渲染的资源分配，降低非重要物体的面数和细节度，从而获得高效率的渲染运算。虽然可以提高渲染效率，但占用内存量即空间换时间。

### 图集

2D项目使用精灵和其他图形来创建其场景的视觉效果，意味着单个项目可能包含许多纹理文件。Unity通常会为场景中的每个纹理发出一个Drall Call；但是，在具有许多纹理的项目中，多个Drall Call会占用大量资源，并会对项目的性能产生负面影响。

精灵图集（Sprite Atlas）是一种将多个纹理合并为一个组合纹理的资源。Unity可以调用此单个纹理来发出单个Drall Call而不是发出多个Drall Call，能够以较小的性能开销一次性访问压缩的纹理。此外，还可以对图片资源压缩（Unity只能压缩大小为2次方的图片），整合不规则图片，控制如何在项目运行时加载精灵图集。

图集打包方式：Packing Tag图集名称，Sprite Packer；创建图集，选择Sprite。安卓使用ETC1压缩，IOS默认使用PVRTC。

- 设计UI时要考虑重用性，如一些边框、按钮等，这些作为共享资源，放在1~3张大图集中，称为**重用图集**；
- 其它非重用UI按照功能模块进行划分，每个模块使用1~2张图集，为**功能图集**；
- 对于一些UI，如果同时用到**功能图集**与**重用图集**，但是其**功能图集**剩下的“空位”较多，则可以考虑将用到的**重用图集**中的元素单独拎出来，合入**功能图集**中，从而做到让UI只依赖于**功能图集**。也就是通过一定的冗余，来达到性能的提升。
- 尽量紧凑，没有太多空白。
- 同一个界面的小图尽量在一个图集里，能减少Draw Call。
- 打开一个界面时只加载必要的图集，关闭时可以方便地释放图集，内存管理方便，加载性能好。
- AssetBundle打包\热更粒度合理，不能出现“热更一个新界面，大量图集都需要热更”的情况。
- 维护方便，当界面变化时，调整方便，包括生成图集、调整引用、新图集尺寸变化的影响、新图集AssetBundle变化的影响等等。
- 图集间隙尽量少，主要靠图集工具，常见的比如更紧凑的多边形Mesh替代Rect Mesh、旋转、切割等等。

### 优化

在渲染上，GPU、CPU两者的性能瓶颈往往是CPU；GPU的性能瓶颈往往是像素点填充率（Overdraw导致），CPU的性能瓶颈往往是Drawcall。所以，渲染性能排查，几项指标关注优先级应该是：Drawcall > Overdraw > 面片。

#### Draw Call优化

Draw Call优化主要通过精灵打包成图集与合批技术。

合批（Draw Call Batching）是在一次Draw Call中批量处理多个物体。只要物体的变换和材质相同，GPU就可以按完全相同的方式进行处理，即可以把它们放在一个Draw Call中。Unity提供了**动态合批**（Dynamic Batching）和**静态合批**（Static Batching）两种方式。

静态合批是将静态（不移动）GameObject组合成大网格，然后进行绘制。静态合批使用比较简单，PlayerSettings中开启Static Batching，然后对需要静态合批物体的Static打钩即可，Unity会自动合并被标记为Static的对象，前提它们共享相同的材质，且不移动、旋转或缩放。

静态批处理需要额外的内存来存储合并的几何体。如果多个GameObject在静态批处理之前共享相同的几何体，则会在编辑器或运行时为每个GameObject创建几何体的副本，这会增大内存的开销。静态合批在大多数平台上的限制是64k顶点和64k索引（OpenGLES上是48k索引，macOS上是32k索引）。

动态合批是将一些足够小的网格，在CPU上转换它们的顶点，将许多相似的顶点组合在一起，并一次性绘制它们。动态合批处理的工作是在cpu上将所有GameObject顶点转换到世界空间，因此，如果该工作小于执行绘制调用，则这是一个优势。

- 动态合批处理动态的GameObjects的每个顶点都有一定的开销，因此动态合批处理仅应用于包含不超过900个顶点和不超过300个顶点的网格。
- 如果GameObjects在Transform上包含镜像，则不会对其进行动态合批处理（例如，scale 为1的GameObject A和scale为-1的GameObject B无法一起动态合批处理。对于两个相同的物体，当两个物体三个轴向的负缩放的个数为偶数个时可以合批）。
- 使用不同的Material实例会导致GameObjects不能一起批处理，即使它们基本相同。阴影渲染是例外，不同材质的阴影会动态合批，只要绘制阴影的pass是相同的，因为阴影跟其他贴图等数据无关
- 带有光照贴图的GameObjects有额外的渲染器参数：保存光照贴图的索引和偏移/缩放。一般来说，动态光照贴图的GameObjects应指向完全相同的光照贴图位置才能被动态合批处理。
- 使用多个pass的shader不会被动态合批处理。
- 目前,只有 Mesh Renderers, Trail Renderers, Line Renderers, Particle Systems和Sprite Renderers支持合批处理，而skinned Meshes，Cloth和其他类型的渲染组件不支持合批处理。
- 渲染器仅与其他相同类型的渲染器进行合批处理。
- 对于半透明的GameObject，按照从前到后的顺序绘制，Unity首先按这个顺序对GameObjects进行排序，然后尝试对它们进行批处理，但由于必须严格满足顺序，这通常意味着对于半透明的材质更少使用合批处理。
- 手动的合并GameObject是代替合批处理的好办法，比如使用Mesh.CombineMeshes，或者直接在建模时将多个网格合并成单个网格。
- 动态合批时，保证同一图集的元素在Hierarchy中连续；少用Mask，改用RectMask2D；

#### Overdraw

Overdraw是指屏幕上的某个像素在同一帧的时间内被绘制了多次。UWA分析报告中，以总填充总数来表达一帧内渲染的像素数量，过多Overdraw可能会引起GPU过载，影响动画的播放和界面响应速度。

Mask组件自带两层Overdraw，RectMask2d 就是一层；Text组件的Shadow会增加一层Overdraw，而OutLine是复制了四份Shadow实现的。对于弹出窗口，位于底层的窗口如果被上层遮挡，请将它从Camera渲染层级里移除并将不可见的Canvas设置enable = false，不在Camera渲染层级里的Canvas.enable = true，它下面的UI仍然会产生OverDraw。

对于被遮挡的Canvas，如果用SetActive(false)，会导致Canvas的VBO数据失效，随意再次激活时会导致rebuild和rebatch，对CPU造成负担。通常可以将遮挡的Canvas移出视野或者使用CanvasGroup组件将alpha设为0。

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
