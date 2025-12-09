- 命名空间：`System`
- 程序集：`System.Runtime.dll`

`public class Object`

`Object`是所有.NET类的最终基类，是类型层次结构的根。通常不需要声明继承`Object`，因为是隐式继承的。

## 构造函数

`public Object()`

## 普通方法

### Equals

确定两对象是否相等。

- `public virtual bool Equals(Object)`
- `public static bool Equals(Object, Object)`

类型重载相等运算符时，还必须重写`Equals(Object)`以提供相同的功能，这通常是使用重载的相等运算符实现。

如果当前实例是引用类型，则`Equals(Object)`等效于`ReferenceEquals`，比较实例是否引用同一对象。如果当前实例是值类型，则比较值相等性：类型是否相同、公共和私有字段是否相等。

派生类经常重写`Equals(Object)`以实现值相等性。此外，类型还经常通过实现`IEquatable`接口为`Equals`提供额外的强类型重载。

自定义类型将继承`Equals`功能。对于值类型，应始终重写`Equals`，因为依赖于反射的相等性测试会降低性能。对于引用类型，重写`Equals`可以测试值相等性而不是引用相等性。

重写`Equals(Object)`时，请遵循以下准则：

- 实现`IComparable`接口的类型必须重写`Equals(Object)`；
- 重写`Equals(Object)`的类型还必须重写`GetHashCode`，否则，哈希表可能无法正常工作；
- 应考虑实现`IEquatable`接口以支持强类型测试的相等性，且应返回与`Equals`一致的结果；
- 重载给定类型的相等运算符必须重写`Equals(Object)`方法，以返回与相等运算符相同的结果，有助于确保使用`Equals`的类库代码的行为方式与应用程序代码使用相等运算符的方式一致。

引用类型重写`Equals(Object)`准则：

- 如果类型的语义基于类型表示某些值的事实，请考虑重写`Equals`；
- 大多数引用类型不得重载相等运算符，即使重写了`Equals`。但如果要实现具有值语义的引用类型则必须重写相等运算符；
- 不应在可变引用类型上重写`Equals`，因为重写`Equals`需要同时重写`GetHashCode`，这意味着可变引用类型的实例的哈希代码在其生存期内可能会更改，将会导致对象在哈希表中丢失。

值类型重写`Equals(Object)`准则：

- 包含引用类型字段的值类型应重写`Equals(Object)`，因为值类型的`Equals(Object)`对字段都是值类型的值类型执行逐字节比较，而对字段包含引用类型的值类型执行逐字段比较；

- 重写`Equals`必须重载相等运算符；

- 应实现`IEquatable`接口以避免装箱。

### Finalize

  在对象被回收前允许其尝试释放资源并执行其他清理操作。

  - `~Object()`

  该方法用于在销毁对象之前对当前对象持有的非托管资源执行清理操作。此方法受到保护，因此只能通过此类或派生类进行访问。

### GetHashCode

获取对象的哈希值。

- `public virtual int GetHashCode()`

两个相等的对象返回相同的哈希值，但返回相同的哈希值不代表两对象相等，因为不同的对象可以具有相同的哈希值。此外，在不同版本的.NET或平台上返回的哈希值可能不同。并且，哈希值不是永久值。因此：

- 不能序列化哈希值或将其存储在数据库中；
- 不能使用哈希值作为键从键控集合中检索对象；
- 不能跨应用程序域或进程发送哈希值；
- 不能通过哈希值的相等性确定两个对象是否相等。

如果引用类型未重写`GetHashCode`，则通过调用`Object.GetHashCode`来计算哈希值；该方法基于对象的引用计算哈希值，即两个引用同一实例的对象具有相同的哈希值。如果值类型未重写`GetHashCode`，则通过调用`ValueType.GetHashCode`来计算哈希值；该方法使用反射基于字段的值计算哈希值，即字段具有相等值的值类型具有相等哈希代码。

重写`GetHashCode`的派生类还必须重写`Equals(Object)`以确保两个相等的对象具有相同的哈希值，否则哈希表可能无法正常工作。

### GetType

获取对象的类型。

- `public Type GetType()`

 .NET可识别以下五类类型：

- 派生自`System.Object`的类；
- 值类型；
- 接口；
- 枚举；
- 委托。

### MemberwiseClone

创建对象的浅拷贝副本。

- `protected object MemberwiseClone()`

该方法通过创建新对象，然后将当前对象的非静态字段复制到新对象来创建浅表副本。如果字段是值类型，则复制值；如果字段是引用类型，则会复制引用；因此，原始对象及其浅拷贝副本引用同一对象。

深拷贝可以通过多种方式实现：

- 当对象的构造函数可对其全字段初始化时，可以通过调用构造函数来创建深拷贝副本；
- 调用`MemberwiseClone`创建一个浅拷贝副本，然后将原始对象中引用类型的任何属性或字段的**新**对象分配给副本；
- 将对象序列化为深拷贝副本，然后将序列化的数据还原到其他对象变量；
- 将反射与递归一起使用来执行深拷贝操作。

### ToString

获取对象的字符串形式，默认返回对象的完全限定类型名称。

- `public virtual string ToString()`

重写`ToString`以提供更合适的特定类型的字符串表示形式；重载`ToString`以提供对格式字符串或区域性敏感的格式设置的支持。

## 静态方法

### ReferenceEquals

确定两对象是否指向同一实例。

- `public static bool ReferenceEquals(Object, Object)`

在比较值类型时，即使两对象相等，但在比较过程中进行了装箱，所以会返回`false`。在比较字符串时，比较二者在字符串池中的引用。