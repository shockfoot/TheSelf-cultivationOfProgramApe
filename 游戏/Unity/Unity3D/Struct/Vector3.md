# Vector3

所属命名空间`UnityEngine`，实现施于`UnityEngine.CoreModule`。

`Vector3`用来表示三维的向量和点。

## 静态属性

### right

- `public static Vector3 right`

`Vector3(1,0,0)` 的简便方法。

### left

- `public static Vector3 left`

`Vector3(-1,0,0)` 的简便方法。

### up

- `public static Vector3 Up`

`Vector3(0,1,0)` 的简便方法。

### down

- `public static Vector3 down`

`Vector3(0,-1,0)` 的简便方法。

### forward

- `public static Vector3 forward`

`Vector3(0,0,1)` 的简便方法。

### back

- `public static Vector3 back`

`Vector3(0,0,-1)` 的简便方法。

### zero

- `public static Vector3 zero`

`Vector3(0,0,0)` 的简便方法。

### one

- `public static Vector3 one`

`Vector3(1,1,1)` 的简便方法。

### positiveInfinite

- `public static Vector3 positiveInfinite`

`Vector3(float.PositiveInfinite,float.PositiveInfinite,float.PositiveInfinite)` 的简便方法。

### negativeInfinite

- `public static Vector3 NegativeInfinite`

`Vector3(float.NegativeInfinite,float.NegativeInfinite,float.NegativeInfinite)` 的简便方法。

## 属性

### magnitude

- `public float magnitude`

当前向量的模（只读），等于`x*x+y*y+z*z`的平方根，以`Mathf.Sqrt(Vector3.Dot(v, v))`方式进行计算。`Sqrt`计算相当复杂，执行时间比普通算术运算要长。如果只需要比较一些向量的大小，则可以使用`sqrMagnitude`比较它们的平方数，计算基本相同，只是取消了执行缓慢的`Sqrt`调用，因此更快。

### sqrMagnitude

- `public float sqrMagnitude`

当前向量模的平方（只读）。比`magnitude`性能更快。

### normalized

- `public Vector2 normalized`

当前向量保持不变，返回一个新的归一化向量即单位向量（只读）。进行标准化时，向量方向保持不变，但其长度为1.0。要归一化当前向量可用`Normalize`方法。如果向量太小而无法标准化，则返回零向量。

### this[int]

- `public float this[int]`

分别使用[0]、[1]、[2]访问`x`、`y`、`z`分量。

### x

- `public float x`

当前向量的`x`分量。

### y

- `public float y`

当前向量的`y`分量。

### z

- `public float z`

当前向量的`z`分量。

## 构造函数

- `public Vector3(float x, float y, float z)`

使用给定的`x`、`y`、`z`分量构造新的三维向量。

## 公共方法

### Equals

- `public bool Equals(object other)`

如果给定向量与当前向量完全相等，则返回`true`。由于浮点数不准确，对于本质上相等（但不完全相等）的向量，这可能会返回`false`，使用`==`运算符判断两个向量的近似相等性。

### Set

- `public void Set(float newX, float newY, float newZ)`

设置当前向量的`x`、`y`、`z`分量。

### ToString

- `public string ToString()`
- `public string ToString(string format)`
- `public string ToString(string format, IFormatProvider formatProvider)`

返回当前向量的格式化字符串。默认显示为两位小数（`format="F2"`）。

## 静态方法

### Normalize

- `public static Vector3 Normalize(Vector3 value)`

将`value`单位化，使其`magnitude`为1。此方法将更改`value`。如果向量太小而无法标准化，则将其设置为零。

### OrthoNormalize

- `public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tanget)`
- `public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tanget, ref Vector3 binormal)`

将`normal`和`target`标准化并使二者正交。

### Angle

- `public static float Angle(Vector3 from, Vector3 to)`

获取`from`到`to`之间的无符号角度，以度为单位。该方法返回两向量之间的最小角度而永远不会返回反射角，即结果范围在0-180度之间。

### SignedAngle

- `public static float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)`

获取`from`到`to`之间的带符号的逆时针角度，以度为单位。默认情况下，两向量角度指二者同一平面上的角度。`axis`可以指定两向量旋转方向，从而影响角度的符号。该方法返回两向量之间的最小角度而永远不会返回反射角，即结果范围在0-180度之间。

### ClampMagnitude

- `public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)`

返回`vector`的副本，其大小被限制为`maxLength`。

### Distance

- `public static float Distance(Vector3 a, Vector3 b)`

返回两向量之间的距离，`Distance(a,b)`与`(a-b).magnitude`相同。

### Dot

- `public static float Dot(Vector3 lhs, Vector3 rhs)`

返回两向量的点积`lhs.x*rhs.x+lhs.y*lhs.y+lhs.z*lhs.z`。点积的几何意义为两向量模长与其夹角的余弦值的积。对于标准化向量，如果两向量方向相同，返回1；相反返回-1；垂直则返回0。对于其他情况，返回介于-1与1之间的数字。

### Cross

- `public static Vector3 Cross(Vector3 lhs, Vector3 rhs)`

返回两向量的叉积。叉积结果为向量， 该向量垂直于两个输入向量，大小等于将两个输入向量的大小相乘，然后乘以二者之间角度的正弦值。可以使用“左手规则”确定结果向量的方向。

### Project

- `public static Vector3 Project(Vector3 vector, Vector3 onNormal)`

返回`vector`在`onNormal`上的投影。此方法不过是重新缩放`onNormal`，以使其到达`vector`的投影。如果`onNormal`近乎为零，则该函数返回零向量。

### ProjectOnPlane

- `public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)`

返回`vector`在法线`planeOnNormal`确定的平面上的投影。此方法不过是重新缩放`onNormal`，以使其到达`vector`的投影。如果`onNormal`近乎为零，则该函数返回零向量。

### Reflect

- `public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)`

返回入射向量`inDirection`在法线`inNormal`定义的平面上反射的向量，结果是与`inDirection`大小相等、方向为其反射方向的向量。

### Max

- `public static Vector3 Max(Vector3 lhs, Vector3 rhs)`

返回由两个向量的最大分量组成的向量。

### Min

- `public static Vector3 Min(Vector3 lhs, Vector3 rhs)`

返回由两个向量的最小分量组成的向量。

### Scale

- `public static Vector3 Scale(Vector3 a, Vector3 b)`
- `public static void Scale(Vector3 scale)`

返回两个向量的分量相乘得到的向量。

### Lerp

- `public static Vector3 Lerp(Vector3 a, Vector3 b, float t)`

在向量`a`与`b`之间按`t`进行线性插值，等于`a+(b-a)*t`。参数`t`限制在范围[0,1]内，常用于查找起/终点之间特定百分比的点。当`t`为0时，返回`a`。当`t`为1时，返回`b`。当`t`为0.5时，返回`a`和`b`的中点。

### LerpUnclamped

- `public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)`

在向量`a`与`b`之间按`t`进行线性插值。参数`t`限制在范围[0,1]内，常用于查找起/终点之间特定百分比的点。当`t`为0时，返回`a`。当`t`为1时，返回`b`。当`t`为0.5时，返回`a`和`b`的中点。

### Slerp

- `public static Vector3 Slerp(Vector3 a, Vector3 b, float t)`

在`a`和`b`之间按`t`进行球形插值。球形插值与线性插值`Lerp`的区别在于，向量被视为方向而不是空间中的点。返回的向量的方向通过角度进行插值，其`magnitude`在`from`和`to`的大小之间进行插值。参数`t`限制在范围[0,1]内。

### SlerpUnclamped

- `public static Vector3 SlerpUnclamped(Vector3 a, Vector3 b, float t)`

在`a`和`b`之间按`t`进行球形插值。球形插值与线性插值`Lerp`的区别在于，向量被视为方向而不是空间中的点。返回的向量的方向通过角度进行插值，其`magnitude`在`from`和`to`的大小之间进行插值。此静态方法可以在`a`和`b`之外进行球形插值，即`t`可以小于0或大于1。

### MoveTowards

- `public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)`

将`current`移向`target`，但每次移动距离不超过`maxDistanceDelta`。可以平滑地将其移向目标。`maxDistanceDelta`为负值时会将`current`推离`target`。

### RotateTowards

- `public static Vector3 RotateTowards(Vector3 current, Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta)`

将`current`朝`target`方向旋转`maxRadiansDelta`的角度，但其将准确地落在目标上而不会超过目标。如果`current`和`target`的大小不同，则在旋转期间对结果大小进行线性插值。如果为`maxRadiansDelta`使用负值，则向量将朝远离`target`的方向旋转，直到它指向完全相反的方向，然后停止。

### SmoothDamp

- `public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed = Mathf.Infinity, float deltaTime = Time.deltaTime)`

随时间推移将一个向量逐渐改变为目标向量。向量通过某个类似于弹簧-阻尼的函数（它从不超过目标）进行平滑。

## 运算符

### 加+

- `public static Vector3 operator + (Vector3 a, Vector3 b)`

返回`a`+`b`的向量，即用`a`的每个分量加上`b`的每个分量，新向量的方向为两向量组成的平行四边形的同起点的对象方向。

### 减-

- `public static Vector3 operator - (Vector3 a, Vector3 b)`

返回`a`-`b`的向量，即用`a`的每个分量减去`b`的每个分量，新向量的方向为减向量指向被减向量。

- `public static Vector2 operator - (Vector3 a)`

对向量求反，即每个分量都求反。

### 乘*

- `public static Vector3 operator * (Vector3 a, float d)`
- `public static Vector3 operator * (float d, Vector3 a)`

将`a`的每个分量乘以数值`d`。

### 除/

- `public static Vector3 operator / (Vector3 a, float d)`

将`a`的每个分量除以数值`d`。

### 相等==

- `public static bool operator == (Vector3 lhs, Vector3 rhs)`

如果两个向量大致相等，则返回`true`。考虑到浮点值的不准确性，如果两个向量的差值小于1e-5，则认为它们是相等的。

### 不等!=

- `public static bool operator != (Vector3 lhs, Vector3 rhs)`

如果两个向量不相等，则返回`true`。