- 命名空间：`UnityEngine`
- 程序集：`UnityEngine.CoreModule`

`Vector2`用来表示二维的向量和点。

## 静态属性

### up

- `public static Vector2 Up`

`Vector2(0, 1)` 的简便方法。

### down

- `public static Vector2 down`

`Vector2(0, -1)` 的简便方法。

### right

- `public static Vector2 right`

`Vector2(1, 0)` 的简便方法。

### left

- `public static Vector2 left`

`Vector2(-1, 0)` 的简便方法。

### zero

- `public static Vector2 zero`

`Vector2(0, 0)` 的简便方法。

### one

- `public static Vector2 one`

`Vector2(1, 1)` 的简便方法。

### positiveInfinite

- `public static Vector2 positiveInfinite`

`Vector2(float.PositiveInfinite, float.PositiveInfinite)` 的简便方法。

### negativeInfinite

- `public static Vector2 NegativeInfinite`

`Vector2(float.NegativeInfinite, float.NegativeInfinite)` 的简便方法。

## 属性

### magnitude

- `public float magnitude`

当前向量的模，只读。如果只需要比较一些向量的大小，则可以使用`sqrMagnitude`比较它们的平方数（计算平方数更快）。

### sqrMagnitude

- `public float sqrMagnitude`

当前向量模的平方，只读。比`magnitude`性能更快。

### normalized

- `public Vector2 normalized`

当前向量保持不变，返回一个新的归一化向量即单位向量，只读。进行标准化时，向量方向保持不变，但其模为1.0。要归一化当前向量可用`Normalize`方法。如果向量太小而无法标准化，则返回零向量。

### this[int]

- `public float this[int]`

分别使用[0]或[1]访问`x`或`y`分量。

### x

- `public float x`

当前向量的`x`分量。

### y

- `public float y`

当前向量的`y`分量。

## 构造函数

- `public Vector2(float, float)`

使用给定的分量构造新的二维向量。

## 普通方法

### Equals

- `public bool Equals(Object)`

如果给定向量与当前向量完全相等，则返回`true`。由于浮点数不准确，对于本质上相等（但不完全相等）的向量，这可能会返回`false`，使用`==`运算符判断两个向量的近似相等性。

### Normalize

- `public void Normalize()`

将当前向量单位化，使`magnitude`为1。此方法将更改当前向量。如果向量太小而无法标准化，则将其设置为零。

### Set

- `public void Set(float, float)`

设置当前向量的`x`和`y`分量。

### ToString

- `public string ToString()`
- `public string ToString(string)`
- `public string ToString(string, IFormatProvider)`

返回当前向量的格式化字符串。默认显示为两位小数（`format="F2"`）。

## 静态方法

### Angle

- `public static float Angle(Vector2, Vector2)`

获取两向量的之间的最小无符号角度而永远不会返回反射角，以度为单位，结果范围在0-180度之间。

### SignedAngle

- `public static float SignedAngle(Vector2, Vector2)`

获取两向量之间的最小带符号的逆时针角度而永远不会返回反射角，，以度为单位，结果范围在-180-180度之间。

### ClampMagnitude

- `public static Vector2 ClampMagnitude(Vector2, float)`

返回`vector`的副本，其大小被限制为`maxLength`。

### Distance

- `public static float Distance(Vector2, Vector2)`

返回两向量之间的距离，`Distance(a,b)`与`(a-b).magnitude`相同。

### Dot

- `public static float Dot(Vector2, Vector2)`

返回两向量的点积。点积的几何意义为两向量模长与其夹角余弦值的积。对于标准化向量，如果两向量方向相同，返回1；相反返回-1；垂直则返回0。对于其他情况，返回介于-1与1之间的数字。

### Perpendicular

- `public static Vector2 Perpendicular(Vector2)`

返回垂直于`inDirection`的 2D 向量。对于Y轴向上的2D坐标系来说，结果始终沿逆时针方向旋转90度。

### Reflect

- `public static Vector2 Reflect(Vector2, Vector2)`

返回入射向量前者在后者上反射的向量，结果是与入射向量大小相等、方向为其反射方向的向量。

### Max

- `public static Vector2 Max(Vector2, Vector2)`

返回由两个向量的最大分量组成的向量。

### Min

- `public static Vector2 Min(Vector2, Vector2)`

返回由两个向量的最小分量组成的向量。

### Scale

- `public static void Scale(Vector2)`
- `public static Vector2 Scale(Vector2, Vector2)`

返回两个向量的分量相乘得到的向量。

### Lerp

- `public static Vector2 Lerp(Vector2, Vector2, float)`

在两向量之间进行线性插值。插值步幅限制在范围[0,1]内。当步幅为0时，返回初始向量。当步幅为1时，返回目标向量。当`t`为0.5时，返回二者的中间向量。

### LerpUnclamped

- `public static Vector2 LerpUnclamped(Vector2, Vector2, float)`

### MoveTowards

- `public static Vector2 MoveTowards(Vector2, Vector2, float)`

将初始向量移向目标向量，但每次移动指定距离。指定距离为负值时会将初始向量推离目标向量。

### SmoothDamp

- `public static Vector2 SmoothDamp(Vector2, Vector2, ref Vector2, float, float maxSpeed = Mathf.Infinity, float deltaTime = Time.deltaTime)`

随时间推移将一个向量逐渐改变为目标向量。向量通过某个类似于弹簧-阻尼的函数（它从不超过目标）进行平滑。

## 运算符

### 加+

- `public static Vector2 operator + (Vector2, Vector2)`

### 减-

- `public static Vector2 operator - (Vector2)`
- `public static Vector2 operator - (Vector2, Vector2)`

### 乘*

- `public static Vector2 operator * (Vector2, float)`
- `public static Vector2 operator * (float, Vector2)`
- `public static Vector2 operator * (Vector2, Vector2)`

### 除/

- `public static Vector2 operator / (Vector2, float)`

- `public static Vector2 operator / (Vector2, Vector2)`

### 相等==

- `public static bool operator == (Vector2, Vector2)`

### Vector2

可以将`Vector3`隐式转换为`Vector2`（z分量被丢弃）。

### Vector3

可以将`Vector2`隐式转换为`Vector3`（z分量设置为0）。