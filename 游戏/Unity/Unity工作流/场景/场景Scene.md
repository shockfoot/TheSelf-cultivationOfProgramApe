# 场景

场景是在Unity中处理内容的地方，包含游戏或应用的全部或部分的资产。可以在项目中创建任意数量的场景。

当创建一个新项目并第一次打开时，Unity会自动打开一个只包含相机和平行光的示例场景。

运行时，编辑器会显示一个名为DontDestroyOnLoad的场景，无法访问DontDestroyOnLoad。

# API概述

## SceneManager

在运行时管理场景。

属性：已加载场景数量`sceneCount`，所有场景个数`sceneCountInBuildingSettings`。

方法：

- 创建场景`CreateScene`。
- 获取场景`GetActiveScene`、`GetSceneAt`、`GetSceneByBuildIndex`、`GetSceneByName`、`GetSceneByPath`。
- 加载场景：
- - 同步`LoadScene`：会在下一帧加载场景，场景名不区分大小写，除非是从AB包中加载。名称匹配第一个，路径完全匹配。以`LoadSceneMode.Single`加载一个场景时，Unity会自动调用`Resources.UnloadUnusedAssets`。
- - 异步`LoadSceneAsync`：如上。
- 合并场景`MerageScene`。
- 卸载场景`UnloadSceneAsync`。

## Scene

提供场景的信息，如索引`buildIndex`、名字`name`、路径`path`、根物体信息`rootCount`、有效性`IsValid()`和状态（加载`isLoaded`、修改`isDirty`等）。

## CreateScemeParamet

## LocalPhysicsMode

枚举LocalPhysicsMode为场景场景时提供是否创建本地物理模式选项。

默认情况下，当创建或加载场景时，任何添加到场景中的游戏对象中的2D或3D物理组件都会添加到默认物理场景中。然而，每个场景都有能力创建并拥有自己的（本地）2D和/或3D物理场景。此选项可以在创建或加载场景时使用，以指定是否应该创建2D和/或3D物理场景。

当场景创建自己的2D和/或3D物理场景时，物理场景的生命周期与场景相同，也就是说，如果场景被销毁/卸载，那么任何创建的物理场景也是如此。