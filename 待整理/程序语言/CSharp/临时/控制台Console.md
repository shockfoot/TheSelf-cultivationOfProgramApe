# Console类

控制台是一个操作系统窗口，用户通过计算机键盘输入文本，并从计算机终端读取文本输出，从而与操作系统或基于文本的控制台应用程序进行交互。控制台缓冲区是指可以显示内容的区域，左上角为原点，右为x的正方向，下为y的正方向，并且在视觉上1y = 2x。

类`Console`表示控制台应用程序的标准输入、输出和错误流， 为从控制台读取字符和向控制台写入字符的应用程序提供基本支持。`Console`类位于`System`命名空间，继承自`Object`，不能被继承。

# API

- 读取输入：`Read()`、`ReadKey()`、`ReadLine()`。
- 输出：`Write()`、`WriteLine()`、`MoveBufferArea()`。
- 清空：`Clear()`。
- 设置控制台尺寸（**先设置窗口尺寸再设置缓冲区，缓冲区不能小于窗口尺寸，窗口不能大于屏幕尺寸**）：`WindowWidth`、`WindowHeight`、`WindowTop`、`WindowLeft`、`LargestWindowWidth`、`LargestWindowHeight`、`BufferWidth`、`BufferHeight`；`SetWindowSize()`、`SetBufferSize()`。
- 设置控制台位置：`SetWindowPosition()`。
- 设置控制台颜色：`ForegroundColor`、`BackgroundColor`；`ResetColor()`。
- 设置控制台标题：`Title`。
- 光标位置：`CursorVisible`、`CursorTop`、`CursorLeft`、`CursorSize`；`SetCursorPosition()`、`GetCursorPosition()`。
- 流相关：`In`、`Out`、`Error`；`SetIn()`、`SetOut()`、`SetError()`、`OpenStandardInput()`、`OpenStandardOutput()`、`OpenStandardError()`。
- 警报音：`Beep()`。
- 其他：`CapsLock`是否启用大写、`NumberLock`是否启用数组小键盘。
- 关闭控制台：`Environment.Exit(0)`。

# ConsoleColor枚举

枚举`ConsoleColor`指定了常用的颜色常量，用于定义控制台的前景色和背景色。

# ConsoleKey枚举

枚举`ConsoleKey`指定了控制台上的标准键。`ConsoleKey`枚举通常用于由控制台`ReadKey()`方法返回的信息结构中，来指示已按下控制台上的哪个键。

# ConsoleKeyInfo结构体

值类型`ConsoleKeyInfo`用于描述按下的控制台标准按键的字符以及功能符键的状态。`ConsoleKeyInfo`只能作为`ReadKey()`方法的返回值而无法在外部创建。

- 属性`Key`指示按键。
- 属性`KeyChar`指示按键对应的Unicode字符。
- 属性`ConsoleModifiers`判断SHIFT、ALT或CTRL功能键是否同时被按下。

# ConsoleModifiers枚举

枚举`ConsoleModifiers`表示键盘上的SHIFT、ALT和CTRL功能键。此枚举支持其成员值的按位组合。左右SHIFT、ALT和CTRL键之间没有区别。

# ConsoleSpecialKey枚举

枚举`ConsoleSpecialKey`表示能够终止进程的组合键（Ctrl+Break或Ctrl+C）。

# ConsoleCancelEventHandler委托

委托`ConsoleCancelEventHandler`表示处理控制台`CancelKeyPress`事件的方法。

# ConsoleCancelEventArgs类

类`ConsoleCancelEventArgs`是控制台`CancelKeyPress`事件参数类，不能被继承。

用户可以同时按Ctrl+C或Ctrl+Break来中断控制台应用程序进程。因此，.Net为控制台的事件处理程序提供了一个`ConsoleCancelEventArgs`类用于指示`CancelKeyPress`事件是否应该取消进程。

- 属性`Cancel`指示按下终止组合键时是否终止当前进程。默认值为`false`，表示终止当前进程。
- 属性`SpecialKey`获取中断当前进程的组合键。