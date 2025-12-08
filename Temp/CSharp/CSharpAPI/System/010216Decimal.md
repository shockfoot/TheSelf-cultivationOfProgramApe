- 命名空间：`System`
- 程序集：`System.Runtime.dll`
- 继承：`Object` > `ValueType`

`public readonly struct Decimal`

`Decimal`，表示十进制浮点数，其值范围由-79228162514264337593543950335~79228162514264337593543950335，还包括零、正/负无穷大、非数字。适用于需要大量有效整数和小数位数且无舍入误差的财务计算。`Decimal`无法消除舍入的误差，但能将舍入导致的误差降至最低。

`Decimal`是一个浮点值，由符号、数值（其中每个数字的范围为0到9）和缩放因子（scaling factor）组成；缩放因子指示分隔数值的整数和小数部分的浮动小数点的位置。其二进制有128位，由一个96位整数和一组32位标志组成，这些标志表示符号和缩放因子等用于指定小数部分的内容。因此，`Decimal`的二进制范围表示为-2^96^~2^96^即10^0~28^，其中-(2^96^-1)为`MinValue`，2^96^-1为`MaxValue`。缩放因子还保留十进制数中的任何后置零。

## 字段

### MinValue

- `public static readonly decimal MinValue = -79228162514264337593543950335`

### MaxValue

- `public static readonly decimal MaxValue = 79228162514264337593543950335`

### One

- `public static readonly decimal One = 1`

### MinusOne

- `public static readonly decimal MinusOne = -1`

### Zero

- `public static readonly decimal Zero = 0`

## 构造函数

- `public Decimal(Int32)`
- `public Decimal(Int64)`
- `public Decimal(UInt32)`
- `public Decimal(UInt64)`
- `public Decimal(Single)`
- `public Decimal(Decimal)`
- `public Decimal(ReadOnlySpan<Int32>)`
- `public Decimal(Int32[])`
- `public Decimal(Int32, Int32, Int32, Boolean, Byte)`

## 普通方法

### CompareTo

- `public int CompareTo(Object)`
- `public int CompareTo(Decimal)`

### Equals

- `public override bool Equals(Object)`
- `public bool Equals(Decimal)`

### GetHashCode

- `public override int GetHashCode()`

### GetTypeCode

- `public TypeCode GetTypeCode()`

### ToString

- `public override string ToString()`
- `public string ToString(IFormatProvider)`
- `public string ToString(String)`
- `public string ToString(String, IFormatProvider)`

### TryFormat

- `public bool TryFormat(Span<Char>, out Int32, ReadOnlySpan<Char>, IFormatProvider)`

## 静态方法

### Abs

- `public static decimal Abs(Decimal)`

### Add

求和。

- `public static decimal Add(Decimal, Decimal)`

### Ceiling

- `public static decimal Ceiling(Decimal)`

### Floor

- `public static decimal Floor(Decimal)`

### Clamp

- `public staic decimal Clamp(Decimal, Decimal, Decimal)`

### Compare

比较两个数的大小。

- `public static int Compare(Decimal, Decimal)`

### CopySign

- `public static decimal CopySign(Decimal, Decimal)`

### CreateChecked

- `public static decimal CreateChecked<TOther>(TOther) where TOther : System.Numerics.INumberBase<TOther>`

### CreateSaturaing

- `public static decimal CreateChecked<TOther>(TOther) where TOther : System.Numerics.INumberBase<TOther>`

### CreateTruncating

- `public static decimal CreateTruncating<TOther>(TOther) where TOther : System.Numerics.INumberBase<TOther>`

### Divide

- `public static decimal Divide(Decimal, Decimal)`

### Equals

- `public static bool Equals(Decimal, Decimal)`

### FromOACurrency

将`Int64`转换为等效的`Decimal`类型。

- `public static decimal FromOACurrency(Int64)`

### GetBits

将`Decimal`转换为等效的二进制表示形式。

- `public static int GetBits(Decimal, Span<Int32>)`
- `public static int[] GetBits(Decimal)`

### TryGetBits

试图将`Decimal`转换为等效的二进制表示形式。

- `public static bool TryGetBits(Decimal, Span<Int32>, out Int32)`

### IsCanonical

确定值是否在其规范表示形式中。

- `public static bool IsCanonical (Decimal)`

### IsInteger

- `public static bool IsInteger(Decimal)`

### IsEvenInteger

- `public static bool IsEvenInteger(Decimal)`

### IsOddInteger

- `public static bool IsOddInteger(Decimal)`

### IsPositive

- `public static bool IsPositive(Decimal)`

### IsNegative

- `public static bool IsNegative(Decimal)`

### Max

- `public static decimal Max(Decimal, Decimal)`

### MaxMagnitued

- `public static decimal MaxMagnitude(Decimal, Decimal)`

### Min

- `public static decimal Min(Decimal, Decimal)`

### MinMagnitued

- `public static decimal MinMagnitude(Decimal, Decimal)`

### Multiply

求积。

- `public static decimal Multiply(Decimal, Decimal)`

### Negate

求相反数。

- `public static decimal Negate(Decimal)`

### Parse

- `public static decimal Parse(String)`
- `public static decimal Parse(String, NumberStyles)`
- `public static decimal Parse(String, IFormatProvider)`
- `public static decimal Parse(String, NumberStyles, IFormatProvider)`
- `public static decimal Parse(ReadOnlySpan<Char>, IFormatProvider)`
- `public static decimal Parse(ReadOnlySpan<Char>, NumberStyles, IFormatProvider)`

### TryParse

- `public static bool TryParse(String, out Decimal)`
- `public static bool TryParse(String, IFormatProvider, out Decimal)`
- `public static bool TryParse(String, NumberStyles, IFormatProvider, out Decimal)`
- `public static bool TryParse(ReadOnlySpan<Char>, out Decimal)`
- `public static bool TryParse(ReadOnlySpan<Char>, IFormatProvider, out Decimal)` 
- `public static bool TryParse(ReadOnlySpan<Char>, NumberStyles, IFormatProvider, out Decimal)`

### Remainder

求余数。

- `public static decimal Remainder(Decimal, Decimal)`

### Round

- `public static decimal Round(Decimal)`
- `public static decimal Round(Decimal, Int32)`
- `public static decimal Round(Decimal, MidpointRounding)`
- `public static decimal Round(Decimal, Int32, MidpointRounding)`

### Sign

- `public static int Sign(Decimal)`

### Subtract

求差。

- `public static decimal Subtract(Decimal, Decimal)`

### ToSByte

将`Decimal`转化为`SByte`。

- `public static sbyte ToSByte(Decimal)`

### ToByte

将`Decimal`转化为`Byte`。

- `public static byte ToByte(Decimal)`

### ToInt16

将`Decimal`转化为`Int16`。

- `public static short ToInt16(Decimal)`

### ToUInt16

将`Decimal`转化为`UInt16`。

- `public static ushort ToUInt16(Decimal)`

### ToInt32

将`Decimal`转化为`Int32`。

- `public static int ToInt32(Decimal)`

### ToUInt32

将`Decimal`转化为`UInt32`。

- `public static uint ToUInt32(Decimal)`

### ToInt64

将`Decimal`转化为`Int64`。

- `public static long ToInt64(Decimal)`

### ToUInt64

将`Decimal`转化为`UInt64`。

- `public static ulong ToUInt64(Decimal)`

### ToSingle

将`Decimal`转化为`Single`。

- `public static float ToSingle(Decimal)`

### ToDouble

将`Decimal`转化为`Double`。

- `public static double ToDouble(Decimal)`

### ToOACurrency

将`Decimal`转化为OLE Automation Currency，该值包含在64位有符号整数中。

- `public static long ToOACurrency(Decimal)`

### Truncate

- `public static decimal Truncate(Decimal)`