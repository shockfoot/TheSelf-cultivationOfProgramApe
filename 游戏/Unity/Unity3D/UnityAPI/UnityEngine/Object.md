- 命名空间：`UnityEngine`
- 程序集：`UnityEngine.CoreModule`

`Object`是所有Unity对象的基类。从`Object`派生的任何公共变量都将在Inspector面板中显示。虽然`Object`是一个类，但其本意不是为了在脚本中广泛使用。

## 属性

- 隐藏标识：`HideFlags`。
- 名字：`name`。组件与游戏对象及所有附加组件使用相同的名称。如果一个类派生自`MonoBehaviour`，它将从`MonoBehaviour`继承`name`字段。如果也将该类附加到`GameObject`，则`name`字段将设置为该`GameObject`的名称。

## 普通方法

- 获取实例ID：`GetInstanceID`。每个`Object`的实例ID都是唯一的，且会在游戏运行时更改，因此针对实例ID的操作是不稳定的。
- 转化为字符串：`ToString`。返回`Object`的名称。

## 静态方法

- 销毁相关
  - 在一定时间后（默认立即）销毁：`Destory`。此方法延迟到当前`Updata`结束，但在下一帧渲染之前执行。
  - 立即销毁并选择是否销毁其使用的Assets资源：`DestoryImmedieate`。此方法*仅应在编写编辑器代码时使用*。
  - 加载新场景时不销毁`Object`。：`DontDestroyOnLoad`。此方法*仅适用于根游戏对象或根游戏对象上的组件*。
- 查找
  - 根据类型查找：`FindObjectOfType`，`FindObjectsOfType`。返回已激活并加载的特定类型的对象，否则返回`null`。此方法不会返回Asset资源（网格mesh、贴图texture、预制体perfab等）和未激活对象（除非显示指定了查找范围），不会返回设置为`HidFlags.DontSave`的对象。默认情况下，此方法仅在Sence中搜索。此方法**非常消耗资源**，通常使用单例而不是在每帧都调用。
- 创建：`Instantiate`。克隆源对象并返回克隆的对象。如果克隆`GameObject`，可以指定其位置和旋转（默认为源对象的位置和旋转）。如果克隆`Component`，其所连接的`GameObject`也会被克隆。克隆对象的所有子对象和组件也会被克隆，且属性与源对象的相同。为了防止堆栈溢出，Unity限制了这种嵌套克隆。如果克隆的对象大小超过堆栈的一半，将报错`ExecutionStackException`。默认新对象的父对象为空，而不是源对象的父对象，可显示设置。如果指定了父对象而未指定位置和旋转，则将源对象的赋给新对象的**局部**位置和旋转；如果指定按**世界**坐标克隆，则赋给世界位置和旋转。如果指定了位置和旋转，则会被赋给新对象的**世界**位置和旋转。克隆的`GameObject`的激活状态保持不变。未激活的`MonoBehaviour`和`Component`不会调用`Awake`和`OnEnable`。此方法创建的新实例对象不会链接到预制体。要创建链接由预制体的对象可使用`PrefactUtility.InstantiatePrefab`实现。此方法可以直接克隆脚本实例。

## 运算符

- 是否存在：可以直接作`bool`值表达式或与`true`、`flase`以及`null`比较。
- 等于和不等：用于判断两对象是否引用不同的实例或者与`null`比较。需要注意，实例化`GameObject`时会将其添加到当前场景中，所以该`GameObject`已经完全实例化，不为`null`；而实例化`Object`时没有这样的语义，因此该`Object`处于销毁状态，即为`null`。