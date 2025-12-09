- 命名空间：`System`
- 程序集：`System.Runtime.dll`
- 继承：`Object` > `ValueType`

`public readonly struct UIntPtr`

`UIntPtr`，即`nuint`，表示指针位宽数量的无符号整数，其范围由操作系统位宽决定。

## 字段

### Zero

- `public static readonly nuint Zero = 0`

## 属性

### MinValue

- `public static nuint MinValue { get; }`

### MaxValue

- `public static nuint MaxValue { get; }`

### Size

- `public static int Size { get; } `

## 构造函数

- `public UIntPtr(Int32)`
- `public UIntPtr(Int64)`
- `public UIntPtr(Void*)`

## 普通方法

### CompareTo

- `public int CompareTo(Object)`
- `public int CompareTo(UIntPtr)`

### Equals

- `public override bool Equals(Object)`
- `public bool Equals(UIntPtr)`

### GetHashCode

- `public override int GetHashCode()`

### ToPointer

- `public void* ToPointer()`

### ToString

- `public override string ToString()`
- `public string ToString(IFormatProvider)`
- `public string ToString(String)`
- `public string ToString(String, IFormatProvider)`

### ToUInt32

将此实例转换为32位无符号整数。

- `public uint ToUInt32()`

### ToUInt64

将此实例转换为64位无符号整数。

- `public ulong ToUInt64()`

### TryFormat

- `public bool TryFormat(Span<Char>, out Int32, ReadOnlySpan<Char>, IFormatProvider)`

## 静态方法

### Add

- `public static nuint Add (UIntPtr, Int32)`

### Subtract

- `public static nuint Subtract (UIntPtr, Int32)`

### Clamp

- `public staic nuint Clamp(UIntPtr, UIntPtr, UIntPtr)`

### CreateChecked

- `public static nuint CreateChecked<TOther>(TOther) where TOther : System.Numerics.INumberBase<TOther>`

### CreateSaturaing

- `public static nuint CreateChecked<TOther>(TOther) where TOther : System.Numerics.INumberBase<TOther>`

### CreateTruncating

- `public static nuint CreateTruncating<TOther>(TOther) where TOther : System.Numerics.INumberBase<TOther>`

### DivRem

- `public static ValueType<UIntPtr, UIntPtr> DivRem(UIntPtr, UIntPtr)`

### IsEvenInteger

- `public static bool IsEvenInteger(UIntPtr)`

### IsOddInteger

- `public static bool IsOddInteger(UIntPtr)`

### IsPow2

- `public static bool IsPow2(UIntPtr)`

### Log2

- `public static nuint Log2(UIntPtr)`

### LeadingZeroCount

- `public static nuint LeadingZeroCount(UIntPtr)`

### TrailingZeroCound

- `public static nuint TrailingZeroCount(UIntPtr)`

### Max

- `public static nuint Max(UIntPtr, UIntPtr)`

### Min

- `public static nuint Min(UIntPtr, UIntPtr)`

### Parse

- `public static nuint Parse(String)`
- `public static nuint Parse(String, NumberStyles)`
- `public static nuint Parse(String, IFormatProvider)`
- `public static nuint Parse(String, NumberStyles, IFormatProvider)`
- `public static nuint Parse(ReadOnlySpan<Char>, IFormatProvider)`
- `public static nuint Parse(ReadOnlySpan<Char>, NumberStyles, IFormatProvider)`

### TryParse

- `public static bool TryParse(String, out UIntPtr)`
- `public static bool TryParse(String, IFormatProvider, out UIntPtr)`
- `public static bool TryParse(String, NumberStyles, IFormatProvider, out UIntPtr)`
- `public static bool TryParse(ReadOnlySpan<Char>, out UIntPtr)`
- `public static bool TryParse(ReadOnlySpan<Char>, IFormatProvider, out UIntPtr)` 
- `public static bool TryParse(ReadOnlySpan<Char>, NumberStyles, IFormatProvider, out UIntPtr)`

### PopCount

- `public static nuint PopCount(UIntPtr)`

### RotateLeft

- `public static nuint RotateLeft(UIntPtr, Int32)`

### RotateRight

- `public static nuint RotateRight(UIntPtr, Int32)`

### Sign

- `public static int Sign(UIntPtr)`
