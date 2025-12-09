- 命名空间：`System`
- 程序集：`System.Runtime.dll`
- 继承：`Object` > `ValueType`

`public struct ValueTuple`

`public struct ValueTuple<T1, ..., TRest>`

值元组是.NET Framework 4.7中引入的元组类型，用于在运行时实现C#中的元组。不同于元组类`Tuple`，`ValueTuple`是结构（值类型）而不是类（引用类型），其数据成员是字段而不是属性，且字段是可变的而非只读的。

`ValueTuple`表示没有元素的值元组，主要提供创建和比较值元组实例的静态方法。`ValueTuple<T1>`、...、`ValueTuple<T1, ..., TRest>`可表示具有不同数量元素的值元组。

## 构造函数

- `public ValueTuple(T1)`
- ...
- `public ValueTuple(T1, ..., T8)`

## 普通方法

### CompareTo

比较指定对象与本实例的大小。因为`ValueTuple`不含元素，因此两个`ValueTuple`相等。

- `public int CompareTo(ValueTuple)`

### Equals

- `public override bool Equals(Object)`
- `public bool Equals(ValueTuple)`

### GetHashCode

- `public override int32 GetHashCode()`

### ToString

- `public override string ToString()`

## 静态方法

### Create

创建值元组对象。

- `public static ValueTuple Create()`
- ...
- `public static ValueTuple<T1, ..., ValueTuple<T8>> Create<T1, ..., T8>(T1, T8)`