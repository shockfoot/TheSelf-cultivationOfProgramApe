# 可扩展标记语言

可扩展标记语言（eXtensible Markup Language，XML）被设计用来传输和存储数据，即结构化、存储以及传输信息。

XML文本第一行是文档声明，定义了XML版本和所使用的编码格式。XML中的文本使用标签`<>`包裹，称为元素。XML必须包含根元素，该元素是其他所有元素的父元素。所有元素都可以拥有子元素。XML文档中的元素形成一颗文档树，从根元素开始，向下扩展到树的最低端。标签可以指定属性。

XML标签区分大小写。XML必须包含根元素。所有XML元素都必须有关闭元素。元素必须正确嵌套。标签的属性值需用`""`包裹。注释以`<!-- -->`包裹。XML标签名称可以包含字母、数字以及其他字符，但不能以数字和标点符号以及`xml`/`Xml`/`XML`开始，不能包含空格。

``` xml
<skills>
    <skill>
        <id>2</id>
        <name lang="cn">天下无双</name>
        <damage>123</damage>
    </skill>
    <skill>
        <id>3</id>
        <name lang="cn">永恒零度</name>
        <damage>93</damage>
    </skill>
    <skill>
        <id>4</id>
        <name lang="cn">咫尺天涯</name>
        <damage>400</damage>
    </skill>
</skills>
```

.Net框架提供了`XmlDocument`类来对XML文档进行操作，包括查询、增加、修改、删除、保存等。

``` C#
// XML中的信息通常用类来管理
class Skill
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Lang { get; set; }
    public int Damage { get; set; }
}
// 读取XML
XmlDocument xmlDoc = new XmlDocument();
xmlDoc.Load("URL"); // xmlDoc.Load(string)

XmlNode root = xmlDoc.ChildNotes[1];
XmlNode[] skillList = root.ChildNotes;
List<Skill> skills = new List<Skill>();
// 通过遍历获取节点
foreach (XmlNode skill in skillList)
{
    Skill skillObj = new Skill();
    // 循环遍历获取节点值
    foreach (XmlNode node in skill.ChildNodes)
    {
        if (node.Name == "id")
            skillObj.ID = Int32.Parse(node.InnerText);
        else if (node.Name == "name")
        {
            skillObj.Name = node.InnerText;
            skillObj.Lang = node.Attributes[0].value;
        }
        else
            skillObj.Damage = Int32.Parse(node.InnerText);
    }
    skills.Add(skillObj);
    // 直接获取节点元素
    XmlElement ele = skill["name"];
    skillObj.Name = ele.InnerText;
    skillObj.ID = Int32.Parse(skill["id"].InnerText);
    skillObj.Damage = Int32.Parse(skill["damage"].InnerText);
    // 获取属性
    XmlAttributeCollection attributes = ele.Attributes;
    XmlAttribute attribute = attributes["lang"];
    skillObj.Lang = attribute.value;
}
```