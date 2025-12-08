## 介绍

AssetBundle是将资源使用Unity提供的一种用于存储资源的压缩格式打包后的集合（.assetbundle或.unity3d），可以存储任何一种Unity可以识别的资源，如模型、纹理、音频、场景等，但无法包括C#脚本，因此通常使用Lua进行热更新。

相对于Resources文件夹下的资源，AB包更好管理：

- Resources的资源在打包时会把资源压缩了一起打包，而且打包后的资源是只读的，无法修改的；而AB包是可以动态的更新，存储位置和压缩方式可以自定义，后期还可以配合热更新进行动态的更新。
- 减小包体的大小。因为AB包在打包时进行压缩资源，可以节省硬盘空间，减少初始包体大小。
- 热更新。资源热（UI）、脚本（Lua脚本）热更新。热更新基本规则：客户端会自带一部分很少的默认资源，启动客户端之后，第一步会去服务端获取资源服务器地址，第二部通过在资源服务器上资源对比文件最新的各种AB包来去检测哪些需要热更新，哪些需要下载，下载AB包。

Unity支持三种 AssetBundle打包的压缩方式：

- LZMA（Standard Compression）：BuildAssetBundleOptions.None。是一种默认的压缩形式。这种标准压缩格式是一个单一LZMA流序列化数据文件，并且在使用前**需要解压缩**整个包体。LZMA压缩是比较流行的压缩格式，能使压缩后文件达到最小，但是解压相对缓慢，导致加载时需要较长的解压时间。
- LZ4（Chunk Based Compression）：BuildAssetBundleOptions.ChunkBasedCompression。Unity支持LZ4压缩，能使得压缩量更大，而且在使用资源包前**不需要解压**整个包体。LZ4压缩是一种“Chunk-based”算法，因此当对象从LZ4压缩包中加载时，只有这个对象的对应模块被解压即可，速度更快，意味着不需要等待解压整个包体。LZ4压缩格式是在Unity5.3版本中开始引入的，之前的版本不可用。
- 不压缩：BuildAssetBundleOptions.UncompressedAssetBundle。不压缩的方式打包后包体会很大，导致很占用空间，但是一旦下载Assetbundle，访问非常快。不推荐这种方式打包，因为现在的加载功能做的很友好了，完全可以用加载界面来进行后台加载资源，而且时间也不长。

使用AB包动态加载资源首先要获取AB对象，然后从AB包中加载目标资源。

运行时可以通过两种方式获取AB对象：

- 先获取WWW对象，再通过WWW.assetBundle获取AssetBundle对象。
- 直接获取AssetBundle。

前者的Load操作在内存中进行，相比后者的IO操作开销更小；不形成缓存文件，而后者则需要额外的磁盘空间存放缓存；能通过WWW.texture、WWW.bytes、WWW.audioClip等接口直接加载外部资源，而后者只能用于加载AssetBundle。然而，前者每次加载都涉及到解压操作，而后者在第二次加载时就省去了解压的开销。

有两种方式卸载AB包：

- Assetbundle.Unload：该方法会卸载运行时内存中包含在bundle中的所有资源。当传入的参数为true，则不仅仅内存中的AssetBundle对象包含的资源会被销毁。根据这些资源实例化而来的游戏内的对象也会销毁。当传入的参数为false，则仅仅销毁内存中的AssetBundle对象包含的资源。
- Resource.UnloadUnusedAssets和Resources.UnloadAsset：卸载掉所有没用到的Assets。需要注意的是，该接口作用于整个系统，而不仅仅是当前的AssetBundle，而且不会卸载从当前AssetBundle文件中加载并仍在使用的Assets。
- 对于WWW对象，可以使用www=null（不会立即释放内存，而是系统的自动回收机制启动时回收）或www.dispose（立即调用系统的回收机制来释放内存）来卸载。对于Web Stream数据，它所占用的内存会在其引用计数为0时，被系统自动回收。

## 原理

当AssetBundle解压加载到内存之后，可以通过WWW.assetbundle属性获得AssetBundle对象来得到各个Assets，并对这些Assets进行加载或者实例化操作。在加载过程中，Unity会将AssetBundle中的数据流转变成Unity可识别的信息类型，如材质、纹理等。加载完成之后，就可以对其进行更多操作，如对象的实例化、材质复用、纹理替换等等。

一般开发流程为：

1. 创建Asset bundle。在Unity编辑器中通过脚本将所需要的资源打包成AssetBundle文件。
2. 上传服务器。将打包好的AssetBundle文件上传至服务器中，使得游戏客户端能够获取当前的资源，进行游戏的更新。
3. 下载AssetBundle。首先将其下载到本地设备中，然后再通过AsstBudle的加载模块将资源加到游戏之中。
4. 加载。通过Unity提供的API可以加载资源里面包含的模型、纹理图、音频、动画、场景等来更新游戏客户端。
5. 卸载AssetBundle，卸载之后可以节省内存资源，并且要保证资源的正常更新。