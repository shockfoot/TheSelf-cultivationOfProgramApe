# Behaviour

所属命名空间`UnityEngine`，继承自`Component`，实现于`UnityEngine.CoreModule`。

`Behaviour`是可以启用或禁用的组件。

## 属性

### enable

- `public bool enable`

启用的`Behaviour`可以被加载，而禁用的不能。该属性即Inspector面板上的启/禁用选框。

### isActiveAndEnable

- `public bool isActiveAndEnable`

报告`GameObject`及其关联的`Behaviour`是否处于激活和启用状态。如果`GameObject`已激活且其关联的`Behaviour`启用则返回`true`，否则返回`false`。
