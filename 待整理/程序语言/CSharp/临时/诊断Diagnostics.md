# 概述

`System.Diagnostics`命名空间提供了系统进程、事件日志、性能计数、调试应用程序并跟踪代码相关的功能。

# Stopwatch

`Stopwatch`用于精确地获取经过的时间。通常先调用`Start()`方法，然后调用`Stop()`方法，通过`Elapsed`、`ElapsedMilliseconds`、`ElapsedTicks`等属性获取经过的时间。

`Stopwatch`只有两种状态：运行或停止。可以使用`IsRunning`属性确定其状态。可以在运行或停止时查看已经过的时间。在运行状态下，已经过时间不断增加；在停止状态下则保持不变。

默认情况下，`Stopwatch`的测量时间值等于运行时间的总和。调用`Start()`方法后将继续累计测量时间；调用`Start()`方法后会结束测量。使用`Reset()`方法可以清除累积测量的时间。

`Stopwatch`通过计算底层计时器的时间刻来测量时间。如果硬件和系统支持高频率性能测试，则`Stopwatch`会使用该计时器来测量时间，否则使用系统默认计时器实现测量。可以通过`Frequency`字段和`IsHighResolution`字段获取`Stopwatch`计时时的频率。

另外，`Stopwatch`提供了`Restart()`和`StartNew()`方法来重置测量时间并开始测量。