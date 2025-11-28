# 语句

程序操作使用**语句**进行表示。Python支持几种不同的语句，其中许多语句是从嵌入语句的角度来定义的。

- 代码块：一组相同缩进的语句。
- 声明语句：用于声明局部变量和常量。
- 表达式语句：用于计算表达式，包括方法调用、赋值等。
- 选择语句：用于根据一些表达式的值从多个可能的语句中选择一个以供执行，如`if`和`match`。
- 循环语句：用于重复执行嵌入语句，如`while`和`for`。
- 跳转语句：用于转移控制权，如`break`、`continue`、`return`、`goto`、`throw`和`yield`。
- 异常处理语句：用于捕获并处理在代码块执行期间发生的异常，如`try`和`finally`。

## 选择语句

选择语句也叫分支语句，可以根据表达式的值从许多可能的路径中选择要执行的语句。`if`语句根据布尔表达式的值来选择要执行的语句，`match`语句根据与表达式匹配的模式在语句列表中选择要执行的语句。

### if

`if`语句有三种形式：不包含`else`部分的`if`语句仅在布尔表达式计算结果为`true`时执行其主体；包含`elif`和`else`部分的`if`语句根据布尔表达式的值选择一个分支来执行。

``` python
if condition_1:
    statement_block_1
elif condition_2:
    statement_block_2
else:
    statement_block_3
```

每个条件后面要使用冒号`:`，表示接下来是满足条件后要执行的语句块。可嵌套`if`语句来检查多个条件。**如果语句块中只有一条语句时，可以跟在`:`后面写在同一行。**

### match

Python 3.10增加了`match-case`的条件判断。`match`语句根据与表达式的模式在语句列表中选择要执行的语句。`match`语句按文本顺序从上到下对`case`进行匹配，若匹配成功则进入相应`case`。`_`指定匹配表达式与其他任何`case`都不匹配时要执行的语句。如果匹配表达式与任何`case`都不匹配，且没有`default`，控制就会贯穿`switch`语句。

一个`case`也可以设置多个匹配条件，条件使用`|`隔开

``` python
match subject:
    case <pattern_1>:
        <action_1>
    case <pattern_2>:
        <action_2>
    case <pattern_3> | <pattern_4>:
        <action_3>
    case _:
        <action_wildcard>
```

## 循环语句

循环语句用于重复执行一条语句或代码块，通常包括四个部分：**声明并初始化循环控制变量、循环条件表达式、循环体、迭代操作**。

- 通常需要声明并初始化循环控制变量。
- 循环条件表达式是一个布尔表达式，用于确定是否应执行循环中的下一个迭代。若该表达式计算结果为`True`或不存在，执行下一个迭代，否则退出循环。
- 循环体是循环执行的操作，可包含一条语句或一个代码块。
- 迭代操作是在每此循环体执行后将执行的操作，用于改变循环控制变量从而控制是否该退出循环。

在循环语句里循环体中的任何位置都可以**使用`break`语句中断循环，或者使用`continue`语句继续执行循环中的下一次迭代**。

### while

在循环条件表达式的计算结果为`True`时，`while`语句会执行循环体。**由于在每次计算此表达式之后才执行循环体，所以`while`循环体会执行0次或多次。** 应在`while`循环体中改变循环控制变量。

``` python
i = 0
while (i < 5)
{
    i += 1
}
```

在`while`后面添加`else`时，可在循环正常结束（非`break`中断）时则执行`else`的语句块。

```python
while <expr>:
    <statement(s)>
else:
    <additional_statement(s)>
```

### for

`for`循环可以遍历任何可迭代对象，如一个列表或者一个字符串。

``` python
for <variable> in <sequence>:
    <statements>
else:
    <statements>
```

`range()`函数可以生成数列。

``` python
range(5) # 0 1 2 3 4
range(5,9) # 5 6 7 8
range(0,10,3) # 0 3 6 9
range(-10,-100,-30) # -10 -40 -70
```

## 跳转语句

跳转语句可以无条件地转移程序控制。

### break

`break`**终止最近的封闭循环语句**（即`for`、`while`），并将控制权转交给已终止语句后面的语句（若有）。

在嵌套循环中，`break`仅终止包含它的最内部循环。

如果通过`break`中断了循环，对应循环的`else`将不执行。

### continue

`continue`**中止当前轮的循环**，开始最近的封闭循环语句（即`for`、`while`）新一轮的循环。

### return

`return`**终止其所在的方法**，并将控制权和方法结果（若有）返回给调用方。如果方法无返回值，则使用不带表达式的`return`语句，无`return`语句时在执行完最后一条语句后终止。

如果`return`语句具有表达式，该表达式必须可隐式转换为函数成员的返回类型，除非它是**异步的**。对于`async`函数，表达式必须可隐式转换为`Task<TResult>`或`ValueTask<TResult>`类型，以函数的返回类型为准。如果`async`函数的返回类型为`Task`或`ValueTask`，则使用不带表达式的`return`语句。

默认情况下，`return`语句返回表达式的值。从C# 7.0开始，可以使用带`ref`关键字的`return`语句返回对变量的引用。

``` csharp
ref int FindFirst(int[] numbers, Func<int, bool> predicate)
{
    for (int i = 0; i < numbers.Length; i++)
    {
        if (predicate(numbers[i]))
        {
            return ref numbers[i];
        }
    }
    throw new InvalidOperationException("No element satisfies the given condition.");
}
```

## 异常处理语句

使用`try`语句在可能出现异常的地方捕获异常，`catch`语句处理异常，`finally`语句释放资源，`throw`关键字引发异常。

### throw

引发程序执行期间出现异常的信号。然后方法调用方使用`try-catch`或`try-catch-finally`块来处理引发的异常。

``` csharp
// 语法
throw [e]; // e是一个派生自System.Exception类的实例

public class NumberGenerator
{
   int[] numbers = { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };
   public int GetNumber(int index)
   {
      if (index < 0 || index >= numbers.Length)
      {
         throw new IndexOutOfRangeException();
      }
      return numbers[index];
   }
}
```

`throw`也可以用于`catch`块，以重新引发在`catch`块中处理的异常。此时，`throw`不使用异常操作数。

``` csharp
public char GetFirstCharacter()
{
    try
    {
        return Value[0];
    }
    catch (NullReferenceException e)
    {
        throw;
    }
}
```

从C# 7.0开始，`throw`可以用作表达式和语句，这允许在上下文中引发异常。

``` csharp
// 在C# 7.0之前，此逻辑需在if/else中实现
string arg = args.Length >= 
    1 ? args[0] : throw new ArgumentException("You must supply an argument");
```

### try-catch

try-catch语句为后接一或多个`catch`子句的`try`块，这些子句指定不同异常的处理程序。`try`块包含可能导致异常的受保护的代码。

引发异常时，公共语言运行时（CLR）查找处理此异常的`catch`语句。如果当前正在执行的方法不包含此类`catch`块，则CLR查看调用了当前方法的方法，并以此类推遍历调用堆栈。如果未找到任何`catch`，则CLR向用户显示一条未处理的异常消息，并停止执行程序。

不推荐使用不带参数`catch`子句来捕获任何类型的异常。**通常只应捕获知道如何从其恢复的异常。** 因此，应始终指定派生自`System.Exception`的对象参数。异常类型应尽可能具体，以避免不正确地接受异常处理程序实际上无法解决的异常。因此，最好是在`Exception`基类型上使用具体的异常。

``` csharp
// 处理异常
int[] gen = new int[] { 1 };
try
{
    int value = gen.GetNumber(index);
    Console.WriteLine($"Retrieved {value}");
}
catch (IndexOutOfRangeException e)
{
    Console.WriteLine($"{e.GetType().Name}: {index} is outside the bounds of the array");
}
// Output: IndexOutOfRangeException: 10 is outside the bounds of the array
```

可以使用同一try-catch语句中的多个特定`catch`子句。在这种情况下，`catch`子句的顺序很重要，因为`catch`子句是按顺序检查的。在**使用更笼统的子句之前获取更细节的异常**。如果`catch`块的排序使得永不会达到后面的`catch`块，则编译器将产生错误。

``` csharp
try
{
    string s = null;
    ProcessString(s);
}
// Most specific:
catch (ArgumentNullException e)
{
    Console.WriteLine("{0} First exception caught.", e);
}
// Least specific:
catch (Exception e)
{
    Console.WriteLine("{0} Second exception caught.", e);
}
```

筛选想要处理的异常的一种方式是使用`catch`参数。也可以使用异常约束进一步检查该异常以决定是否要对其进行处理。如果异常约束返回`false`，则继续搜索处理程序。

``` csharp
catch (InvalidCastException e)
{
    if (e.Data == null)
    {
        throw;
    }
    else
    {
        // Take some action.
    }
}
// 等价于
catch (InvalidCastException e) when (e.Data != null)
{
    // Take some action.
}
```

异常约束要优于捕获和重新引发（如下所述），因为约束将保留堆栈不受损坏。如果之后的处理程序转储堆栈，可以查看到异常的原始来源，而不只是重新引发它的最后一个位置。异常约束表达式的一个常见用途是日志记录。可以创建一个始终返回`false`并输出到日志的异常约束，能在异常通过时进行记录，且无需处理并重新引发它们。

可在`catch`块中使用`throw`语句以重新引发已由`catch`语句捕获的异常。此时，`throw`不使用异常操作数。可以捕获一个异常而引发一个不同的异常。执行此操作时，请指定作为内部异常捕获的异常。

``` csharp
catch (InvalidCastException e)
{
    // Perform some action here, and then throw a new exception.
    throw new YourCustomException("Put your error message here.", e);
}
```

在`try`块内，仅能初始化在其内部声明的变量；否则，在完成执行块之前，可能会出现异常。

### try-finally

通过使用`finally`块，可以清除`try`块中分配的任何资源，即使在`try`块中发生异常，也可以运行代码。通常情况下，`finally`块的语句会在控制离开`try`语句时运行。

已处理的异常中会保证运行相关联的`finally`块。但是，如果异常未经处理，则`finally`块的执行将取决于异常解除操作的触发方式。反过来，这又取决于计算机的设置方式。只有在`finally`子句不运行的情况下，才会涉及程序被立即停止的情况。

通常情况下，当未经处理的异常终止应用程序时，`finally`块是否运行已不重要。但是，如果`finally`块中的语句必须在这种情况下运行，则可以将`catch`块添加到`try-finally`语句。另一种解决方法是，可以捕获可能在调用堆栈上方的`try-finally`语句的`try`块中引发的异常。可以通过以下几种方法来捕获异常：调用包含`try-finally`语句的方法、调用该方法或调用堆栈中的任何方法。如果未捕获异常，则`finally`块的执行取决于操作系统是否选择触发异常解除操作。

### try-catch-finally

使用`try-catch-finally`语句来处理在`try`块执行期间可能发生的异常，并指定当控制离开`try`语句时必须执行的代码。当异常由`catch`块处理时，`finally`块在该`catch`块执行之后执行（即使在执行`catch`块期间发生另一个异常）。

## 其他语句

`pass`语句是空语句，用于保持程序结构的完整性，一般用作占位语句。

``` python
while True:
    pass

class MyEmptyClass:
    pass
```
