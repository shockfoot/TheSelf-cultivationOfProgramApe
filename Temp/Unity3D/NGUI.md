# NGUI

NGUI是专门针对Unity引擎、用C#编写的一套插件，已经成为应用最广、最成熟的Unity制作UI的插件，完美地弥补了Unity引擎原生GUI系统的不足。NGUI最早版本发布于2011年12月，被广大开发者人为是Unity平台最强大的第三方UI系统。程序员可以利用它提供的一整套UI框架和事件通知系统来进行自己项目的UI设计和制作。

Unity是基于组件形式的引擎，任何一个功能都是一个独立的组件。组件其上就是一个类、一个对象、一个脚本文件。NGUI中的所有UI也都是通过组件的形式体现的。

| NGUI                        | UGUI                             |
| --------------------------- | -------------------------------- |
| UI Root只有世界坐标         | Canvas有世界坐标和屏幕坐标       |
| 裁剪使用Panel中的Clip       | 裁剪使用遮罩Mask                 |
| 事件需要碰撞器才能触发      | 实现事件系统接口                 |
| 锚点相对对象可选            | 锚点相对于父物体                 |
| 使用Sprite需要制作Atlas图集 | 没有Atlas图集，使用Sprite packer |
| 支持图文混排                | 不支持图文混排                   |
| 通过Depth顺序渲染           | 通过物体顺序渲染                 |

创建一个新的Unity工程项目后，导入NGUI插件资源。导入成功后Unity菜单栏会出现一个“NGUI”菜单，包含了NGUI所有的功能。

NGUI插件目录结构为Editor编辑器扩展、Examples工程示例、Resources资源、Scripts脚本。

NGUI中所有UI元素位于UI Root父物体下，默认自带一个Camera。NGUI的UI元素只能通过相机渲染。

NGUI只提供了两个基础控件Sprite和Label。高级控件都是由此制作。NGUI中所有涉及交互的控件都需要具有碰撞器组件。

## 功能

### Selection选择

选择UI元素。

### Create创建

创建UI元素。

### Attach添加

给选择的UI元素添加组件，主要是碰撞器和相关控件脚本。

### Tween动画

NGUI提供了许多动画组件：Alpha、Color、Field of View、Height、Width、Orthographic Size、Position、Rotation、Scale、Transform、Volume。这些组件可以按需求设置动画曲线Anition Curve以控制动画播放的趋势。

### Open打开

- Altas Maker图集制作和管理：NGUI制作图集后生成三个文件：材质、预制体、图集。减少Draw Call。
- Font Maker字体制作：Bitmap模式将字符放在位图上，无法普适所有字符（减少内存使用），通常用于数字、字母，可以与图片放在同一图集以减少Draw Call，同时可以将图片应用于字符，即表情（图文混排）；Dynamic动态模式可以适配任何字符，增加Draw Call。Characters选择字符范围。制作字体后生成三个文件：图片、材质、预制体。
- Prefab Toolbar
- Panel Tool
- Draw Call Tool
- Camera Tool
- Widget Wizard（Legacy）

### Options选项

选项

## 控件

所有的控件都自带Transform组件，根据功能拥有对应功能组件。

### UI Root画布

- UI Root组件组件：提供缩放功能，让UI控件从视觉上是正常的。
  - Scaling Style缩放模式：Pixel Perfect完美像素（在最小/大宽度Minimun/Maximum Height内保持原有尺寸，会随屏幕的缩放而缩放）；Fixed Size固定大小（随屏幕的缩放而缩放，固定Manual Height，使用图片高度与此值比例缩放）；Fixed Size On Mobiles根据设备固定尺寸（根据设备是移动还是PC选择缩放模式，需要设置Minimun/Maximum Height和Manual Height）。
  - Shrink Portrait UI缩放竖屏UI。
  - Adjust by DPI通过像素适应UI。
- UI Panel组件：收集管理其下所有Widget，通过Widget的Geometry创建实际的Draw Call。没有Panel的元素无法被渲染。
  - Alpha透明度：管理同一Panel下的所有控件的透明度。
  - Depth深度：深度越大越后渲染。
  - Clipping裁剪模式：超出Panel部分的渲染模式。None全显；Soft Clip超出部分不显示；Constrain But Dont Clip。
  - Anchors锚点：不可设置。
  - Show Draw Cell。

### Camera渲染相机

相机的清除标识模式Clear Flags为仅深度Depth Only（仅渲染有色彩对象），投影Projection为正交Orthographic。

- UI Camera组件：负责事件。
  - Event Type事件类型：World；UI；Unity 2D。
  - Event Mask事件遮罩层级。
  - Event Sources事件触发源：Mouse；Touch；KeyBoard，Controller。

### Widget容器

通常一个界面/Panel上的UI元素放在同一个Widget下统一管理。

- Color颜色。
- Pivot轴心点。
- Depth深度。
- Dimensions尺寸。其中Snap是还原为图片的原始尺寸。
- Aspect Ratio缩放模式：Free自有；Based On Width基于宽度；Based On Height基于高度。

### Sprite精灵

- UI Sprite组件：
  - Altas图集。
  - Sprite精灵图片。
  - Type类型：Simple；Sliced九宫切图（主要用于UI控件背景）；Tiled平铺；Filled填充；Advanced高级。
  - Flip翻转：Nothing；Horizontally水平；Vertically垂直；Both垂直加水平。
  - Anchors锚点：None没有；Unified普通模式；Advanced高级模式。

尽量保证一个图层/Panel上的UI元素来自一个图集，并且Depth一样，否则会增加Draw Cells。

### Label文本

- UI Label组件：
  - Unity/NGUI字体。Unity可以直接使用字体文件，NGUI需要将字体文件制作为字体集后才能使用。
  - Font Size字体尺寸：不会超过容器大小。
  - Material材质。
  - Text文本内容。
  - Overflow溢出：Shrink Content文字尺寸不会超过容器；Clamp Content裁剪无法显示的文字；Resize Freely文本容器大小适应文字尺寸；Resize Height文本容器大小仅在高度上适应文字尺寸。
  - Alignment对齐。
  - Keep Crisp保持锐化。
  - Gradient过渡。
  - Effect效果：阴影和外边框。
  - Spacing间距。
  - Max Lines最大行数。
  - BBCode
  - Anchors锚点。

### Anchor锚点

管理和设置UI元素的自适应。如果要UI元素自适应屏幕大小，将该UI元素作为Anchor元素的子物体，设置Anchor的位置。

- UI Anchor组件：
  - UI Camera相机。
  - Container容器，锚点相对对象，默认为UI Root。
  - Side锚点位置。
  - Run Only Once。
  - Relative Offset相对偏移：为物体设置以屏幕百分比为单位的偏移。
  - Pixel Offset像素偏移：让物体在Window系统显示时有像素的偏移。

### Button按钮

按钮由背景（Sprite）和文字（Label）组成，并为其父物体添加Button Script和碰撞器组件。

- UI Button
  - Tween Target按钮点击目标：点击在目标上才有效。
  - Drag Over物体在按钮上通过时，触发事件选项。
  - Transition过渡效果。
  - Colors不同状态下的颜色。
  - Sprites不同状态下的背景图片。
  - OnClick点击事件。

### Toggle单选

单选框通常由背景（Sprite）和勾选状态的勾（Sprite）组成，并为父物体添加Toggle Script和碰撞器组件。

- UI Toggle组件：
  - Group组：如果多个Toggle元素为同一组实现单选。
  - Starting State开始状态：默认是否被勾选。
  - Sprite精灵。
  - Animation动画。
  - Transition过渡：Smooth平滑；Instant瞬间。
  - On Value Change值改变事件。

### Slider滑块

滑块由一个前背景（Sprite）、后背景（Sprite）和滑块（Sprite）组成，并为父物体添加Slider Script和碰撞器组件。前背景无法为0。

- UI Slider组件：
  - Vlaue值。
  - Alpha透明度。
  - Steps进度：滑动的步骤。
  - Foreground前背景。
  - Background后背景。
  - Thumb滑块。
  - Direction方向。
  - On Value Change值改变事件。

### InputFiled输入框

输入框通常由一个背景（Sprite）和一个文本框（Label）组成，并为父物体添加Input Filde Script和碰撞器组件。

- UI Input组件：
  - Label绑定的文本框。
  - Starting Value开始值。
  - Saved As。
  - Active Text Color激活时文本颜色。
  - Inactive Color未激活时文本颜色。
  - Caret Color光标颜色。
  - Selection Color选中文本颜色。
  - Input Type输入模式。
  - On Return Key。
  - Validation。
  - Character Limit字符数量限制。
  - On Submit提交事件。
  - On Change改变事件。

### Scroll View滑动视窗

滑动视窗通常单独在一个Panel中，并添加Scroll View脚本，该组件自动为Panel添加刚体组件；设置裁剪模式为Soft Clip。

将滑动视窗中的子元素放在Panel之下，并为每个子物体添加碰撞器和拖拽滚动视窗Drag Scroll View脚本。

在Panel的同级制作两个滚动条元素，并绑定给Panel。

### Grid网格布局

- UI Grid组件：
  - Arrangement网格排列方向：Horizontal水平；Vertical垂直。
  - Cell Width/Height网格之间宽/高差距：子物体锚点的间距。
  - Column Limit列数限制。
  - Sorting排序方式：None按Index排序；Alphabetic按名字；Horizontal/Vertical按Local Position；Custom自定义。
  - Pivot轴心点。
  - Smooth Tween平滑动画。
  - Hide Inactive。
  - Constrain to Panel。

### Table布局

- UI Table组件：
  - Columns列数。
  - Direction排列方向：Down向下；Up向上。
  - Sorting排序方式：None；Alphabetic按名字；Horizontal；Vertical；Custom。
  - Hide Inactive。
  - Keep Within Panel。
  - Padding子物体之间间距。

## 事件监听

### 直接监听

把相应事件的委托方法脚本直接绑定在控件上，当满足触发条件时就能监听到，但不灵活。

``` C#
// 脚本示例
void OnClick()
{
    Debug.Log("Button is Click!");
}
```

### 拖拽实现监听

给控件添加Event Trigger脚本后，可以在监视面板中选择对象、选择脚本及方法。只能绑定`public`方法。

### UI Listener监听

获取监听的对象后，为该对象添加本类的方法。

``` C#
GameObject button = this.gameObject;
UIEventListener.Get(button).OnClick = Method;
```