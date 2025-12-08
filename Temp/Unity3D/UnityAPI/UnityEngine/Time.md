- 命名空间：`UnityEngine`
- 程序集：`UnityEngine.CoreModule`

`Time`主要用于获取时间信息。

## 静态属性

### captureDeltaTime

- `public static float captureDeltaTime`

减慢应用程序的播放时间，以允许Unity在帧之间保存屏幕截图。如果此属性具有非零值，则`Time.time`将以`captureDeltaTime`（按`Time.timeScale`缩放）的间隔增加，而与实时和帧的持续时间无关。此值不会影响`Time.unsacledTime`。

### captureFramerate

- `public static int captureFramerate`

`Time.captureDeltaTime`的倒数四舍五入后的整数值。设置此值会影响`Time.captureDeltaTime`。此值为0时，`Time.captureDeltaTime`也为0。

### time

- `public static float time`

此帧开始时的时间，只读。这是应用程序启动后的时间，受Time.timeScale缩放和`Time.maxiumDeltaTime`调整影响。当从`FixedUpdate`内部调用时返回`Time.fixedTime`。该值在`Awake`期间未定义，并在所有消息完成后开始计算。如果编辑器暂停，则该值不会更新。

### timeAsDouble

- `public static double timeAsDouble`

### unscaledTime

- `public static float unscaledTime`

此帧自游戏开始以来的与时间缩放无关的时间，只读。

### unscaledTimeAsDouble

- `public static double unscaledTimeAsDouble`

此帧自游戏开始以来的与时间缩放无关的时间，只读。

### fixedTime

- `public static float fixedTime`

自上次`FixedUpdate`启动以后的时间，只读。这是游戏运行后的总时间。此值以等于`Time.fixedDeltaTime`的累计和。

### fixedTimeAsDouble

- `public static double fixedTimeAsDouble`

### fixedUnscaledTime

- `public static float ixedUnscaledTime`

上一个`FixedUpdate`阶段到当前阶段的与`Time.timeScale`无关的游戏运行后的总时间（秒），只读。此值以等于`Time.fixedUnscaledDeltaTime`的累计和。

### fixedUnscaledTimeAsDouble

- `public static double fixedTimeAsDouble`

### deltaTime

- `public static float deltaTime`

上一帧到当前帧的间隔（以秒为单位），只读，通常用于**位移、旋转**等操作。当从`FixedUpdate`内部调用时，它将返回`Time.fixedDeltaTime`。`OnGUI`中的`deltaTime`不可靠，因为Unity可能会在每帧中多次调用它。

### unscaledDeltaTime

- `public static float unscaledDeltaTime`

从上一帧到当前帧的与时间缩放无关的间隔（秒），只读。

### fixedDeltaTime

- `public static float fixedDeltaTime`

执行物理和其他固定帧速率更新的时间间隔（秒）。Unity不会根据`Time.timeScale`调整`fixedDeltaTime`。`fixedDeltaTime`间隔始终与`Time.timeScale`影响的游戏内时间相关。

### fixedUnscaledDeltaTime

- `public static float ixedUnscaledDeltaTime`

上一个`FixedUpdate`阶段到当前阶段的与`Time.timeScale`无关的间隔（秒），只读。

### frameCount

- `public static int frameCount`

自游戏运行以来渲染的总帧数， 只读。在Unity内部使用一个64位整数，当调用此值时，会向下转换为32位，并丢弃最高有效的32位。

### inFixedTimeStep

- `public static bool inFixedTimeStep`

在固定时间步进回调（如`FixedUpdate`）内调用，则返回`true`，否则返回`false`。

### maximumDeltaTime

- `public static float maximumDeltaTime`

任何给定帧中`Time.deltaTime`的最大值。这是一个以秒为单位的时间，限制了两帧之间`Time.time`的增加。当出现非常慢的帧时，`maximumDeltaTime`会限制下一帧中`Time.deltaTime`的值，以避免其过大而产生不良副作用。Unity强制`maximumDeltaTime`至少与`Time.fixedDeltaTime`一样大。

### maximumDeltaTime

- `public static float maximumParticleDeltaTime`

帧可以用于粒子更新的最长时间。如果帧花费的时间超过此时间，则更新将拆分为多个较小的更新。使用此值可以根据性能目标平衡粒子模拟的精度。使用较小的值可以提供更高质量的粒子模拟，但需要更多的处理时间。如果帧时间超过提供的阈值，粒子更新将以较小的时间增量运行多次。相反，更高的值可确保粒子模拟不会分解为每帧多个步骤，从而提供最佳性能，但在使用某些更高级的粒子模拟功能时会失去模拟精度。

### realtimeSinceStartup

- `public static float realtimeSinceStartup`

游戏开始后的实时时间（秒），只读。这是应用程序启动后的时间。如果在一个帧中多次调用，则该时间不是恒定的。`Time.timeScale`不影响此属性。在几乎所有情况下，都应该使用`Time.time`或`Time.unscaledTime`。

### realtimeSinceStartupAsDouble

- `public static float realtimeSinceStartupAsDouble`

### smoothDeltaTime

- `public static float smoothDeltaTime`

平滑的`Time.deltaTime`，只读。当该值恒定时（即平滑帧速率），该值等于`Time.deltaTime`。当`Time.deltaTime`在帧之间变化时（例如，在帧挂接上），该值在多个帧上向`Time.deltaTime`接近。

### timeScale

- `public static float timeScale`

时间流逝的尺度，用于慢动作效果或加速应用程序。当`timeScale`为1时，时间与实时一样快。当`timeScale`为0.5时，经过的时间比实时慢2倍。当`timeScale`为0时，如果所有功能都与帧速率无关，则应用程序将像暂停一样运行。负值将被忽略。更改时间比例仅对之后的帧生效。每帧执行`FixedUpdate`的频率取决于`timeScale`。因此，要保持每帧`FixedUpdate`回调的数量不变，还必须将`Time.fixedDeltaTime`乘以`timeScale`。这种调整是否可取取决于游戏。

### timeSinceLevelLoad

- `public static float timeSinceLevelLoad`

此帧启动后的时间，只读。这是最后一个非添加场景完成加载后的时间，以秒为单位。

### timeSinceLevelLoadAsDouble

- `public static double timeSinceLevelLoadAsDouble`
