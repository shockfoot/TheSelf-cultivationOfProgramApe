# Transform

所属命名空间`UnityEngine`，继承自`Component`，实现于`UnityEngine.CoreModule`。

`Transform`用来存储对象的位置、旋转和缩放。场景中的每个对象都有一个变换。每个变换都可以有一个父级（“Hierarchy”面板中显示的层级视图），让您能够分层应用位置、旋转和缩放。支持枚举器。

## 属性

### childCount

- `public int childCount`

父变换（不包括）具有的子项数。

### eulerAngles

- `public Vector3 eulerAngles`

以欧拉角表示的世界空间中的旋转（以度为单位）。可以通过设置此属性来设置四元数的旋转，并且可以通过读取此属性来读取欧拉角形式的值。使用此属性设置旋转时，虽然提供X、Y和Z旋转值描述旋转，但是这些值不存储在旋转中，而是将X、Y和Z值转换为四元数的内部格式。读取此属性时，Unity将四元数的内部旋转表示形式转换为欧拉角。因为可通过多种方式使用欧拉角表示任何给定旋转，所以读出的值可能与分配的值截然不同。如果尝试逐渐增加值以生成动画，则这种情况可能会导致混淆。

不要单独设置某个`eulerAngles`轴，这会导致偏差和不希望的旋转。在将它们设置为新值时，请一次性设置所有 轴。

在检视面板中查看`GameObject`的旋转时，可能会看到与此属性中存储的值不同的角度值，这是因为检视面板显示本地旋转。

欧拉角可以通过围绕各个轴执行三个单独的旋转来表示三维旋转。在 Unity 中，围绕Z轴、X轴和Y轴（按该顺序）执行这些旋转。

### forward

- `public Vector3 forward`

当前`Transform`在世界空间中的z轴，即正前方。

### hasChanged

- `public bool hasChanged`

自上次将标志设置为`false`以来，变换是否发生更改？对变换的更改可以是任何能够导致重新计算其矩阵的操作：对其位置、旋转或缩放的任意调整。注意，在设置该标志之前，能够更改变换的操作不会实际检查旧值和新值是否不同。因此，设置`transform.position`等将始终在变换上设置`hasChanged`，而不管是否有任何实际更改。

### hierarchyCapacity

- `public int hierarchyCapacity`

变换的层级视图数据结构的变换容量。Unity内部使用自己的打包数据结构表示每个变换的层级视图，即一个根及其所有深层子项。当其中的变换数量超过其容量时，将调整该数据结构的大小。将容量设置为略大于最大预期大小的值可减少内存使用量，并提高超大层级视图的`Transform.SetParent`和`Object.Destroy`的性能。