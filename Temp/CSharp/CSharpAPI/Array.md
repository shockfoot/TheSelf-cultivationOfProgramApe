### Array类

命名空间：`System`

程序集：System.Runtime.dll

继承：`object`

实现：`ICollection`、`IEnumerable`、`IList`、`IStructuralComparable`、`IStructuralEquatable`、`ICloneable`

`Array`类提供了一些方法，用于创建、处理、搜索数组并对数组进行排序，从而充当公共语言运行时中所有数组的基类。`Array`类不是命名空间的`System.Collections`一部分，但仍被视为集合，因为它基于`IList`接口。

数组的元素是一个值。数组长度是最大可包含的元素总数。数组的下限是其第一个元素的索引，可以是任意值，但默认从0开始。使用`CreateInstance`创建`Array`类实例时可以自定义下限。多维数组的每个维度可以具有不同的边界。数组最多可以拥有40亿个元素、32个维度，每个维度的最大索引为0X7FEFFFFF。

与命名空间`System.Collections`中的类不同，数组具有固定的容量。若要增加容量，必须创建具有所需容量的新`Array`对象，将元素从旧`Array`对象复制到新对象，然后删除旧`Array`对象。

对于.NET框架，数组最大占2GB。在64位运行环境，可以将`gcAllowVeryLargeObjects`的`enable`属性设置为`true`来避免大小限制。

一维数组可以实现`IList＜T＞`、`ICollection<T>`、`IEnumerable<T>`、`IReadOnlyList＜T＞`和`iRadonlyCollection＜T＞`泛型接口。这些实现在运行时提供给数组，因此，泛型接口不会出现在`Array`类的声明语法中。此外，对于仅通过将数组强制转换为泛型接口类型才能访问的接口成员，没有参考主题。当将数组强制转换到这些接口之一时，添加、插入或删除元素会导致NotSupportedException错误。

`Type`对象提供有关数组类型声明的信息。具有相同数组类型的数组对象共享同一`Type`对象。`Type.IsArray`和`Type.GetElementType`可能不会对数组返回预期的结果，因为如果数组被强制转换为`Array`类型，则结果是对象`object`，而不是数组。

`Array.Copy`方法不仅可以在相同类型的数组之间，也能在不同类型的标准数组之间复制元素；它会自动处理类型。

数组不保证是已排序的。在执行需要对数组进行排序的操作（如`BinarySearch`）之前，必须对其排序。

不支持在本机代码中使用指针的数组对象，会导致NotSupportedException错误。

#### 属性

##### IsFixedSize

获取一个`bool`值，指示该数组是否具有固定大小。对于所有数组，此属性值始终为`true`。具有此属性是因为`IList`接口需要。运算复杂度为O(1)。

##### IsReadOnly

获取一个`bool`值，指示该数组是否为只读。对于所有数组，此属性值始终`false`。具有此属性是因为`IList`接口需要。如果将数组强制转换为`IList`接口对象，则`IList.IsReadOnly`属性返回`false`。然而将数组强制转换为`IList<T>`接口对象，则`IsReadOnly`属性返回`true`。运算复杂度为O(1)。

##### IsSynchronized

获取一个`bool`值，指示对该数组的访问是否同步（线程安全）。对于所有数组，此属性值始终`false`。具有此属性是因为`ICollection`接口需要。运算复杂度为O(1)。

##### SyncRoot

获取一个`object`对象，用于同步访问`Array`。此属性实现`ICollection`接口。基于`Array`的类通过此属性实现自己的同步。同步代码必须在集合的`SyncRoot`属性上而不是直接在集合上执行操作。这确保了从其他对象派生的集合正确的执行操作，即它与可能同时修改集合的其他线程保持适当的同步。请注意，`SyncRoot`可能返回数组本身。运算复杂度为O(1)。

##### Length

获取一个`Int32`值，表示该数组中所有维数的总元素数。运算复杂度为O(1)。

##### LongLength

获取一个`Int64`值，表示该数组中所有维数的总元素数。运算复杂度为O(1)。

##### static MaxLength

获取一个`Int32`值，表示任何数组可包含的最大元素数。访问此属性值的运算复杂度为 O(1)。此属性值代表运行时允许的最大元素数，虽无法确保分配此长度的数组一定成功，但分配大于此长度的数组一定失败。此属性仅适用于单维零绑定（SZ）数组。多维数组的`Length`属性返回的值可能大于此属性的值。

##### Rank

获取一个`Int32`值，表示该数组的秩（维数）。需要注意的是，交错数组是一维数组，其`Rank`属性值为1。运算复杂度为O(1)。

#### 方法

##### CreateInstance

6个重载。创建指定索引（默认0）起始的、具有指定类型和维长的一/多维`Array`实例。引用类型数组初始化为`null`，值类型数组初始化为0。此方法不是构造函数。复杂度为O(n)，其中n为所有维长`lengths`的积。

- `Static CreateInstance(Type elementType, int length)`：索引从0开始、指定类型和长度的一维数组，其中长度为32位整数。
- `Static CreateInstance(Type elementType, params int[] lengths)`：索引从0开始、指定类型和维长的多维数组，其中维长由32位整数数组指定。
- `Static CreateInstance(Type elementType, params long[] lengths)`：索引从0开始、指定类型和维长的多维数组，其中维长由64位整数数组指定。
- `Static CreateInstance(Type elementType, int length1, int length2)`：索引从0开始、指定类型和维长的二维数组，其中维长由32位整数指定。
- `Static CreateInstance(Type elementType, int length1, int length2, int length3)`：索引从0开始、指定类型和维长的三维数组，其中维长由32位整数指定。
- `Static CreateInstance(Type elementType, int[] lengths, int[] lowerBounds)`：指定类型、维长和索引下限的多维数组，其中维长和索引下限均由32位整数数组指定。

##### Empty

`static Empty<T>()`可以返回一个空数组。

##### Clone

`Clone()`创建数组的浅表副本。数组的浅表副本（区别于深层副本）仅复制数组的元素（不管它们是引用类型还是值类型），而不是引用引用的对象。新数组的引用指向原数组引用所指向的对象。副本与原数组的类型相同。复杂度为O(n)，其中n为长度`Length`。



##### AsReadOnly

`static AsReadOnly<T>(T[] array)`返回指定数组`array`的只读集合`ReadOnlyCollection<T>`。当`array`为`null`时导致ArgumentNullException错误。此方法用于防止对数组进行任何修改。该集合仅阻止修改集合，但若对基础数据进行更改，只读集合将反映这些更改。复杂度为O(1)。



##### ConvertAll

`static ConvertAll<TInput,TOutput>(TInput[] array, Converter<TInput,TOutput> converter)`：将一种类型的数组转换为另一种类型的数组。`Converter<TInput,TOutput>`是将对象转换为目标类型的方法的委托。源数组的元素（保持不变）传入`Converter<TInput,TOutput>`，转换后的元素存入新数组。复杂度为O(n)，其中n为长度`Length`。



##### BinarySearch

8个重载。使用二进制搜索算法在一维升序数组中搜索值，即将`array`的每个元素与`value`实现`IComparable`接口（即使`value`为`null`），返回一个`Int32`值，表示`value`在`array`中的索引。如果找不到`value`，返回一个负数，其中若`value`小于`array`中的一个或多个元素，则返回的负数是大于`value`的第一个元素的索引的按位求补；若`value`大于`array`中的所有元素，则返回的负数是最后一个元素的索引加1的按位求补。此方法不支持搜索包含负索引的数组。如果使用未增值排序的数组调用此方法，返回值则可能不正确并且可能会返回负数，即使数组中存在`value`。如果`array`存在多个`value`，该方法只返回其中一个（不一定是第一个）匹配项的索引。如果`value`不能实现`IComparable`接口，则不会与数组元素进行`IComparable`接口测试，若在搜索中遇到数组中无法实现`IComparable`接口的元素，则报错。复杂度为O(log n)，其中n为长度`Length`。

- `static BinarySearch(Array array, object? value)`：实现`IComparable`接口。

- `static BinarySearch(Array array, object? value, IComparer? comparer)`：使用指定`IComparer`接口。
- `static BinarySearch(Array array, int index, int length, object? value)`：指定数组搜索范围并实现`IComparable`接口。
- `static BinarySearch(Array array, int index, int length, object? value, IComparer? comparer)`：指定数组搜索范围并使用指定`IComparer`接口。
- `static BinarySearch<T>(T[] array, T value)`：实现`IComparable<T>`泛型接口。
- `static BinarySearch<T>(T[] array, T value, IComparer<T>? comparer)`：使用指定`IComparer<T>`泛型接口。
- `static BinarySearch<T>(T[] array, int index, int length, T value)`：指定数组搜索范围并实现`IComparable<T>`泛型接口。
- `static BinarySearch<T>(T[] array, int index, int length, T value, IComparer<T>? comparer)`：指定数组搜索范围并使用指定`IComparer<T>`泛型接口。







##### Clear

2个重载。将数组中的每个元素重置为元素类型的默认值。多维数组的重置范围会跨行。此方法仅重置元素的值，而不删除元素本身。复杂度为O(n)，其中n为长度`Length`。

- `static Clear(Array array)`：将数组中全部元素设为默认值。

- `static Clear(Array array, int index, int length)`：将数组指定范围内的元素设为默认值。



##### Copy

4个重载。将一个数组的部分元素复制到另一个数组中，并根据需要执行类型转换和装箱。`Copy`**不会**在执行操作之前验证数组类型的兼容性。`sourceArrat`和`destinationArray`必须具有相同的维数，且`destinationArray`已维度化并具有足够数量的元素来容纳复制的数据。在多维数组之间复制时，数组的行为类似于长一维数组，其中行（或列）在概念上以端到端方式布局。从引用类型数组复制到值类型数组时，将取消装箱并复制每个元素，反之将装箱每个元素，然后复制。当从引用类型或值类型数组复制到`object`数组时，将创建一个`object`用于保存每个值或引用，然后复制。如果`sourceArray`和`destinationArray`同时是引用类型数组或两个类型`object`数组，则执行浅表复制。复杂度为O(n)，其中n为长度`Length`。

- `static Copy(Array sourceArray, Array destinationArray, int length)`：复制第一个元素之后指定长度的元素，其中长度为32位整数。
- `static Copy(Array sourceArray, Array destinationArray, long length)`：复制第一个元素之后指定长度的元素，其中长度为64位整数。
- `static Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)`：复制指定范围的元素到指定位置，其中长度和索引为32位整数。
- `static Copy(Array sourceArray, long sourceIndex, Array destinationArray, long destinationIndex, long length)`：复制指定范围的元素到指定位置，其中长度和索引为64位整数。

##### ConstrainedCopy

`static ConstrainedCopy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)`将源数组中指定索引开始的一系列元素复制到目标数组中的指定位置，并保证在复制未成功完成的情况下撤消所有更改。`ConstrainedCopy`在执行任何操作之前会验证数组类型的兼容性，`sourceArrat`和`destinationArray`必须具有相同的维数，且`sourceArrat`类型必须派生自`destinationArray`或与其相同。在多维数组之间进行复制时，数组的行为类似于长一维数组，其中行（或列）在概念上是端到端排列的。如果`sourceArray`和`destinationArray`同时是引用类型数组或两个类型`object`数组，则执行浅表复制。浅表副本是一个新的数组，包含与原数组元素相同的引用而不会复制元素本身或元素引用的任何内容。复杂度为O(n)，其中n为长度`Length`。

##### CopyTo

2个重载。将当前数组的所有元素复制（浅表复制）到指定数组的指定位置。目标数组必须维度化并具有足够数量的元素容纳复制的元素。此方法支持`ICollection`接口，若不需要实现该接口，请使用`Copy`方法。复杂度为O(n)，其中n为长度`Length`。

- `CopyTo(Array destinationArray, int destinationIndex)`：索引为32位整数。
- `CopyTo(Array destinationArray, long destinationIndex)`：索引为64位整数。



