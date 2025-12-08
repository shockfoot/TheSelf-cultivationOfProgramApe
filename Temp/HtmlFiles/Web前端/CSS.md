# 层叠样式表

层叠样式表（Cascading Style Sheets，CSS）又叫级联样式表，用于HTML文档中元素样式的定义。使用CSS的唯一目的就是让网页具有美观、一致的页面。CSS文件后缀为.css。

CSS语法主要由选择器和声明组成。选择器是要改变样式的元素，声明由属性/值对组成，其中属性是希望设置的样式属性，属性与之以冒号分隔。

# 注释

CSS注释以`/*`开始，以`*/`结束。

# 引入方式

- 内联样式：在开始标签使用style属性指定内联样式。内联样式优先级最高，但缺乏整体性和规划性，不利于维护。
- 内部样式：在head标签内使用style标签添加内部样式。单个页面内的样式具有统一性和规划性，但多个页面之间容易造成混乱。
- 外部样式：在head标签内使用link标签添加外部样式。

# 选择器

- 继承。
- 全局选择器：通配符`*`，可以与任何元素匹配，通常用于初始化元素样式。
- 元素选择器：标签名，匹配全部同名元素。
- 类选择器：`.`加类名。
- ID选择器：`#`加ID。
- 合并选择器：使用`,`连接选择器，表示为各选择器添加相同样式。
- 后代选择器：使用` `连接选择器，表示匹配前者后代中指定的元素。
- 子代选择器：使用`>`连接选择器，表示匹配前者子代中指定的元素。
- 相邻兄弟选择器：使用`+`连接选择器，表示匹配前者之后的第一个指定的兄弟元素。
- 通用兄弟选择器：使用`~`连接选择器，表示匹配前者之后的所有指定的兄弟元素。

选择器具有优先级。高优先级的样式会覆盖低优先级的样式。继承和全局选择器最低，元素选择器权重为1，类选择器权重为10，ID选择器权重为100，内联样式权重为1000。

权重越大越优先。相同权重时，后出现的优先与先出现的。创作者的样式优先于浏览器的默认样式。继承的样式不如显示指定的。`!important`的优先级最大。

# 属性

属性以名称/值对出现。颜色相关属性的值可以设置为颜色关键字、十六进制、RGB、RGBA。字体大小相关属性的值可以使用rem相对于根字体尺寸、em相对于当前字体尺寸、px像素。宽高相关属性的值可以设置为px、%等。

## 字体属性

| 属性           | 说明                                                         |
| :------------- | :----------------------------------------------------------- |
| `font-size`    | 字体字号。                                                   |
| `font-weight`  | 字体粗细，值可为100-900，其中400等同于defult，700等同于bold，更粗为bolder，细为lighter。 |
| `font-style`   | 字体样式，默认为normal，italic斜体。                         |
| `font-family`  | 字体，值之间使用` `分隔，按顺序使用，如果字体包含空格以及特殊符号则必须使用双引号`""`包裹。 |
| `font-variant` | 定义小型大写字母文本，值可为nromal（默认），small-caps小型大写字母。 |

字体属性声明可以简写，使用`font`属性，值的顺序为style、variant、weight、size/line-height、family，其中size和family是必须的，其他的如果缺省将以默认值填入（如果有）。

## 背景属性

| 属性                    | 说明                                                         |
| :---------------------- | :----------------------------------------------------------- |
| `background-color`      | 背景颜色。                                                   |
| `background-image`      | 背景图片，以`url("URL")`设置路径。                           |
| `background-position`   | 背景图片渲染位置，可以使用数值或百分比，位置关键字left、right、top、bottom、center。 |
| `background-size`       | 背景图片大小，可以使用数值或百分比设置宽高或宽高百分比，cover保持宽高比将其缩放至覆盖全部区域的最小大小，contain保持宽高比将其缩放至充满容器的最大大小。 |
| `background-repeat`     | 背景图片平铺方式，默认repeat都平铺，repeat-x水平方向，repeat-y垂直方向，no-repeat不平铺。 |
| `background-attachment` | 背景图片是否可以滚动，属性值可为scroll（默认），fixed固定，local随元素内容滚动。 |

背景属性可以简写，使用`backgrount`，属性值的顺序为color、image、position/size、repeat、origin、clip、attachment。

## 文本属性

| 属性                  | 说明                                                         |
| :-------------------- | :----------------------------------------------------------- |
| `color` | 文本颜色。 |
| `text-align`    | 文本水平对齐方向，left、right、center。                                                 |
| `text-decoration` | 文本修饰，underline下划线，overline上划线，line-through中划线。 |
| `text-transform` | 文本大小写，captialize单词首字母大写，uppercase大写，lowercase小写。 |
| `text-indent`   | 文本首行缩进，常用em为单位。 |
| `line-height` | 行高。当行高等于容器高度时，文本可以垂直居中。 |
| `direction` | 文本方向，属性值可谓ltr从左向右（默认），rtl从右向左。 |
| `letter-spacing` | 字符间距。 |
| `text-shadow` | 文本阴影，后跟h-shadow水平、v-shadow垂直距离，可选blur模糊距离和color颜色 |
| `vertical-align` | 垂直对齐方式，默认baseline基线，sub下标，sup上标，top最高元素的顶端，text-top父元素字体顶端，middle中部，bottom底部，text-bottom父元素字体底部，数值等。 |
| `white-space` | 空白处理方式，默认normal忽略，pre保留，nowrap不换行，pre-wrap保留空白并正常换行，pre-line合并空白但保留换行符， |
| `word-spacing` | 字间距。 |

## 表格属性

| 属性              | 说明                                                         |
| :---------------- | :----------------------------------------------------------- |
| `width`           | 宽度。                                                       |
| `height`          | 高度。                                                       |
| `margin`          | 外边距。一个值设置四边，两个则设置上下和左右，四个则设置上、右、下、左。 |
| `padding`         | 内边距。                                                     |
| `border-width`    | 边框尺寸。                                                   |
| `border-color`    | 边框颜色。                                                   |
| `border-style`    | 边框样式，值可为none，dotted点，dashed虚线，solid实线，double双实线等。 |
| `border-radius`   | 边框圆角。                                                   |
| `border-collapse` | 边框是否折叠成一条线，默认不折叠，collapse折叠。             |

边框属性可以简写，属性值顺序为width、color、style。边框可以对四边进行分别设置。

## 链接属性

| 属性        | 说明             |
| :---------- | :--------------- |
| `a:link`    | 未访问过的链接。 |
| `a:visited` | 已访问过的链接。 |
| `a:hover`   | 鼠标悬停时样式。 |
| `a:active`  | 被点击时的样式。 |

## 列表属性

| 属性                  | 说明                                                         |
| :-------------------- | :----------------------------------------------------------- |
| `list-style-type`     | 列表项标记类型                                               |
| `list-style-image`    | 自定义列表项标记图片。                                       |
| `list-style-position` | 相对于内容如何绘制列表项标记，值可为outside（默认），inside。 |

列表属性可以简写，使用`list-style`，属性值顺序为type、position、image。

## 布局属性

| 属性                  | 说明   |
| :-------------------- | :----- |
| `width`               | 宽度。 |
| `height`              | 高度。 |
| `background-position` |        |
| `background-size`     |        |
| `font-family`         |        |

# 盒子模型

所有HTML元素都可以看作盒子。在CSS中，使用盒子模型（Box model）用于设计和布局。

盒子模型包括外边距（Margin）、边框（Border）、内边距（Padding）和实际内容（Content）。