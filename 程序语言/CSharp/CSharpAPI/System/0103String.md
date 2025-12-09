- 命名空间：`System`
- 程序集：`System.Runtime.dll`
- 继承：`Object`

`public sealed class String`

`String`用于表示文本字符的有序集合，其对象是`Char`的有序集合。其值是不可变，即只读的。内存中`String`的最大大小为2GB，约10亿个字符。

`String`可以通过以下方式实例化：

- 直接分配字符串文本；
- 调用构造函数；
- 使用字符串串联运算符`&`或`+`；
- 通过检索属性或调用返回字符串的方法；
- 调用格式设置方法，将值或对象转换为其字符串表示形式。

可以通过`String`中`this[index]`索引器访问特定位置的`Char`对象。索引是从从字符串中的第一个位置即零开始的非负数字。由于`String`类实现`IEnumerable`接口，因此还可以使用`foreach`循环访问字符串中的`Char`对象。

已声明但尚未赋值的字符串为`null`。不同于空字符串`""`。

`String`对象是不可变（只读）的。 用于修改`String`对象的方法实际上会返回一个新`String`对象。由于字符串是不可变的，因此，对显示为单个字符串的内容执行重复添加或删除操作的字符串操作可能会显著降低性能。

`String`的成员对`String`对象执行顺序或区域性敏感（语言）操作。顺序操作作用于每个`Char`对象的数值。区域性敏感操作作用于String对象的值，并考虑区域性特定的大小写、排序、格式化和解析规则。

字符串的比较和排序因文化而异。排序还取决于语言和文化使用字母表的顺序。比较可以区分大小写或不区分大小写，大小写规则可能因文化而异。顺序比较在比较和排序字符串时使用字符串中单个字符的Unicode码点。排序规则决定Unicode字符的字母顺序以及两个字符串之间的比较。

## 构造函数

字符串构造函数分为两类：不带指针参数的类，以及带有指针参数的构造函数。使用指针的构造函数不符合CLS。C#要求使用指针的代码在不安全的上下文中运行。

- `String(SByte*)`
- `String(SByte*, Int32, Int32)`
- `String(SByte*, Int32, Int32, Encoding)`
- `String(Char, Int32)`
- `String(Char[])`
- `String(Char[], Int32, Int32)`
- `String(Char*)`
- `String(Char*, Int32, Int32)`
- `String(ReadOnlySpan<Char>)`

## 字段

### Empty

- `public static readonly string Empty`

长度为零的`""`字符串。此字段最常用于赋值，以将字符串变量初始化为空字符串。

## 属性

### Length

获取当前`String`对象中的字符数。在.NET中，可以在字符串中嵌入空字符。当字符串包含一个或多个空字符时，这些字符将包含在总字符串的长度中。

- `public int Length { get; }`

### this[index]

获取当前`String`对象中位于指定位置的`Char`对象。`index`参数是从零开始的。

- `public char this[int index] { get; }`

## 普通方法

### Clone

创建一个指向当前实例的引用。

- `public object Clone()`

### CampareTo

- `public int CompareTo(String)`
- `public int CompareTo(Object)`

### Contains

判断字符串中是否出现指定子字符串。如果指定值为空`''`或`""`或出现在此字符串中，返回`true`，否则返回`false`。此方法执行区分大小写 (不区分区域性的序号) 比较。

- `public bool Contains(Char)`
- `public bool Contains(Char, StringComparison)`
- `public bool Contains(String)`
- `public bool Contains(String, StringComparison)`

### CopyTo

复制指定的字符到目标数组。

- `public void CopyTo(Int32, Char[], Int32, Int32)`
- `public void CopyTo(Span<Char>)`

### TryCopyTo

将此字符串的内容复制到目标范围中。如果复制成功，则返回`true`，否则返回`false`。

- `public bool TryCopyTo(Span<Char>)`

### StartsWith

此字符串实例的开头是否与指定值匹配。此方法执行区分大小写和不区分区域性的比较。

- `public bool StartsWith(Char)`
- `public bool StartsWith(String)`
- `public bool StartsWith(String, StringComparison)`
- `public bool StartsWith(String, Boolean, CultureInfo)`

### EndsWith

确定此字符串实例的结尾是否与指定值匹配。此方法执行区分大小写和不区分区域性的比较。

- `public bool EndsWith(Char)`
- `public bool EndsWith(String)`
- `public bool EndsWith(String, StringComparison)`
- `public bool EndsWith(String, Boolean, CultureInfo)`

### EnumerateRunes

从此字符串返回`Rune`的枚举。

- `public System.Text.StringRuneEnumerator EnumerateRunes()`

### GetEnumerator

返回一个可以循环访问此字符串中的每个字符的枚举器对象。

- `public CharEnumerator GetEnumerator()`

### Equals

- `public bool Equals(String)`
- `public bool Equals(String, StringComparison)`
- `public override bool Equals(Object)`

### GetHashCode

- `public int GetHashCode(StringComparison)`
- `public override int GetHashCode()`







### IndexOf

- `public int IndexOf (char value)`：获取`value`在此实例中第一个匹配项开始的索引。
- `public int IndexOf (char value, StringComparison comparisonType)`：获取`value`在此实例中第一个匹配项开始的索引。参数指定搜索规则。
- `public int IndexOf (char value, int startIndex)`：获取`value`在此实例中第一个匹配项开始的索引。参数指定起始搜索位置。
- `public int IndexOf (char value, int startIndex, int count)`：获取`value`在此实例中第一个匹配项开始的索引。参数指定起始搜索位置、搜索字符数量。
- `public int IndexOf (string value)`：获取`value`在此实例中第一个匹配项开始的索引。
- `public int IndexOf (string value, StringComparison comparisonType)`：获取`value`在此实例中第一个匹配项开始的索引。参数指定搜索规则。
- `public int IndexOf (string value, int startIndex)`：获取`value`在此实例中第一个匹配项开始的索引。参数指定起始搜索位置。
- `public int IndexOf (string value, int startIndex, StringComparison comparisonType)`：获取`value`在此实例中第一个匹配项开始的索引。参数指定起始搜索位置以及搜索规则。
- `public int IndexOf (string value, int startIndex, int count)`：获取`value`在此实例中第一个匹配项开始的索引。参数指定起始搜索位置、搜索字符数量。
- `public int IndexOf (string value, int startIndex, int count, StringComparison comparisonType)`：获取`value`在此实例中第一个匹配项开始的索引。参数指定起始搜索位置、搜索字符数量以及搜索规则。

获取指定Unicode字符或字符串在此实例中的第一个匹配项开始的索引（从零开始）。如果未在此实例中找到该字符或字符串，则返回 -1。

### IndexOfAny

- `public int IndexOfAny (char[] anyOf)`：获取`anyOf`中任意字符在此实例中第一个匹配项开始的索引。
- `public int IndexOfAny (char[] anyOf, int startIndex)`：获取`anyOf`中任意字符在此实例中第一个匹配项开始的索引。参数指定起始搜索位置。
- `public int IndexOfAny (char[] anyOf, int startIndex, int count)`：获取`anyOf`中任意字符在此实例中第一个匹配项开始的索引。参数指定起始搜索位置、搜索字符数量。

获取在此实例中第一次找到Unicode字符数组中的任意字符的索引位置（从零开始）；如果未找到，则返回 -1。此方法搜索区分大小写。如果Unicode字符数组为空数组，该方法将在字符串的开头找到匹配项（即索引零）。

### LastIndexOf

- `public int LastIndexOf (char value)`：获取`value`在此实例中最后一个匹配项开始的索引。
- `public int LastIndexOf (char value, int startIndex)`：获取`value`在此实例中最后一个匹配项开始的索引。参数指定起始搜索位置。
- `public int LastIndexOf (char value, int startIndex, int count)`：获取`value`在此实例中最后一个匹配项开始的索引。参数指定起始搜索位置、搜索字符数量。
- `public int LastIndexOf (string value)`：获取`value`在此实例中最后一个匹配项开始的索引。
- `public int LastIndexOf (string value, StringComparison comparisonType)`：获取`value`在此实例中最后一个匹配项开始的索引。参数指定搜索规则。
- `public int LastIndexOf (string value, int startIndex)`：获取`value`在此实例中最后一个匹配项开始的索引。参数指定起始搜索位置。
- `public int LastIndexOf (string value, int startIndex, StringComparison comparisonType)`：获取`value`在此实例中最后一个匹配项开始的索引。参数指定起始搜索位置以及搜索规则。
- `public int LastIndexOf (string value, int startIndex, int count)`：获取`value`在此实例中最后一个匹配项开始的索引。参数指定起始搜索位置、搜索字符数量。
- `public int LastIndexOf (string value, int startIndex, int count, StringComparison comparisonType)`：获取`value`在此实例中最后一个匹配项开始的索引。参数指定起始搜索位置、搜索字符数量以及搜索规则。

获取指定Unicode字符或字符串在此实例中的最后一个匹配项开始的索引（从零开始）。如果未在此实例中找到该字符或字符串，则返回 -1。

### LastIndexOfAny

- `public int LastIndexOfAny (char[] anyOf)`：获取`anyOf`中任意字符在此实例中最后一个匹配项开始的索引。
- `public int LastIndexOfAny (char[] anyOf, int startIndex)`：获取`anyOf`中任意字符在此实例中最后一个匹配项开始的索引。参数指定起始搜索位置。
- `public int LastIndexOfAny (char[] anyOf, int startIndex, int count)`：获取`anyOf`中任意字符在此实例中最后一个匹配项开始的索引。参数指定起始搜索位置、搜索字符数量。

获取在此实例中最后一次找到Unicode字符数组中的任意字符的索引位置（从零开始）；如果未找到，则返回 -1。此方法搜索区分大小写。如果Unicode字符数组为空数组，该方法将在字符串的开头找到匹配项（即索引零）。



### Insert

- `public string Insert (int startIndex, string value)`

返回在此实例中的指定的索引位置插入指定的字符串后的字符串。如果`startIndex`等于此实例的长度，`value`则将追加到此实例的末尾。

### Normalize

- `public string Normalize ()`：返回一个新字符串，其文本值与此字符串相同，但其二进制表示形式符合Unicode范式C。
- `public string Normalize (System.Text.NormalizationForm normalizationForm)`：返回一个新字符串，其文本值与此字符串相同，但其二进制表示形式符合指定的Unicode范式。

返回一个新字符串，其二进制表示形式符合特定的Unicode范式。如果该字符串包含无效字符，则报错。

### IsNormalized

- `public bool IsNormalized ()`：指示此字符串是否符合范式C。
- `public bool IsNormalized (System.Text.NormalizationForm normalizationForm)`：指示此字符串是否符合由`normalizationForm`参数指定的范式。

如果此字符串符合特定的Unicode范式，则返回`true`，否则返回`false`。此方法会在遇到字符串中第一个非规范化字符时立即返回`false`；在非规范化字符前遇到无效Unicode字符时会报错。

某些Unicode字符具有多个等效的二进制表示形式，其中包含组合和/或复合Unicode字符的集合。单个字符存在多个表示形式会使搜索、排序、匹配和其他操作复杂化。Unicode标准定义了一个名为规范化的进程，该进程在给定任何等效的二进制表示形式的字符时返回一个二进制表示形式。可以通过多个算法（称为标准化形式）来执行规范化，它们遵循不同的规则。

### PadLeft

- `public string PadLeft (int totalWidth)`：在此实例字符左侧填充空格达到指定总长度，实现右对齐。
- `public string PadLeft (int totalWidth, char paddingChar)`：在此实例字符左侧填充指定字符。

返回一个指定长度的新字符串，其中在当前字符串的开头填充空格或指定的Unicode字符。

### PadRight

- `public string PadRight (int totalWidth)`：在此实例字符结尾填充空格达到指定总长度，实现右对齐。
- `public string PadRight (int totalWidth, char paddingChar)`：在此实例字符结尾填充指定字符。

返回在当前字符串的结尾填充空格或指定Unicode字符到达指定长度后的新字符串。

### Remove

- `public string Remove (int startIndex)`：删除指定位置后所有的字符。
- `public string Remove (int startIndex, int count)`：在指定位置删除指定数量的字符。

返回从当前字符串删除了指定数量的字符的新字符串。

### Replace

- `public string Replace (char oldChar, char newChar)`：将`oldChar`替换为`newChar`。
- `public string Replace (string oldValue, string? newValue)`：将`oldValue`替换为`newValue`。
- `public string Replace (string oldValue, string? newValue, StringComparison comparisonType)`：将`oldValue`按提供的比较类型替换为`newValue`。
- `public string Replace (string oldValue, string? newValue, bool ignoreCase, System.Globalization.CultureInfo? culture)`：将`oldValue`按提供的区域性和区分大小写替换为`newValue`。

返回已将当前字符串中指定Unicode字符或`String`的所有匹配项替换为其他指定字符或`String`后的新字符串。此方法执行区分大小写（不区分区域性的序号）。

### ReplaceLineEndings

- `public string ReplaceLineEndings ()`：将所有换行序列替换为`NewLine`。
- `public string ReplaceLineEndings (string replacementText)`：将所有换行序列替换为`replacementText`。

返回已将当前字符串中换行字符替换为指定字符的新字符串。

### SubString

- `public string Substring (int startIndex)`：指定位置开始并一直到末尾的子字符串。
- `public string Substring (int startIndex, int length)`：指定位置开始指定长度的子字符串。

获取当前字符串中的子字符串。

### ToLower

- `public string ToLower ()`：此方法将考虑当前区域性的大小写规则。
- `public string ToLower (System.Globalization.CultureInfo? culture)`：根据指定区域性的大小写规则转换。

返回当前字符串小写形式的副本。此方法不会修改当前实例的值，它会返回一个新字符串。

### ToLowerInvariant

- `public string ToLowerInvariant ()`

使用固定区域性的大小写规则返回当前字符串小写形式的副本。固定区域性表示不区分区域性。此方法不会修改当前实例的值，它会返回一个新字符串。

### ToUpper

- `public string ToUpper ()`：此方法将考虑当前区域性的大小写规则。
- `public string ToUpper (System.Globalization.CultureInfo? culture)`：根据指定区域性的大小写规则转换。

返回当前字符串大写形式的副本。此方法不会修改当前实例的值，它会返回一个新字符串。

### ToUpperInvariant

- `public string ToUpperInvariant ()`

使用固定区域性的大小写规则返回当前字符串大写形式的副本。固定区域性表示不区分区域性。此方法不会修改当前实例的值，它会返回一个新字符串。

### Trim

- `public string Trim ()`：删除当前字符串开头和结尾的空白。
- `public string Trim (char trimChars)`：删除当前字符串开头和结尾的`trimChars`。
- `public string Trim (params char[]? trimChars)`：删除当前字符串开头和结尾的`trimChars`中出现的字符。

返回从当前字符串开头和结尾删除字符数组中出现的字符后的新字符串。只有当遇到字符数组中不存在的字符时，此操作才停止。如果从当前实例无法删除字符，则返回未更改的当前实例。

### TrimStart

- `public string TrimStart ()`：删除当前字符串开头的空白。
- `public string TrimStart (char trimChars)`：删除当前字符串开头的`trimChars`。
- `public string TrimStart (params char[]? trimChars)`：删除当前字符串开头的`trimChars`中出现的字符。

返回从当前字符串开头删除字符数组中出现的字符后的新字符串。只有当遇到字符数组中不存在的字符时，此操作才停止。如果从当前实例无法删除字符，则返回未更改的当前实例。

### TrimEnd

- `public string TrimEnd ()`：删除当前字符串结尾的空白。
- `public string TrimEnd (char trimChars)`：删除当前字符串结尾的`trimChars`。
- `public string TrimEnd (params char[]? trimChars)`：删除当前字符串结尾的`trimChars`中出现的字符。

返回从当前字符串结尾删除字符数组中出现的字符后的新字符串。只有当遇到字符数组中不存在的字符时，此操作才停止。如果从当前实例无法删除字符，则返回未更改的当前实例。

### ToCharArray

- `public char[] ToCharArray ()`：将当前字符串转化为字符数组。
- `public char[] ToCharArray (int startIndex, int length)`：指定位置开始指定长度的子字符串转化为字符数组。

将当前字符串中特定字符转化为字符数组。

### Split

- `public string[] Split (char separator, StringSplitOptions options = System.StringSplitOptions.None)`：根据`separator`将字符串拆分为最大数量为`count`的子字符串。
- `public string[] Split (char separator, int count, StringSplitOptions options = System.StringSplitOptions.None)`：根据`separator`和（可选）选项将字符串拆分为最大数量为`count`的子字符串。
- `public string[] Split (char[]? separator)`：根据`separator`将字符串拆分为子字符串。
- `public string[] Split (char[]? separator, int count)`：根据`separator`将字符串拆分为最大数量为`count`的子字符串。
- `public string[] Split (char[]? separator, StringSplitOptions options)`：根据`separator`和（可选）选项将字符串拆分为子字符串。
- `public string[] Split (char[]? separator, int count, StringSplitOptions options)`：根据`separator`和（可选）选项将字符串拆分为最大数量为`count`的子字符串。
- `public string[] Split (string? separator, StringSplitOptions options = System.StringSplitOptions.None)`：根据`separator`和（可选）选项将字符串拆分为子字符串。
- `public string[] Split (string? separator, int count, StringSplitOptions options = System.StringSplitOptions.None)`：根据`separator`和（可选）选项将字符串拆分为最大数量为`count`的子字符串。
- `public string[] Split (string[]? separator, StringSplitOptions options)`：根据`separator`和（可选）选项将字符串拆分为子字符串。
- `public string[] Split (string[]? separator, int count, StringSplitOptions options)`：根据`separator`和（可选）选项将字符串拆分为最大数量为`count`的子字符串。

返回将当前字符串按指定要求分割后的字符串数组。分隔符不包含在返回数组的元素中。可选择忽略结果中的空子字符串。如果字符串已被拆分`count - 1`次，但尚未到达字符串的末尾，则返回数组中的最后一个字符串将包含此实例的剩余尾随子字符串。

### Clone

- `public object Clone ();`

返回现有的字符串实例（不是此实例的独立副本，所以很少需要直接调用它）。



## 静态方法

### Compare

比较两个指定的字符串对象，并返回一个32位有符号整数，小于零时，在排序顺序中，第一个比较数在第二个比较数之前；等于零时，二者出现的位置相同或`Length`为零；大于零时，第一个比较数在第二个比较数后。非`null`字符串大于`null`字符串。

- `public static int Compare(String, String)`
- `public static int Compare(String, String, Boolean)`
- `public static int Compare(String, String, Boolean, CultureInfo)`
- `public static int Compare(String, String, StringComparison)`
- `public static int Compare(String, String, CultureInfo, CompareOptions)`
- `public static int Compare(String, Int32, String, Int32, Int32)`
- `public static int Compare(String, Int32, String, Int32, Int32, Boolean)`
- `public static int Compare(String, Int32, String, Int32, Int32, Boolean, CultureInfo)`
- `public static int Compare(String, Int32, String, Int32, Int32, StringComparison)`
- `public static int Compare(String, Int32, String, Int32, Int32, CultureInfo, CompareOptions)`

### CompareOrdinal

- `public static int CompareOrdinal(String, String)`
- `public static int CompareOrdinal(String, Int32, String, Int32, Int32)`

### Concat

连接一个或多个`String`实例，或连接一个或多个`Object`实例的字符串表示形式。此方法在连接每个对象时不添加任何分隔符。使用`Empty`字符串替换任何`null`对象。如果任何一个参数为数组引用，则该方法将连接一个表示该数组的字符串，而不是它的成员。

- `public static string Concat(Object)`
- `public static string Concat(Object, Object)`
- `public static string Concat(Object, Object, Object)`
- `public static string Concat(Object[])`
- `public static string Concat(String, String)`
- `public static string Concat(String, String, String)`
- `public static string Concat(String, String, String, String)`
- `public static string Concat(String[])`
- `public static string Concat(ReadOnlySpan<Char>, ReadOnlySpan<Char>)`
- `public static string Concat(ReadOnlySpan<Char>, ReadOnlySpan<Char>, ReadOnlySpan<Char>)`
- `public static string Concat(ReadOnlySpan<Char>, ReadOnlySpan<Char>, ReadOnlySpan<Char>, ReadOnlySpan<Char>)`
- `public static string Concat(IEnumerable<String>)`
- `public static string Concat(IEnumerable<T>)`

### Create

创建新字符串。

- `public static string Create(IFormatProvider, Span<Char>, ref DefaultInterpolatedStringHandler)`
- `public static string Create(IFormatProvider, ref DefaultInterpolatedStringHandler)`
- `public static string Create<TState>(Int32, TState, SpanAction<Char,TState>)`

### Equals

- `public static bool Equals(String, String)`
- `public static bool Equals(String, String, StringComparison)`

### Format

将对象的值转换为基于指定格式的字符串，并将其插入到另一个字符串，返回最终的组合字符串。

格式字符串中格式项`{0}`为占位符，是字符串值将插入到该位置的对象的索引，索引从0开始。如果要插入的对象不是字符串，则调用其`ToString`方法转换为字符串再插入结果字符串中。

格式项的语法为：`{index[,alignment][:formatString]}`。

`alignment`为对齐方式，是有符号整数，指示插入参数的字段的总长度以及是右对齐（正整数）还是左对齐（负整数）。如果省略`alignment`，则相应参数的字符串表示形式将插入到没有前导空格或尾随空格的字段中。如果`alignment`小于要插入的参数的长度，则忽略对齐方式，并将参数的完整字符串长度用作字段宽度。

`formatString`指定相应参数的结果字符串的格式。省略时调用相应参数的无参数`ToString`方法来生成其字符串表示形式，指定时则格式项引用的参数必须实现`IFormattable`接口。

- `public static string Format(String, Object)`
- `public static string Format(String, Object, Object)`
- `public static string Format(String, Object, Object, Object)`
- `public static string Format(String, params Object[])`
- `public static string Format(IFormatProvider, String, Object)`
- `public static string Format(IFormatProvider, String, Object, Object)`
- `public static string Format(IFormatProvider, String, Object, Object, Object)`
- `public static string Format(IFormatProvider, String, params Object[])`

### GetHashCode

- `public static int GetHashCode(ReadOnlySpan<Char>)`
- `public static int GetHashCode(ReadOnlySpan<Char>, StringComparison)`







### IsNullOrEmpty

- `public static bool IsNullOrEmpty (string? value)`

指示指定的字符串是`null`字符串还是空字符串`""`。如果参数`value`为`null`或空字符串`""`，则返回`true`，否则返回`false`。

### IsNullOrWhiteSpace

- `public static bool IsNullOrWhiteSpace (string? value)`

指示指定的字符串是`null`、空还是仅由空白字符组成。如果参数`value`为`null`、空字符串`""`或仅由空白字符组成，则返回`true`，否则返回`false`。

### Join

- `public static string Join (char separator, params string?[] values)`：连接`values`每个成员，并使用`separator`分隔。
- `public static string Join (char separator, params object?[] values)`：连接`values`每个成员的字符串表示形式，并使用`separator`分隔。
- `public static string Join (string? separator, params string?[] value)`：连接`values`每个成员，并使用`separator`分隔。
- `public static string Join (string? separator, params object?[] values)`：连接`values`每个成员，并使用`separator`分隔。
- `public static string Join (char separator, string?[] value, int startIndex, int count)`：连接`values`从`startIndex`位置开始`count`个元素，并使用`separator`分隔。
- `public static string Join (string? separator, string?[] value, int startIndex, int count)`：连接`values`从`startIndex`位置开始`count`个元素，并使用`separator`分隔。
- `public static string Join<T> (char separator, System.Collections.Generic.IEnumerable<T> values)`：连接`values`的成员，并使用`separator`分隔。
- `public static string Join (string? separator, System.Collections.Generic.IEnumerable<string?> values)`：连接`values`每个成员，并使用`separator`分隔。
- `public static string Join<T> (string? separator, System.Collections.Generic.IEnumerable<T> values)`：连接`values`的成员，并使用`separator`分隔。

连接指定数组的元素或集合的成员，在每个元素或成员之间使用指定的分隔符。如果`values`有零个元素，则返回`Empty`。

### Copy

- `public static string Copy (string str)`： 创建一个与`str`具有相同值的新`String`实例。

此方法返回一个`String`对象，该对象的值与原始字符串相同，但表示不同的对象引用。不同于引用操作。



### Intern

- `public static string Intern (string str)`

获取系统对指定`String`的引用。如果暂存池中存在`str`，则返回系统对其的引用；否则返回对值为`str`的字符串的新引用。

公共语言运行时通过维护名为暂存池的表来节省字符串存储，该表包含对程序中以编程方式声明或创建的每个唯一文本字符串的单个引用。因此，系统中仅存在一个具有特定值的文本字符串的实例。如果将相同的文本字符串分配给几个变量，则运行时将从暂存池中检索到文本字符串的相同引用，并将其分配给每个变量。`Intern`方法使用暂存池搜索等于值的字符串`str`。如果存在这样的字符串，则返回暂存池中的引用，不存在，则会将对的引用`str`添加到暂存池中，然后返回该引用。暂存字符串具有两个不需要的副作用。首先，在公共语言运行时（CLR）终止之前，为暂存对象分配的内存不大可能被释放。原因在于，在应用程序甚至应用程序域终止后，CLR对暂存对象的引用可能会保持不变。其次，若要暂存字符串，必须先创建字符串。即使将最终回收内存，仍必须分配对象使用的内存。

### IsInterned

- `public static string? IsInterned (string str)`

获取系统对指定`String`的引用。如果暂存池中存在`str`，则返回系统对其的引用；否则返回`null`。

## 运算符

### ==

- `public static bool operator == (string? a, string? b)`

确定两个指定的字符串是否具有相同的值。相等则返回`true`，否则返回`false`。该方法执行区分大小写和不区分区域性的比较。

### !=

- `public static bool operator != (string? a, string? b)`

确定两个指定的字符串是否具有不同的值。不同则返回`true`，否则返回`false`。该方法执行区分大小写和不区分区域性的比较。

### 隐式转换

- `public static implicit operator ReadOnlySpan<char> (string? value)`

给定字符串到只读字符范围的隐式转换。