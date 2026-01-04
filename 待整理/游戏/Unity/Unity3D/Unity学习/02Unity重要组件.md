## 游戏对象GameObject

- 成员：`obj.name`、`go.layer`、`go.scene`、`go.tag`、`go.transform`、`behavior.enable`、`component.gameObject`、`component.transform`、`component.tag`。
- 创建对象：`Object.Instantiate`、`new GameObject()`、`GameObject.CreatePrimitive`。
- 添加组件：`AddComponent`。
- 删除对象：`Object.Destroy`。
- 过场景不删除对象：`Object.DontDestroyOnLoad`。
- 获取对象：`Object.FindObjectOfType`、`Object.FindObjectsOfType`、`GameObject.Find`、`GameObject.FindWithTag`、`GameObject.FindGameObjectsWithTag`。
- 获取组件：`GetComponent`、`GetComponents`、`GetComponentInChildren`、`GetComponentInParent`、`TryGetComponent`。
- 比较标签：`CompareTag`。
- 设置激活：`SetActive`。

## 时间Time

`Time`类主要用于获取时间信息。

- 时间缩放比例（暂停或倍速）：`timeScale`。
- 帧间隔（位移）：`deltaTime`、`unscaledDeltaTime`。
- 游戏进行时间：`time`、`unscaledTime`。
- 物理帧间隔：`fixedDeltaTime`、`fixedUnscaledDeltaTime`。
- 帧数（帧同步）：`frameCount`。

## 变换Transform

处理游戏对象的位移、旋转、缩放、父子关系、坐标转换等操作。

Inspector窗口中变化组件显示的Position、Rotation为相对于父对象的，即`localPosition`、`localEulerAngle`。如果没有父对象，该值等于其在世界坐标系的位置。

对Transform组中**位置、旋转、缩放的赋值不能直接改变分量，只能整体赋值**，并且相对于世界坐标的缩放不能修改。相应的结构体（如`Vector3`）可以直接改变。通常使用API进行位移、旋转和缩放。

### 常用API

- 位移

  - 改变`position`：`transform.position += dir * speed * Time.deltaTime`。
  - 移动`Translate`：`transform.Translate(dir * speed * Time.deltaTime, Space)`。

- 旋转

  - 手动计算：`transform.EulerAngle += dir * speed * Time.deltaTime`。

  - 旋转`Rotate`：`transform.Rotate`、`transform.RotateAround`。

- 缩放：没有API，只能自己实现。

- 看向：`LookAt`。让物体一直朝着某个方向或目标。

- 父子关系

  - 设置/获取父对象：`transform.parent`、`transform.SetParent`。

  - 断绝父子关系：`transform.DetachChildren`。

  - 获取子对象（可以找到失活的子对象，无法找到孙子对象）：`transform.childCount`；`transform.Find`；`transform.GetChild`。

  - 判断自己的父对象：`transform.IsChildOf`。

  - 得到自己在子对象序列中的编号：`transform.GetSiblingIndex`。

  - 将自己设置为第一个子对象：`transform.SetAsFirstSibling`。

  - 将自己设置为最后一个子对象：`transform.SetAslastSibling`。

  - 设置自己在子对象序列中的编号：`transform.SetSiblingIndex`，超出范围设置成最后一个子对象。

### 坐标转换

- 世界坐标转本地坐标：

  - 点：`trnasform.InverseTransformPoint`，受`transform.scale`影响

  - 方向： `trnasform.InverseTransformDirection`，不受`transform.scale`影响； `trnasform.InverseTransformVector`，受`transform.scale`影响。

- 本地坐标转世界坐标：

  - 点：`trnasform.TransformPoint`，受`transform.scale`影响。

  - 方向： `trnasform.TransformDirection`，不受`transform.scale`影响； `trnasform.TransformVector`，受`transform.scale`影响。

## 输入Input

- 鼠标在屏幕中的位置：`mousePosition` 。屏幕坐标原点在屏幕左下角，类型为`Vector3`但只有x和y的值。
- 鼠标输入：`GetMouseButton`，`GetMouseButtonDown`，`GetMouseButtonUp`。
- 鼠标滚轮滚动值：`mouseScrollDelta`，类型为`Vector2`。
- 键盘输入：`GetKey`，`GetKeyDown`，`GetKeyUp`。
- 虚拟轴：`GwtAxis`，`GetAxisRaw`（仅返回1、0、-1）。
- 是否有任意键或鼠标出发：`anyKey`，`anyKeyDown`。
- 当前帧按下的键：`inputString`。
- 手柄输入
  - 获取手柄所有按钮名：`GetJoystickNames`。
  - 检测手柄输入：`GetButton`，`GetButtonDown`，`GetButtonUp`。
- 移动设备
  - 是否启用多点触控：`multiTouchEnabled`。
  - 触点个数：`touchCount`。
  - 触点信息：`touches`。
  - 是否开启陀螺仪：`gyro.enablee`。
  - 重力加速度：`gyro.gravity`。
  - 陀螺仪旋转速度：`gyro.rotationRote`。
  - 陀螺仪当前旋转的四元数：`gyro.attitude`。

## 屏幕Screen

- 当前设备的分辨率：`currentResolution`。
- 屏幕窗口宽高：`width`，`height`。
- 屏幕休眠模式：`sleepTimeout`。
- 运行时是否全屏：`fullScreen`。
- 窗口模式：`fullScreenMode`。独占全屏，全屏窗口，最大化窗口，窗口化。
- 移动设备屏幕转向
  - 允许自动旋转方向：`autorotateToLandscapeLeft`左横向，`autorotateToLandscapeRight`右横向，`autorotateToPortrait`竖屏，`autorotateToPortraitUpsideDown`竖屏倒放。
  - 指定屏幕显示方向：`orientation`。
- 设置分辨率（移动设备不使用）：`SetScreenResolution`。

## 摄像机Camera

### 核心参数

- 清除标识Clear Flags：决定屏幕的空白部分如何处理。Skybox天空盒、Solid Color纯色、Depth Only仅深度（仅渲染游戏物体，通常用于多个摄像机同时渲染，与深度配合使用）、Don't Clear不清除。
- 背景颜色Background。
- 剔除遮罩Cull Mask：选择性渲染层级，指定渲染范围。
- 透视方式Projection
  - 透视模式Perspective：通常用于3D游戏。
    - 视场角Fov Axis：决定视野范围由竖直还是水平方向来计算。
    - 视口范围Field of View。
    - 物理摄像机Physical Camera：勾选后可以模拟真是世界的摄像机。焦距Focal Length，传感器类型Sensor Type，传感器尺寸Sensor Size，透镜移位Lens Shift，闸门配合Gate Fit。
  - 正交模式Orthographic：通常用于2D游戏。
    - 摄制范围Size。
- 摄像机渲染远近点Clipping Planes。
- 深度Depth：多个摄像机的渲染顺序，越大越后渲染。
- 渲染纹理Target Texture：可以将摄像机画面渲染到一张图上，在Project右键创建Render Texture，可用于制作小地图。
- 是否启用遮挡剔除Occlusion Culling。

### 其他参数

- Viewport Rect视口矩形：标明摄像机视图在屏幕上绘制的屏幕坐标，制作多屏。X、Y水平和垂直开始位置，W、H宽高。
- 渲染路径Rendering Path：Use Graphics Settings使用绘图设置、Forward快速渲染（所以对象按照每种材质一个通道的方式渲染）、Deferred延迟照明（所以对象将无照明绘制一次，然后所有对象的照明将一起在渲染队列的末尾被渲染）、Legacy Vertex Lit顶点光照（对所有对象的渲染会作为顶点光照对象渲染）、Legacy Deferred（light prepass）废弃的延迟光照。
- 高动态范围渲染HDR：提供更多的动态范围和图像细节。
- 抗锯齿MSAA。
- 是否允许动态分辨率呈现Allow Dynamic Resolution。
- 渲染目标反应器Tatget Display。

### 常用API

- 获取摄像机：`main`。
- 摄像机数量：`allCameraCount`。
- 所有摄像机：`allCameras`。
- 渲染委托
  - 在遮挡剔除前处理`onPerCall`。
  - 在渲染前处理`onPreRender`。
  - 在渲染后处理`onPostRender`。
- 获取摄像机对象的参数：`Camera.main.XXX`。
- 世界坐标转屏幕坐标：`Camera.main.WorldToScreenPoint`。
- 屏幕坐标转世界坐标：`Camera.main.ScreenToWorldPoint`。
- 世界坐标转视口坐标：`Camera.main.WorldToViewportPoint`。
- 视口坐标转世界坐标：`Camera.main.ViewportToWorldPoint`。
- 视口坐标转屏幕坐标：`Camera.main.ViewportToScreenPoint`。
- 屏幕坐标转视口坐标：`Camera.main.ScreenToViewportPoint`。