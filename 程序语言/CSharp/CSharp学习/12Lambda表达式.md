使用Lambda表达式来创建匿名函数。使用Lambda声明运算符`=>`从其主体中分离Lambda参数列表。Lambda表达式分表达式和语句两种形式。若要创建Lambda表达式，需要在Lambda运算符左侧指定输入参数（如果有），然后在另一侧输入表达式或语句块。`delegate`关键字可以省略。

任何Lambda表达式都可以转换为委托类型。Lambda表达式可以转换的委托类型由其参数和返回值的类型定义。Lambda表达式还可以转换为表达式树类型。

``` C#
// 表达式形式
delegate (input-parameters) => expression
// 语句形式
delegate (input-parameters) => { <sequence-of-statements> }
```

位于表达式运算符`=>`右侧的Lambda表达式称为“表达式Lambda”。表达式Lambda会返回表达式的结果。表达式Lambda的主体可以包含方法调用。不过，若要创建在.NET公共语言运行时CLR的上下文之外计算的表达式树，则不得在Lambda表达式中使用方法调用。在CLR上下文之外，方法将没有任何意义。

位于表达式运算符`=>`右侧的Lambda语句称为“语句Lambda”，被一组大括号`{}`包裹。语句Lambda的主体可以包含任意数量的语句，但通常不会多于两个或三个，当主体只有一条语句时可以省略大括号和`return`，编译器会自动添加。不能使用语句Lambda创建表达式树。

``` C#
Func<int, int, bool> plus = (x, y) => { return x + y; };
// 进一步简化
Func<int, int, bool> plus = (x, y) => x + y;
```

Lambda表达式的输入参数被`()`包裹。使用空括号指定零个输入参数。如果只有一个输入参数，则括号可省略。多个输入参数以`,`分隔。编写Lambda表达式时，通常不必为输入参数指定类型，因为编译器可以根据表达式主体、参数类型以及C#语言规范中描述的其他因素来推断类型。有时，编译器无法推断输入参数的类型，可显式指定类型。输入参数类型必须全部为显式或全部为隐式，否则便会生成CS0748错误。从C# 9.0开始，可以使用**弃元**指定Lambda表达式中不使用的两个或更多输入参数。

``` C#
// 零个参数时使用空括号
Action line = () => Console.WriteLine();
// 一个参数时可以省略参数括号
Func<double, double> cube = x => x * x * x;
// 多参数以','分隔
Func<int, int, bool> testForEquality = (x, y) => x == y;
// 显示指定类型
Func<int, string, bool> isTooLong = (int x, string s) => s.Length > x;
// 弃元
Func<int, int, int> constant = (_, _) => 42;
```

通常，Lambda表达式的返回类型是显而易见的并且是推断出来的。但对于某些表达式，编译器可能无法推断返回类型。从C# 10开始，可以在输入参数前面指定Lambda表达式的返回类型。指定显式返回类型时，必须将输入参数括起来。

``` C#
var choose = (bool b) => b ? 1 : "two"; // 无法推断返回类型
// 显示指定返回类型
var choose = object (bool b) => b ? 1 : "two";
```

从C# 10开始，可以将属性添加到Lambda表达式、其参数或返回值。将属性添加到Lambda表达式或其参数时，必须将输入参数括起来。

``` C#
Func<string, int> parse = [Example(1)] (s) => int.Parse(s);
var choose = [Example(2)][Example(3)] object (bool b) => b ? 1 : "two";
var sum = ([Example(1)] int a, [Example(2), Example(3)] int b) => a + b;
var inc = [return: Example(1)] (int s) => s++;
```

Lambda表达式可以引用外部变量。这是一个非常好的功能，但如果不正确使用，也会非常危险。这些变量是在定义Lambda表达式的方法中或包含Lambda表达式的类型中的范围内的变量。必须明确地分配外部变量，然后才能在Lambda表达式中使用该变量。以这种方式捕获的变量将进行存储以备在Lambda表达式中使用。捕获的变量将不会被作为垃圾回收，直至引用变量的委托符合垃圾回收的条件。在封闭方法中看不到Lambda表达式内引入的变量。Lambda表达式无法从封闭方法中直接捕获`in`、`ref`或`out`参数。Lambda表达式中的`return`语句不会导致封闭方法返回。如果相应跳转语句的目标位于Lambda表达式块之外，Lambda表达式不得包含`goto`、`break`或`continue`语句；同样，如果目标在块内部，在Lambda表达式块外部使用跳转语句也是错误的。从C# 9.0开始，可以将`static`修饰符应用于Lambda表达式，以防止由Lambda无意中捕获本地变量或实例状态。静态Lambda无法从封闭范围中捕获本地变量或实例状态，但可以引用静态成员和常量定义。

``` C#
Func<double, double> square = static x => x * x;
```