# GameObject

所属命名空间`UnityEngine`，继承自`Object`，实现于`UnityEngine.CoreModule`。

`GameObject`是Unity场景中所有实体的基类。

## 属性

### activeInHierarchy

- `public bool activeInHierarchy`

`GameObject`在场景中是否激活。此属性会受到父物体激活状态的影响。

### activeSelf

- `public bool activeSelf`

`GameObject`自身的激活状态（只读）。此属性不受父物体激活状态的影响，即有可能返回`true`但`GameObject`在场景中是未激活的。

### isStatic

- `public bool isStatic`

获取并设置`GameObject`的`StaticEditorFlags`。如果`GameObject`设置了任何的`StaticEditorFlags`则返回`true`，否则返回`false`。如果给该属性赋`true`则启用所有的`StaticEditorFlags`，赋`false`则禁用所有的`StaticEditorFlags`。

### layer

- `public int layer`

`GameObject`所在层。可以使用层让摄影机进行选择性渲染或忽略射线。Unity最多支持32个层，按0-31编号，其中0-5是Unity系统自带的层。

### scene

- `public SceneManagement.Scene scene`

游戏对象所在场景。

### sceneCullingMask

- `public ulong sceneCullingMask`

Unity用来决定在哪个场景渲染`GameObject`的场景剔除掩码。

### tag

- `public string tag`

`GameObject`的标签。标签可以用来标识游戏对象。标签必须在使用之前先声明。不应该在`Awake`和`OnValidate`方法中设置标签，因为组件唤醒的顺序是不确定的，从而导致意外行为，比如在唤醒时标签被覆盖。

### transform

- `public Transform transform`

`GameObject`上的`Transform`组件。

## 构造函数

- `public GameObject()`
- `public GameObject(string name)`
- `public GameObject(string name, params Type[] components)`

创建一个新的`GameObject`，可以指定名字和附加的组件。游戏对象总会附加`Transform`组件。使用一个/无参构造函数创建的`GameObject`只有`Transform`组件。

## 公共方法

### AddComponent

- ~~`public Component AddComponent(string className)`~~
- `public Component AddComponent(Type componentType)`
- `public T AddComponent()`

为`GameObject`添加组件。没有`RemoveComponent()`，若要移除组件， 使用`Object.Destoty`。

### SendMessage

- `public void SendMessage(string methodName, object value = null, SendMessageOptions options = SendMessageOptions.RequireReceiver)`

在`GameObject`上的每个`MonoBehaviour`调用名为`methodName`的方法，其中`value`为传递的参数。接收方法可以使用无参方法以选择性忽略所传参数。如果设置了`SendMessageOptions.RequireReceiver`却无任何组件接收到消息时会报错。消息不会发送给未激活对象（不管是在编辑器中禁用还是用`SetActive`禁用的）。

### SendMessageUpwards

- `public void SendMessageUpwards(string methodName, object value = null, SendMessageOptions options = SendMessageOptions.RequireReceiver)`

在`GameObject`及其祖先上的每个`MonoBehaviour`调用名为`methodName`的方法，其中`value`为传递的参数。接收方法可以使用无参方法以选择性忽略所传参数。如果设置了`SendMessageOptions.RequireReceiver`却无任何组件接收到消息时会报错。消息不会发送给未激活对象（不管是在编辑器中禁用还是用`SetActive`禁用的）。

### BroadcastMessage

- `public void BroadcastMessage(string methodName, Object parameter = null, SendMessageOptions options = SendMessageOptions.RequireReceiver)`

在`GameObject`及其子对象的每个`MonoBehaviour`调用名为`methodName`的方法，其中`parameter`为传递的参数。接收方法可以使用无参方法以选择性忽略所传参数。如果设置了`SendMessageOptions.RequireReceiver`，但没有任何一个组件接收消息时会报错。

### CompareTag

- `public bool CompareTag(string tag)`

判断`GameObject`的标签是否是`tag`。

### GetComponent

- `public Component GetComponent(Type type)`

- `public T GetComponent()`

- `public Component GetComponent(string type)`

返回`GameObject`上附加的`type`类型的组件，如果没有则返回`null`。将返回找到的第一个组件，并且顺序不确定。要提高代码的性能，不要使用`string`参数的方法。如果希望返回多个相同类型的组件，使用`GetComponents`，并可以循环遍历。

### TryGetComponent

- `public bool TryGetComponent(Type type, out Component component)`
- `public bool TryGetComponent(out T component)`

获取`GameObject`上指定类型的组件（如果存在），获取成功时返回`true`，否则返回`false`。与`GetComponent`不同，此方法将尝试检索给定类型的组件，当请求的组件不存在时，此方法不会在编辑器中分配内存。

### GetComponentInChildren

- `public Component GetComponentInChildren(Type type)`
- `public Component GetComponentInChildren(Type type, bool includeInactive)`
- `public T GetComponentInChildren(bool includeInactive = false)`

使用深度优先搜索返回`GameObject`或其任何子对象中类型为`type`的组件。除非另有指定，否则只返回已激活`GameObject`上的组件。

### GetComponentInParent

- `public Component GetComponentInParent(Type type)`
- `public Component GetComponentInParent(Type type, bool includeInactive)`
- `public T GetComponentInParent(bool inlcudeInactive = false)`

返回`GameObject`或其祖先中类型为`type`的组件。该方法会向父级对象递归，直到找到具有`type`组件的`GameObject`。除非另有指定，否则只匹配已激活`GameObject`上的组件。

### GetComponents

- `public Component[] GetComponents(Type type)`
- `public T[] GetComponents()`

- `public void GetComponents(Type type, List<Component> results)`
- `public void GetComponents(List<T> results)`

返回`GameObject`上所有`type`类型的组件；或者将这些组件返回到列表`results`中，其中`results`的元素类型是`Component`或`T`。如果要返回`MonoBehavior`的派生类型并且无法加载相关脚本时返回`null`。

### GetComponentsInChildren

- `public Component[] GetComponentsInChildren(Type type, bool includeInactive = false)`
- `public T[] GetComponentsInChildren()`
- `public T[] GetComponentsInChildren(bool includeInactive)`
- `public void GetComponentsInChildren(List<T> results)`
- `public void GetComponentsInChildren(bool includeInactive, List<T> results)`

使用深度优先搜索返回`GameObject`及其子对象上所有`type`类型的组件；或者将这些组件返回到列表`results`中。如果要返回`MonoBehavior`的派生类型并且无法加载相关脚本时返回`null`。

### GetComponentsInParent

- `public Component[] GetComponentsInParent(Type type, bool includeInactive = false)`
- `public T[] GetComponentsInParent()`
- `public T[] GetComponentsInParent(bool includeInactive)`
- `public void GetComponentsInParent(bool includeInactive, List<T> results)`

返回`GameObject`及其祖先上所有`type`类型的组件；或者将这些组件返回到列表`results`中。对组件的搜索是在父对象上递归执行的，因此包括父对象的父对象等。如果要返回`MonoBehavior`的派生类型并且无法加载相关脚本时返回`null`。

### SetActive

- `public void SetActive(bool value)`

根据`value`激活/停用`GameObject`。

`GameObject`可能因为父对象未激活而处于未激活状态。在这种情况下，调用`SetActive`也不会激活它，而只会设置`GameObject`的自身状态，使用`GameObject.activeSelf`可以获取其自身激活状态。当`GameObject`的父级对象都激活时，才可使用此方法激活之。停用`GameObject`将禁用其上的每个组件，包括渲染器、碰撞器、刚体和脚本。当`GameObject`被`SetActive(true)`或`SetActive(false)`时，其上的脚本会调用`OnEnable`或`OnDisable`方法。

## 静态方法

### CreatePrimitive

- `public static GameObject CreatePrimitive(PrimitiveType type)`

根据指定的`type`创建带有合适的网格渲染器和碰撞器的`GameObject`。此方法可能在运行时失败。如果项目未引用以下组件：MeshFilter，Meshrenderer、Boxcollider或SphereCollider时，运行中会发生这种情况。避免这种崩溃的推荐方法是声明这些类型的私有属性。剥离系统将认为它们会在导出游戏时被使用，因此不会删除这些组件。

### Find

- `public static GameObject Find(string name)`

根据`name`查找并返回`GameObject`。此方法仅返回激活的`GameObject`。如果找不到以`name`为名的`GameObject`，则返回`null`。如果`name`中包含一个`/`字符，则会像路径名一样按层次结构查找。出于性能考虑，不建议在每帧中使用此方法。相反，通常在`Awake()`和`Start()`中将结果缓存在成员变量或使用`GameObject.FindWithTag`代替。如果要找子游戏对象，使用`Transform.Find`将会更适合。如果游戏正在运行多个场景，则可以在所有场景中搜索。

`GameObject.Find`对于在运行时自动连接到其他对象很有用。

### FindWithTag

- `public static GameObject FindWithTag(string tag)`

返回激活的标签为`tag`的`GameObject`，如果未发现则返回`null`。在使用标签之前，必须在标签管理器中声明。当标签不存在以及传递空字符串或`null`时会报错。此方法返回第一个符合条件的`GameObject`。如果场景包含具有指定标签的多个已激活`GameObject`，则无法保证此方法将返回特定的`GameObject`。

### FindGameObjectsWithTag

- `public static GameObject[] FindGameObjectsWithTag(string tag)`

返回激活的标签为`tag`的`GameObject`数组，如果没有符合的`GameObject`则返回空数组。在使用标签之前，必须在标签管理器中声明。当标签不存在以及传递空字符串或`null`时会报错。