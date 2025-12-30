- 命名空间：`UnityEngine`
- 程序集：`UnityEngine.CoreModule`
- 继承：`Object` > `Component` > `Behaviour`

`MonoBehaviour`是所有Unity脚本的基类。C#脚本必须显示继承该类。`MonoBehaviour`提供了脚本框架；可以启动、停止和管理协程；提供对大量事件消息的访问，可以根据项目中当前发生的情况执行代码。

在Unity编辑器中有一个用于启用或禁用`MonoBehaviour`的复选框。如果脚本中有以下函数：`Start`、`Update`、`FixedUpdate`、`LateUpdate`、`OnGUI`、`OnEnable`、`OnDisable`，则Unity编辑器不会显示复选框。

## 属性

- 运行模式：`runInEditMode`。允许特定实例在编辑模式下运行（仅可在Editor中使用）。默认为`false`，即仅在运行模式下执行。
- 是否使用GUI布局：`useGUILayout`。默认为`true`，设置`false`可跳过GUI布局阶段。仅当不在`OnGUI`调用中使用 `GUI.Window`和GUI布局时才能使用该属性。

## 普通方法

- 延迟调用：如果需要将参数传递给方法，请考虑改用协程。协程还提供了更好的性能。
  - 单次：`Invoke`。
  - 重复：`InvokeRepeating`。
- 取消所有或指定的`Invoke`调用：`CanselInvoke`。
- 检测是否有任何待处理的或指定的`Invoke`调用：`IsInvoking`。
- 开始协程：`StartCoroutine`。可以使用`yield`语句，随时暂停协程的执行。使用`yield`语句时，协程会暂停执行，并在下一帧自动恢复。已创建的协程可以启动另一个协程。使用字符串方法名称启动协程的运行开销更高，并且只能传递一个参数。当脚本被销毁，或脚本所附加到的游戏对象被禁用时，会停止协程。禁用脚本时，不会停止协程。
- 停止协程
  - 停止指定协程：`StopCoroutine`。
  - 停止所有协程：`StopAllCoroutines`。

## 静态方法

- 打印内容至控制台：`print`。

## 消息

- 脚本生命周期

  - `Reset`：当用户点击Inspector上下文菜单中的Reset按钮或首次添加组件时调用。此函数仅在编辑器模式下调用。重置最常用于在检查器中提供良好的默认值。
  - `Awake`：加载场景时初始化激活的游戏对象，激活未激活的游戏对象，或`Object.Instantiate`实例化游戏对象后调用。即使已激活游戏对象上的脚本被禁用，也将调用`Awake`。`Awake`在脚本实例的生存期内只调用一次。Unity在初始化场景中所有已激活游戏对象后才调用`Awake`，因此可以在`Awake`中安全地使用`GameObject.FindWithTag`等方法以查询其他游戏对象。Unity调用每个游戏对象的`Awake()`的顺序不是确定的，所以通常在`Awake`中初始化引用，并在`Start`中来回传递任何信息。`Awake`总是在任何`Start`之前调用。
  - `OnEnable`：当对象被激活时调用。
  - `Start`：在调用完所有`Awake`之后、第一次调用任何`Update`之前调用`Start`。`Start`在脚本实例的生存期内只调用一次。与`Awake`不同，如果在初始化时未启用脚本，则不会在与`Awake`相同的帧上调用`Start`。如果在游戏过程中实例化对象，则在场景对象的`Awake`完成后调用其`Start`。
  - `FixedUpdate`：固定时间间隔调用。默认固定时间为0.02s，可以通过`Time.fixedDeltaTime`访问此值，可以在脚本中设置首选值或者在导航栏的Edit > Settings > Time > Fixed Timestep设置时间间隔。通常在`FixedUpdate`中更新刚体的逻辑。
  - 碰撞：`OnCollisionEnter`，`OnCollisionEnter2D`，`OnCollisionStay`，`OnCollisionStay2D`，`OnCollisionExit`，`OnCollisionExit2D`，`OnControllerColliderHit`（当控制器在执行移动过程中撞击碰撞器时调用。用于在对象与角色碰撞时推动对象）。传递的是碰撞类`Collision`，包含有关接触点、碰撞速度等的信息。在不使用碰撞信息时，省略参数可以避免不必要的计算。注意：仅当其中一个碰撞器还连接了非运动学刚体时，才会发送碰撞事件。碰撞事件将被发送到禁用的`MonoBehaviours`，以允许在碰撞时启用`Behaviour`。
  - 触发：`OnTriggerEnter`，`OnTriggerEnter2D`，`OnTriggerStay`，`OnTriggerStay2D`，`OnTriggerExit`，`OnTriggerExit2D`。当游戏对象与另一个游戏对象碰撞时调用。两个GameObjects都必须包含碰撞器组件，其中至少一个必须启用`Collider.isTrigger`，并包含刚体，此时不会发生物理碰撞。
  - `Update`：每帧都会调用此方法。
  - `LateUpdate`：在调用所有`Update`函数后调用。
  - `OnWillRenderObject`：如果对象可见而不是UI元素，则为每个相机调用。如果禁用MonoBehavior，则不会调用该函数。在剔除过程中，在渲染每个剔除对象之前调用该函数。从UI元素调用时无效。
  - `OnGUI`：用于呈现和处理GUI事件，是唯一可以实现用于呈现和处理GUI事件的“即时模式”GUI（IMGUI）系统的功能。`OnGUI`可能会在每个帧中调用多次（每个事件调用一次）。如果`MonoBehavior`的`enabled`属性设置为`false`，则不会调用`OnGUI`。
  - `OnDisable`：当`Behaviour`被禁用或销毁时调用，可以用于任何的清理代码。
  - `OnDestroy`：销毁`Behaviour`时调用。此方法在脚本的生命周期内只会调用一次。
- 程序状态

  - `OnApplicationFocus`：当应用程序失去或获得焦点时调用。当用户切离Unity应用程序时，所有的游戏对象会接收参数设置为`false`的`OnApplicationFocus`调用。当用户切换回Unity应用程序时，所有的游戏对象会收到一个参数设置为`true`的`OnApplicationFocus`调用。
  - `OnApplicationPause`：应用程序暂停时调用。
  - `OnApplicationQuit`：退出应用程序时调用。
- 父子对象更改
  - `OnTransformChildrenChanged`：当游戏对象变换的子对象列表发生更改时调用。
  - `OnTransformParentChanged`：当游戏对象变换的直接或间接父对象发生更改时调用。
- 鼠标事件
  - 鼠标进入物体碰撞器：`OnMouseEnter`。当该鼠标开始接触刚体/碰撞器时调用。此事件将发送到带有碰撞器的游戏对象的所有脚本。父对象或子对象的脚本不会接收此事件。忽略射线层的对象不会接收此消息。
  - 鼠标在物体碰撞器内：`OnMouseOver`。
  - 鼠标退出物体碰撞器：`OnMouseExit`。
  - 在碰撞器上按下鼠标：`OnMouseDown`。
  - 单击碰撞器并仍按住鼠标：`OnMouseDrag`。
  - 在碰撞器上松开鼠标：`OnMouserUp`。
  - 在按下时的碰撞器上松开鼠标按钮：`OnMouseUpAsButton`。
-  动画
  - `OnAnimatorMove`：处理动画移动以修改根运动的回调。在评估状态机和动画之后，但在`OnAnimatorIK`之前，将在每帧调用此回调。
  - `OnAnimatorIK`：设置动画IK（反向运动学）的回调。`OnAnimatorIK`在Animator组件更新其内部IK系统之前立即被其调用。此回调可用于设置IK目标的位置及其各自的权重。
- 音频：`OnAudioFilterRead`将音频块发送到过滤器时调用。
- 粒子
  - `OnParticleCollision`：当粒子撞击碰撞器时调用。此消息将发送到附加到粒子系统和碰撞器的脚本。仅在粒子系统碰撞模块启用发送碰撞消息时，才会发送消息。当使用碰撞器从附加到游戏对象的脚本调用`OnParticleCollision`时，游戏对象参数表示`ParticleSystem`。即使粒子系统在当前帧中使用多个粒子撞击碰撞器，碰撞器在任何给定帧中都最多接收一条与之碰撞的粒子系统消息。要检索有关`ParticleSystem`引起的所有冲突的详细信息，必须使用`ParticlePhysicsExtensions.GetCollisionEvents`来检索`ParticleSystem.CollisionEvent`的数组。当从连接到`ParticleSystem`的脚本调用`OnParticleCollision`时，游戏对象参数表示连接了`ParticleSystem`撞击的碰撞器的游戏对象。粒子系统最多接收一个撞击的碰撞器的信息。
  - `OnParticleTrigger`：当粒子系统中任何粒子满足触发模块中的条件时调用，用于销毁或修改碰撞体积内部或外部的粒子。
  - `OnParticleUpdateJobScheduled`：当已计划粒子系统的内置更新作业时调用，用于附加自定义托管作业以在默认粒子更新后运行。
  - `OnParticleSystemStopped`：在系统中所有粒子都销毁且不会产生新粒子时调用，用于通知脚本粒子系统何时完成。为了接收回调，必须将`ParticleSystem.MainModule.stopAction`属性设置为`Callback`。
- 渲染：对于避免仅在对象可见时才需要的计算非常有用。
  - `OnDrawGizmos`：在场景中绘制可视的控件/标志。
  - `OnDrawGizmosSelected`：当对象被选择时在场景中绘制可视的控件/标志。
  - `OnBecameVisible`：当渲染器对于任何摄影机可见时调用。
  - `OnBecameInvisible`：当渲染器对于任何摄影机不再可见时调用。此消息将发送到附加到渲染器的所有脚本。
  - `OnPreCull`：在摄影机剔除场景之前调用。在内置渲染管道中，在摄影机执行决定其所能看到的内容的剔除操作之前，Unity调用`OnPreCull`时，其`MonoBehvioour`与已启用的摄影机组件连接到同一游戏对象。使用`OnPreCull`可以在执行消隐操作之前更改摄影机的设置，以影响摄影机所看到的内容。
  - `OnPreRender`：在摄影机渲染场景之前调用。在内置渲染管道中，在摄影机渲染场景之前，Unity调用`OnPreRender`时，其`MonoBehaviour`作为已启用的摄影机组件附加到同一游戏对象。使用`OnPreRender`可以更改视觉设置以在给定摄影机渲染时影响场景。
  - `OnPostRender`：在摄影机渲染场景后调用。在内置渲染管道中，在摄影机渲染场景之后，Unity调用`OnPostRender`是，其`MonoBehaviour`作为已启用的摄影机组件附加到同一个游戏对象。使用`OnPostRender`可以更改视觉效果。
  - `OnRenderImage`：在摄影机完成渲染后调用，修改摄影机的最终图像。在内置渲染管道中，在摄影机完成渲染后，Unity调用`OnRenderImage`是，其`MonoBehaviour`作为已启用的摄影机组件附加到同一游戏对象。可以使用`OnRenderImage`创建全屏后处理效果，这些效果通过从源图像读取像素，使用Unity着色器修改像素的外观，然后将结果渲染到目标图像中来实现。如果同一个摄影机上的多个脚本实现`OnRenderImage`，Unity将按照它们在Inspector窗口中出现的顺序（从顶部开始）调用它们。一个操作的目的地是下一个操作源；在内部，Unity创建一个或多个临时RenderText来存储这些中间结果。
  - `OnRenderObject`：在摄影机渲染场景后调用。这可以用于使用`Graphics.DrawMeshNow`或其他函数渲染自己的对象。该函数类似于`OnPostRender`，但`OnRenderObject`是在任何具有该函数脚本的对象上调用的；无论它是否连接到相机。应该只使用此函数进行绘制，而不更改任何高级渲染状态。此函数可能会对性能产生影响，因为它对每个使用此回调的脚本的游戏对象都会运行。
- 服务器
  - `OnPlayerConnected`：当玩家成功连接时在服务器上调用。
  - `OnPlayerDisconnected`：当玩家与服务器断开连接时在服务器上调用。
  - `OnFailedToConnect`：当连接尝试由于某种原因失败时在客户端上调用。失败的原因作为`NetworkConnectionError`枚举传入。
  - `OnConnectedToServer`：成功连接到服务器后在客户端上调用。
  - `OnDisconnectedFromServer`：当连接丢失或与服务器断开连接时在客户端上调用。
  - `OnFailedToConnectToMasterServer`：当连接到MasterServer时出现问题时在客户端或服务器上调用。失败的原因作为`NetworkConnectionError`枚举传入。
  - `OnMasterServerEvent`：从MasterServer报告事件时在客户端或服务器上调用。
  - `OnNetworkInstantiate`：对已使用`Network.Instantiate`进行网络实例化的对象调用。对于禁用或启用已实例化对象的组件非常有用，这些对象的行为取决于它们是本地拥有还是远程拥有。
  - `OnServerInitialized`：每当调用`Network.InitializeServer`并完成时在服务器上调用。
- 其他
  - 当连接到同一游戏对象的关节断裂时调用：`OnJointBreak`，`OnJointBreak2D`。当力高于关节的断裂力时，关节将断裂时调用，并传递应用于关节的断裂作用力。调用结束后，关节将自动从游戏对象中移除并删除。
  - `OnSerializeNetworkView`：用于自定义网络视图监视的脚本中变量的同步。
  - `OnValidate`：仅当加载脚本或Inspector中的值更改时Unity调用的编辑器函数。使用此选项可以在Inspector中的值更改后执行操作，例如，确保数据保持在一定范围内。
