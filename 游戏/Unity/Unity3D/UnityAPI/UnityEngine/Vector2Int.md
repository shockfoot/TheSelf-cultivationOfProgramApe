- 命名空间：`UnityEngine`
- 程序集：`UnityEngine.CoreModule`

`Vector2Int`用来表示二维的**整数**向量和点。

## 静态属性

### up

- `public static Vector2Int Up`

### down

- `public static Vector2Int down`

### right

- `public static Vector2Int right`

### left

- `public static Vector2Int left`

### zero

- `public static Vector2Int zero`

### one

- `public static Vector2Int one`

## 属性

### magnitude

- `public float magnitude`

### sqrMagnitude

- `public int sqrMagnitude`

### this[int]

- `public int this[int]`

### x

- `public int x`

### y

- `public int y`

## 公共方法

### Clamp

- `public void Clamp(Vector2Int, Vector2Int)`

将当前向量限制在给定的边界内。

### Equals

- `public bool Equals(Object)`

### GetHashCode

- `public int GetHashCode()`

获取当前向量的HashCode。

### Set

- `public void Set(int, int)`

### ToString

- `public string ToString()`
- `public string ToString(string)`
- `public string ToString(string, IFormatProvider)`

## 静态方法

### CeilToInt

- `public static Vector2Int CeilToInt(Vector2)`

对向量的每个值执行Ceiling操作将其从`Vector2`转换为`Vector2Int`。Ceiling操作返回大于或等于所给数字表达式的最小整数。因为存在浮点数到整数的转换，所以精度会降低。

### FlootToInt

- `public static Vector2Int FloorToInt(Vector2)`

对向量的每个值执行Floor操作将其从`Vector2`转换为`Vector2Int`。Floor操作返回小于或等于所给数字表达式的最大整数。因为存在浮点数到整数的转换，所以精度会降低。

### RoundToInt

- `public static Vector2Int RoundToInt(Vector2)`

对向量的每个值执行Round操作将其从`Vector2`转换为`Vector2Int`。Round操作按四舍五入取整，但因计算机内部计算时会受到浮点数的干扰，所以有时在.5时会出现问题，因此在有出现.5的情况并且需要用到Round时，可加上一个很小的数字（比如0.0001）来确保不会出现bug，同时也不会多进一位。因为存在浮点数到整数的转换，所以精度会降低。

### Distance

- `public static float Distance(Vector2Int, Vector2Int)`

### Max

- `public static Vector2Int Max(Vector2Int, Vector2Int)`

### Min

- `public static Vector2Int Min(Vector2Int, Vector2Int)`

### Scale

- `public static void Scale(Vector2Int)`
- `public static Vector2Int Scale(Vector2Int, Vector2Int)`

## 运算符

### 加+

- `public static Vector2Int operator + (Vector2Int, Vector2Int)`

### 减-

- `public static Vector2Int operator - (Vector2Int, Vector2Int)`

### 乘*

- `public static Vector2Int operator * (Vector2Int, int)`
- `public static Vector2Int operator * (Vector2Int, Vector2Int)`

### 除/

- `public static Vector2Int operator / (Vector2Int, int)`

### 相等==

- `public static bool operator == (Vector2Int, Vector2Int)`

### 不等!=

- `public static bool operator != (Vector2Int, Vector2Int)`

### Vector2

可以将`Vector2Int`转换为`Vector2`。

### Vector3Int

可以将`Vector2Int`转换为`Vector3Int`（z分量设置为0）。