# Object

所属命名空间`UnityEngine`，实现施于`UnityEngine.CoreModule`。

`Object`是所有Unity对象的基类。从`Object`派生的任何公共变量都将在Inspector面板中显示。虽然`Object`是一个类，但其本意不是为了在脚本中广泛使用。

不支持`null`条件运算符`?.`和`null`合并运算符`??`。

## 属性

### hideFlags

- `public HideFlags hideFlags`

`Object`应该隐藏、随场景一起保存还是由用户修改？

### name

- `public string name`

`Object`的名称。组件与游戏对象及所有附加组件使用相同的名称。如果一个类派生自`MonoBehaviour`，它将从`MonoBehaviour`继承`name`字段。如果也将该类附加到`GameObject`，则`name`字段将设置为该`GameObject`的名称。

## 公共方法

### GetInstanceID

- `public int GetInstanceID()`

获取`Object`的实例ID。当传入一个原点对象（origin object）时返回正值，当传入一个实例对象（instance object）时则返回负值。每个`Object`的实例ID都是唯一的。实例ID会在游戏运行时更改，因此在编辑器与游戏运行之间通过ID执行相关操作是不可靠的。

### ToString

- `public string ToString()`

返回`Object`的名称。

## 静态方法

### Destory

- `public static void Destory(Object obj, float t = 0.0F)`

在当前`Updata`结束后立即销毁`obj`，如果指定了时间`t`，则在`t`秒后销毁。如果`obj`是`Component`，则从`GameObject`上移除并销毁之；当销毁一个`MonoBehaviour`脚本时，在脚本移除之前会调用`OnDisable`和`OnDestory`。如果`obj`是`GameObject`，则销毁其、其上所有`Component`及其`Transform`下的子对象。实际上，`obj`的销毁始终延迟到当前`Updata`结束，但总是在渲染之前执行。

### DestoryImmedieate

- `public static void DestoryImmedieate(Object obj, bool allowDestoryingAssets = false)`

立即销毁`obj`并选择是否销毁Asset资源。因为编辑器模式下无法调用延迟销毁方法，所有此方法仅应在编写编辑器代码时使用。在游戏代码中，应使用`Object.Destoty`。绝对不要在遍历数组时销毁数组的元素。

### DontDestoryOnLoad

- `public static void DontDestoryOnLoad(Object target)`

加载新场景时不销毁`target`。加载新场景时会销毁当前场景中的所有`Object`。通过调用此方法可以在场景加载过程中保留目标`Object`。如果`target`是`Component`或`GameObject`，则其`Transform`下的子对象也会保留。此方法仅适用于根游戏对象或跟游戏对象上的组件。

### FindObjectOfType

- `public static T FindObjectOfType()`

- `public static T FindObjectOfType(bool includeInactive)`

- `public static Object FindObjectOfType(Type type)`

- `public static Object FindObjectOfType(Type type, bool includeInactive)`

返回第一个已激活并加载的`type`或`T`类型对象，若未找到则返回`null`。此方法不会返回Asset资源（网格mesh、贴图texture、预制体perfab等）和未激活对象（除非指定了查找范围包含未激活对象），不会返回设置为`HidFlags.DontSave`的对象。此方法**非常消耗资源**，通常使用单例而不是在每帧都调用。

### FindObjectsOfType

- `public static T[] FindObjectsOfType()`
- `public static T[] FindObjectsOfType(bool includeInactive)`
- `public static Object[] FindObjectsOfType(Type type)`
- `public static Object[] FindObjectsOfType(Type type, bool includeInactive)`

返回已激活并加载的`type`或`T`类型对象的列表，若未找到则返回`null`。此方法不会返回Asset资源（网格mesh、贴图texture、预制体perfab等）；不会返回设置为`HidFlags.DontSave`的对象；只有当`iscludeInactive`为`true`时才返回未激活`GameObject`上的`object`，`Rrsources.FindObjectsOfTyleAll`不受此限制。默认情况下，此方法仅在Sence中搜索。此方法**非常消耗资源**，通常使用单例而不是在每帧都调用。

### Instantiate

- `public static Object Instantiate(Object original)`
- `public static Object Instantiate(Object original, Transform parent)`
- `public static Object Instantiate(Object original, Transform parent, bool instantiateInWorldSpace)`
- `public static Object Instantiate(Object original, Vector3 position, Quaternion rotation)`
- `public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent)`

克隆源对象`original`并返回克隆的对象。此函数以类似于编辑器中的复制命令的方式在运行时复制对象。如果要克隆`GameObject`，可以指定其位置和旋转（否则默认为源对象`original`的位置和旋转）。如果要克隆一个`Component`，那么它所连接的`GameObject`也会被克隆，同样可以指定位置和旋转。

不管是克隆`GameObject`还是`Component`，其所有子对象和组件也会被克隆，且属性与源对象的相同。为了防止堆栈溢出，Unity限制了这种嵌套克隆。如果克隆的对象大小超过堆栈的一半，将报错`ExecutionStackException`。

默认新对象的父对象为空，而不是`original`的父对象，但仍可使用重载的方法设置父对象。如果指定了父对象且未指定位置和旋转，则会将源对象`original`的位置和旋转赋给新对象的**局部**位置和旋转；如果指定`instantiateInWorldSpace`为`true`，则赋给**世界**位置和旋转。指定的位置和旋转会被赋给新对象的**世界**位置和旋转。

克隆的`GameObject`的激活状态保持不变，因此若源对象`original`处于未激活状态，新对象也将是未激活的。对于Hierarchy中的所有对象和子对象，只有其处于激活状态下调用此方法，其`MonoBehaviour`和`Component`才能才能调用`Awake`和`OnEnable`方法。

此方法创建的新实例对象不会链接到预制体。要创建链接由预制体的对象可使用`PrefactUtility.InstantiatePrefab`实现。

此方法可以直接克隆脚本实例。

- `public static T Instantiate(T original)`
- `public static T Instantiate(T original, Transform parent)`
- `public static T Instantiate(T original, Transform parent, bool instantiateInWorldSpace)`
- `public static T Instantiate(T original, Vector3 position, Quaternion rotation)`
- `public static T Instantiate(T original, Vector3 position, Quaternion rotation, Transform parent)`

可以使用泛型来实例化对象。通过使用泛型，不需要将结果强制转换为特定类型。

## 运算符

### 布尔bool

一个`Object`可以直接作`bool`值表达式或与`true`、`flase`以及`null`比较，用以表示该对象是否存在。

### 不等!=

- `public static bool operator !=(Object x, Object y)`

用于判断`x`和`y`是否引用不同的实例或者与`null`比较。

### 相等==

- `public static bool operator ==(Object x, Object y)`

用于比较`x`和`y`是否引用相同实例或者与`null`比较。需要注意的时，实例化`GameObject`时会将其添加到当前场景中，所以该`GameObject`已经完全实例化，不为`null`；而实例化`Object`时没有这样的语义，因此该`Object`处于销毁状态，即为`null`。

``` C#
GameObject go = new GameObject();
Debug.Log(go == null); // Output: false

Object obj = new Object();
Debug.Log(obj == null); // Output: true
```