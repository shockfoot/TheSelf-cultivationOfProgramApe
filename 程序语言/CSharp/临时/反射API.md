反射相关命名空间`System`和`System.Reflection`，相关类有：

- `System.Type`：表示类型。
- `System.RuntimeType`
- `System.AppDomain`
- `System.Activator`
- `System.Reflection.Assembly`：定义和加载程序集，加载在程序集中的所有模块以及从此程序集中查找类型并创建该类型的实例。
- `System.Reflection.Module`：获取包含模块的程序集以及模块中的类等，还可以获取在模块上定义的所有全局方法或其他特定的非全局方法。
- `System.Reflection.ConstructorInfo`：获取构造函数的名称、参数、访问修饰符（如pulic 或private）和实现详细信息（如abstract或virtual）等。
- `System.Reflection.FieldInfo`：获取字段的名称、访问修饰符（如public或private）和实现详细信息（如static）等，并获取或设置字段值。
- `System.Reflection.ParameterInfo`：获取参数的名称、数据类型、是输入参数还是输出参数，以及参数在方法签名中的位置等。
- `System.Reflection.MethodInfo`：获取方法的名称、返回类型、参数、访问修饰符（如pulic 或private）和实现详细信息（如abstract或virtual）等。
- `System.Reflection.PropertyInfo`：获取属性的名称、数据类型、声明类型、反射类型和只读或可写状态等，获取或设置属性值。
- `System.Reflection.EventInfo`：获取事件的名称、事件处理程序数据类型、自定义属性、声明类型和反射类型等，添加或移除事件处理程序。
- `System.Reflection.MemberInfo`：获取字段、事件、属性等各种信息。
- `System.Reflection.CustomAttributeData`：获取自定义特性的信息。

# Type

`System.Type`表示声明的类型：类类型、接口类型、数组类型、值类型、枚举类型、类型参数、泛型类型定义、构造的泛型类型。`System.Type`是线程安全的；多个线程可以并发地访问`Type`实例。

`Type`是反射系统的核心，是访问元数据的主要方式。CLR在反射请求时为加载的类型创建`Type`。通过`Type`可以获取相关类型声明、类型成员（例如构造函数、方法、字段、属性和事件）以及类的模块和程序集信息。

获取`Type`对象：

- 使用`typeof`操作符获取指定类的类型对象。
- 通过某对象的`Object.GetType`实例方法获取该类的类型对象。
- `Type.GetType`静态方法可以获取指定**完全限定名**（Fully Qualified Name）的类型对象。
- `Module.GetType`实例方法获取指定**完全限定名**的类型对象；`Module.GetTypes`实例方法获取模块中定义的所有公共或私有类型的类型对象；`Module.FindTypes`实例方法获取符合筛选条件的所有类型对象。
- `Assembly.GetType`实例方法获取指定**完全限定名**的类型对象；`Assembly.GetTypes`实例方法获取程序集中定义的所有公共或私有类型的类型对象；`Assembly.GetExportedTypes`实例方法获取程序集中定义的、外部可见的所有类型对象。
- `Type.MakeArrayType`实例方法获取当前类型的数组类型对象，`Type.MakeByRefType`实例方法获取当前类型进行引用传递时的类型对象，`Type.MakeGenericType`实例方法获取当前泛型类型替换类型参数后的泛型类型对象，`Type.MakePointerType`实例方法获取指向当前类型的指针类型对象。

获取程序集相关信息：

- `AssemblyQualifiedName`属性获取所属程序集限定名称（含命名空间的类型名加程序集显示名称）。
- `NameSpace`属性获取所属命名空间。
- `FullName`属性获取类型的完全限定名（包含命名空间但不包含程序集）。
- `Name`属性获取成员的简单名。
- `Assembly`属性获取所属程序集。对于泛型类型，获取其泛型定义所在程序集，而不是创建和使用特性泛型类型的程序集。
- `Moudle`属性获取所属模块。

获取成员信息：

- 获取特性：`Attributes`、`CustomAttributes`属性，`GetCustomAttributes`、`GetCustomAttributesData`方法。
- `BaseTyoe`属性获取直接继承的基类类型。
- 获取继承或实现的接口：`FindInterfaces`、`GetInterface`、`GetInterfaces`方法。
- 获取构造方法：`GetConstructor`、`GetConstructors`方法、
- 获取枚举信息：`GetEnumName`、`GetEnumNames`、`GetEnumValues`、`GetEnumUnderlyingType`、`GetEnumValuesAsUnderlyingType`、`IsEnumDefined`方法。
- 获取事件：`GetEvent`、`GetEvents`方法。
- 获取字段：`GetField`、`GetFields`方法。
- 获取方法：`GetMethod`、`GetMethods`方法。
- 获取属性：`GetProperty`、`GetProperties`方法。
- 获取成员：`GetMember`、`FindMembers`、`GetMembers`方法。

调用成员：`InvokeMember`方法。

检查类型信息：`ContainsGenericParameters`，`DeclaringMethod`，`DeclaringType`，`IsAbstrac`、`IsClass`、`IsEnum`、`IsInterface`、`IsPrimitive`、`IsValueType`、`IsSubclassOf`。

检查泛型信息：`GenericParameterPosition`、`GenericTypeArguments`、`IsGenericMethodParameter`、`IsGenericParameter`、`IsGenericType`、`IsGenericTypeDefinition`、`IsGenericTypeParameter`。

检查访问级别：`IsPublic`、`IsNotPublic`、`IsSealed`、`IsNested`、`IsVisible`、`IsNestedAssembly`、`IsNestedFamANDAssem`、`IsNestedFamily`、`IsNestedFamORAssem`、`IsNestedPrivate`、`IsNestedPublic`。

`Type`对象支持引用性相等的比较。