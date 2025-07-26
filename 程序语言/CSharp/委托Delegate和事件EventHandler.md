# 概述

| 类名 | 说明 |
| --- | --- |
| `Delegate` | 所有委托的基类。 |
| `MulticastDelegate` | 多播委托。 |
| `EventArgs` | 事件参数基类。 |
| `EventHandler` | 无参数事件委托。 |
| `EventHandler<TEventArgs>` | 有参数事件委托。 |
| `Action` | 内置的可以具有0-16个参数、无返回值的泛型委托。 |
| `Function<TResult>` | 内置的可以具有0-16个参数、1个返回值的泛型委托。 |

# Delegate

```csharp
public abstract class Delegate : ICloneable, System.Runtimer.Serialization.ISeriblizable
```

`Delegate`类是所有委托的基类，不能显示派生出自定义类。只有系统和编译器才能从`Delegate`类或`MlticastDelegate`类显示派生委托。`Delegate`类不被视为委托类型，它是用于派生委托类型的类。

CLR为所有`Delegate`类提供了`Invoke()`方法（以方便调用或反射）、`BeginInvoke()`和`EndInvoke()`以异步调用委托。

`Delegate`类有`Method`和`Target`两个属性。`Method`属性存储委托引用的方法信息。`Target`属性指向委托引用的对象，如果是静态方法则为`null`；对于多播委托，`Target`属性返回调用列表中最后一个方法的对象。

`Delegate`类可以通过`Clone()`方法浅拷贝自身、`DynamicInvoke()`和`DynamicInvokeImpl()`动态调用自身、`Equal()`相等性判断、`GetInvocationList()`获取自身调用列表（结果中每个委托仅表示一个方法且顺序与调用列表相同）、`GetMethodImpl()`实现`Method`属性、`GetMethodInfo()`获取自身方法信息（拓展实现）、`CombineImpl()`添加委托到自身调用列表末尾、`RemoveImpl()`移除自身调用列表中符合的调用列表（只移除最后一个匹配，如果）。

`Delegate`类提供了静态方法，包括`CreateDelegate()`创建委托、`Combine()`合并委托的调用列表、`Remove()`移除委托的调用列表（只移除最后一个匹配，如有）、`RemoveAll()`移除委托的调用列表（移除所有匹配项）。

此外，`Delegate`类实现了相等性和不等性判断：**具有相同目标、方法和调用列表的同类型委托才相等。** 如果委托类型不同则不相同；对于静态方法的委托，如果是同一类的相同方法，则两委托相等；对于实例方法的委托，如果是同一对象的相同方法，则两委托相等；对于多播委托，如果调用列表顺序相同且对应每个元素表示相同的方法和目标时才相等。

# MulticastDelegate

``` csharp
public abstract class MulticastDelegate : Delegate
```

`MulticastDelegate`类表示多播委托。使用`delegate`关键字声明的委托都继承自`MulticastDelegate`类。与`Delegate`类相同，`MulticastDelegate`类也不能显示派生出自定义类。

`MulticastDelegate`类具有链式的委托列表，称为**调用列表**，由一个或多个元素组成。 调用多播委托时，调用列表中的委托按显示顺序同步调用。 如果在执行列表期间发生错误，则会引发异常。

# EventArgs

``` csharp
public class EventArgs
```

`EventArgs`类是用于封装事件参数的类。自定义事件参数类应派生自`EventArgs`类，名称应以EventArgs结尾并提供用于存储必要数据的属性。

`EventArgs`类提供了一个不包含任何数据的事件参数类对象`Empty`。

# EventHandler

``` csharp
public delegate void EventHandler(object? sender, EventArgs e);
```

`EventHandler`类表示处理无参数的事件委托。对于具有参数的事件，推荐使用`EventHandler<TEventArgs>`泛型类。

# EventHandler&lt;TEventArgs&gt;

``` csharp
public delegate void EventHandler<TEventArgs>(object? sender, TEventArgs e);
```

`EventHandler<TEventArgs>`类表示处理有参数的事件委托。

# Action

`Action`是C#内置的可以具有0-16个参数、无返回值的泛型委托。

# Function

`Function<TResult>`是C#内置的可以具有0-16个参数、1个返回值的泛型委托