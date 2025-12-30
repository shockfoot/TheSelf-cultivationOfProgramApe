# Vector2Int

所属命名空间`UnityEngine`，实现施于`UnityEngine.CoreModule`。

`Vector2Int`用来表示二维的**整数**向量和点。

## 静态属性

### up

- `public static Vector2Int Up`

`Vector2Int(0,1)` 的简便方法。

### down

- `public static Vector2Int down`

`Vector2Int(0,-1)` 的简便方法。

### right

- `public static Vector2Int right`

`Vector2Int(1,0)` 的简便方法。

### left

- `public static Vector2Int left`

`Vector2Int(-1,0)` 的简便方法。

### zero

- `public static Vector2Int zero`

`Vector2Int(0,0)` 的简便方法。

### one

- `public static Vector2Int one`

`Vector2Int(1,1)` 的简便方法。

## 属性

### magnitude

- `public float magnitude`

当前向量的模（只读），等于`x*x+y*y`的平方根。如果只需要比较一些向量的大小，则可以使用`sqrMagnitude`比较它们的平方数（计算平方数更快）。

### sqrMagnitude

- `public int sqrMagnitude`

当前向量模的平方（只读）。比`magnitude`性能更快。

### this[int]

- `public int this[int]`

分别使用[0]或[1]访问`x`或`y`分量。

### x

- `public int x`

当前向量的`x`分量。

### y

- `public int y`

当前向量的`y`分量。

## 公共方法

### Clamp

- `public void Clamp(Vector2Int min, Vector2Int max)`

将当前向量限制在`min`和`max`给定的边界内。

### Equals

- `public bool Equals(object other)`

如果给定向量与当前向量完全相等，则返回`true`。

### GetHashCode

- `public int GetHashCode()`

获取当前向量的Hash码。

### Set

- `public void Set(int newX, int newY)`

设置当前向量的`x`和`y`分量。

### ToString

- `public string ToString()`
- `public string ToString(string format)`
- `public string ToString(string format, IFormatProvider formatProvider)`

返回当前向量的格式化字符串。

## 静态方法

### CeilToInt

- `public static Vector2Int CeilToInt(Vector2 v)`

对`v`的每个值执行Ceiling操作将其从`Vector2`转换为`Vector2Int`。Ceiling操作返回大于或等于所给数字表达式的最小整数。因为存在浮点数到整数的转换，所以精度会降低。

### FlootToInt

- `public static Vector2Int FloorToInt(Vector2 v)`

对`v`的每个值执行Floor操作将其从`Vector2`转换为`Vector2Int`。Floor操作返回小于或等于所给数字表达式的最大整数。因为存在浮点数到整数的转换，所以精度会降低。

### RoundToInt

- `public static Vector2Int RoundToInt(Vector2 v)`

对`v`的每个值执行Round操作将其从`Vector2`转换为`Vector2Int`。Round操作按四舍五入取整，但因计算机内部计算时会受到浮点数的干扰，所以有时在`.5`时会出现问题，因此在有出现`.5`的情况并且需要用到Round时，可加上一个很小的数字（比如0.0001）来确保不会出现bug，同时也不会多进一位。因为存在浮点数到整数的转换，所以精度会降低。

### Distance

- `public static float Distance(Vector2Int a, Vector2Int b)`

返回两向量之间的距离，`Distance(a,b)`与`(a-b).magnitude`相同。

### Max

- `public static Vector2Int Max(Vector2Int lhs, Vector2Int rhs)`

返回由两个向量的最大分量组成的向量。

### Min

- `public static Vector2Int Min(Vector2Int lhs, Vector2Int rhs)`

返回由两个向量的最小分量组成的向量。

### Scale

- `public static Vector2Int Scale(Vector2Int a, Vector2Int b)`
- `public static void Scale(Vector2Int scale)`

返回两个向量的分量相乘得到的向量。

## 运算符

### 加+

- `public static Vector2Int operator + (Vector2Int a, Vector2Int b)`

返回`a`+`b`的向量，即用`a`的每个分量加上`b`的每个分量，新向量的方向为两向量组成的平行四边形的同起点的对象方向。

### 减-

- `public static Vector2Int operator - (Vector2Int a, Vector2Int b)`

返回`a`-`b`的向量，即用`a`的每个分量减去`b`的每个分量，新向量的方向为减向量指向被减向量。

### 乘*

- `public static Vector2Int operator * (Vector2Int a, int d)`

将`a`的每个分量乘以数值`d`。

- `public static Vector2Int operator * (Vector2Int a, Vector2Int b)`

将`a`的每个分量乘以向量`b`的相应分量。

### 除/

- `public static Vector2Int operator / (Vector2Int a, int d)`

将`a`的每个分量除以数值`d`。

### 相等==

- `public static bool operator == (Vector2Int lhs, Vector2Int rhs)`

如果两个向量相等，则返回`true`。

### 不等!=

- `public static bool operator != (Vector2Int lhs, Vector2Int rhs)`

如果两个向量不相等，则返回`true`。

### Vector2

可以将`Vector2Int`转换为`Vector2`。

### Vector3Int

可以将`Vector2Int`转换为`Vector3Int`（z分量设置为0）。