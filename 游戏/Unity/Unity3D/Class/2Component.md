# Component

所属命名空间`UnityEngine`，继承自`Object`，实现于`UnityEngine.CoreModule`。

`Component`是所有附加在`GameObject`上组件的基类。

## 属性

### gameObject

- `public GameObject gameObject`

`Component`附加到的`GameObject`。`Component`始终附在`GameObject`上。

### tag

- `public string tag`

`Component`附加到的`GameObject`上的`tag`。

### transform

- `public Transform transform`

`Component`附加到的`GameObject`上的`Transform`组件。

## 公共方法

### SendMessage

- `public void SendMessage(string methodName)`
- `public void SendMessage(string methodName, object value)`
- `public void SendMessage(string methodName, object value, SendMessageOptions options)`
- `public void SendMessage(string methodName, SendMessageOptions options)`

在`Component`附加到的`GameObject`上的每个`MonoBehaviour`调用名为`methodName`的方法，其中`value`为传递的参数。接收方法可以使用无参方法以选择性忽略所传参数。如果设置了`SendMessageOptions.RequireReceiver`却无任何组件接收到消息时会报错。消息不会发送给未激活对象（不管是在编辑器中禁用还是用`SetActive`禁用的）。

### SendMessageUpwards

- `public void SendMessageUpwards(string methodName, SendMessageOptions options)`
- `public void SendMessageUpwards(string methodName, object value = null, SendMessageOptions options = SendMessageOptions.RequireReceiver)`

在`Component`附加到的`GameObject`及其祖先上的每个`MonoBehaviour`调用名为`methodName`的方法，其中`value`为传递的参数。接收方法可以使用无参方法以选择性忽略所传参数。如果设置了`SendMessageOptions.RequireReceiver`却无任何组件接收到消息时会报错。消息不会发送给未激活对象（不管是在编辑器中禁用还是用`SetActive`禁用的）。

### BroadcastMessage

- `public void BroadcastMessage(string methodName, Object parameter = null, SendMessageOptions options = SendMessageOptions.RequireReceiver)`
- `public void BroadcastMessage(string methodName, SendMessageOptions options)`

在`Component`附加到的`GameObject`及其子对象的每个`MonoBehaviour`调用名为`methodName`的方法，其中`parameter`为传递的参数。接收方法可以使用无参方法以选择性忽略所传参数。如果设置了`SendMessageOptions.RequireReceiver`，但没有任何一个组件接收消息时会报错。

### CompareTag

- `public bool CompareTag(string tag)`

判断`Component`附加到的`GameObject`的标签是否是`tag`。

### GetComponent

- `public Component GetComponent(Type type)`

- `public T GetComponent()`

- `public Component GetComponent(string type)`

返回`Component`附加到的`GameObject`上的`type`类型组件，如果没有则返回`null`，有将返回找到的第一个组件，但顺序不确定。要提高代码的性能，不要使用`string`参数的方法。如果希望返回多个相同类型的组件，使用`GetComponents`，并可以循环遍历。如果要获取不同`GameObject`上的组件，需要先用`GameObject.Find`获取`GameObject`的引用，再使用`GameObject.GetComponent`获取其上组件。

### TryGetComponent

- `public bool TryGetComponent(Type type, out Component component)`
- `public bool TryGetComponent(out T component)`

获取`Component`附加到的`GameObject`上指定类型的组件（如果存在），获取成功时返回`true`，否则返回`false`。与`GetComponent`不同，此方法将尝试检索给定类型的组件，当请求的组件不存在时，此方法不会在编辑器中分配内存。

### GetComponentInChildren

- `public Component GetComponentInChildren(Type type)`
- `public Component GetComponentInChildren(Type type, bool includeInactive)`
- `public T GetComponentInChildren(bool includeInactive = false)`

使用深度优先搜索返回`Component`附加到的`GameObject`或其任何子对象中类型为`type`的组件。除法另有指定，否则只返回已激活`GameObject`上的组件。

### GetComponentInParent

- `public Component GetComponentInParent(Type type)`
- `public Component GetComponentInParent(Type type, bool includeInactive)`
- `public T GetComponentInParent()`
- `public T GetComponentInParent(bool inlcudeInactive = false)`

返回`Component`附加到的`GameObject`或其祖先中类型为`type`的组件。该方法会向父级对象递归，直到找到具有`type`组件的`GameObject`。除非另有指定，否则只匹配已激活`GameObject`上的组件。

### GetComponents

- `public Component[] GetComponents(Type type)`
- `public T[] GetComponents()`


返回`Component`附加到的`GameObject`上所有`type`类型的组件。

### GetComponentsInChildren

- `public Component[] GetComponentsInChildren(Type type, bool includeInactive = false)`
- `public T[] GetComponentsInChildren()`
- `public T[] GetComponentsInChildren(bool includeInactive)`

使用深度优先搜索（递归）返回`Component`附加到的`GameObject`及其子对象上所有`type`类型的组件。

### GetComponentsInParent

- `public Component[] GetComponentsInParent(Type type, bool includeInactive = false)`
- `public T[] GetComponentsInParent()`
- `public T[] GetComponentsInParent(bool includeInactive)`

返回`Component`附加到的`GameObject`及其祖先上所有`type`类型的组件。