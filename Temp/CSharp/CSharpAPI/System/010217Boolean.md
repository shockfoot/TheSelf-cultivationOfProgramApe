- 命名空间：`System`
- 程序集：`System.Runtime.dll`
- 继承：`Object` > `ValueType`

`public readonly struct Boolean`

`Boolean`表示布尔值`true`或`false`，其字符串形式为“True”或“False”，二者仅由只读的`TrueString`和`FalseString`定义。由于`Boolean`只有两个值，因此可以使用条件运算符轻易的自定义其格式。

`Boolean`可以与除`Char`和`DateTime`以外的任意类型相互转化。从整数或浮点数到`Boolean`的所有转换都会将非零值转换为`true`，将零值转换为`false`；而从`Boolean`转换为数值时，将`true`转换为1，将`false`转换为0。`Boolean`可以转换为字符串，而只有字符串形式为“True”或“False”（不区分大小写和前、后空格）才能转化为`Boolean`。

`Boolean`最常用作标志，以表示某个条件，用于计算和逻辑。

## 字段

### TrueString

`Boolean`为`true`时的字符串，只读。

- `public static readonly string TrueString = "True"`

### FalseString

`Boolean`为`false`时的字符串，只读。

- `public static readonly string FalseString = "False"`

## 普通方法

### CompareTo

- `public int CompareTo(Boolean)`
- `public int CompareTo(Object)`

`true`大于`false`。返回结果小于0表示当前实例小于指定对象，反之表示当前实例大于指定对象或指定对象为`null`，返回结果等于0时表示二者相等。

### Equals

- `public override bool Equals(Object)`
- `public bool Equals(Boolean)`

### GetHashCode

- `public override int GetHashCode()`

### GetTypeCode

获取对象的类型枚举值。

- `public TypeCode GetTypeCode()`

### ToString

- `public override string ToString()`
- `public string ToString(IFormatProvider)`

### TryFormat

将对象的值格式化为字符串，返回结果指示转换成功还是失败。

- `public bool TryFormat(Span<Char>, out Int32)`

## 静态方法

### Parse

将字符串形式的布尔值转化为等效的`Boolean`类型。

- `public static bool Parse(String)`
- `public static bool Parse(ReadOnlySpan<Char>)`

### TryParse

将字符串转换为`Boolean`类型对象，返回结果指示转换成功还是失败。

- `public static bool TryParse(String, out Boolean)`
- `public static bool TryParse(ReadOnlySpan<Char>, out Boolean)`
