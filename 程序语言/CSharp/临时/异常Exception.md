# 概述

异常处理功能可处理程序运行时发生的任何意外或异常情况。**使用`try`、`catch`和`finally`关键字在可能出现异常的地方捕获、处理异常并释放资源，使用`throw`关键字引发异常。** 异常可以由公共语言运行时（CLR）、.NET或第三方库以及程序代码生成。

运行时错误可能由于各种原因而发生，但并非所有错误都应作为代码中的异常进行处理。下面是在运行时可能发生的一些错误类别，以及响应这些错误的适当方法：

- 使用错误是程序逻辑中的错误导致的异常。这些错误不应该通过异常处理来解决，而应该通过修改错误代码来解决。
- 程序错误是一种运行时错误，不能通过编写无Bug的代码来避免。在某些情况下，程序错误可能反映预期的错误情况如格式错误等，此时通常避免使用异常处理，而是重试该操作。
- 系统故障是运行时错误，无法以有效的方式以编程方式处理。

在许多情况下，异常可能不是由代码直接调用的方法引发的，而是由调用堆栈中更靠后的另一个方法引发的。**引发异常时，CLR将展开堆栈，查找具有特定异常类型的`catch`语句，并将执行找到的第一个`catch`语句。如果它调用堆栈中的任何位置找不到合适的`catch`语句，它将终止进程并显示异常消息。**

异常具有以下属性：

- 异常最终都派生自`System.Exception`。
- 在可能引发异常的地方使用`try`语句可以捕获异常。一旦`try`语句中发生异常，控制流将跳转到调用堆栈中任何位置的第一个关联的异常处理程序，使用`catch`关键字定义异常处理程序。
- **通常按异常类型从最具体到最不具体的顺序排序`catch`语句，符合条件的`catch`语句之后的`catch`不会被调用（异常筛选器）。** 此外，可以在异常筛选器后使用`where`筛选特定的`catch`语句。
- 可以`catch`语句中定义异常变量以获取更多详细信息，如调用堆栈的状态和异常文本说明。
- 在代码中可以使用`throw`关键字抛出一个异常。
- 无论是否引发异常，都会执行`finally`语句中的代码。使用`finally`语句释放`try`语句中的资源而无需等待GC回收对象，如文件流等。
- 如果异常没有相关异常处理程序，程序将停止执行并显示异常消息。
- 除非可以处理异常，否则不要捕获异常。

**引发或处理异常会消耗大量系统资源和执行时间。引发异常只是为了处理真正的异常情况，而不是为了处理可预测的事件或流控制。**

# 常用的.NET异常

| 异常 | 描述 |
| --- | --- |
| AccessViolationException | 尝试读取或写入受保护的内存 |
| AggregateException | 在应用程序执行期间发生的一个或多个错误 |
| ArgumentException | 传递给方法的非空参数无效 |
| ArgumentNullException | 传递给方法的参数为Null |
| ArgumentOutOfRangeException | 参数超出有效值的范围 |
| AuthenticationException | 身份验证流的身份验证失败 |
| BadImageFormatException | 动态链接库(DLL)或可执行程序的文件映像无效 |
| DirectoryNotFoundException | 没有发现目录 |
| DivideByZeroException | 除法运算的分母为0 |
| DriveNotFoundException | 驱动器不存在或不可用 |
| FileNotFoundException | 文件不存在 |
| FormatException | 值的格式不适合Parse将字符串转换为指定的类型 |
| IndexOutOfRangeException | 索引超出数组或集合的边界 |
| InvalidOperationException | 在当前对象状态下调用此方法无效 |
| KeyNotFoundException | 访问集合中成员的指定键不存在 |
| ObjectDisposedException | 对已释放的对象执行操作 |
| NotImplementedException | 此方法或操作未实现 |
| NotSupportedException | 不支持此方法或操作 |
| NullReferenceException | 空引用 |
| OperationCanceledException | 线程取消正在执行的操作 |
| OutOfMemoryException | 没有足够的内存继续执行程序 |
| OverflowException | 算术或强制类型转换会导致溢出 |
| PathTooLongException | 路径或文件名超过了系统定义的最大长度 |
| PlatformNotSupportedException | 当前平台不支持此操作 |
| RankException | 传递给方法的数组参数维度错误 |
| StackOverflowException | 执行堆栈超过堆栈大小 |
| TimeoutException | 超过分配给操作的时间间隔 |
| TypeLoadException | 类型加载失败 |
| UnauthorizedAccessException | 操作系统由于I/O错误或特定类型的安全错误而拒绝访问 |
| UriFormatException | 使用了无效的统一资源标识符（URI） |

# 自定义异常

自定义异常类时，应该继承`Exception`，并至少提供定义三个构造函数：一个无参数构造函数，一个设置`message`属性（人类可读的引发异常原因的说明），另一个同时设置`message`和`innerException`属性（子或内部异常）。

``` csharp
public InvalidDepartmentException() : base() { }
public InvalidDepartmentException(string message) : base(message) { }
public InvalidDepartmentException(string message, Exception inner) : base(message, inner) { }
```