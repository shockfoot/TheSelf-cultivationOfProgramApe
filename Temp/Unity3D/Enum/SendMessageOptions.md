# SendMessageOptions

所属命名空间`UnityEngine`。定义如何发送消息的选项，`GameObject`和`Component`中的`SendMessage`和`BroadcastMessage`使用。

## 值

- `RequireReceiver`：默认值，发出或广播的消息需要被接收。如果没有被接收，则在控制台中报错。
- `DontRequireReceiver`：发出或广播的消息不需要被接收。如果`GameObject`上没有组件（脚本）实现该方法，则不会生成错误。