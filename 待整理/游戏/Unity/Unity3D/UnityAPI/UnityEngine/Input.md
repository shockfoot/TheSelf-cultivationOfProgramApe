# Input

所属命名空间`UnityEngine`，实现施于`UnityEngine.InputLegacyModule`。

`Input`是连接输入系统，用于获取传统游戏中的输入和移动设备上的多点触控/加速度传感器等数据。

轴（Axis）是输入系统的一个概念。不同的轴映射不同的输入。“Horizontal”和“Vertical”映射至手柄的操纵杆、A、D、W、S键以及方向键；“Mouse X”和“Mouse Y”映射至鼠标的水平和垂直方向上的滑动。“Fire1”、“Fire2”和“Fire3”映射到Ctrl、Alt、Cmd键或三个鼠标键或者手柄上的按钮。可以通过输入管理器添加新的输入轴。

通过使用`Input.GetAxis`而不是`Input.GetButton`进行移动操作，因为`Input.GetAxis`可以映射可以映射到键盘，手柄或鼠标，提供更平滑且可配置的输入，并使脚本更精炼、简单。

输入标志直到下一个`Update`前才重置，因此应在`Update`循环中进行所有输入调用。

iOS和Android设备能够同时跟踪多个触摸屏幕的手指。

## 静态属性

### anyKey

- `public static bool anyKey`

当前是否任何键或鼠标按钮处于被按下状态（只读）？

### anyKeyDown

`public static bool anyKeyDown`

在用户按下任意键或鼠标按钮的第一帧返回`true`（只读）。不检测触屏。

### mousePresent

- `public static bool mousePresent`

是否检测到鼠标设备。在Windows、Android和Metro平台上，该函数检测是否存在物理鼠标，因此可能返回`true`或`false`。在Linux、Mac、WebGL上，该函数将始终返回`true`。 在iOS和游戏主机平台上，该函数将始终返回`false`。

### mousePosition

- `public static Vector3 mousePosition`

以像素坐标表示的鼠标的当前位置（只读）。`mousePosition.z`始终为0。屏幕或窗口的左下角为坐标原点`(0,0)`，右上角坐标为`(Screen.width,Screen.height)`。

即使鼠标不在游戏视图内，`mousePosition`也会提供鼠标位置。以窗口模式运行游戏时，如果鼠标的位置小于0或大于窗口尺寸则说明鼠标在游戏窗口之外。

### mouseScrollDelta

- `public static Vector2 mouseScrollDelta`

当前鼠标的滚动增量（只读）。滚动增量存储在`mouseScrollDelta.y`中，可忽略`x`分量。`mouseScrollDelta`可以是正数（向上）或负数（向下），鼠标滚轮未滚动时为0。触摸板可以通过双指上下移动模拟鼠标滚轮滚动。通常需要根据滚动速率调整`mouseScrollDelta`返回值。

### touchSupported

- `public static bool touchSupported`

指示设备是否支持触屏。根据此属性判断游戏是否需要处理触屏输入，而不是检查平台是否支持触屏，因为平台可能支持多种输入方式。

### multiTouchEnable

- `public static bool multiTouchEnable`

指示系统是否能处理多点触控。

### touchPressureSupported

- `public static bool touchPressureSupported`

指示是否支持触摸压力。

### touchCount

- `public static int touchCount`

触屏次数（只读）。`touchCount`在整个帧期间不会更改。

### touches

- `public static Touch[] touches`

上一帧所以触屏信息的列表（只读）。（分配临时变量）每个元素代表手指触摸屏幕的状态。

### simulateMouseWithTouches

- `public static bool simulateMouseWithTouches`

启/禁用通过触控模拟鼠标操作。默认开启。如果启用，则多点触控（最多三点）将转换为相应的鼠标按钮状态（例如：双指轻击等效于鼠标右键单击）。

### location

- `public static LocationService location`

访问手持设备位置（只读）。

### deviceOrientation

`public static DeviceOrientation deviceOrientation`

操作系统报告的设备的物理方向（只读）。

### compass

- `public static Compass compass`

获取手持设备上的指南针（只读）。

### gyro

- `public static Gyroscope gyro`

获取设备默认陀螺仪的详细信息。在使用此属性之前，先确保设备具有陀螺仪。了解设备的陀螺仪详细信息能够获取需要了解设备方向等功能性数据，通常用于在用户旋转并移动设备时更改摄像头角度或`GameObject`的位置。

### compensateSensors

- `public static bool compensateSensors`

控制输入传感器是否应补偿屏幕方向。补偿传感器有加速度传感器、罗盘、陀螺仪。

### acceleration

- `public static Vector3 acceleration`

设备在三维空间中最近获取到的线性加速度（只读）。

### accelerationEventCount

- `public static int accelerationEventCount`

上一帧获取的加速度数量。

### accelerationEvents

- `public static AccelerationEvent[] accelerationEvents`

上一帧期间获取到的加速度数组列表（只读）。（分配临时变量）

### backButtonLeavesApp

- `public static bool backButtonLeavesApp`

Back键是否能退出应用程序。仅适用于Android、Windows手机或Windows平板电脑。此属性默认值为`false`，通常需要自己写关于按下Back按钮的代码，即通过`Input.GetKey`和`KeyCode.Escape`来写退出程序的代码。如果该属性值设为`true`，在Android平台将推出应用程序，而在Window手机或Windows平板上将挂起应用程序。

### compositionCursorPos

- `public static Vector2 compositionCursorPos`

输入法编辑器（IME）使用的打开当前文本输入窗口的位置。一些语言输入法（如日语）会在用户键入文本时打开窗口，以帮助用户选择正确的输入字符串。这些窗口预计会在当前光标位置弹出，因此IME需要知道输入的显示位置。当使用Unity的内置GUI系统进行文本输入时，Unity将负责设置IME的光标位置。但是，如果您想为文本输入实现自定义的GUI，则需要将其设置为当前的文本输入位置，以使IME窗口正确显示。

### compositionString

- `public static string compositionString`

用户在当前IME中键入的组合字符串。在某些语言（如汉语、日语或韩语）中，通过键入多个键来输入文本，以生成一个或多个字符。当使用Unity的内置GUI系统进行文本输入时，Unity会在用户输入时显示此字符串。对于自定义GUI，则需要注意在当前光标位置显示此字符串。此字符串仅在IME被使用时才更新。

### imeCompositionMode

- `public static IMECompositionMode imeCompositionMode`

启用和禁用IME。有些语言使用复杂的输入方法，需要打开窗口来插入字符。通常，这在游戏中是不可取的，因为游戏可能只是将按键理解为游戏输入，而不是文本。默认情况下，Unity仅在文本输入时启用IME。对于自定义输入GUI，可以使用`imeCompositionMode`属性控制IME。

### imelsSelected

- `public static bool imelsSelected`

用户是否选择了IME键盘输入源。如果用户键盘当前配置为IME输入，则返回true，否则返回false。由于亚洲语言的用户通常可以使用按键打开或关闭IME转换，因此提供了启用IME的视觉指示很有用。

### inputString

- `public static string inputString`

当前帧内的键盘输入（只读）。`inputString`只包含ASCII字符以及两个特殊字符：`\b`退格、`\n`换行或回车。

### stylusTouchSupported

- `public static bool stylusTouchSupported`

指示设备是否支持笔控。

## 静态方法

### GetAxis

- `public static float GetAxis(string axisName)`

获取由`axisName`标识的虚拟轴的值。对于键盘和手柄输入设备，其值范围[-1,1]。值的含义取决于输入控制的类型。如果将轴映射到鼠标，该值会有所不同，并且不会在[-1,1]的范围内。此时，该值为当前鼠标增量乘以轴灵敏度，正值表示鼠标向右/向下移动，负值表示鼠标向左/向上移动。该值与帧率无关；使用该值时，无需担心帧率变化问题。

水平范围和垂直范围从0变为+1或-1，以0.05f的步幅增加/减少。`GetAxisRaw`立即从0变为1或-1，因此没有步幅。

### GetAxisRaw

- `public static float GetAxisRaw(string axisName)`

获取由`axisName`标识的虚拟轴的值。此方法未应用平滑过滤。对于键盘和手柄输入设备，其值范围[-1,1]。由于未对输入进行平滑处理，键盘输入将始终为-1、0或1。应用此方法可以自定义平滑处理。

### GetButton

- `public static bool GetButton(string buttonName)`

当按住`buttonName`标识的虚拟按钮时，返回`true`，松开按钮时返回`false`。`buttonName`参数通常为InputManager中的名称之一，如Jump或Fire1。

### GetButtonDown

- `public static bool GetButtonDown(string buttonName)`

在用户按下由`buttonName`标识的虚拟按钮的第一帧返回`true`。从`Update`中调用此方法，因为状态会在每一帧重置，所以在用户释放按键并再次按下之前，不会返回`true`。

### GetButtonUp

- `public static bool GetButtonUp(string buttonName)`

在用户释放由`buttonName`标识的虚拟按钮的第一帧返回`true`。从`Update`中调用此方法，因为状态会在每一帧重置，所以在用户按下按键并再次释放之前，不会返回`true`。

### GetKey

- `public static bool GetKey(string key)`
- `public static bool GetKey(KeyCode key)`

在用户按下`key`标识的键时返回`true`。此方法将获取指定按键的状态。处理输入时一般使用`GetAxis`或`GetButton`，因此允许用户配置操作按键。

### GetKeyDown

- `public static bool GetKeyDown(string key)`
- `public static bool GetKeyDown(KeyCode key)`

在用户按下`key`标识的键的第一帧返回`true`。从`Update`中调用此方法，因为状态会在每一帧重置，所以在用户释放按键并再次按下之前，不会返回`true`。处理输入时一般使用`GetAxis`或`GetButton`，因此允许用户配置操作按键。

### GetKeyUp

- `public static bool GetKeyUp(string key)`
- `public static bool GetKeyUp(KeyCode key)`

在用户释放`key`标识的键的第一帧返回`true`。从`Update`中调用此方法，因为状态会在每一帧重置，所以在用户按下按键并再次释放之前，不会返回`true`。处理输入时一般使用`GetAxis`或`GetButton`，因此允许用户配置操作按键。

### GetMouseButton

- `public static bool GetMouseButton(int button)`

在用户按下`button`指定的鼠标按钮时返回`true`。0表示左键，1表示右键，2表示中键。

### GetMouseButtonDown

- `public static bool GetMouseButtonDown(int button)`

在用户按下`button`指定的鼠标按钮的第一帧返回`true`。从`Update`中调用此方法，因为状态会在每一帧重置，所以在用户释放按键并再次按下之前，不会返回`true`。0表示左键，1表示右键，2表示中键。

### GetMouseButtonUp

- `public static bool GetMouseButtonUp(int button)`

在用户释放`button`指定的鼠标按钮的第一帧返回`true`。从`Update`中调用此方法，因为状态会在每一帧重置，所以在用户按下按键并再次释放之前，不会返回`true`。0表示左键，1表示右键，2表示中键。

### GetTouch

- `public static Touch GetTouch(int index)`

根据`index`获取设备屏幕上的触屏信息。`touchCount`提供当前屏幕触摸操作的次数，若大于0，可以用此方法获取触屏操作。每次触屏，都会使`touchCount`增大。未分配临时变量。

### ResetInputAxes

- `public static void ResetInputAxes()`

重置所有输入。将所有轴和所有按钮都恢复为0，并且持续一帧时长。当重新生成玩家角色并且不希望接收来自可能仍处于按下状态的键的任何输入时，此方法非常有用。

### GetAccelerationEvent

- `public static AccelerationEvent GetAccelerationEvent(int index)` 

获取上一帧期间的特定加速度。

### GetJoystickNames

- `public static string[] GetJoystickNames()`

获取与在输入管理器中配置的轴的索引对应的输入设备名称的列表。返回的字符串来自操作系统报告的连接设备的“友好名称”。也就是说，名称不是固定的，很可能会因设备、驱动程序和操作系统本身的差异而有所不同。

### IsJoystickPreconfigured

- `public static bool IsJoystaticPreconfigured(string joystickName)`

确定是否已经预先配置某指定手柄（仅限Linux）。预先配置的游戏杆按以下顺序报告按钮和轴的索引。按钮：A、B、X、Y、左缓冲键、右缓冲键、选择键、开始键、导航键、左摇杆键、右摇杆键；轴：左摇杆x、左摇杆y、左扳机键、右摇杆x、右摇杆y、右扳机键、水平方向键、垂直方向键。