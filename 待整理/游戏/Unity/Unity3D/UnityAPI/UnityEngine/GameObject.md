- 命名空间：`UnityEngine`
- 程序集：`UnityEngine.CoreModule`
- 继承：`Object`

`GameObject`是Unity场景中所有实体的基类。

## 属性

- 激活状态
  - 受物体影响的：`activeInHierarchy`。
  - 自身的（只读）：`activeSelf`。
- 是否是静态：`isStatic`。获取并设置游戏对象的`StaticEditorFlags`。赋值时，`true`表示启用所有`StaticEditorFlags`，`false`为禁用所有`StaticEditorFlags`。
- 层级：`layer`。可以使用层让摄影机进行选择性渲染或忽略射线。Unity最多支持32个层，按0-31编号，其中0-5是Unity系统自带的层。
- 场景：`scene`。
- 场景剔除遮罩：`sceneCullingMask`。Unity用来决定在哪个场景渲染游戏对象的场景剔除遮罩。
- 标签：`tag`。用来标识游戏对象。必须在使用之前先声明。
- 游戏对象上的变换组件：`Transform`。

## 构造函数

`new GameObject()`创建一个新的游戏对象，可以指定名字和附加的组件。游戏对象总附加`Transform`组件。使用一个/无参构造函数创建的`GameObject`只有`Transform`组件。

## 普通方法

- 添加组件：`AddComponent`。没有`RemoveComponent()`，若要移除组件， 使用`Object.Destoty`。
- 获取组件：`GetComponent`（返回找到的第一个组件，并且顺序不确定），`TryGetComponent`，`GetComponents`，`GetComponentInChildren`（深度优先搜索，默认只查找激活对象），`GetComponentsInChildren`，`GetComponentInParent`（递归向上搜索），`GetComponentsInParent`。
- 比较标签：`CompareTag`。
- 设置激活状态：`SetActive`。停用游戏对象将禁用其上的每个组件，包括渲染器、碰撞器、刚体和脚本。当设置成功时，游戏对象上的脚本会调用`OnEnable`或`OnDisable`。
- 广播消息：`SendMessage`（不会发送给未激活对象），`BroadcastMessage`（向下发送），`SendMessageUpwards`（向上发送）。可传递参数。如果设置了`SendMessageOptions.RequireReceiver`，但没有任何一个组件接收消息时会报错。

## 静态方法

- 创建游戏对象：`CreatePrimitive`。根据指定的`PrimitiveType`创建带有合适的网格渲染器和碰撞器的游戏对象。
- 查找：`Find`，`FindWithTag`，`FindGameObjecsWithTag`。查找并返回**已激活的**游戏对象。如果场景包含多个满足条件的对象，则无法保证此方法将返回特定的游戏对象。