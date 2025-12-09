# JSON

JSON（JavaScript Object Notation）是储存、交换文本信息的语法，是一种轻量级的数据交换格式，基于ECMAScript的一个子集。JSON比XML更小、更快、更容易解析。JSON使用JavaScript语法描述数据对象，但仍独立于语言和平台。JSON解析器和JSON库支持多种语言。JSON具有自我描述性，易于人的阅读和编写，同时也易于机器的解析和生成。

JSON数据存储在键值对中，数据对以`,`分隔；使用`{}`表示对象、`[]`保数组。键值对中键写在前、值在后，键用`""`包裹，值根据类型决定是否需要`""`，二者以`:`间隔。值可以是数字、字符串、逻辑值、数组、对象、`null`。

JSON简单的说就是JavaScript中的对象和数组，通过对象和数组可以表示各种复杂的结构。

对象表示为以`{}`包裹的内容，数据结构为`{key:value, key:value, ...}`的键值对结构，其中`key`以`""`包裹，`value`根据类型决定是否需要被`""`包裹。在面对对象的语言中，`key`为对象的属性，`value`为对应属性的值，所以可以使用`obj.key`获取对应属性的值，该值可以是数字、字符串、对象、数组。

数组表示为`[]`包裹的内容，数据结构为`[value, value, ...]`，取值方式按索引获取，值可以是数字、字符串、对象、数组。

``` C#
[
    { "id":2, "name":"痴情咒", "damage":200 },
    { "id":3, "name":"决绝", "damage":255 },
    { "id":4, "name":"斩钢闪", "damage":180 }
]
```

C#解析JSON需要下载第三方程序集，通常使用Json.NET、FastJson、LitJson等。当解析JSON时需要创建相应的类，类的成员名要与JSON中的键相同。

``` C#
class Skill
{
    public int id { get; set; }
    public string name { get; set; }
    public int damage { get; set; }
    
    public Skill(int id, string name, int damage)
    {
        this.id = id;
        this.name = name;
        this.damage = damage;
    }
}
// Json.NET解析JSON
Skill[] skillArray = JsonConvert.DeserializeObject<Skill[]>(
    File.ReadAllText("URL")); // 反序列化：字符串转化为对象

// 序列化：将对象转化为字符串
Skill skill = new Skill(100, "涌泉之恨", 400);
string objectStr = JsonConvert.SerializeObject(skill);
// objectStr: { "id":100, "name":"涌泉之恨", "damage":400 }
string[] skillName = { "千载幽咽", "茫茫焦土", "以彼之道" };
string arrayStr = JsonConvert.SerializeObject(skillName);
// arrayStr: [ "千载幽咽", "茫茫焦土", "以彼之道" ]
```