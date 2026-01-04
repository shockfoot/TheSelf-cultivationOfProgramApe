- 命名空间：`UnityEditor`

描述哪些Unity系统将游戏对象视为静态，并将游戏对象包含在Unity编辑器中的预计算中。

## 值

- `ContributeGI`：在全局照明计算中包括此目标网格渲染器。
- `OccluderStatic`：在遮挡剔除系统中将此目标标记为静态遮挡物。
- `OccludeeStatic`：在遮挡剔除系统中将此目标标记为静态被遮挡物。
- `BatchingStatic`：将此目标的网格与其他符合条件的网格相结合，有可能降低运行时渲染成本。
- `NavigationStatic`：在预计算导航数据时包括此目标。
- `OffMeshLinkGeneration`：在预计算导航数据时，尝试生成从此目标开始的脱离网格链接。
- `ReflectionProbeStatic`：预计算`Type`为`Baked`的反射探测器的数据时，请包含此目标。