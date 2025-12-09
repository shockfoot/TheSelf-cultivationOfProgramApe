- 命名空间：`UnityEngine`

位掩码，用于控制`Object`的销毁、保存和在Inspector中的可见性。

## 值

- `None`：普通可见的`Object`，默认值。
- `HideInHierarchy`：在Hierarchy中隐藏`Object`。
- `HideInInspector`：在Inspector中隐藏`Object`。
- `DontSaveInEditor`：`Object`不会保存到Editor中的场景。
- `NotEditable`：`Object`在Inspector中无法编辑。
- `DontSaveInBulid`：构建游戏时不保存`Object`。
- `DontUnlodUnusedAsset`：`Resources.UnloadUnusedAssets`不会卸载`Object`。必须使用`DestroyImmediate`从内存中手动清除`Object`以避免内存泄漏。
- `DontSave`：`Object`不保存到场景。加载新场景时，也不会销毁它，相当于`HideFlags.DontSaveInBuild | HideFlags.DontSaveInEditor | HideFlags.DontUnloadUnusedAsset`。必须使用 `DestroyImmediate`从内存中手动清除`Object`以避免内存泄漏。
- `HideAndDontSave`：`GameObject`不会在Hierarchy中显示，不会保存到场景中，也不会由`Resources.UnloadUnusedAssets`卸载，相当于`HideFlags.HideInHierarchy | HideFlags.DontSaveInEditor | HideFlags.DontUnloadUnusedAsset`。通常用于由脚本创建并且完全由脚本控制的游戏对象。