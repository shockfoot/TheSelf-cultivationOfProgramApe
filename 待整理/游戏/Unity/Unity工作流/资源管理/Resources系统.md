# Resources文件夹

Resources文件夹可以存放游戏运行时需要加载的Asset。在运行时可以通过`Resources`API加/卸载这些Asset中的Object。

Resources文件夹必须在Assets目录下，需要手动创建，其下资源**永远被构建到包中存放资源的archive中，只读且不能动态修改**。在Editor目录下的Resources文件夹中的资源可以通过编辑器脚本加载，但不参与构建。

## 序列化

**当构建项目时，所有Resources文件夹中的Asset和Object都会被合并到一个序列化文件。** 该文件还包含元数据（Metadata）和索引（Indexing）信息，类似于AssetBundle。索引中包括一个用于将给定Object名称转换为相应File GUID和Local ID的序列化查找树，同时它也用于在序列化文件中定位偏移了指定字节数的Object。

**在大多数平台上，使用平衡查找树来查找数据，其时间复杂度为O(nlogn)。因此，索引加载时间随Resources文件夹中Object数量而增长的速度高于线性增长。**

**加载Resources文件夹这一操作无法跳过，它会在应用程序启动显示初始非交互式启动动画时进行。** 在低端移动设备上，初始化包含10000个Asset需要花费数秒，然而Resources文件夹中的大多数Object实际上都不会在第一个场景中使用到，完全没有必要加载。

## 使用

**不建议使用Resources文件夹和Resuorces系统**：

- 使用Resources文件夹使得细粒度内存管理变得更加困难。
- 不正确地使用Resources文件夹会增加应用程序的启动和构建的时间。
-  - 随着Resources文件夹数量的增加，管理这些文件夹中的Asset将变得越来越困难。
-  使用Resources系统会降低项目向特定平台提供定制内容的能力，并导致项目无法进行增量内容更新。
-  - AssetBundle变体（Variant）是Unity针对不同设备调整内容的主要工具。

在下面两种特殊情况下，Resources系统会很有用，而且不会影响开发体验：

- Resources系统简单、易用的特点使其非常**适合于快速开发原型**。不过，当项目进入正式开发阶段时，应该停止使用Resources文件夹。
- Resources文件夹可以用于处理一些简单的内容：
- - 在项目的整个生命周期中都被使用的内容。
- - 非内存密集型内容。
- - 不会添加补丁或者不受平台和设备影响的内容。
- - 不太可能添加补丁或者不受平台和设备影响的内容（Minimal Bootstrapping）。

第二种情况包括用于持有预制体的MonoBehaviour单例，或者包含第三方配置数据的ScriptableObject（如Facebook App ID）。

# Resources类

通过`Resources`类可以查找和访问包括Asset在内的Object。**`Resources`类只能读取Asset目录下任意位置的Resources文件夹中的资源。** 若在不同目录下有多个Resources文件夹，在**加载指定资源时每个Resources文件夹都会被检查**。

Resources类提供了如下功能：

- 查找资源：通过`Resources.FindObjectsOfTypeAll`可以定位Asset和场景Object（包括未激活的）。
- 解析Instance ID：通过`Resources.InstanceIDsToValidArray`、`Resources.InstanceIDToObject`和`Resources.InstanceIDToObjectList`可以解析获取Instance ID所映射的Object。
- 加载资源：
- - 通过`Resources.Load`同步加载Resources文件夹下指定相对路径和类型的Object。
- - 通过`Resources.LoadAll`同步加载Resources文件夹下指定相对路径指向的文件夹或单一Object。如果路径为`""`，则加载Resources文件夹中的全部内容。
- - 通过`Resources.LoadAsync`异步加载Resources文件夹下指定相对路径指向的文件夹或单一Object。如果路径为`""`，则加载Resources文件夹中的全部内容。
- 卸载资源：
- - 通过`Resources.UnloadAsset`从内存中卸载指定Object，卸载后会使其失效，后续引用将重新加载新的Object实例。只有存储在硬盘上的Object才能调用。
- - 通过`Resources.UnloadUnusedAssets`卸载未使用的Object，可以在操作完成之前放弃（Yield）。如果在遍历整个游戏对象层次（包括脚本组件）后未找到某个Object，则该Object将被视为未使用。静态变量也会被检查。然而，不会检查脚本执行堆栈，因此仅被脚本执行堆栈引用的Object也会被卸载。除了ScriptableObject以外的所有Object都将在下一次使用其属性或方法时重新加载。这就需要对在内存中被修改过的Object格外小心，因此确保在触发GC之前调用`EditorUtility.SetDirty`。

> **注意**：
> - 路径使用Resources文件夹开始的相对路径，不区分大小写，不包含文件扩展名。
> - Unity中所有路径使用正斜杠`/`，使用反斜杠`\`是无效的。
> - 同类型的Asset不能重名。
> - 如果有多个相同名称的不同类型的Asset，并且加载时没有指定类型，那么Unity返回的对象是不确定的，因为查找的对象不会以任何特定的方式排序。