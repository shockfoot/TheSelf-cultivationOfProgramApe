# Quaternion

所属命名空间`UnityEngine`，实现施于`UnityEngine.CoreModule`。

`Quaternion`用于表示旋转，其结构紧凑，无万向节死锁，可以轻松插值。Unity内部使用四元数表示所有旋转。

`Quaternion`基于复数，由4个实数定义（三个虚部x、y、z（两两正交）和一个实部w）。x、y和z表示矢量，即旋转轴，w是一个标量，用于存储围绕旋转轴进行的旋转。通常情况下，无法单独访问或修改单个四元数分量，只需要获取现有旋转，然后使用它们构造新的旋转。

Unity使用的是标准化的四元数。

## 静态属性

### identity

- `public static Quaternion identity`

单位旋转（只读）。该四元数对应于“no rotations”，即与世界轴或父轴完全对齐。

## 属性

### eulerAngles

- `public vector3 eulerAngles`

返回或设置旋转的欧拉角表示。可以通过设置此属性来设置四元数的旋转，并且可以通过读取此属性来读取四元数的欧拉角表示的值。设置旋转时，虽然提供X、Y和Z旋转值描述旋转，但是这些值不存储在旋转中。而是将X、Y和Z值转换为四元数的内部格式。读取旋转时，将四元数的内部旋转表示形式转换为欧拉角。因为可通过多种方式使用欧拉角表示任何给定旋转（欧拉角对某一旋转表示不唯一），所以读出的值可能与分配的值截然不同。如果尝试逐渐增加值以生成动画，则这种情况可能会导致混淆。

欧拉角可以通过围绕各个轴执行三个单独的旋转来表示三维旋转。在 Unity 中，围绕Z轴、X轴和Y轴（按该顺序）执行旋转。

### normalized

- `public Quaternion normalized`

获取归一化后的四元数（只读）。归一化后四元数的方向不变，量值为1。当前四元数保持不变，返回一个新的归一化四元数。如果四元数太小而无法归一化，则会返回`identity`。

### this[int]

- `public float this[int]`

分别使用[0]、[1]、[2]、[3]访问四元数x、y、z、w分量。

### x

- `public float x`

四元数的x分量。不能直接修改四元数。

### y

- `public float y`

四元数的y分量。不能直接修改四元数。

### z

- `public float z`

四元数的z分量。不能直接修改四元数。

### w

- `public float w`

四元数的w分量。不能直接修改四元数。

## 构造函数

- `public Quaternion(float x, float y, float z, float w)`

使用给定的x、y、z、w分量构造新的四元数。

## 公共方法

### ToAngleAxis

- `public void ToAngleAxis(out float angle, out Vector3 axis)`

将四元数旋转转换为轴角表示，角单位为度。

### Set

- `public void Set(float newX, float newY, float newZ, float newW)`

设置四元数的x、y、z、w分量。

### SetFromToRotation

- `public void SetFromToRotation(Vector3 fromDirection, Vector3 toDirection)`

创建从`fromDirection`旋转到`toDirection`的旋转，并将结果设置到四元数。必须在脚本中设置这些`fromDirection`和`toDirection`。

### SetLookRotation

- `public void SetLookRotation(Vector3 view, Vector3 up = Vector3.up)`

使用指定的`veiw`和`up`方向创建旋转，并将结果设置到四元数。如果用于对`Transform`进行定向，Z轴将与`view`对齐，Y轴与`up`对齐（假定这些向量是正交的）。如果前进方向为零，则记录错误。

### ToString

- `public string ToString()`
- `public string ToString(string format)`
- `public string ToString(string format, IFormatProvider formatProvider)`

将四元数转换为格式化字符串。默认显示五位小数（`format="F5"`）。

## 静态方法

### Angle

- `public static float Angle(Quaternion a, Quaternion b)`

返回两个旋转`a`和`b`之间的角度（以度为单位）。

### Dot

- `public static float Dot(Quaternion a, Quaternion b)`

返回两个旋转之间的点积。

### Inverse

- `public static Quaternion Inverse(Quaternion rotation)`

返回`rotation`的反转。

### Normalize

- `public static Quaternion Normalize(Quaternion q)`

将四元数单位化，即方向不变，量值为1。此方法将更改当前四元数。如果该四元数太小而无法归一化，则将其设置为`Quaternion.identity`。

### Euler

- `public static Quaternion Euler(float x, float y, float z)`
- `public static Quaternion Euler(Vector3 euler)`

将欧拉角表示的旋转（绕z轴旋转z度，绕x轴旋转x度，绕y轴旋转y度，按该顺序应用）转换为四元数表示的旋转。

### AngleAxis

- `public static Quaternion AngleAxis(float angle, Vector3 axis)`

创建并返回一个围绕`axis`旋转`angle`度的旋转。

### FromToRotation

- `public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)`

创建一个从`fromDirection`到`toDirection`的旋转。

### Lerp

- `public static Quaternion Lerp(Quaternion a, Quaternion b, float t)`

返回在`a`和`b`之间按`t`进行插值并进行标准化处理的结果。参数`t`被限制在[0,1]范围内。该方法比`Slerp`快，但如果旋转相距很远，其视觉效果也更糟糕。

### LerpUnclamped

- `public static Quaternion LerpUnclamped(Quaternion a, Quaternion b, float t)`

返回在`a`和`b`之间按`t`进行插值并进行标准化处理的结果。参数`t`不受限制。该方法比`Slerp`快，但如果旋转相距很远，其视觉效果也更糟糕。

### Slerp

- `public static Quaternion Slerp(Quaternion a, Quaternion b, float t)`

返回四元数`a`与`b`之间按`t`的球形插值。参数`t`被限制在[0,1]范围内。

### SlerpUnclamped

- `public static Quaternion SlerpUnclamped(Quaternion a, Quaternion b, float t)`

返回四元数`a`与`b`之间按`t`的球形插值。参数`t`不受限制。

### LookRotation

- `public static Quaternion LookRotation(Vector3 forward, Vector3 upwards = Vector3.up)`

使用指定的`forward`和`upwards`创建旋转。z轴将与`forward`对齐，x轴将与`forward`和`upwards`的叉积对齐，y轴将与z轴和x轴的叉积对齐。如果`forward`为0，则返回`identity`。

如果`forward`和`upwards`是colinear，或者`upwards`的模长为零，`LookRotation`的结果与`fromDirection`为(0,0,1)、`toDirection`为`forwards`的单位向量时的`FromToRotation`的结果相同。

### RotateTowards

- `public static Quaternion RotateTowards(Quaternion from, Quaternion to, float maxDegreeDelta)`

将`from`四元数朝`to`旋转`maxDegreesDelta`的角度步长（但不会超过）。如果`maxDegreesDelta`为负值，则向远离`to`的方向旋转，直到旋转恰好为相反的方向。

### 运算符

### 乘*

- `public static Quaternion operator * (Quaternion lhs, Quaternion rhs)`

将旋转`lhs`和`rhs`组合到一起。，其效果为：先应用`lhs`，然后相对于`lhs`旋转生成的参考帧应用`rhs`，这意味着旋转不满足交换律。

- `public static Vector3 operator * (Quaternion rotation, Vector3 point)`

对`point`应用旋转`rotation`。

### 相等==

- `public static bool operator == (Quaternion lhs, Quaternion rhs)`

通过判断两个四元数的点积是否接近1.0来判断两个四元数是否相等。由于四元数最多能够表示旋转两周（720 度）的范围， 因此即使最终旋转看起来相同，该比较也可能返回`false`。