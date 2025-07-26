# UI

## 1.附加Canvas Group组件的窗口的UI事件无法响应

附加Canvas Group组件的窗口先加载的但需要后渲染时，通常使用Canvas组件修改其Sort Order，但其UI事件仍会被后加载的UI阻挡。

解决：使用Graphic Raycaster组件修改UI事件检测。

## 2.预制体和UI的鼠标事件

当预制体需要响应鼠标事件时，如OnMouseEnter()、OnMouseExit()、OnMouseDown()等，即使鼠标当前正在对UI对象进行操作，也会出发UI下方预制体的相关回调。因此，**预制体需要响应鼠标事件需要隔离UI上的事件**。

解决：EventSystem提供了IsPointerOverGameObject()接口用于判断当前鼠标事件是否在UI上。预制体的响应操作中可以根据此情况排除已UI事件。需要注意的是，只有启用了Raycast Target的UI对象才能被EventSystem纳入检测。

## 3.预制体响应鼠标点击事件时的局限

当预制体需要响应鼠标点击事件时，如OnMouseDown()、OnMouseUp()，无法直接从回调中获取鼠标按下的键和鼠标位置。

解决：预制体需要有Collider，即使是触发器。在OnMouseDown()通过Input.GetMouseButton()记录鼠标按下的按键。需要注意的是，在OnMouseUp()中无法获取鼠标按下的按键。

即使鼠标已经离开了预制体，即已经触发了OnMouseExit()，也会触发OnMouseUp()。
