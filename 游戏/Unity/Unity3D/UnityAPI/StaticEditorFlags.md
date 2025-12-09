# StaticEditorFlags

所属命名空间`UnityEngine`。描述哪些Unity系统认为`GameObject`是静态的，并将`GameObject`包含在Unity编辑器的预计算中。在运行时设置`StaticEditorFlahs`对这些系统没有影响。

## 值

- `ContributeGI`：在全局照明计算时包括`GameObject`的Mesh Renderer。这些计算是在烘焙时预计算光照数据时进行。此属性值公开了`ReceiveGI`属性。此属性值仅在目标场景启用全局照明设置时生效
- `OccluderStatic`：在遮挡剔除系统中将`GameObject`标记为静态遮挡物体。
- `OccludeeStatic`：在遮挡剔除系统中将`GameObject`标记为静态被遮挡物体。
- `BatchingStatic`：将`GameObject`的网格与其他合格网格结合在一起，以降低运行时渲染成本。可以使用`StaticBatchingUtility.Combine`以组合运行时未启用`StaticEditorFlag.BatchingStatic`的网格。
- `NavigationStatic`：在预计算导航数据时包括`GameObject`。
- `OffMeshLinkGeneration`：在预计算导航数据时，尝试生成从`GameObject`开始的非网格链接。
- `ReflectionProbeStatic`：当为`Type`属性为`Baked`的`Reflection Probe`预先计算数据时，包括 `GameObject`。

