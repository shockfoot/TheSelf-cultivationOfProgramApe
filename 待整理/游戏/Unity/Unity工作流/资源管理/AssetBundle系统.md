# AssetBundle

AssetBundle系统提供了**将一个或多个文件存储到Unity可以索引和序列化的文件中**的方法。**AssetBundle是Unity在应用程序安装后进行分发和更新非代码内容的首选工具。** 通过AssetBundle，开发者可以**用于可下载内容（DLC）以提交更小的程序安装包、根据终端用户设备选择性地加载优化的资源并减少运行时内存压力**。

AssetBundle是一个存储文件，包含特定于平台的**非代码**资产(如模型、纹理、预制体、音频、场景等)，Unity可以在运行时加载。

**AssetBundle可以包含代码对象实例的可序列化数据**，例如`ScriptableObject`的资产。但是，类定义本身被编译为项目程序集之一。当你在Asset Bundle中加载一个可序列化的对象时，Unity会找到匹配的类定义，创建它的一个实例，并使用可序列化的值设置该实例的字段，这意味着可以在AssetBundle中引入新道具，只要这些道具不需要改变类的定义。

磁盘上的AssetBundle文件包含了一个序列化文件（所有资产）和资源文件（为某些资产如纹理和音频单独创建的二进制数据块，以使Unity能够有效地从另一个线程上的磁盘加载它们）。

代码中的AssetBundle指已加载的AssetBundle对象，可以通过路径从此对象中加载指定资源。

AssetBundles可以表达彼此之间的依赖关系。例如，一个AssetBundle中的材质可以引用另一个AssetBundle中的材质。为了通过网络进行有效的交付，您可以根据用例需求(LZMA和LZ4)选择内置算法来压缩AssetBundles。

# AssetBundle原理

## AssetBundle组成

简单来说，一个**AssetBundle文件由两部分组成：数据头和数据段**。

**数据头含有AssetBundle的相关信息，如标识符（Identifier）、压缩类型（Compression Type）和清单文件（Manifest）。** 清单文件是一个以Object名称为键的查找表。表中每个条目提供一个字节索引，用于定位Object在数据段中的未知。在大多数平台上，这个查找表是通过平衡搜索树实现的。具体来说，Windows和OSX派生平台（包括iOS）使用红黑树。因此，**构建清单文件所需的时间将随着AssetBundle中Asset数量变化的增加速度（O(logn)）大于线性增加（O(n)）。**

**数据段包含由序列化AssetBundle中Asset而生成的原始数据。如果指定压缩方式为LZMA，所有Asset的序列化数据会被压缩到一个数组中；如果为LZ4，则不同Asset的字节数据会被单独压缩；而不使用压缩时，数据段将保留为原始的字节流。**

> 在Unity 5.3之前，Object不能在AssetBundle中单独压缩。因此，在5.3之前的Unity中，如果要从已压缩的AssetBundle中读取Object，必须先解压缩整个AssetBundle。通常情况下，Unity会缓存AssetBundle的解压缩副本，以提高对同一AssetBundle的后续加载请求的加载性能。

## 加载AssetBundle

AssetBundles可以通过五个不同的API加载。这五个API会受到下面两种因素的影响而产生不同的行为：

- 压缩方式。
- 加载平台。

这些API是：

- `AssetBundle.LoadFromMemory`（有异步模式可选）。
- `AssetBundle.LoadFromFile`（有异步模式可选）。
- `AssetBundle.LoadFromStream`（有异步模式可选）。
- `UnityWebRequest`的`DownloadHandlerAssetBundle`。
- `WWW.LoadFromCacheOrDownload`（Unity 5.6及更高版本）。

### AssetBundle.LoadFromMemory(Async)

**Unity不推荐使用这个API。**

`AssetBundle.LoadFromMemory`从托管代码的字节数组（C#中的byte[]）加载AssetBundle。该方法总是**将托管代码中的源数据复制到新分配的、连续的本机内存块中**。如果AssetBundle是LZMA压缩的，它将在复制时解压缩AssetBundle。LZ4压缩或未压缩的AssetBundles将被逐字复制（be copied verbatim）。

**此API消耗的内存峰值将至少是AssetBundle大小的两倍：一个拷贝在API创建的本机内存中，另一个拷贝在传递给API的托管字节数组中。** 因此，**通过此API从AssetBundle加载的Asset将在内存中复制三次：一次在托管代码字节数组中，一次在AssetBundle的本机内存副本中，第三次在GPU或系统内存中为资产本身复制。**

### AssetBundle.LoadFromFile(Async)

`AssetBundle.LoadFromFile`是**从本地存储（如硬盘或SD卡）加载AssetBundle**。

LZMA压缩的AssetBundle会被解压缩到内存中，而未压缩的和LZ4压缩的AssetBundle则直接从本地存储读取。

在桌面系统（PC、Mac、Linus）、主机和移动平台上，此API将**只加载AssetBundle数据头，并将剩余的数据留在磁盘上**。AssetBundle中的Object会在调用了加载方法（如`AssetBundle.load`）或其Instance ID被解引用时**按需加载**。这种情况下，不需要消耗额外的内存。

在Unity编辑器中，此API会将整个AssetBundle加载到内存中，就像使用`AssetBundle.LoadFromMemory`从磁盘中读取AssetBundle一样。如果项目是在Unity编辑器中配置的，这个API可能会导致在加载AssetBundle期间出现内存峰值。在发布版本中应该不会对设备性能造成影响。

### AssetBundle.LoadFromStream(Async)

`AssetBundle.LoadFromStream`是**托管流中加载AssetBundle**。

LZMA压缩的AssetBundle会被解压缩到内存中，而未压缩的和LZ4压缩的AssetBundle则直接从流中读取。

以下是对流对象的限制，以优化AssetBundle加载：

- AssetBundle数据索引必须从0开始。
- Unity在加载AssetBundle数据之前将查找位置设置为零。
- Unity假定流中的读取位置不会被任何其他进程改变。这允许Unity进程从流中读取，而不必在每次读取之前调用`Seek()`。
- `stream.CanRead`必须返回`true`。
- `stream.CanSeek`必须返回`true`。
- 流对象必须可以从不同于主线程的线程访问。`Seek()`和`Read()`可以从任何Unity本地线程中调用。
- 在某些情况下，Unity将尝试读取传入的AssetBundle数据的大小。流对象必须在不抛出异常的情况下优雅地处理这个问题。流对象还必须返回实际读取的字节数（不包括AssetBundle数据末尾后的任何字节）。
- 当从AssetBundle数据的末尾开始并尝试读取数据时，流对象必须返回0字节数据并且不抛出异常。

为了减少从本机代码到托管代码的调用次数，使用缓冲区大小为`managedReadBufferSize`的缓冲读取器从流读取数据。

- 改变`managedReadBufferSize`可能会改变加载性能，尤其是在移动设备上。
- `managedReadBufferSize`的最佳值因项目而异，也可能因资产而异。
- 一个比较好的值范围是：8KB、16KB、32KB、64KB、128KB。
- 对于压缩的、或包含较大Asset的、或具有较少Asset但它们是从AssetBundle中顺序加载的AssetBundle，则较大的值可能更好。
- 对于未压缩的、或读取大量小Asset、或具有较多Asset但以随机顺序加载的AssetBundle，较小的值可能更好。

不要在加载AssetBundle或从AssetBundle中加载任何Asset时释放流对象。它的生命周期应该比AssetBundle长。这意味着在调用`AssetBundle.Unload`之后才处理流对象。

### DownloadHandlerAssetBundle

开发者可以使用UnityWebRequest API明确指定Unity如何处理下载的数据，并且可以免除不必要的内存占用。使用UnityWebRequest下载AssetBundle的最简单的方法是调用`UnityWebRequestAssetBundle.GetAssetBundle`。`UnityWebRequestAssetBundle.GetAssetBundle`会创建一个优化的`UnityWebRequest`并通过HTTP GET方式下载AssetBundle。

此外，`DownloadHandlerAssetBundle`类可以使用一个工作线程（Worker Thread）将下载的数据流式传输到固定大小的缓冲区中，然后根据下载处理程序的配置方式，将缓冲的数据转到临时存储或AssetBundle缓存中。所有这些操作都在本机代码中进行，从而避免托管堆内存的增加。此外，这个下载处理程序不保留所有下载字节的本机代码副本，进一步减少下载AssetBundle的内存开销。

LZMA压缩的AssetBundles将在下载过程中解压缩，并使用LZ4压缩进行缓存。这个行为可以通过设置`cache.compressionenabled`来改变。下载完成后，可以通过`assetBundle`属性访问下载的AssetBundle。

如果为UnityWebRequest对象提供了缓存信息，并且请求的AssetBundle已经存在于Unity的缓存中，那么可以立即获取该AssetBundle，这个API将与`AssetBundle.LoadFromFile`相同。

### WWW.LoadFromCacheOrDownload

> **提示**：从Unity 2017.1开始，`WWW.LoadFromCacheOrDownload`被封装到了UnityWebRequest中。因此，在使用Unity 2017.1或者更高版本进行开发时，应该使用UnityWebRequest。`WWW.LoadFromCacheOrDownload`会在将来的发行版中被废弃。

> 下列内容适用于Unity 5.6以及更早的版本。

`WWW.LoadFromCacheOrDownload`是一个允许从远程服务器和本地存储加载Object的API。可以通过`file:// URL`从本地存储加载文件。如果AssetBundle存在于Unity缓存中，这个API的行为将与`AssetBundle.LoadFromFile`相同。

如果AssetBundle还没有被缓存，那么`WWW.LoadFromCacheOrDownload`将从源文件中读取AssetBundle。如果AssetBundle被压缩，它将会在工作线程中被解压缩并写入缓存；否则，它将在工作线程中直接写入缓存。这个缓存在`WWW.LoadFromCacheOrDownload`和UnityWebRequest之间共享。一旦AssetBundle被缓存，`WWW.LoadFromCacheOrDownload`将从缓存中加载AssetBundle的数据头信息。

当数据将被解压缩并通过固定大小的缓冲区写入缓存时，WWW对象将在本机内存中保留一份AssetBundle字节的完整副本。该副本用于维护`WWW.bytes`属性。

由于WWW对象中缓存AssetBundle字节数据会造成内存开销，所以AssetBundle文件应尽量小。

不像UnityWebRequest，每次调用这个API都会生成一个新的工作线程。因此，在内存有限的平台上，比如移动设备，一次只应该使用这个API下载一个AssetBundle，以避免内存峰值。当多次调用此API时，要避免创建过多的线程。如果需要下载超过5个AssetBundle，可在代码中维护一个下载队列，以确保只有几个AssetBundle下载同时运行。

### 建议

通常情况下，应该优先使用`AssetBundle.LoadFromFile`，这一API在速度、磁盘使用和运行时内存占用方面都很高效。

如果项目必须下载AssetBundle，强烈推荐在Unity 5.3以及更新的版本中使用UnityWebRequest、在Unity 5.2以及更早的版本中使用`WWW.LoadFromCacheOrDownload`。

无论是在使用UnityWebRequest还是`WWW.LoadFromCacheOrDownload`，都要确保下载AssetBundle后在合适的位置调用`Dispose`方法。C#的using语句是一个很简单并且可以确保WWW和UnityWebRequest被安全释放的方法。

对于由大型工程团队开发，并且有唯一、特定的缓存或下载需求的项目，可以考虑使用自定义的下载器。开发自定义下载器是一项非凡的工程任务，任何自定义下载器都应该兼容`AssetBundle.LoadFromFile`。

## 从AssetBundle中加载Asset

可以使用AssetBundle对象的几个不同的方法来加载Object，这些方法都有同步和异步两个版本：

- LoadAsset（LoadAssetAsync）
- LoadAllAssets（LoadAllAssetsAsync）
- LoadAssetWithSubAssets（LoadAssetWithSubAssetsAsync）

这3个API的**同步版本都比它们的异步版本速度快，至少会快1帧**。异步方法会在每帧加载多个Object，数量上限受到们的时间片（Time-Slice）限制。

当需要加载多个独立的Object时应该使用LoadAllAssets。只有在AssetBundle中的大多数或全部Object都要用到时才应该使用这个方法。与其他两个API相比，LoadAllAssets比多次单独调用LoadAssets要快一些。因此，**如果要加载的资产数量很大，但一次需要加载的资产少于66%，请考虑将AssetBundle拆分为多个较小的AssetBundle，并使用LoadAllAssets。**

当加载含有多个嵌套Object的复合Asset时使用LoadAssetWithSubbAssets，例如带有嵌入动画的FBX模型或带有多个精灵的精灵图集。如果需要加载的Object都来自同一个资产，但是与许多其他不相关的对象一起存储在AssetBundle中，那么使用这个API。

除了上述几种情况，都应该使用LoadAsset或者LoadAssetAsync。

### 底层加载细节

Object的加载不在主线程执行，而是在工作线程中读取。任何Unity系统中非线程敏感的内容（脚本、图形除外）都会被转移到工作线程上进行。例如，由网格创建VBO、解压纹理等。

从Unity 5.3开始，Object支持并行加载。可以在多个工作线程上对多个Object进行反序列化、处理和继承。当一个Object完成加载时，会调用它的`Awake`回调，并且该对象将在下一帧期间对Unity引擎可用。

同步`AssetBundle.Load`方法将暂停主线程，直到Object加载完成。它也会对Object加载进行时间切片，以便Object集成占用的每帧时间不会超过一定的毫秒数。毫秒数由`Application.backgroundLoadingPriority`属性设置：

- `ThreadPriority.High`：每帧最多50毫秒。
- `ThreadPriority.Normal`：每帧最多10毫秒。
- `ThreadPriority.BelowNormal`：每帧最多4毫秒。
- `ThreadPriority.Low`：每帧最多2毫秒。

从Unity 5.2开始，多个Object会同时加载，直到达到帧时间限制（Frame-time Limit）。

### AssetBundle依赖

当一个AssetBundle的Object引用了其他AssetBundle中的Object时，前者会依赖后者。

AssetBundle是它所包含的通过每个Object的FileGUID和LocalID标识的源数据的数据源。

因为Object会在它的Instance ID首次被解引用时进行加载，而且当加载AssetBundle时Object会被分配一个合法的Instance ID，所以加载AssetBundle的顺序并不重要。但是，在加载Object自身之前加载他所依赖的所有AssetBundle很重要。当加载了父AssetBundle后，Unity不会尝试去自动加载任何子AssetBundle。

示例：假设材质A引用了纹理B；材质A被打包到AssetBundle 1中，纹理B被打包到AssetBundle 2中。此时，在从AssetBundle 1加载材质A之前，必须先加载AssetBundle 2。但这不意味着在加载AssetBundle 1之前必须先加载AssetBundle 2或者从AssetBundle 2中加载纹理B。只要在从AssetBundle 1中加载材质A之前加载了AssetBundle 2就足够了。然而，Unity不会在加载了AssetBundle 1之后自动加载AssetBundle 2。这必须在脚本代码中手动实现。

AssetBundle之间的依赖关系通过两个不同的API自动进行追踪，具体取决于运行时环境。在Unity编辑器中，AssetBundle依赖可以通过AssetDatabase API查询。AssetBundle分配和依赖可以通过AssetImporter访问和修改。在运行时，Unity提供了一个可选的API用于加载在AssetBundle构建过程中通过AssetBundleManifest API生成的依赖信息。

### AssetBundle清单文件（Manifest）

在使用`BuildPipeline.BuildAssetBundles`执行AssetBundle构建管线时，Unity会序列化一个包含每个AssetBundle的依赖信息的Object，它被单独存放到一个AssetBundle中，这个AssetBundle中只有一个AssetBundleManifest类型的Object。

这一Asset会存储在与构建AssetBundle时的目录同名的AssetBundle中。如果项目在(projectroot)/build/Client/文件夹中构建了AssetBundle，那么包含了配置表的AssetBundle会被保存为(projectroot)/build/Client/Client.manifest。

包含配置表的AssetBundle可以像其他AssetBundle一样被加载、缓存和卸载。

AssetBundleManifest对象本身提供了`GetAllAssetBundles`来列出所有和配置表同时构建的AssetBundle，还提供了两个用于查询指定AssetBundle的依赖的方法：

- `AssetBundleManifest.GetAllDependencies`会返回一个AssetBundle的所有层级依赖，包括AssetBundle的直接子级、子级的子级等的依赖。
- `AssetBundleManifest.GetDirectDependencies`只返回AssetBundle的直接子级的依赖。

> **注意**：这两个方法都会分配字符串数组，因此，应该少用它们，并且不要在应用程序的性能敏感时期使用它们。

### 建议

在很多情况下，最好在玩家进入应用程序的性能临界区之前尽量多的加载所需的Object，例如主要的游戏关卡或世界内容。这在移动设备上尤其关键，因为访问本地存储较慢，而且在游戏时加载和卸载Object可能触发垃圾回收器工作。

# AssetBundle工作流

## Asset分配策略

决定怎样将项目中的资源划分到不同的AssetBundle中不是一项简单的工作。很容易想到的两个方案是把每个Object单独放到一个AssetBundle中和把所有Object放到同一个AssetBundle中。但这两种做法都有很明显的缺点：

- AssetBundle太少：增加运行时内存占用、增加加载时间、需要下载的内容更大。
- AssetBundle太多：增加构建时间、使开发更复杂、增加整体下载时间。

分组Object的基本策略是：

- 逻辑实体：根据Asset所代表的项目的功能部分将资产分配给AssetBundle。逻辑实体分组是可下载内容DLC的理想选择，因为以这种方式分离所有内容，您可以对单个实体进行更改，而无需下载其他未更改的Asset。
- Object类型：将类似类型的Asset（例如音轨或语言本地化文件）分配给单个AssetBundle。Object类型分组是构建供多个平台使用的AssetBundle的更好策略之一。
- 同时使用的内容：将Asset捆绑在一起，这些Asset将同时加载和使用。

无论采用哪种策略，这里都有一些额外的提示，需要牢记：

- 将经常更新的对象拆分为AssetBundle，与很少更改的对象分开。
- 对可能同时加载的对象进行分组。例如模型、其纹理和动画。
- 如果多个AssetBundle中的多个对象依赖不同的AssetBundle中的单个资源，可将依赖项移动到单独的AssetBundle。如果多个AssetBundle引用其他AssetBundle中的同一组资源，则可将这些依赖项拉入共享AssetBundle以减少重复。
- 如果不太可能同时加载两组对象，例如标准和高清资源，确保它们位于各自的AssetBundle中。
- 如果同时频繁加载的AssetBundle中少于50%，考虑拆分该AssetBundle。
- 考虑合并规模较小（少于5到10个资产）但其内容经常同时加载的AssetBundle。
- 如果一组对象只是同一对象的不同版本，考虑AssetBundle变体。

避免以下常见问题：

### Asset重复

如果Object的`assetBundleName`属性不为空，那么它会被打包到指定的AssetBundle中。如果Object没有被分配到指定的AssetBundle中，那么所有依赖该Object的AssetBundle都将包含该Object。因此，在加载这些AssetBundle时，该Object会被实例化为多个被标识为不同的Object副本，**增加应用程序的AssetBundle的整体大小和内存占用**。

有几种方式可以定位这种问题：

- 确保打包进不同AssetBundle的Object间没有共同的依赖。把含有共同依赖的Object打包进同一个AssetBundle中。（这种做法不适合含有很多共享依赖的项目。）
- 分离AssetBundle，确保含有共同依赖的AssetBundle不会被同时加载。（这种做法适合某些特定类型的项目，例如基于关卡的游戏，不过它同时也会增加AssetBundle的整体大小、构建时间和加载时间。）
- 确保所有被依赖的资源被打包到了AssetBundle中。这样可以完全消除产生重复资源的风险，但会增加复杂性。应用程序必须追踪AssetBundle之间的依赖关系，并且在调用`AssetBundle.LoadAsset`之前确保加载了正确的AssetBundle。

### 精灵图集重复

所有自动生成的精灵图集都会被分配到用于生成精灵图集的Object所在的AssetBundle中。如果这些精灵Object被分配到了不同的AssetBundle中，那么对应的精灵图集就不会被分配到AssetBundle中，这时它们可能会产生副本。如果这些精灵Object没有被分配到AssetBundle中，它们对应的精灵图集也不会被分配到AssetBundle中。

要确保精灵图集没有重复，请确保**同一个精灵图集中的所有精灵都被打包到了同一个AssetBundle中**。

### Android纹理

因为Android生态系统碎片化十分严重，通常需要将纹理以几种不同的格式进行压缩。所有的Adnroid设备都支持ETC1，但是ETC1不支持带有Alpha通道的纹理。如果应用程序不需要OpenGL ES 2支持，那么解决这一问题的最简洁的方案是使用ETC2，所有的Android OpenGL ES 3设备都支持这种格式。

大多数应用程序都需要兼容不支持ETC 2的老旧设备，有一种解决方案是使用Unity 5中的AssetBundle Variant。

要使用AssetBundle Variant，所有不能够使用ETC1进行规则压缩（Cleanly Compressed）的纹理必须隔离打包到只含有纹理的AssetBundle中。然后，使用设备厂商指定的纹理压缩格式（例如DXT5、PVRTC和ATITC）创建足够多的变体来适配那些不支持ETC2的设备。对于每个AssetBundle Variant，把它们里面的纹理的 TextureImporter 设置修改为所使用的压缩格式。

在运行时，可以使用`SystemInfo.SupportsTextureFormat`检测某种纹理格式是否受支持。应该使用这一信息来选择和加载包含受支持的压缩纹理的AssetBundle Variant。

## 管理已加载的AssetBundle

在内存敏感的环境中，仔细控制加载的Object的大小和数量十分重要。当Object被从活动Scene中移除时，Unity不会自动将其卸载。Asset清理会在特殊时期触发，但也可以手动触发。

需要对AssetBundle本身进行仔细的管理。一个由本地存储文件支撑的AssetBundle（在Unity缓存中或者由`AssetBundle.LoadFromFile`加载）的内存开销很小，很少会超过几十KB。但是，如果同时存在大量的AssetBundle，这一开销仍然可能引发问题。

因为大多数游戏允许玩家重新体验游戏内容（例如重玩某关），所以知晓该在什么时候对AssetBundle进行加载和卸载很重要。如果某个AssetBundle被不恰当地卸载，这可能造成Object在内存中产生重复的副本。在某些情况下，不恰当地卸载AssetBundle也会引起其他不良行为地产生，例如导致纹理丢失。

在调用`Unload(bool unloadAllLoadedObjects)`方法时，传入的`unloadAllLoadedObjects`参数会导致不同的行为，这对于管理资源和AssetBundle来说非常重要。此API会卸载调用方法的AssetBundle的数据头信息，是否同时卸载由此AssetBundle实例化的所有Object由`unloadAllLoadedObjects`参数决定。如果参数为true，来自于此AssetBundle的所有Object会被立即卸载——即使它们目前正在活动Scene中被使用着。调用`AssetBundle.Unload(false)`会破坏AssetBundle和Object之间的链接关系。如果之后又加载了AssetBundle，会将AssetBundle中的Object的新的副本加载到内存中，这样，内存中会出现两份Object副本。

对于大多数项目来说，这种行为是不好的，应该使用`AssetBundle.Unload(true)`并且采用一些方法来确保没有多余的Object副本。有两个常用的方法：

- **在应用程序生命周期中合理定义节点去卸载短期使用的AssetBundle**，例如在关卡之间或者在加载画面时卸载。这是最简单也是最常用的选项。
- **维护每个Object的引用数量**，并且只在AssetBundle的Object都没被使用时才卸载AssetBundle。这样应用程序就可以卸载和重新加载每个Obejct，而不会产生多余的副本。

如果必须在应用程序中使用`AssetBundle.Unload(false)`，那么只有在下列两种情况下可以卸载Object：

- 消除对不再使用的Object的所有引用，无论是在Scene中还是代码中，然后调用`Resources.UnloadUnusedAssets`。
- 非附加式的加载Scene。这会销毁当前Scene中的所有Object并自动调用`Resources.UnloadUnusedAssets`。

如果游戏中定义了良好的节点让用户等待Object加载和卸载，例如切换游戏模式和关卡时，应该在这些节点处尽量多的卸载无用的Object和加载新的Object。实现上述操作的最简单的方法是，将游戏中不相关的部分分别打包到多个Scene中，然后分别将这些Scene连同它们的全部依赖打包到AssetBundle中。这样，游戏就可以进入到一个“加载”Scene中，完全卸载包含旧Scene的AssetBundle，然后加载包含新Scene的AssetBundle。

## 分发AssetBundle

有两个分发AssetBundle的基本方法：和程序一同安装或者在安装程序后下载。

选择随程序安装还是安装后下载取决于程序运行平台的功能和限制。在移动平台上通常选择安装后下载，这样可以减小初始安装大小。在主机和PC上通常选择随程序安装。

具有合理的结构的项目可以在安装后发布新内容或添加补丁，并且不用关心AssetBundle是如何分发的。

将AssetBundle附带在项目中是分发AssetBundle的最简单方法，因为这样做不需要额外的下载管理代码，并且可以减少程序构建时间、加快开发迭代速度，提供可更新内容的基础版本。

在安装应用后下载下载AssetBundle能够让用户在更新时不用重新下载整个应用。此时，将AssetBundle置于Web服务器上，然后通过UnityWebRequest下载。Unity会自动在本地存储上缓存下载的AssetBundle。如果下载的AssetBundle使用LZMA压缩，它会在解压或者重新以LZ压缩（取决于`Caching.compressionEnabled`设置）后再存储，以便在以后更快地加载。如果下载的AssetBundle使用LZ4压缩，它会被保留压缩格式存储。如果缓存区已满，Unity会移除最近一段时间内使用次数最少的AssetBundle。一般情况下推荐优先使用UnityWebRequest，只有在下列情况中才有必要开发自定义的下载系统：

- 特定项目的内置API内存消耗和缓存行为性能不能满足需求。
- 项目必须运行依赖于特定平台的代码才能实现需求。

### StringAssets文件夹

StreamingAssets文件夹中的所有内容都会在构建时被复制到最终的应用程序中。在运行时，可以通过`Application.streamingAssetsPath`属性获取`AssetBundle.LoadFromFile`在设备上的完整路径。在大多数平台上都可以通过`AssetBundle.LoadFromFile`加载其中的AssetBundle。

在Android中，StreamingAssets里面的资源存储在APK中，如果它们被压缩过，可能会花费更多时间加载，在APK中存储文件可能使用不同的压缩算法。在不同版本的Unity中，压缩算法可能会不同。如果采用了压缩，`AssetBundle.LoadFromFile()`的执行速度会更慢，这时，可以使用`UnityWebRequestAssetBundle.GetAssetBundle`检索已缓存的版本。AssetBundle会在首次运行UnityWebRequest时被解压，这可以让后续的操作速度更快。需要注意的是，这样做会占用更多的存储空间，因为AssetBundle会被复制到缓存。一个替代方案是，在构建时导出Gradle项目并对AssetBundle添加扩展，然后再编辑build.gradle文件并将扩展添加到不压缩的部分。这样做以后，就可以使用`AssetBundle.LoadFromFile()`而不必产生解压开销。

> **注意**：在某些平台上，StreamingAssets是只读的。如果要在程序安装之后更新AssetBundle，请使用`WWW.LoadFromCacheOrDownload`或者自定义下载器。

### Unity内置缓存

Unity内置了一个缓存系统，用于缓存通过UnityWebRequest下载的AssetBundle。UnityWebRequest 方法有一个接受AssetBundle版本号作为参数的重载，这个版本号并不存储在AssetBundle中，也不由AssetBundle系统生成。

缓存系统通过UnityWebRequest来维护最新的版本号。当调用带有版本号的重载方法时，缓存系统会通过对比版本号来检查是否已经有了缓存的AssetBundle。如果版本号匹配成功，系统会加载已缓存的AssetBundle，否则Unity会下载新的副本，作为参数传入的版本号会被分配给这个新的副本。

缓存系统中的AssetBundle仅以它们的文件名进行标识，而不是通过下载它们的完整URL标识。这意味着，一个AssetBundle可以被存储在多个不同的网络位置，例如内容分发网络（CDN）。只要文件名相同，Unity就会把它们认为是同一个AssetBundle。

应该根据每个应用程序的不同需求去设置合适的AssetBundle版本号分配策略，并在UnityWebRequest中使用这些版本号。版本号可以是各种唯一标识符，例如CRC值。`AssetBundleManifest.GetAssetBundleHash()`的返回值也可以用作版本号，但不推荐这样做，因为该API并没有进行真正的哈希计算，只是提供了一个近似值。

## 存储AssetBundle

在所有的平台上，`Application.persistentDataPath`都会指向一个可写的磁盘位置，这个位置用于存储在程序运行过程中需要持久化的数据。在开发自定义下载器时，强烈推荐将下载的数据存储在`Application.persistentDataPath`的子目录中。

将AssetBundle缓存放到`Application.streamingAssetPath`中是一个糟糕的选择，而且有些时候这个文件夹是不可写的：

- OSX：在.app包中，不可写。
- Windows：在安装目录中（例如Program Files），经常不可写。
- iOS：在.ipa包中，不可写。
- Android：在.apk文件中，不可写。

## AssetBundle Variant

AssetBundle系统的一个关键功能是对AssetBundle Variant的引入。AssetBundle Variant的目的是让应用程序调整其内容来更好的适配运行时环境。在加载Object和处理Instance ID引用时，变体可以让来自不同AssetBundle中的不同Object表现为同一个Object。从理论上讲，这可以使两个Object共享同样的File GUID和Local ID，并且用一个字符串形式的Variant ID标识要实际加载的Object。

这个系统由两个基本用例：

- 针对给定的平台适当地简化AssetBundle的加载。
> 例如：构建系统可以为支持DirectX的Windows系统创建一个含有高分辨率纹理贴图和复杂的着色器的AssetBundle，为Android系统创建另一个含有低精度内容的AssetBundle。在运行时，项目的资源加载代码会根据平台加载合适的AssetBundle Variant，但传递给`AssetBundle.Load`的Object名称不需要改变。
- 使应用程序可以在同一平台的不同硬件上加载不同内容。
> 这是大范围适配各种移动设备的关键。在任何模拟真实世界的应用中，iPhone 4都不能和最新的iPhone显示同样高精度的画面。在碎片化十分严重的Android上，AssetBundle Variant可以用于处理对不同屏幕长宽比和像素密度的适配。

AssetBundle Variant系统的一个关键性**限制**是它需要从不同的Asset来构建变体。即使这些Asset之间唯一的区别只是它们的导入设置不同，它们也会受到这种限制。如果一个纹理被打包到变体A和变体B中，而其中仅有的区别是导入时选择的纹理压缩算法不同，那A和B也会是完全不同的变体，即A和B会是磁盘上的两个不同的文件。

这一限制增加了大型项目的管理难度，因为必须在版本控制系统中维护特定Asset的多个副本。当开发者修改了这些Asset时，它们的所有副本进行更新。这种问题没有内建的解决办法。

很多团队实现了他们自己的AssetBundle Variant格式——定义良好的后缀名并附加到文件名中，以此来标识AssetBundle的特定变体。自定义程序代码，在打包AssetBundle的时候修改它所包含的Asset的导入设置。有些开发者扩展了它们的自定义系统，这样就可以修改父子到预制体上的组件上的参数。

## 压缩选择

是否对AssetBundle进行压缩需要考虑下列的几个重要的因素：

- 加载时间：从本地存储或本地缓存中加载未压缩的AssetBundle要比加载压缩过的AssetBundle的速度快得多。
- 构建时间：LZMA和LZ4压缩文件的速度非常慢，而且Unity会连续的处理AssetBundle。含有大量AssetBundle的项目会花费大量的时间进行压缩。
- 应用大小：如果AssetBundle被附带在项目中，将它们压缩可以减小应用程序的体积。不过AssetBundle可以选择在安装之后再下载。
- 内存占用：再Unity 5.3之前，所有的解压机制都需要将被压缩的AssetBundle整个加载到内存中进行解压。如果考虑内存占用量，请使用不压缩或者以LZ4压缩的AssetBundle。
- 下载时间：如果AssetBundle很大或者用户网络带宽有限，那可能需要压缩。

## AssetBundle和WebGL

**强烈推荐开发者不要在WebGL项目中使用压缩的AssetBundle。**

在WebGL项目中所有AssetBundle的解压和加载操作都会发生在主线程中。因为目前Unity的WebGL导出选项不支持工作线程。AssetBundle的下载使用XMLHttpRequest委托给了浏览器，它会在Unity的主线程中执行。

如果在使用Unity 5.5或更早地版本，尽量避免使用LZMA压缩，使用按需进行压缩的LZ4来取代它。如果需要比LZ4还小的分发文件，可以配置Web服务器在HTTP协议层面对文件进行GZip压缩。在Unity 5.6中，移除了WebGL平台的LZMA压缩选项。