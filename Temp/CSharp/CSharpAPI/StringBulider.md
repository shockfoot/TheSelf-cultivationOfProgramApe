# StringBuilder

命名空间：`System.Text`

程序集：System.Runtime.dll

继承：`object`

实现：`ISerializable`

`StringBuilder`用来表示可变字符串，在创建类的实例后，可以通过追加、删除、替换或插入字符来对其进行修改。此类不能被继承。

尽管`StringBuilder`和`String`都表示字符序列，但实现方式不同。`String`是一种不可变类型。每个修改`String`的操作实际上都创建了一个新字符串。对于执行大量重复修改字符串的操作可能会严重降低性能。当然，当代码对字符串所做的更改数较小、只执行固定数量的串联操作或执行大量搜索操作时，`StringBuilder`提升的性能可忽略不计或者没有性能提升。

`StringBuilder`对象维护缓冲区以容纳对字符串的扩展。如果添加的字符数导致对象的长度超过其当前容量，则分配新内存，将容量增加一倍，并将原始缓冲区的数据复制到新缓冲区，然后将新数据追加到新缓冲区，并调整其容量与长度。当`StringBuilder`对象达到最大容量时，将无法为其分配更多内存，此时尝试添加字符或将其扩展到最大容量之外会引发异常。

`StringBuilder`对象默认容量为16个字符，最大容量为`int32.MaxValue`。

## 构造函数

- `public StringBuilder ()`：容量为默认容量。
- `public StringBuilder (int capacity)`：指定容量。
- `public StringBuilder (string? value)`：指定初始值。
- `public StringBuilder (int capacity, int maxCapacity)`：指定容量与最大容量。
- `public StringBuilder (string? value, int capacity)`：指定初始值和容量。
- `public StringBuilder (string? value, int startIndex, int length, int capacity)`：初始化`value`中指定的子串并指定容量。

按要求初始化`StringBuilder`实例。

## 属性

### Length

- `public int Length { get; set; }`

获取或设置当前实例的长度。如果指定的长度小于当前长度，则当前对象将被截断为指定长度。如果指定的长度大于当前长度，则当前对象的字符串值的结尾将用空字符填充。

### Capacity

- `public int Capacity { get; set; }`

获取或设置当前实例内存中所分配的可容纳的最大字符数。

### MaxCapacity

- `public int MaxCapacity { get; }`

获取此实例的最大容量，默认值为`int32.MaxValue`，可以调用构造函数来显式设置对象的最大容量。

### this[index]

- `public char this[int index] { get; set; }`

获取或设置此实例中指定字符位置处的字符。因为每次字符访问都会遍历区块的整个链接列表以查找要索引到的正确缓冲区，所以当`StringBuilder`对象很大时会严重影响性能受到，尤其在循环遍历时。此时，将`StringBuilder`对象转换为`String`或更小的`StringBuilder`可以节省性能。

## 普通方法

### CopyTo

- `public void CopyTo (int sourceIndex, char[] destination, int destinationIndex, int count)`
- `public void CopyTo (int sourceIndex, Span<char> destination, int count)`

将此实例的指定字符复制到目标处。

### EnsureCapacity

- `public int EnsureCapacity (int capacity)`

确保此实例的容量至少是指定值。如果当前容量小于`capacity`参数，则将此实例的内存重新分配为至少容纳`capacity`数量的字符，否则，不会更改任何内存。

### Clear

- `public System.Text.StringBuilder Clear ()`

移除当前实例中所有字符，其`Length`为0。

### Append

- `public System.Text.StringBuilder Append (sbyte value)`：追加8位有符号整数，不符合CLS。
- `public System.Text.StringBuilder Append (byte value)`：追加8位无符号整数。
- `public System.Text.StringBuilder Append (short value)`：追加16位有符号整数。
- `public System.Text.StringBuilder Append (ushort value)`：追加16位无符号整数，不符合CLS。
- `public System.Text.StringBuilder Append (int value)`：追加32位有符号整数。
- `public System.Text.StringBuilder Append (uint value)`：追加32位无符号整数，不符合CLS。
- `public System.Text.StringBuilder Append (long value)`：追加64位有符号整数。
- `public System.Text.StringBuilder Append (ulong value)`：追加64位无符号整数，不符合CLS。
- `public System.Text.StringBuilder Append (float value)`：追加单精度浮点数。
- `public System.Text.StringBuilder Append (double value)`：追加双精度浮点数。
- `public System.Text.StringBuilder Append (decimal value)`：追加十进制数。
- `public System.Text.StringBuilder Append (bool value)`：追加布尔值。
- `public System.Text.StringBuilder Append (char value)`：追加`value`。
- `public System.Text.StringBuilder Append (char value, int repeatCount)`：追加`repeatCount`次`value`。
- `public System.Text.StringBuilder Append (ReadOnlySpan<char> value)`：追加`value`。
- `public System.Text.StringBuilder Append (ReadOnlyMemory<char> value)`：追加`value`。
- `public System.Text.StringBuilder Append (char[]? value)`：追加`value`。
- `public System.Text.StringBuilder Append (char[]? value, int startIndex, int charCount)`：追加`value`中指定字符。
- `public System.Text.StringBuilder Append (char* value, int valueCount)`：追加`value`中指定字符，不符合CLS。
- `public System.Text.StringBuilder Append (string? value)`：追加`value`。
- `public System.Text.StringBuilder Append (string? value, int startIndex, int count)`：追加`value`中指定字符。
- `public System.Text.StringBuilder Append (ref System.Text.StringBuilder.AppendInterpolatedStringHandler handler)`：追加指定内插字符串。
- `public System.Text.StringBuilder Append (IFormatProvider? provider, ref System.Text.StringBuilder.AppendInterpolatedStringHandler handler)`：追加使用指定格式指定内插字符串。
- `public System.Text.StringBuilder Append (System.Text.StringBuilder? value)`：追加`value`。
- `public System.Text.StringBuilder Append (System.Text.StringBuilder? value, int startIndex, int count)`：追加`value`中指定字符。
- `public System.Text.StringBuilder Append (object? value)`：追加`value`。

向此实例追加指定对象的字符串表示形式。此方法返回修改后的现有实例，而不是新的实例。

### AppendFormat

- `public System.Text.StringBuilder AppendFormat (string format, object? arg0)`
- `public System.Text.StringBuilder AppendFormat (IFormatProvider? provider, string format, object? arg0)`
- `public System.Text.StringBuilder AppendFormat (string format, object? arg0, object? arg1)`
- `public System.Text.StringBuilder AppendFormat (IFormatProvider? provider, string format, object? arg0, object? arg1)`
- `public System.Text.StringBuilder AppendFormat (string format, object? arg0, object? arg1, object? arg2)`
- `public System.Text.StringBuilder AppendFormat (IFormatProvider? provider, string format, object? arg0, object? arg1, object? arg2)`
- `public System.Text.StringBuilder AppendFormat (string format, params object?[] args)`
- `public System.Text.StringBuilder AppendFormat (IFormatProvider? provider, string format, params object?[] args)`

向此实例追加指定对象的复合格式处理后的字符串表示形式。

### AppendJoin

- `public System.Text.StringBuilder AppendJoin (char separator, params string?[] values)`
- `public System.Text.StringBuilder AppendJoin (char separator, params object?[] values)`
- `public System.Text.StringBuilder AppendJoin (string? separator, params string?[] values)`
- `public System.Text.StringBuilder AppendJoin (string? separator, params object?[] values)`
- `public System.Text.StringBuilder AppendJoin<T> (char separator, System.Collections.Generic.IEnumerable<T> values)`
- `public System.Text.StringBuilder AppendJoin<T> (string? separator, System.Collections.Generic.IEnumerable<T> values)`

向此实例追加由分隔符连接的集合成员的字符串表示形式。

### AppendLine

- `public System.Text.StringBuilder AppendLine ()`：只追加行终止符。
- `public System.Text.StringBuilder AppendLine (string? value)`
- `public System.Text.StringBuilder AppendLine (ref System.Text.StringBuilder.AppendInterpolatedStringHandler handler)`：内插字符串。
- `public System.Text.StringBuilder AppendLine (IFormatProvider? provider, ref System.Text.StringBuilder.AppendInterpolatedStringHandler handler)`：按指定格式追加内插字符串。

向此实例追加指定字符串后再追加默认的行终止符。

### Insert

- `public System.Text.StringBuilder Insert (int index, sbyte value)`：在指定位置插入8位带符号整数，不符合CLS。
- `public System.Text.StringBuilder Insert (int index, byte value)`：在指定位置插入8位无符号整数。
- `public System.Text.StringBuilder Insert (int index, short value)`：在指定位置插入16位带符号整数。
- `public System.Text.StringBuilder Insert (int index, ushort value)`：在指定位置插入16位无符号整数，不符合CLS。
- `public System.Text.StringBuilder Insert (int index, int value)`：在指定位置插入32位带符号整数。
- `public System.Text.StringBuilder Insert (int index, uint value)`：在指定位置插入32位无符号整数，不符合CLS。
- `public System.Text.StringBuilder Insert (int index, long value)`：在指定位置插入64位带符号整数。
- `public System.Text.StringBuilder Insert (int index, ulong value)`：在指定位置插入64位无符号整数，不符合CLS。
- `public System.Text.StringBuilder Insert (int index, float value)`：在指定位置插入单精度浮点数。
- `public System.Text.StringBuilder Insert (int index, double value)`：在指定位置插入双精度浮点数。
- `public System.Text.StringBuilder Insert (int index, decimal value)`：在指定位置插入十进制数。
- `public System.Text.StringBuilder Insert (int index, bool value)`：在指定位置插入布尔值。
- `public System.Text.StringBuilder Insert (int index, char value)`：在指定位置插入字符。
- `public System.Text.StringBuilder Insert (int index, char[]? value)`：在指定位置插入字符数组。
- `public System.Text.StringBuilder Insert (int index, char[]? value, int startIndex, int charCount)`：在指定位置插入数组的指定字符。
- `public System.Text.StringBuilder Insert (int index, string? value)`：在指定位置插入`value`。
- `public System.Text.StringBuilder Insert (int index, string? value, int count)`：在指定位置插入`count`次`value`。
- `public System.Text.StringBuilder Insert (int index, ReadOnlySpan<char> value)`：在指定位置插入字符序列。
- `public System.Text.StringBuilder Insert (int index, object? value)`：在指定位置插入`value`。

将指定对象的字符串表示形式插入到此实例中的指定字符位置。

### Remove

- `public System.Text.StringBuilder Remove (int startIndex, int length)`

将指定范围的字符从此实例中移除。将`startIndex + length`的字符移动到`startIndex`，并缩短当前实例的字符串`length`。当前实例的容量不受影响。

### Replace

- `public System.Text.StringBuilder Replace (char oldChar, char newChar)`
- `public System.Text.StringBuilder Replace (char oldChar, char newChar, int startIndex, int count)`：指定此实例中替换的位置。
- `public System.Text.StringBuilder Replace (string oldValue, string? newValue)`
- `public System.Text.StringBuilder Replace (string oldValue, string? newValue, int startIndex, int count)`：指定此实例中替换的位置。

将此实例中出现的所有指定字符或字符串替换为其他的指定字符或字符串。此方法执行序号、区分大小写的比较。

### GetChunks

- `public System.Text.StringBuilder.ChunkEnumerator GetChunks ()`

返回一个对象，该对象可用于循环访问此实例创建的`ReadOnlyMemory<Char>`中表示的字符区块。

## 静态方法

