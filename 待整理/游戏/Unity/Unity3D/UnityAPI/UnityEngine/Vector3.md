- 命名空间：`UnityEngine`
- 程序集：`UnityEngine.CoreModule`

`Vector3`用来表示三维的向量和点。

## 静态属性

### right

- `public static Vector3 right`

### left

- `public static Vector3 left`

### up

- `public static Vector3 Up`

### down

- `public static Vector3 down`

### forward

- `public static Vector3 forward`

### back

- `public static Vector3 back`

### zero

- `public static Vector3 zero`

### one

- `public static Vector3 one`

### positiveInfinite

- `public static Vector3 positiveInfinite`

### negativeInfinite

- `public static Vector3 NegativeInfinite`

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

## 构造函数

- `public Vector3(float, float, float)`
- `public Vector3(float, float)`

使用给定的分量构造新的三维向量，如果只指定两个分量，则`z`为0。

## 公共方法

### Equals

- `public bool Equals(Object)`

### Set

- `public void Set(float, float, float)`

### ToString

- `public string ToString()`
- `public string ToString(string)`
- `public string ToString(string, IFormatProvider)`

## 静态方法

### Normalize

- `public static Vector3 Normalize(Vector3)`

将向量单位化，使其模为1。此方法将更改向量。如果向量太小而无法标准化，则将其设置为零。

### OrthoNormalize

- `public static void OrthoNormalize(ref Vector3, ref Vector3)`
- `public static void OrthoNormalize(ref Vector3, ref Vector3, ref Vector3)`

将两向量标准化并使二者正交。

### Angle

- `public static float Angle(Vector3, Vector3)`

### SignedAngle

- `public static float SignedAngle(Vector3, Vector3, Vector3)`

获取两向量之间的带符号的逆时针角度，以度为单位。默认情况下，两向量角度指二者同一平面上的角度。可以指定两向量旋转方向，从而影响角度的符号。该方法返回两向量之间的最小角度而永远不会返回反射角，即结果范围在-180-180度之间。

### ClampMagnitude

- `public static Vector3 ClampMagnitude(Vector3, float)`

返回向量的副本，其大小被限制。

### Distance

- `public static float Distance(Vector3, Vector3)`

### Dot

- `public static float Dot(Vector3, Vector3)`

### Cross

- `public static Vector3 Cross(Vector3, Vector3)`

返回两向量的叉积。叉积结果为向量， 该向量垂直于两个输入向量，大小等于将两个输入向量的大小相乘，然后乘以二者之间角度的正弦值。可以使用“右手规则”确定结果向量的方向。

### Project

- `public static Vector3 Project(Vector3, Vector3)`

返回一向量在另一向量上的投影。此方法不过是重新缩放被投影向量，以使其到达投影向量的投影。如果被投影向量近乎为零，则该函数返回零向量。

### ProjectOnPlane

- `public static Vector3 ProjectOnPlane(Vector3, Vector3)`

返回投影向量在法线确定的平面上的投影。

### Reflect

- `public static Vector3 Reflect(Vector3, Vector3)`

### Max

- `public static Vector3 Max(Vector3, Vector3)`

### Min

- `public static Vector3 Min(Vector3, Vector3)`

### Scale

- `public static void Scale(Vector3)`
- `public static Vector3 Scale(Vector3, Vector3)`

### Lerp

- `public static Vector3 Lerp(Vector3, Vector3, float)`

### LerpUnclamped

- `public static Vector3 LerpUnclamped(Vector3, Vector3, float)`

### Slerp

- `public static Vector3 Slerp(Vector3, Vector3, float)`

在两向量之间进行球形插值。球形插值与线性插值`Lerp`的区别在于，向量被视为方向而不是空间中的点。返回的向量的方向通过角度进行插值，其`magnitude`在两向量的大小之间进行插值。参数插值步幅限制在范围[0,1]内。

### SlerpUnclamped

- `public static Vector3 SlerpUnclamped(Vector3, Vector3, float)`

此静态方法可以在两向量之外进行球形插值，即插值步幅可以小于0或大于1。

### MoveTowards

- `public static Vector3 MoveTowards(Vector3, Vector3, float)`

### RotateTowards

- `public static Vector3 RotateTowards(Vector3, Vector3, float, float)`

将初始向量朝目标向量方向旋转指定角度，但其将准确地落在目标上而不会超过目标。如果两向量的大小不同，则在旋转期间对结果大小进行线性插值。如果为角度插值步幅使用负值，则初始向量将朝远离目标向量的方向旋转，直到它指向完全相反的方向，然后停止。

### SmoothDamp

- `public static Vector3 SmoothDamp(Vector3, Vector3, ref Vector3, float, float maxSpeed = Mathf.Infinity, float deltaTime = Time.deltaTime)`

## 运算符

### 加+

- `public static Vector3 operator + (Vector3, Vector3)`

### 减-

- `public static Vector2 operator - (Vector3)`
- `public static Vector3 operator - (Vector3, Vector3)`

### 乘*

- `public static Vector3 operator * (Vector3, float)`
- `public static Vector3 operator * (float, Vector3)`

### 除/

- `public static Vector3 operator / (Vector3, float)`

### 相等==

- `public static bool operator == (Vector3, Vector3)`

### 不等!=

- `public static bool operator != (Vector3, Vector3)`
