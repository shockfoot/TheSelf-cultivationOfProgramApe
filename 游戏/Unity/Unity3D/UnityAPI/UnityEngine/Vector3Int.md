- 命名空间：`UnityEngine`
- 程序集：`UnityEngine.CoreModule`

`Vector3Int`用来表示三维的**整数**向量和点。

## 静态属性

### up

- `public static Vector3Int Up`

### down

- `public static Vector3Int down`

### right

- `public static Vector3Int right`

### left

- `public static Vector3Int left`

### forward

- `public static Vector3Int forward`

### back

- `public static Vector3Int back`

### zero

- `public static Vector3Int zero`

### one

- `public static Vector3Int one`

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

### z

- `public int z`

## 构造函数

- `public Vector3Int(int, int, int)`
- `public Vector3Int(int, int)`

## 公共方法

### Clamp

- `public void Clamp(Vector3Int, Vector3Int)`

### Equals

- `public bool Equals(Object)`

### GetHashCode

- `public int GetHashCode()`

### Set

- `public void Set(int, int, int)`

### ToString

- `public string ToString()`
- `public string ToString(string)`
- `public string ToString(string, IFormatProvider)`

## 静态方法

### CeilToInt

- `public static Vector3Int CeilToInt(Vector3)`

### FlootToInt

- `public static Vector3Int FloorToInt(Vector3)`

### RoundToInt

- `public static Vector3Int RoundToInt(Vector3)`

### Distance

- `public static float Distance(Vector3Int, Vector3Int)`

### Max

- `public static Vector3Int Max(Vector3Int, Vector3Int)`

### Min

- `public static Vector3Int Min(Vector3Int, Vector3Int)`

### Scale

- `public static void Scale(Vector3Int)`
- `public static Vector3Int Scale(Vector3Int, Vector3Int)`

## 运算符

### 加+

- `public static Vector3Int operator + (Vector3Int, Vector3Int)`

### 减-

- `public static Vector3Int operator - (Vector3Int, Vector3Int)`

### 乘*

- `public static Vector3Int operator * (Vector3Int, int)`
- `public static Vector3Int operator * (Vector3Int, Vector3Int)`

### 除/

- `public static Vector3Int operator / (Vector3Int, int)`

### 相等==

- `public static bool operator == (Vector3Int, Vector3Int)`

### 不等!=

- `public static bool operator != (Vector3Int, Vector3Int)`

### Vector3

可以将`Vector3Int`转换为`Vector3`。

### Vector2Int

可以将`Vector3Int`转换为`Vector2Int`（z分量被舍弃）。