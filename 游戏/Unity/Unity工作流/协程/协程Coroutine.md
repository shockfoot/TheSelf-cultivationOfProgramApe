# 协程

在Unity中，**协程（协同程序，Coroutine）是具有多个返回点的方法，可以暂停执行并将控制权返还给Unity，并且当重新获得控制权时，协程能从其暂停的地方继续执行。**

在大多数情况下，方法会在当前帧更新中完成并将结果和控制权返还给调用方。协程可以将任务（如等待HTTP传输、资源加载、I/O操作等）分散在多帧中完成。

**协程的本质是迭代器。** 定义协程方法要满足两个条件：方法返回值为`IEnumerator`、方法中有`yield`关键字。其中，`yield`关键字用于将代码分割成片段，Unity在每帧中调用`IEnumerator`中的`MoveNext`方法以按顺序执行各代码片段。

C#编译器会自动生成协程类。当开启一个协程时会创建该协程类实例，并存储该协程中的变量（将局部变量提升到全局变量，将变量分配在堆上，可存储协程暂停时的状态一边协程继续执行）。Unity通过该协程类实例来跟踪协程状态和管理协程。因此，**开启协程的内存开销为固定的启动协程开销加上协程局部变量存储开销。** 

当协程被开启并执行到第一个`yield`语句时，剩余的代码片段会被托管到Unity主循环中的DelayedCallManager。当`yield`语句中的条件满足时，Unity会再次条用剩余的代码片段。

**在Unity的主线程中只会有一个处于运行状态的协程**，即多个协程是按顺序执行。Unity中有个协程调度器，其维护了一个协程列表，以确保主线程中只能有一个处于运行状态的协程。因此，协程之间不存在临界区资源访问的问题。

尽管协程能够嵌套并且嵌套协程能够提高代码的清晰度和可维护性，但由于启动协程需要创建协程类跟踪对象，因此会增加内存开销。

当开启协程的脚本失活时，不会影响协程的执行（如果要需要，可在`OnDisable`中停止协程，可保留协程状态和数据，协程可在脚本激活后继续执行）。当挂载脚本的游戏对象失活后，协程不执行，重新激活后也不会执行。当脚本或游戏对象销毁后，协程停止执行。

# 协程与异步

进程：系统进行资源分配的基本单位。进程是线程的容器。每个进程拥有自己的独立内存空间，不同进程通过进程间通信实现通信。由于进程比较重量、占据独立内存，因此上下文进程间的切换开销（栈、寄存器、虚拟内存、文件句柄等）比较大。

线程：系统能够进行资源调度的最小单位（即CUP调度和分派的基本单位）。线程是进程中实际运作的单位。线程除了拥有在运行在必不可少的资源如程序计数器、一组寄存器和栈之外，基本上不拥有系统资源。线程可以与其同属一个进程的其他线程共享进程所拥有的公共资源。线程间通信主要通过内存共享，上下文切换快、资源开销少。

> **注意**：
> - 同属一个进程的线程间切换不会引起进程的切换，只有不同进程之间的线程切换才会引起进程的切换。
> - CPU时间片是直接分配给线程的，而不是先分配给进程然后再由进程分配给线程的。线程获取到CPU时间片即可执行。所有的进程并行、线程并行都是看起来是并行，其实都是轮换使用CPU时间片。

进程与线程的区别：

- 进程是资源分配和拥有的单位，同一个进程内的线程共享进程的资源。
- 线程是CPU调度的基本单位。

协程与线程的区别：

- **协程是伪异步。协程不是线程，也不是异步执行的。** 协程是把`yield return`之间的语句分配到不同帧执行。**协程仍然运行在主线程上。**
- 线程是真异步。线程与脚本生命周期无关，子线程不能访问GameObject、Component和相关Unity API。
- 协程（实质是方法）之间的切换不是由线程切换，而且由程序控制，因此没有线程切换开销。协程依次执行，不存在线程安全问题，变量访问不会冲突，共享资源无需枷锁，因此执行效率比线程高。

# 语法

## 使用协程

通过`MonoBehaviour.StartCoroutine`开启一个协程（协程先在当前帧执行到第一个`yield`，然后在下一时刻执行`yield`之后的代码片段），参数可以是`IEnumerator`或者方法名。`MonoBehaviour.StartCoroutine`会返还一个`Coroutine`实例。`Coroutine`类仅用于引用协程实例，而不包含任何公开的属性或方法。

可以使用`MonoBehaviour.StopCoroutine`停止该`MonoBehaviour`实例启动的协程。其中，如果参数为方法名且启动了多个协程，则只停止第一个；其他参数则停止对应的协程。

`MonoBehaviour.StopAllCoroutine`会停止该`MonoBehaviour`实例启动的所有协程。

> **注意**：`StopCoroutine`方法必须与`StartCoroutine`使用相同的参数。

## 关于yield

`yield`关键字用于分割代码并指定后续代码片段的执行时机。常用用法有：

``` csharp
yield break; // 直接结束协程
yield return null; // 等待一帧
yield return 0/*或其他数字*/; // 等待一帧
yield return new WaitForSeconds(/*秒数*/); // 等待指定秒数（受程序内时间速度影响）
yield return new WaitForSecondsRealtime(/*秒数*/); // 等待指定的实际秒数
yield return new WaitForEndOfFrame(); // 等待帧结束（所有摄像机和GUI渲染结束，屏幕渲染该帧之前）
yield return new WaitForFixedUpdate(); // 等待下一次FixedUpdate结束
yield return StartCoroutine(/*协程*/); // 等待某协程执行完毕
yield return /*具体异步操作*/; // 等待某异步操作结束
yield return /*具体GameObject*/; // 等待某GameObject被获取到
yield return /*具体WWW操作*/; // 等待某网络请求完成
yield return new WaitUntil(/*具体委托*/); // 等待，直到某委托结果为true
yield return new WaitWhile(/*具体委托*/); // 等待，直到某委托结果为false
```

根据脚本生命周期，上述各`yield`语句执行时机不尽相同：

- `yield WaitForFixedUpdate`在`FixedUpdate`（更具体的是在`OnCollisionXXX`）之后执行。
- `yield null/数字`、`yield WaitForSeconds`、`yield WWW`、`yield StartCoroutine`、`yield WaitUntil`、`yield WaitWhile`在`Update`和`LateUpdate`之间执行。
- `yield WaitForEndOfFrame`在帧结束后执行。

由上述可知，**协程执行完整个流程至少需要两帧**。

``` csharp
public IEnumerator SmallestCoroutine()
{
    // 在当前帧执行到此，等待下一帧
    yield return null;
    // 在下一帧执行结束
}
```