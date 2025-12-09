- 命名空间：`System`
- 程序集：`System.Runtime.dll`
- 继承：`Object`

`public static class Tuple`

`public class Tuple<T1, ..., TRest>`

`public static class TupleExtensions`

元组是具有特定数量和序列的值的数据结构。`Tuple`类本身并不表示元组，只是提供了用于创建八个及以下数量元素的元组对象的静态方法。.NET支持具有一至七个元素的元组`Tuple<T1>`、...、`Tuple<T1, ..., T7>`，八个及以上的元组则是`Tuple<T1, ..., TRest>`。`TupleExtensions`类为元组提供拓展方法。

元组通常有四种使用方式：

- 表示一组数据；
- 提供对数据集的轻松访问和操作；
- 不使用输出参数即可从方法中返回多个结果；
- 通过一个参数传递给方法多个值。

## 构造函数

- `public Tuple()`

## 普通方法

### Equals

- `public override bool Equals(Object)`

### GetHashCode

- `public override int GetHashCode()`

### ToString

- `public override string ToString()`

## 静态方法

### Create

创建`Tuple`对象。

- `public static Tuple<T1> Create<T1>(T1)`
- ...
- `public static Tuple<T1, ...,Tuple<T8>> Create<T1, ...,T8>(T1, ...,T8)`

## 扩展方法

### Deconstruct

将元组的组件分解为单独的变量。

- `public static void Deconstruct<T1>(this Tuple<T1>, T1)`
- ...
- `public static void Deconstruct<T1, ..., T21>(this Tuple<T1, ..., Tuple<T8, ..., Tuple<T15, ..., T21>>>, T1, ..., T21)`

### ToTuple

将值元组转化为元组。

- `public static Tuple<T1> ToTuple(this ValueTuple<T1>)`
- ...
- `public static Tuple<T1, ..., Tuple<T8, ..., Tuple<T15, ..., T21>>> ToTuple(this ValueTuple<T1, ..., ValueTuple<T8, ..., ValueTuple<T15, ..., T21>>>)`

### ToValueTuple

将元组转化为值元组

- `public static ValueTuple<T1> ToValueTuple<T1>(this Tuple<T1>)`
- ...
- `public static ValueTuple<T1, ..., ValueTuple<T8, ..., ValueTuple<T15, ..., T21>>> ToValueTuple(this Tuple<T1, ..., Tuple<T8, ..., Tuple<T15, ..., T21>>>)`
