## 脚本

### 基本规则

C#脚本创建规则：

1. 不在脚本编辑器中创建；
2. 在Assets下创建；
3. **类名必须与文件名一致**，不然无法挂载（反射机制通过文件名确定Type）。

Unity的C#脚本默认继承MonoBehaviour。只有继承了MonoBehaviour才能挂载到GameObject上。同时，继承了MonoBehaviour的脚本不能`new`，只能手动挂载让Unity实例化之，因此其不能写构造函数。一个GameObject上可以挂载同一脚本多个。继承MonoBehaviour的类仍可以被继承。

未继承MonoBehaviour的类不能挂载在GameObject上，使用时需要`new`。未继承MonoBehaviour的类一般是单例模式的类或者数据结构类，用于管理模块或存储数据。

脚本可以设置执行顺序，执行时机越小越先执行。

脚本模板文件名开头的数字表示编辑器加载模板时的优先级，数字越小越优先。所有的模板文件必须是文本文件（.txt）。所有的模板文件必须存放在ScriptTemplates文件夹中，重启编辑器才能应用自定义脚本模板。模板文件命名`00-MenuName_SubMenuName-ScriptName.fileExtension.txt`即`优先级-菜单名_次级菜单名-默认文件名.扩展名.txt`。

### 生命周期函数

游戏的本质就是一个死循环，每一次循环处理游戏逻辑就会更新一次画面。当切换画面的速度达到一定时人眼即认为画面是流畅的。人眼舒适放松时可视帧数为24fps。一帧就是执行一次循环。

Unity底层提供了循环处理游戏逻辑的生命周期函数。生命周期函数是继承MonoBehaviour的脚本对象依附的GameObject对象从出生到消亡整个生命周期中会通过反射自动调用的特殊函数。Unity会记录GameObject对象依附的脚本，通过反射执行固定名字的函数。

生命周期函数访问修饰符一般为`private`和`protected`，返回值为`void`，因为生命周期不需要外部调用。

- `Awake`：当脚本对象被创建时执行。只会执行一次。
- `OnEnable`：脚本对象所依附的GameObject对象每次激活时执行。
- `Start`：脚本对象被创建后，第一次帧更新之前执行。只会执行一次。
- `FixedUpdate`：物理帧更新，固定间隔时间执行，间隔时间可以设置。
- `Update`：逻辑帧更新，每帧执行。
- `LateUpdate`：每帧执行，于`Update`之后执行。
- `OnDisable`：脚本对象所依附的GameObject对象每次失活时执行。
- `OnDestroy`：当脚本对象被销毁时执行。只会执行一次。

生命周期函数支持继承和多态。

不使用的生命周期函数不要写，可以减少函数的执行。

### Insperctor窗口可编辑变量

Insperctor窗口的可编辑变量即脚本的公共成员变量。使用`[SerializeField]`特性（强制序列化字段特性）可以让私有和保护的成员变量被显示和编辑。使用`[HideInInspector]`特性（在Inspector窗口隐藏特性）可以在Inspector窗口隐藏公共成员变量。

附加到GameObject上的脚本，当改变其脚本中的默认值时，无法改变Inspector窗口中的值。运行中脚本的值无法保存。

大部分类型都可以在Insperctor窗口显示和编辑。使用`[System.Serializable]`特性（可序列化字段特性）可以让自定义类型的成员变量被显示和编辑。字典不可以在Insperctor窗口显示和编辑。

### 辅助特性

- 分组特性`[Header("分组说明")]`：在Inspector窗口中为成员分组。
- 悬停注释`[ToolTip("说明内容")]`：在Inspector窗口中悬停在变量上时显示变量说明。
- 间隔特性`[Space()]`：让两个字段间出现间隔。
- 范围`[Range(最小值, 最大值)]`：修饰数值的滑条范围。
- 字符串多行显示`[Multiline(行数)]`：默认不写参数显示3行。
- 字符串滚动条显示`[TextArea(最小行数, 最大行数)]`：默认不写参数即超过3行是显示滚动条。
- 为变量添加快捷方法`[ContextMenuItem("显示按钮名", "方法名")]`：参数2为无参无返回值的方法。
- Inspector窗口中执行方法`[ContexMenu("测试函数")]`：用于在编辑模式执行方法，测试用。
