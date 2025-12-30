## 概述

Lua语言是巴西大学研究项目，设计目的是嵌入应用程序中，提供灵活的扩展和定制功能。

Lua可以应用于游戏开发、工业级应用程序、嵌入式设备和智能移动、Web应用、扩展与数据库插件、安全系统等。

## 注释

单行注释：`--`

多行注释：`--[[注释文本]]`或`--[[]]--`或`--[[注释文本--]]`。

## 变量

Lua中的简单变量类型有`nil`空、`number`数值、`string`字符串和`boolean`布尔。`nil`表示`null`。`number`表示所有的数值。`string`使用单引号或双引号包裹，没有`char`。

Lua的复杂数据类型有`function`函数、`table`表、`userdata`数据结构、`thread`协同程序。

Lua中所有的变量声明都不需要声明类型，可以随便赋值，会自动识别类型，可以通过`type()`函数得到变量的类型，而`type()`函数的返回值是`string`类型的。

Lua中使用没有声明过的变量不会报错，该变量的值为`nil`。

Lua可以同时对多个变量赋值`a, b = 1, true`。如果前后个数不匹配会补空`nil`或舍弃多余的值。

## 字符串操作

获取字符串长度：`#string`。Lua中英文字符长度为1，中文字符（UTF-8）长度为3。

多行打印：转移字符`string1\nstring2`；`[[string]]`中保留字符串的格式。

拼接：`variable1 .. variable2`使用两个点可以将任意两个变量以字符串形式拼接；`string.Format(string)`，其中占位符格式与C语言相同，即`%d`数字、`%a`字母、`%s`字符串等。

类型转字符串：显示转换`tostring(variable)`；打印时自动隐式转换。

小写转大写：`string.upper(string)`，返回新字符串而不改变原字符串。

大写转小写：`string.lower(string)`，返回新字符串而不改变原字符串。

反转字符串：`string.reverse(string)`，返回新字符串而不改变原字符串。

字符串索引查找：`string.find(string, aimString)`，两个返回值，字符串的起始和结束位置。**Lua中的索引从1开始**。

截取字符串：`string.sub(string, start)`或`string.sub(string, start, end)`。

字符串重复：`string.rep(string, time)`将字符串重复拼接指定次数。

字符串修改：`string.gsub(string, oldStr, newStr)`，两个返回值，修改后的字符串和修改的次数。

字符转ASCII码：`string.byte(string, position)`。

ASCII码转字符串：`string.char(number)`。

## 运算符

算数运算符：加+、减-、乘`*`、除/、取余%、幂运算^。Lua**没有自增自减和复合运算符**。能转换成`number`类型的`string`可以使用算数运算符，会自动转换为`number`。

比较运算符：大于>、大于等于>=、小于<、小于等于<=、等于==、不等于~=。

条件逻辑运算符：逻辑与and、逻辑或or、逻辑非not。Lua**支持逻辑短路**。

Lua**不支持位运算符**。

Lua**不支持三目运算符**。

## 语句

### 条件语句

``` Lua
-- 无分支
if condition then
    operation
end
-- 双分支
if condition then
    operation
else
    operation
end
-- 多分支
if condition then
    operation
elseif condition then -- elseif不能分开
    operation
else
    operation
end
```

Lua**没有`switch`**。

### 循环语句

``` Lua
-- while循环
while condition do
    operation
end
-- do while循环
repeat
    operation
until condition -- condition为退出条件，C#中是继续进入的条件
-- if循环
for i = 1,5 do -- 第一个参数为初始值，第二个参数为终值，Lua默认增幅为1
    operation
end

for i = 1,5,2 do -- 第三个参数为自定义增幅
    operation
end
```

## 函数

具体语法：

``` Lua
function FunctionName([param])
    body
end
-- 直接赋值时不能写函数名
a = function([param])
    body
end
```

Lua必须在函数声明后才能调用。函数是一种`function`类型的变量，类似于C#的委托和事件。

函数可以没有参数。如果指定参数却没显示指定类型，可以传入任意类型的参数，函数内部只有遇到该类型参数无法进行的行为时才会报错。如果传入的参数个数与指定个数不匹配，不会报错，会补空`nil`或者丢弃多余参数。

函数返回值使用`return`关键字，可以返回多个值。如果返回值个数与接取变量个数不匹配时补空`nil`或者丢弃多余返回值。

函数不支持重载，会调用最后声明的同名函数。

在函数的参数中使用`...`表示变长参数，在函数内使用表接取参数。变长参数的类型可以不统一。

``` Lua
function F(...)
    a = {...}
end
```

函数可以返回一个函数，因为返回的是函数变量，因此返回的函数不能写名字。

``` Lua
function F1()
    F2 = function()
        body
    end
    return F2
end
等价于
function F1()
    return function()
        body
    end
end   
```

在一个函数内部返回一个函数，该函数改变了临时变量的生命周期，就形成了一个闭包。

``` Lua
function F(x)
    return function(y)
        return x + y -- 改变了传入参数的生命周期
    end
end
```

## 表

Lua中所有的复杂类型都是用表`table`实现的，以`{}`包裹。

### 基于表的复杂数据类型

数组：`a = {elements}`。数组中元素的类型可以不同，可以为`nil`。默认所引从1开始。使用`[index]`获取相应索引的元素。使用`#`可以获取长度。计算长度时，如果最后一个元素为`nil`，该元素会被忽略；同时，如果中间存在`nil`，那么此元素及其之后的元素也将被忽略。

二维数组：`a = {{elements},{elements}}`。使用`[index][index]`获取相应元素。

Lua可以使用`[index]=`对指定元素自定义索引。计算长度时，会忽略小于等于0的索引的元素。如果自定义索引跳跃设置，如果只跳1格，长度不会断，受最大自定义索引影响；如果间隔超过1，不会计算后续的元素。坑，巨坑。

字典：`a = {["key"] = value, ...}`，可以通过`a["key"]`或`a.key`获取值，其中`.key`中的`key`不能是数字。直接使用新的键即可新增元素。没有删除的概念，因为即使是不存在的键也可以使用，只是没有赋值的时候为`nil`而已，所以此处删除即为置空，置空后会回收内存。字典只能用`pairs`遍历，会忽略置空的元素。

Lua没有面向对象，只能自己实现。

类：通过表实现，如下，可以在表内添加不同类型的变量。声明表后，可以在表外继续添加新的变量。

``` Lua
Student = {
    name = "Yuan",
    sex = true,
    Introduce = function()
        print("I am Yuan")
    end
}

Student.name
Student.Introduce()

Student.age = 14
Student.Learn = function()
    print("Learn")
end
function Student.Speak()
    print("Speak")
end
```

表中的函数中如果要获取表中的其他变量，需要使用`表名.变量名`或将自己作为参数。 

``` Lua
a = {
    b = 1,
    B = function()
        print(b) -- 此处的b为全局变量，而不是表a中的元素b
    end
    
    C = function() -- 直接指定自己的元素
        print(a.b)
    end
    
    D = function(t) -- 将自己作为参数传入
        print(t.b)
    end
}

a.D(a)
-- 语法糖
a:D()

function a:E()
    print("E")
end
```

`.`正常调用函数，`:`将调用者作为第一个参数传入函数；在表外声明声明函数时也可以使用，表面该函数有一个默认的参数，即自己，使用`self`来使用该参数。`self`表示默认传入的第一个参数。

### 表的公共操作

插入：`table.insert(t1, t2)`将表2插入到表1后。

删除：`table.remove(t)`移除表最后一个索引的内容。`table.remove(t, index)`移除表指定索引的内容。

排序：`table.sort(t)`默认升序。`table.sort(t, function(a,b) if a > b then return true end end)`降序，第二个是排序规则。

拼接：`table.concat(t,"分隔符")`将表中元素以指定分隔符拼接成字符串。

## 迭代器遍历

迭代器遍历主要用于遍历表。

``` Lua
a = {[0] = 1, 2, [-1] = 3, 4, 5, [5] = 6}
```

`ipairs`迭代器遍历键值对，从索引1开始往后遍历，索引小于等于0的元素获取不到；且只能找到连续索引的键，如果索引断序了，后面的内容将无法获取。

``` Lua
for i,k in ipairs(a) do -- i为键，k为值
    print(i.."_"..k)
end
Output: 1_2
		2_4
		3_5
```

`pairs`迭代器可以获取所有的键。

``` Lua
for k,v in ipairs(a) do -- k为键，v为值
    print(k.."_"..v)
end
Output: 1_2
		2_4
		3_5
		0_1
		-1_3
		5_6
```

迭代器只写一个参数则只遍历键。迭代器无法只遍历值。

## 多脚本执行

在Lua脚本中直接声明的变量都是全局变量。在变量名前使用`local`关键字声明局部变量。

使用`require("路径/脚本名")`可以执行对应脚本，此后，可以直接使用该脚本中的全局变量。`require`加载执行过的脚本在未卸载之前再`require`将不会执行。`require`可以返回一个脚本中`return`的任何变量。

`package.loaded["路径/脚本名"]`判断该脚本是否被加载执行过。被加载执行过的脚本的返回值为`true`。

卸载脚本将`package.loaded["路径/脚本名"] = nil`即可。

大G表`_G`是一个总表，本质也是`table`，存储所有声明的全局变量，本地变量是不会保存到大G表中。之所以我们可以在任何地方都能访问全局变量，就是因为大G表存储了这些全局变量。

## 特殊用法

多变量赋值和多返回值：个数不匹配时补空`nil`或者丢弃多余值。

`and`和`or`不仅可以连接`boolean`类型变量，可以连接任何东西，返回指定的值。在Lua中只有`nil`和`false`才认为是假。

``` Lua
1 and 2 -- Output: 2
0 and 1 -- Output: 1
nil and 1 -- Output: nil
false and 1 -- Output: false
true and 1 -- Output: 1

true or 1 -- Output: true
false or 1 -- Output: 1
nil or 1 -- Output: 1
```

根据`and`和`or`的特性，可以实现三目运算符`condition and trueRes or falseRes`。

``` Lua
(x>y) and x or y
两种情况：
x>y 为真，(x>y) and x 返回x，然后x or y返回x。
x>y 为假，(x>y) and x 返回(x>y)，然后(x>y) or y返回y。
```

## 协同程序

通过`coroutine.create(functionName)`创建协程，返回的变量类型是`thread`。协程的本质是一个线程对象。此方法创建的协程使用`coroutine.resume(coroutineName)`执行，默认返回一个值表示协程是否启动成功，如果协程中的`yield()`中有返回值，第二个返回值即为该返回值。

通过`coroutine.warp(functionName)`创建的协程返回的变量类型是`function`。直接`coroutineName()`即可执行，直接返回`yield()`中有返回值，而不会返回协程是否启动成功。

使用`coroutine.yield()`可以挂起协程。括号中可以写返回值，该值可以被接收。由于Lua自上而下执行，所以挂起的协程需要手动执行。

`coroutine.status(coroutineName)`可以获取指定协程的状态：`dead`结束、`suspended`暂停、`running`进行中。

`coroutine.running()`获取当前正在运行的协程的线程编号。

##  元表

任何表变量都可以作为另一个表变量的元表。任何表变量都可以有自己的元表。当对有元表的表进行一些特定操作时，会执行元表中的内容。

使用`setmetatable(表, 元表)`，给表设置元表。

元表中的特定操作：

- `__tostring`：函数类型。当表要被当作字符串使用时，默认调用元表中的此函数，并在调用时默认将表自身传入，因此可以在此函数中使用表中的元素。
- `__call`：函数类型。当表要被当作函数使用时，默认调用元表中的此函数，并在调用时默认将表自身传入。
- `__index`：表类型。当在表中找不到某一个变量时，会到元表中此变量指定的表中去寻找。此方法的查找可以层层往上查找，直到没有元表或找到为止。在元表内部无法赋值元表自己，因为元表还未构建完成，因此要在元表外部才能将赋值元表自身。
- `__newindex`：表类型。当对表赋值时，如果赋值给表中不存在的变量，会将值赋值到元表中此变量所指的表而不表自己中添加新变量。此方法也会层层往上查找，直到某表中存在指定变量或没有元表为止。
- 运算符重载：`__add`加、`__sub`减、`__mul`乘、`__div`除、`__mod`取余、`__pow`幂、`__eq`相等、`__lt`小于、`__le`小于等于、`__concat`拼接，都是函数类型。当对两个表执行相应运算符操作时，调用元表中相应函数，并将两个表传入。如果要用比较运算符比较两个表，两个表的元表需要相同。
- `getmetatable(表)`：获取表的元表。
- `rawget(表, "varible")`：忽略`__index`只在指定表中查找某变量。
- `rawset(表, "varible", 值)`：忽略`__newindex`强制在表中添加新变量。

## 面向对象

### 封装

Lua实现万物之源`Object`。

``` Lua
Object = {} -- 该变量类似于静态类
Object.id = 1
-- 构造函数
function Object:new() -- `:`默认将自身作为第一个参数传入
    local obj = {}
    setmetatable(obj, self) -- 设置元表，类似于继承Object类
    self.__index = self
    return obj -- 返回一个新的变量，即新对象
end

local myObj = Object.new();
print(myObj.id) -- Output: 1
```

### 继承

使用_G表根据字符串类名创建表示该静态类的表，然后使用元表和`__index`实现继承。

``` Lua
function Object:subClass(className) -- 继承方法：
    -- 使用_G表
    _G[className] = {} -- 在_G表中构建空表
    -- 继承的规则
    local obj = _G[className]
    setmetatable(obj, self)
    self.__index = self
    obj.base = self -- 模拟C#中的base
end

Object:subClass("Person")
local p = Person:new()
print(p.id) -- Output: 1
-- 查找顺序：p表中没有去p的元表Person中找，也没有，就去Person的元表Object中找
```

### 多态

``` Lua
Object:subClass("Father")
Father.posX = 0;
Father.posY = 0;
function Father:Position()
    self.posX = self.posX + 1 -- 先调用：没有，找父类的；后赋值：直接在本表中添加新元素
    self.posY = self.posY + 1
    print(self.posX.."_"..self.posY)
end

Father:subClass("Son")

local s1 = Son:new()
s1:Position() -- Output: 1_1

-- 重写：直接在子类中添加重名函数即可
-- 如果子类调用可以在子类中找到该变量，不会到元表的__index中寻找
function Son:Position()
    self.base:Position() -- 保留父类逻辑
end
s1:Position()
-- Output: 2_2

-- 坑
local s2 = Son:new()
s2:Position()
-- Output: 3_3
-- 不同对象使用的变量是同一个：`:`调用父类逻辑时将父类传入了
-- 为了防止这种问题，用`.`调用父类逻辑并传自身：self.base.Position(self)
```

## 自带库

Lua自带了许多公共方法，如`string`、`table`中提供的，还有`os`、`math`、`package`、`io`、`coroutine`等。

### 时间

- 系统时间：`os.time()`获取系统时间戳；`os.date("*t")`获取系统时间，信息更详细。
- 表转换为时间：`os,time({year = 2014, month = 6, day = 14})`将表转换为时间。

### 数学

- 绝对值：`math.abs(number)`。
- 弧度转角度：`math.deg(math.pi)`。
- 三角函数：`math.cos(math.pi)`等。
- 取整：`math.floor(number)`向下取整；`math.ceil(number)`向上取整。
- 最值：`math.max(number1, number2)`最大值；`math.min(number1, number2)`最小值。
- 小数分离成整数和小数两个部分：`math.modf(number)`，返回整数和小数两个返回值。
- 幂运算：`math.pow(number1, number2)`number1的number2次方。
- 随机数：需要先设置随机数种子`math.randomseed(number)`，`math.random(number)`随机数。
- 开方：`math.sqrt(number)`平方根。

### 路径

- 判断脚本是否被加载执行过：`package.loaded["路径/脚本名"]`，被加载执行过的脚本返回`true`。
- Lua脚本加载路径：`package.path`。在这些路径下的脚本可以加载，各路径以`;`分隔。可以修改`package.path = package.path .. ";C:\\"`。

## 垃圾回收

关键字为`collectgarbage`。使用：

- 获取当前Lua占用内存数：`collectgarbage("count")`。单位KB，乘以1024可以获取到字节数。
- 进行垃圾回收：`collectgarbage("collect")`。

Lua有自动定时垃圾回收的方法，但在Unity开发中不应使用，因为消耗性能，通常在特定时刻如切换场景时手动GC。