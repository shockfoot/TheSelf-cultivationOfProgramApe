# MonoBehaviour

所属命名空间`UnityEngine`，继承自`Behaviour`，实现于`UnityEngine.CoreModule`。

`MonoBehaviour`是所有Unity脚本的基类。C#脚本必须显示继承该类。`MonoBehaviour`提供了脚本框架；可以启动、停止和管理协程；提供对大量事件消息的访问，可以根据项目中当前发生的情况执行代码。

## 属性

### runInEditMode

- `public bool runInEditMode`

允许特定实例在编辑模式下运行（仅可在Editor中使用）。默认为`false`，即脚本仅在运行模式下执行。

### useGUILayout

- `public bool useGUILayout`

默认为`true`，设置`false`可跳过GUI布局阶段。仅当不在`OnGUI`调用中使用 `GUI.Window`和GUI布局时才能使用该属性。

## 公共函数

### Invoke

- `public void Invoke(string methodName, float time)`

在`time`秒后调用`methodName`方法。如果时间设置为0，并且在第一帧更新之前调用`Invoke`，则在`Update`之前的下一个更新周期调用该方法。在这种情况下，最好直接调用函数。将时间设为负值与0相同。在其他情况下，方法的执行顺序取决于调用的时间。如果需要将参数传递给方法，请考虑改用协程。协程还提供了更好的性能。

### InvokeRepeating

- `public void InvokeRepeating(string methodName, float time, float repeatRate)`

在`time`秒后调用`methodName`方法，然后每`repeatRate`秒调用一次。如果将`time`设为0，该函数将不起作用。

### IsInvoking

- `public bool IsInvoking()`

- `pbulic bool IsInvoking(string methodName)`

是否有任何待处理的（名为`methodName`的）`Invoke`调用。

### CanselInvoke

- `public void CancelInvoke()`

- `public void CancelInvoke(string methodName)`

取消所有（名为`methodName`）的`Invoke`调用。

### StartCoroutine

- `public Coroutine StartCoroutine(IEnumerator routine)`

启动协程。

可以使用`yield`语句，随时暂停协程的执行。使用`yield`语句时，协程会暂停执行，并在下一帧自动恢复。此方法在第一个`yield`返回时返回，不过可以生成结果，这会等到协程完成执行。即使多个协程在同一帧中完成，也不能保证它们按照与启动相同的顺序结束。销毁脚本时，或是如果脚本所附加到的`GameObject`已禁用，也会停止协程。禁用脚本时，不会停止协程。

- `public Coroutine StartCoroutine(string methodName, object value = null)`

启动一个名为`methodName`的协程。使用字符串方法名称的`StartCoroutine`能够使用具有特定方法名称的`StopCoroutine`，缺点是字符串版本在启动协程时的运行开销更高，并且只能传递一个参数。已创建的协程可以启动另一个协程。这两个协程能按多种方式共同运行：并行运行两个协程、一个协程可让另一协程停止而自己继续运行。

### StopCoroutine

- `public void StopCoroutine(string methodName)`

- `public void StopCoroutine(IEnumerator routine)`

- `public void StopCoroutine(Coroutine routine)`

停止该脚本上第一个名为`methodName`或存储在`routine`中的协程。此方法使用三个参数，用于停止指定协程；参数应根据`StartCoroutine`中所用参数而选择。

### StopAllCoroutines

- `public void StopAllCoroutines()`

停止该脚本上运行的所有协程。

## 静态函数

### print

- `public static void print(object message)`

将`message`输出至控制台（与`Debug.Log`相同）。

## 消息

### Awake

在加载脚本时调用`Awake()`。当在加载场景过程中初始化激活的`GameObject`，或者未激活的`GameObject`设置激活，或者使用`Object.Instantiate`实例化`GameObject`后，都会调用该`GameObject`上的脚本的`Awake()`。即使已激活`GameObject`上的脚本被禁用，也将被调用该脚本的`Awake()`。

Unity在脚本实例的生存期内只调用`Awake()`一次。脚本的生命周期持续到卸载包含其的场景为止。如果再次加载场景，Unity将再次加载脚本实例，因此将再次调用`Awake()`。如果场景被多次加载，Unity将加载多个脚本实例，因此将多次调用`Awake()`（每个实例一次）。

对于场景中的激活的`GameObject`，Unity在初始化场景中的所有已激活`GameObject`后才调用`Awake()`，因此可以在`Awake()`中安全地使用`GameObject.FindWithTag`等方法以查询其他`GameObject`。

Unity调用每个`GameObject`的`Awake()`的顺序不是确定的。因此，无法确定一个`GameObject`的`Awake()`在另一个之前或之后被调用。所以，通常在`Awake()`中初始化引用，并在`Start()`中来回传递任何信息。

`Awake()`总是在任何`Start()`之前调用。可借此定义脚本的“初始化顺序”。

`Awake()`不能作为协程。

使用`Awake()`而不是构造函数进行初始化，因为组件的串行状态在构造时是不确定的。

### Start

在调用完所有`Awake()`之后、第一次调用任何`Update()`之前调用`Start()`。

`Start()`在脚本实例的生存期内只调用一次。与`Awake()`不同，如果在初始化时未启用脚本，则可能不会在与`Awake()`相同的帧上调用`Start()`。