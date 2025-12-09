- 命名空间：`UnityEngine`
- 程序集：`UnityEngine.CoreModule`

`Vector4`用来表示四维的向量和点。

## 静态属性

### zero

- `public static Vector4 zero`

### one

- `public static Vector4 one`

### positiveInfinite

- `public static Vector4 positiveInfinite`

### negativeInfinite

- `public static Vector4 NegativeInfinite`

## 属性

### magnitude

- `public float magnitude`

### sqrMagnitude

- `public float sqrMagnitude`

### normalized

- `public Vector2 normalized`

### this[int]

- `public float this[int]`

### x

- `public float x`

### y

- `public float y`

### z

- `public float z`

### w

- `public float w`

## 构造函数

- `public Vector4(float, float, float, float)`
- `public Vector4(float, float, float)`
- `public Vector4(float, float)`

## 普通方法

### Equals

- `public bool Equals(Object)`

### Set

- `public void Set(float, float, float, float)`

### ToString

- `public string ToString()`
- `public string ToString(string)`
- `public string ToString(string, IFormatProvider)`

## 静态方法

### Normalize

- `public static Vector4 Normalize(Vector4)`

### Distance

- `public static float Distance(Vector4, Vector4)`

### Dot

- `public static float Dot(Vector4, Vector4)`

### Project

- `public static Vector4 Project(Vector4, Vector4)`

### Max

- `public static Vector4 Max(Vector4, Vector4)`

### Min

- `public static Vector4 Min(Vector4, Vector4)`

### Scale

- `public static void Scale(Vector4)`
- `public static Vector4 Scale(Vector4, Vector4)`

### Lerp

- `public static Vector4 Lerp(Vector4, Vector4, float)`

### LerpUnclamped

- `public static Vector4 LerpUnclamped(Vector4, Vector4, float)`

### MoveTowards

- `public static Vector4 MoveTowards(Vector4, Vector4, float)`

## 运算符

### 加+

- `public static Vector4 operator + (Vector4, Vector4)`

### 减-

- `public static Vector2 operator - (Vector4)`
- `public static Vector4 operator - (Vector4, Vector4)`

### 乘*

- `public static Vector4 operator * (Vector4, float)`
- `public static Vector4 operator * (float, Vector4)`

### 除/

- `public static Vector4 operator / (Vector4, float)`

### 相等==

- `public static bool operator == (Vector4, Vector4)`

### Vector4

将`Vector2`或`Vector3`转换为`Vector4`，缺少的分量赋0。

### Vector3

将`Vector4`转换为`Vector3`，丢弃`w`分量。

### Vector2

将`Vector4`转换为`Vector2`，丢弃`z`、`w`分量。