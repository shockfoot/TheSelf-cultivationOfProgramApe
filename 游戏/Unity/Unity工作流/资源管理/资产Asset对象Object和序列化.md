# Asset和Object

为了理解如何在Unity中正确管理数据，理解Unity如何识别和序列化数据很重要。

**Asset（资产）是Unity项目中Assets文件夹下的硬盘文件**，如纹理Texture、模型、音频等。有些Asset包含Unity原生格式的数据，如材质Material；有些Asset则需要被转换成Unity原生格式，如FBX文件。

**Object（对象）是一组用于描述某个具体资源（Resource）的特定实例的序列化数据集合**，可以是Unity支持的任何原生资源，如网格Mesh、精灵Sprite、音频片段Audio Clip或动画片段Animation Clip等。所有的Object都是`UnityEngine.Object`的子类。

> **注意**：此处的Asset和Object不同于Unity API中的命名约定。在Unity API中，Object通常被称作Asset，如`AssetBundle.LoadAsset`和`Resources.UnloadUnusedAssets`。此处所称Asset通常只暴露于构建相关的Unity API中，如`AssetDatabase`和`BuildPipeline`，在这种情况下，Asset被称为File（文件）。

大多数Object类型都是Unity内置（Built-in）的，除了：

- ScriptableObject方便开发人员自定义数据类型。这些类型可以被Unity原生序列化和反序列化，且可以在Unity编辑器的Inspector窗口中进行操作。
- MonoBehaviour提供了对MonoScript的封装。MonoScript是Unity内部数据类型。Unity通过MonoScript持有特定程序集和命名空间下特定脚本类的引用。MonoScript中不包含任何实际的可执行代码。

**Asset和Object是一对多的关系，即任何给定的Asset都包含一个或多个Object。**

# Object之间的引用

所有的Object都可以持有其他Object的引用，被引用的Object可能在同一个Asset文件中，也可能在导入的其他Asset文件中。例如，一个材质Object通常持有一个或多个纹理Object的引用，而这些纹理Object通常是从一个或多个Asset文件（如PNG或JPG）中导入的。

在序列化时，这些引用由两个独立的数据片段构成：**File GUID（guid）和Local ID（fileID）。File GUID标识存储目标资源的Asset文件，Local ID标识Asset文件中的每个Object。** 同一Asset文件中，Local ID是唯一的。

File GUID存储在.meta文件中。在Unity首次导入Asset文件时会在同级目录下生成相应的.meta。

上述标识和引用可以通过文本编辑器查看。

# 为什么使用File GUID和Local ID

使用File GUID和Local ID来标识引用能够*保证系统的健壮性，提供灵活的、独立于平台的工作流*。

**File GUID提供了对文件位置的抽象。** 只要某个File GUID可以关联到特定文件，那么该文件在磁盘的位置就变得无关紧要，它可以被自由移动而不必更新引用该文件的所有Object。

因为任何给定Asset文件都可能包含（或通过导入产生）多个Object，因此需要Local ID来明确区分不同的Object。

如果与某个Asset文件关联的File GUID丢失，那么对该Asset文件中所有Object的引用也将丢失，因此.meta文件必须与相关联的Asset文件同名，且位于同一文件夹。Unity会重新生成删除或位置错误的.meta文件。

Unity编辑器存储了一个文件路径和已知File GUID的映射表（Map）。当加载或导入Asset时，Unity编辑器会记录其映射关系（Map Entry），用以链接该Asset的具体路径和File GUID。如果在Unity编辑器打开时丢失.meta文件且没有移动Asset文件路径，那么编辑器可以确保该Asset文件持有相同的File GUID。如果在Unity编辑器关闭时丢失了.meta文件或者在移动Asset文件时没有移动.meta文件，则该Asset文件中所有的Objet引用都会被破坏。

# 复合Asset和导入器

非Unity原生格式的Asset需要导入、转换成Unity原生格式才能被使用。这一操作由导入器（Asset Importer）完成。导入器通常被自动调用，但可以通过`AssetImporter`相关API在脚本中访问导入器。例如，`TextureImporter`提供了对单个纹理Asset（如PNG文件）的导入设置的访问。

导入结束后会创建一个或多个Object，它们通常以“一个父Asset附加多个子Asset”的形式显示在Unity编辑器中。例如，以精灵图集Sprite Atlas形式导入的纹理Asset中包含多个嵌套的精灵。这些Objet共用一个File GUID，因为它们的源数据存储在同一个Asset文件中，它们在导入过程中通过Local ID区分。

导入时会将源Asset转换为Unity编辑器中目标平台的格式。导入过程可以包含一些耗时的重量级操作，例如纹理压缩。导入后的结果被缓存在Library文件夹中（以Asset文件的File GUID前两个字符命名的文件夹中，来自该Asset的每个Objet都会被序列化到与Asset的File GUID同名的二进制文件中），从而避免重复导入。

所有的Asset（而不仅仅是非原生Asset）都会经历导入过程。原生Asset在导入时不需要漫长的转换处理和序列化。

# 序列化与实例

虽然File GUID和Local ID构成的引用系统是健壮的，但File GUID对比速度慢，在运行时需要更高效的引用系统。

Unity在内部维护了一个缓存（Persistent Manager），将File GUID和Local ID转换为简单的、会话唯一的（Session-unique）整数，即Instance ID。当有新的Object注册到该缓存时，分配其一个自增的Instance ID。

Persistent Manager维护了Instance ID、由File GUID和Local ID定义的Object源数据位置以及内存中的Object实例（如果有的话）之间的映射关系。这使得Object之间可以维持健壮的引用关系。通过解析Instance ID可以快速返回已加载的Object；如果该Object尚未加载，则可以过File GUID和Local ID定位到Object源数据，然后进行即时（Just-in-time）加载。

**在启动时，游戏会立即初始化、缓存项目需要的所有Object的Instance ID（如构建的场景中引用的Object）以及Rescources文件夹中所有的Object。** 在运行时导入新的Asset或从AssetBundle中加载Object时，会向缓存中新增该Object的条目。只有当特定File GUID和Local ID指向的AssetBundle被卸载时，缓存中的Instance ID条目才会被移除，此时，Instance ID、File GUID和Local ID之间的映射将被删除以节省内存。如果该AssetBundle重新被加载，将为从该AssetBundle中加载的Object创建新的Instance ID。

在特定的平台上，某些事件会强制将Object从内存中清除。例如，在iOS平台上，当应用程序被挂起（Suspend）时，图形资源（Graphical Asset）会从显存中卸载。如果来自AssetBundle的Object被卸载, Unity将无法重新加载该Object数据，现有的所有对该Object的引用都将失效。

> **注意**：在运行时，上述控制流并不十分准确。在进行重量级加载操作时去比较File GUID和Local ID并不高效。当构建Unity项目时，File GUID和Local ID被确切地映射成更简单的格式，但其中的思想和原理仍然是相同的。这也是为什么不能在运行时查询Asset文件File GUID的原因。

# MonoScript

**MonoBehaviour持有对MonoScript的引用，而MonoScript只包含用于定位特定脚本类所需的信息。** 这两种类型的Object都不包含脚本类的可执行代码。

MonoScript中含有3个字符串：程序集名称、类名和命名空间。

在构建项目时，Unity将Assets文件夹中所有零散的脚本文件编译到Mono程序集中。**不在Plugins文件夹中的脚本将被放到*Assembly-CSharp.dll*中。Plugins子文件夹中的脚本放在*Assembly-CSharp-firstpass.dll*中。** 此外，Unity 2017.3还提供了自定义托管程序集的功能。

这些程序集，以及预构建的程序集DLL文件，都被包含在最终构建的Unity应用程序中。它们也是MonoScript所引用的程序集。与其他资源不同，Unity应用程序中包含的所有程序集都会在启动时全部加载。

MonoScript Object就是为什么一个AssetBundle（或场景Scene、预制体Perfab）中的任何MonoBehaviour组件都不包含实际可执行代码的原因。这允许不同的Monobehavior可以引用特定的共享类，即使这些Monobehavior位于不同的AssetBundle中。

# 资源（Resource）生命周期

为了减少资源加载时间和管理应用程序的内存占用，有必要了解Object的资源生命周期。

**Object会在明确的具体时刻被加载到内存或从内存中卸载。**

Object会在下列时刻自动**加载**：

- 映射到Object的Instance ID被解引用（Dereference）。
- Object当前没有被加载到内存中。
- Object的源数据可以被定位。

Object也可以在脚本中通过创建或调用资源加载API（如`AssetBundle.LoadAsset`）加载。当Object被加载后，Unity会把其File GUID和Local ID转换到Instance ID以快速查找引用目标。如果满足了下面的两个条件，在Object的Instance ID首次被解引用时它会被请求式（On-demand）加载，这通常发生在引用本身被加载和解析后不久。

- Instance ID引用的Object未被加载。
- Instance ID在缓存中含有合法的File GUID和Local ID。

如果一个File GUID和Local ID没有对应的Instance ID，或者如果已卸载Object的Instance ID引用了非法的File GUID和Local ID，那么该引用将会被保存但不会加载任何实际的Object。这在Unity编辑器中显示为“(Missing)”引用。在正在运行的应用程序或Scene视图中，“(Missing)”的Object会以不同的方式显示，如网格会变得不可见，纹理会呈品红色。

Objtect会在以下情况被**卸载**：

- **无用的Asset被清理会自动卸载相关Object。** 这个过程会在场景被破坏性地改变（如通过`SceneManager.LoadScene`非增量地加载场景）或通过脚本调用`Resources.UnloadUnusedAssets`时触发。此过程只卸载未引用的Object；一个Object只有在没有任何Mono变量和其他激活的Object持有其引用时才会被卸载。此外，任何被标记为`HideFlags.DontUnloadUnusedAsset`和`HideFlags.HideAndDontSave`的Object都不会被卸载。
- **通过调用`Resources.UnloadAsset`精确地卸载从Resources文件夹下加载的Object。** 这些Object的Instance ID仍然是有效，并且仍然映射有效的File GUID和Local ID条目。如果任何Mono变量或其他Object持有对已被`Resources.UnloadAsset`卸载的Object的引用，那么一旦被解引用，该Object将被重新加载。
- **当调用`AssetBundle.Unload(true)`时，来自AssetBundle的Object会自动并立即卸载。** 这会使Object的Instance ID包含的File GUID和Local ID失效，并且对该Object的任何引用都将成为“(Missing)”引用。在c#脚本中，试图访问未加载Object的方法或属性将引发`NullReferenceException`异常。如果`AssetBundle.Unload(false)`被调用，来自已卸载的AssetBundle的激活的Object不会被销毁，但Unity会使其Instance ID包含的File GUID和Local ID失效。如果这些Object被从内存中卸载而仍有任何对该Object的引用，Unity无法重新加载这些Object（估计需要先加载AssetBundle）。

> **注意**：Object在运行时被从内存中清除而不卸载的最常见情况发生在Unity失去对其图形上下文的控制时。例如，当一个移动应用程序被挂起并强制进入后台时，可能会发生这种情况。在这种情况下，移动操作系统通常会从GPU内存中驱逐所有图形资源。当应用程序返回前台时，Unity必须重新加载所有需要的纹理、着色器和网格到GPU，然后才能渲染场景。

# 加载大型层次结构

**当序列化Unity中GameObject的层次结构时，例如序列化预制件，整个层次结构都将被完全序列化。** 也就是说，层次结构中的每个GameObject和Component都将被单独地列化到数据中。这会对加载和实例化GameObject层次结构所需的时间产生微妙的影响。

当创建任何GameObject层次结构时，CPU主要进行：

- （从存储设备、AssetBundle、其他GameObject等中）读取源数据。
- 在新的Transform之间设置父子关系。
- 实例化新的GameObject和Component。
- 在主线程中唤醒新的GameObject和Component。

无论是从现有的层次结构克隆还是从存储中加载GameObject层次结构，后三个时间成本通常都是不变的。然而，**读取源数据的时间会随着GameObject层次结构中的GameObject和Component的数量线性增加，并且还会受到读取速度的影响。**

在现有的所有平台上，从内存中读取数据比从存储设备读取要快得多。此外，不同平台的不同存储介质上的性能特征差异很大。因此，当在低速存储设备上加载预制体时，读取预制体序列化数据所花费的时间可能会很容易超过实例化预制件所花费的时间。也就是说，加载操作的开销受到存储设备I/O性能的影响。

如前所述，**当序列化整个预制体时，每个GameObject和Component的数据都会被单独序列化，这可能会产生重复的数据。** 例如，一个UI窗口中具有30个相同元素，这些元素会被序列化30次，从而产生大量重复的二进制数据。在加载时，这30个重复元素上的所有GameObject和Component的数据都要全部从从磁盘中读取出来，然后才能转换成新的Object实例。

在实例化大型预制体的整体时间开销中，文件读取时间占了很大比重。因此，**对于大型的层次结构，应该将其分模块实例化，然后在运行时整合到一起。**

> **注意**：Unity 5.4改变了Transform在内存中的存储形式。每个根Transform的整个子层次结构都被存储在紧凑、连续的内存块中。当实例化新的需要被立即放置到其他层级下的GameObject时，考虑使用`GameObject.Instantiate`中接受父节点参数的重载方法。使用此API可以避免为新GameObject的根Transform层次结构分配内存空间。在测试中，这可以将实例化操作所需的时间减少约5-10%。