# 概述

内置粒子系统通过**Particle System组件**实现。当选择带有Particle System组件的游戏对象时，Scene窗口将显示一个Particle Effect面板，用于预览Particle System组件的效果。

- Play暂停、Restart重新播放、Stop停止。
- Playback Speed：用于加快或减慢粒子模拟速度。
- Playback Time：粒子开始播放的累计时间（秒）。
- Particles：显示已存在粒子数量。
- Speed Range。
- Simulate Layers：预览未选定的粒子系统。默认情况下，只有选定的粒子系统才能在Scene窗口中预览。但是，将Simulate Layers设置为Nothing以外的任何其他选项时，相应层的粒子系统无需选中就会自动播放。
- Resimulate：启用此属性后，粒子系统会立即将属性更改应用于已生成的粒子，否则粒子系统仅将属性更改应用于新粒子。
- Show Bounds：启用时，显示粒子系统的边界。
- Show Only Selected：启用时，隐藏未选择的粒子系统（Unity 2021.3.5f1实测不生效）。

由于Particle System组件非常复杂，所以其属性在Inspector窗口上被分为许多可折叠的模块，其中Open Editor按钮可以打开一个新窗口以设置Particle System组件，从而允许同时编辑多个Particle System组件。Particle System组件有以下模块：

- Main：控制粒子初始状态和全局状态。
- Emission：控制粒子的发射速率、时间、波次。
- Shape：定义发射粒子体积的形状。
- Velocity over Lifetime：控制粒子在其生命周期内的速度。
- Limit Velocity over Lifetime：限制粒子在其生命周期内的速度。
- Inherit Velocity：控制粒子的速度如何随时间推移而受到其父对象移动的影响。
- Lifetime by Emitter Speed：根据粒子生成时发射器的速度控制每个粒子的初始生命周期。
- Force over Lifetime：指定力对粒子产生的影响。
- Color over Lifetime：控制颜色和透明度在其生命周期内的变化。
- Color by Speed：设置粒子的颜色根据粒子速度的变化。
- Size over Lifetime：控制粒子在其生命周期内的大小。
- Size by Speed：设置粒子的大小根据粒子速度的变化。
- Rotation over Lifetime：控制粒子在其生命周期内的旋转。
- Rotation by Speed：设置粒子的旋转根据粒子速度的变化。
- Extrnal Forces：控制外部力场对粒子的影响。
- Noise：为粒子添加噪声。
- Collision：控制粒子如何与场景中其他游戏对象发生碰撞。
- Triggers：控制粒子的触发。
- Sub Emitters：在生命周期内创建子发射器。
- Texture Sheet Animation：控制序列帧播放。
- Lights：控制粒子光照。
- Trails：控制粒子尾迹。
- Custom Data：在Editor中定义要附加到粒子的自定义数据格式。
- Renderer：设置粒子的贴图或网格如何着色和绘制。

除了Main模块，Particle System组件**默认启用了Emission、Shape和Renderer模块**。单击模块名称可以展开/折叠模块，勾选/取消勾选某个模块前的复选框可启用/禁用某个模块。

# Particle System组件各模块

## Main模块

Main模块的名称在Inspector窗口中显示为Particle System组件所附加到的游戏对象的名称。Main模块包含和影响整个粒子系统的全局属性，多数用于控制新创建粒子的初始状态。

- Duration：运行（发射）的时间长度（秒）。
- Looping：启用时，粒子系统将重复运行（无论是在Scene窗口还是运行时）。
- Prewarm：启用此属性，粒子系统将初始化，就像已经完成一个完整周期一样（仅当Looping也启用时生效）。
- Start Delay：运行延迟（秒，仅当Prewarm未启用时生效）。
- Start Lifetime：粒子的初始生命周期（秒）。
- Start Speed：粒子的初始速度。
- 3D Start Size：启用时，设置三个方向上的初始尺寸。
- Start Size：粒子的初始尺寸（同时用于三个方向，仅当3D Start Size未启用时生效）。
- 3D Start Rotation：启用时，设置三个方向上的初始旋转。
- Start Rotation：粒子的初始旋转（同时用于三个方向，仅当3D Start Rotation未启用时生效）。
- Filp Rotation：使指定比例的粒子以相反的方向旋转（值介于0-1）。
- Start Color：粒子的初始颜色。
- Gravity Modifier：重力。
- Simulation Space：控制粒子的运动是相对于父对象、世界空间还是自定义对象。
- Simulation Speed：粒子系统更新的速度。
- Delta Time：是否受`Time.timeScale`的影响。
- Scaling Mode：选择如何缩放。Hierarchy指跟随父级对象进行缩放，Local指忽略父级对象缩放仅应用粒子系统自身Transform的缩放，Shape指应用粒子系统Shape模块中的缩放而不改变粒子本身的大小。
- Play On Awake：启用时，粒子系统会在创建对象时自动启动。
- Emitter Velocity Mode：选择粒子系统是通过Transform组件还是Rigidbody组件运动。
- Max Particles：最大粒子数。
- Auto Random Seed：是否使用随机种子。
- Random Seed：随机种子（仅当Auto Random Seed未启用时生效）。
- Stop Action：当属于粒子系统的所有粒子都已完成时，可执行Disable禁用对象、Destroy摧毁对象、Callback调用附加到游戏对象上任何脚本的`OnParticleSystemStopped`回调操作。当一个系统的所有粒子都已死亡，并且系统存活时间已超过Duration设定的值时，判定该系统已完成。对于启用Looping的系统，只有在通过脚本停止系统时才会发生这种情况。
- Culling Mode：选择粒子系统不在摄像机范围内时是否暂停粒子系统模拟。Automatic指未启用Looping时不暂停，启用时暂停。Pause and Catch-up指粒子系统在摄像机范围外时暂停模拟，当重新进入摄像机范围内时，计算到此时应该处于的状态。Pause指暂停模拟。Always Simulate指总是模拟。
- Ring Buffer Mode：选择粒子缓冲区模式。Disabled指粒子生命周期结束时立即回收指缓冲区。Pause Until Replaced指在粒子生命周期结束时暂停模拟但不回收，直到系统粒子数达到上限时才回收旧粒子。Loop Until Replaced指在粒子生命周期结束时回到指定比例的生命周期处但不回收，直到系统粒子数达到上限时才回收旧粒子。
- Loop Range：指定粒子生命周期结束时回到的生命周期比例（值介于0-1，仅当Ring Buffer Mode为Loop Until Replaced生效）。

## Emission模块

Emission模块用于控制粒子每秒发射数量、波次、间隔等。

- Rate over Time：每秒发射的粒子数。
- Rate over Distance：每移动单位距离发射的粒子数。
- Bursts：用于设置粒子爆发的参数，制作如爆炸、间歇性喷射等效果。
- - Time：爆发时间（粒子系统开始播放后的累计秒数）。
- - Count：数量。
- - Cycles：次数。
- - Interval：间隔（秒）。
- - Probability：概率（值介于0-1）。

## Shape模块

Shape模块用于定义粒子的发射体积的形状和发射方向。由于内置的发射体积众多，不同发射体积的设置方式也不尽相同，但总体可分为三个部分：定义发射形状、纹理、变换。

### 发射形状

内置发射体积主要有Sphere（球体）、Hemisphere（半球）、Cone（锥台）、Donut（甜甜圈）、Box（长方体）、Mesh/Mesh Renderer/Skinner Mesh Renderer（网格）、Sprite/Sprite Renderer（精灵）、Circle（圆圈）、Edge（边）、Rectangle（矩形）。

球体和半球在所有方向均匀发射粒子，二者的参数设置相同。

- Radius：半径。
- Radius Thicknese：发射粒子的径向体积比例（值介于0-1）。0表示从外表面发射粒子，1表示从整个体积发射粒子，介于两者之间的值将使用体积的一定比例。
- Arc：设置球体的弧度，即球体中整圆的范围。
- - Mode：定义如何在形状的弧形周围生成粒子。Random随机生成，Loop沿着形状生成，Ping-Pong沿着形状来回生成，Burst Spread指在形状周围均匀分布粒子的生成位置。
- - Spread：设置发射器产生粒子的离散间隔（值介于0-1）。0表示形状的任意位置，0.5表示在两个位置产生粒子，1表示只在一个位置产生粒子。
- - Speed：发射位置绕弧形移动的速度（仅当Mode为Loop和Ping-Pong时生效）。

锥台从底部或主体发射粒子，比球体和半球多了一些参数。

- Angle：设置锥台斜面的开合范围，0度时是圆柱体，90度时是圆盘。
- Radius：锥台下底面半径。
- Emit form：锥台发射粒子的部位。Base指底面，Volume指体积中任意位置。
- Length：锥台长度（仅当Emit form为Volume时生效）。

甜甜圈比球体和半球多了一个参数。

- Radius：主圆环的半径，即环体圆心到圈体圆心的距离。
- Donut Radius：环体的半径。

长方体从边、表面或主体发射粒子，粒子在发射器对象的前向（Z）方向上移动，其参数主要有：

- Emit from：发射粒子的部位。Volume从体积中任意位置，Shell从外边缘发射，Edge从边缘发射。
- Box Thickness：可以控制各方向发射粒子的体积比例（值介于0-1，仅当Emit from设置为Shell或Edge时生效）。

Mesh/Mesh Renderer/Skinner Mesh Renderer从指定的网格发射粒子，三者的参数设置相同。

- Type：指定从何处发射粒子。Vertex从顶点发射，Edge从边缘发射，Triangle从三角面发射。
- - Mode：定义发射粒子的顺序。Random随机生成，Loop沿着形状生成，Ping-Pong沿着形状来回生成（仅当Emit from设置为Vertex或Edge时生效）。
- - Spread：设置发射器产生粒子的离散间隔（值介于0-1，仅当Emit from设置为Edge时生效）。
- - Speed：发射位置绕弧形移动的速度（仅当Emit from设置为Edge且Spread设置为Loop或Ping-Pong时生效）。
- Mesh：提供发射器形状的网格。
- Single Material：指定是否从特定子网格（由材质索引号标识）发射粒子。如果启用此属性，则会显示一个数字字段，可以使用该字段指定材质索引号。
- Use Mesh Colors：使用网格顶点颜色调整粒子颜色，如果顶点颜色不存在，则使用材质中着色器颜色属性`Color`或`TintColor`。
- Normal Offset：在距离网格表面多远处发射粒子（在表面法线的方向上）。

Sprite/Sprite Renderer从指定的精灵形状发射粒子，二者的参数设置相同。

- Type：指定从何处发射粒子。Vertex从顶点发射，Edge从边缘发射，Triangle从三角面发射。
- Sprite：提供发射器形状的精灵。
- Normal Offset：在距离精灵表面多远处发射粒子（在表面法线的方向上）。

Circle从圆形的中心或边缘均匀发射粒子，粒子沿径向向外移动，其参数设置与球和半球相同。

Edge从边发射粒子，粒子在发射器对象向上（Y）方向上移动。

- Radius：长度。
- Mode：定义如何在形状的弧形周围生成粒子。Random随机生成，Loop沿着形状生成，Ping-Pong沿着形状来回生成，Burst Spread指在形状周围均匀分布粒子的生成位置。
- Spread：设置发射器产生粒子的离散间隔（值介于0-1）。0表示形状的任意位置，0.5表示在两个位置产生粒子，1表示只在一个位置产生粒子。
- Speed：发射位置绕弧形移动的速度（仅当Mode为Loop和Ping-Pong时生效）。

Rectangle从矩形发射粒子，粒子在发射器对象向上（Y）方向上移动。

### 纹理

纹理主要用于设置粒子效果。

- Texture：在粒子表面贴上图片纹理，此属性如果不设置，则不会出现以下属性。纹理贴图总是出现在发射形状的底部。
- Clip Channel：裁剪通道，可以选择R、G、B、A通道以对粒子进行操作。
- Clip Threshold：裁剪阈值，当上述选择通道的指超过此值时通过，否则裁剪（值介于0-1）。0表示全通过，1表示全不通过。
- Color affects Particles：启用时粒子颜色受到纹理颜色影响。
- Alpha affects Particles：启用时粒子透明度受到纹理透明度影响。
- Bilinear Fitering：在读取纹理时，无论纹理尺寸如何，均组合4个相邻样本以获得更平滑的粒子颜色变化。

### 变换

变换主要用于设置粒子发射器的位置、旋转和缩放。

- Position：对粒子发射器添加一个位置偏移。
- Rotation：对粒子发射器添加一个旋转。
- Scale：对粒子发射器添加一个缩放。
- Align To Direction：启用时，根据粒子的初始行进方向定向粒子。**未启用时，粒子的旋转不会受到游戏对象Transform旋转和形状变换中Rotation的影响，而启用时则会受到游戏对象Transform旋转的影响。**
- Randomize Direction：设置混合随机方向的粒子的比例。0时全不随机，1时全随机。
- Spherize Direction：将粒子方向朝球面方向混合，从它们的变换中心向外行进。0时不起作用，1时粒子方向从中心向外（与Shape设置为Sphere时的行为相同）。
- Randomize Position：随机粒子偏移量，值越大偏移量越大。

## 生命周期相关模块

### Velocity over Lifetime模块

Velocity over Lifetime模块用于控制粒子在其生命周期内的速度变化。

- Linear X/Y/Z：粒子在X、Y和Z轴上的线性速度。
- - Space：Linear X/Y/Z是参照本地坐标系还是世界坐标系。
- Orbital X/Y/Z：粒子围绕X、Y和Z轴旋转的轨道速度。
- Offset X/Y/Z：围绕X、Y和Z轴旋转中心的位置偏移。
- Radial：粒子远离/朝向中心位置的径向速度。
- Speed Modifier：对粒子速度应用一个乘数。

### Limit Velocity over Lifetime模块

Limit Velocity over Lifetime限制粒子在其生命周期内的速度。

- Separate Axes：是否在每个轴上独立控制粒子最大速度。
- - Separated Speed X/Y/Z：单独的X、Y和Z分量（仅当Separate Axes启用时生效）。
- - Space：速度分量是参照本地坐标系还是世界坐标系（仅当Separate Axes启用时生效）。
- - Speed：最大速度（仅当Separate Axes未启用时生效）。
- - Dampen：当粒子速度超过限制时，粒子速度衰减的比例（值介于0-1）。0表示不衰减，1表示立即衰减值最大速度。
- Drag：对粒子速度施加线性阻力（模拟空气阻力），作用于全局，不受上述参数影响。
- - Multiply by Size：启用时，较大粒子会更大程度上受到阻力的影响。
- - Multiply by Velocity：启用时，较快的粒子会更大程度上受到阻力的影响。

### Lifetime by Emitter Speed模块

Lifetime by Emitter Speed根据粒子生成时发射器的速度控制每个粒子的初始生命周期。它将粒子的初始生命周期乘以一个值，该值取决于产生粒子时发射器的速度。对于大多数粒子系统，发射器的速度为游戏对象的速度；但子发射器的速度来自粒子所源自的父粒子。

- Multiplier：应用于粒子生命周期的系数。
- Speed Range：粒子系统映射到Multiplier曲线上的速度的最大和最小值（仅当 Multiplier设置为Curve或Random Between Two Curves时生效）。

### Force over Lifetime模块

Force over Lifetime用于设置力（风或吸力）对粒子产生的影响。

- X/Y/Z：在X、Y和Z轴上施加到每个粒子的力。
- Space：选择是在局部空间还是在世界空间中施力。
- Randomize：启用时会在每帧在定义的范围内随机新的作用力方向以产生更动荡、不稳定的运动（仅当X/Y/Z使用Random Between Two Constants或Random Between Two Curves时生效）。

### Color over Lifetime模块

Color over Lifetime用于指定粒子的颜色和透明度在其生命周期中如何变化。

- Color：粒子在其生命周期内的颜色渐变。左侧点表示寿命的开始，右侧表示寿命的结束。

### Color by Speed模块

Color by Speed用于设置粒子速度（每秒的距离单位）对其的颜色影响。

- Color：在速度范围内定义的粒子的颜色渐变。

- Speed Range：颜色渐变映射到的速度范围的下限和上限（超出范围的速度将映射到端点）。

### Size over Lifetime模块

Size over Lifetime用于设置粒子在生命周期内尺寸的变化。

- Separate Axes：是否在每个轴上独立控制粒子大小。

- Size：尺寸变化。

### Size by Speed模块

Size by Speed用于设置粒子速度（每秒的距离单位）对其的尺寸影响。

- Separate Axes：是否在每个轴上独立控制粒子大小。

- Size：尺寸变化。

- Speed Range：尺寸映射到的速度范围的下限和上限（超出范围的速度将映射到端点）。

### Rotation over Lifetime模块

Rotation over Lifetimeu用于粒子在生命周期内旋转的变化。

- Separate Axes：是否在每个轴上独立控制粒子旋转。

- Angular Velocity：旋转速度（度每秒）。

### Rotation by Speed模块

Rotation by Speed用于设置粒子速度（每秒的距离单位）对其的旋转影响。

- Separate Axes：是否在每个轴上独立控制粒子旋转。
- Angular Velocity：旋转速度（度每秒）。
- Speed Range：旋转速度映射到的速度范围的下限和上限（超出范围的速度将映射到端点）。

## Inherit Velocity模块

Inherit Velocity用于控制粒子的速度如何随时间推移而受到其父对象移动的影响。

- Mode：应用模式。Current指将速度应用到每一帧上的所有粒子。Initial指在生成粒子时应用一次。

- Multiplier：粒子受到影响的比例。

## Sub Emitters模块

Sub Emitters用于在粒子的生命周期内创建子发射器。

- 触发时机：Birth创建、Collision碰撞、Death消亡、Trigger触发、Manual脚本。Collision、Death、Trigger、Manual只能使用Emission模块中的爆发发射。

- 继承的属性：Color颜色、Size尺寸、Rotation旋转、Lifetime寿命、Duration持续时间等。

- Emit Probability配置子发射器事件的触发概率。1保证事件将触发，而更小的值则会降低概率。

## External Force模块

External Force控制力场对粒子的影响。

- Multiplier：施加到粒子的力的系数因子，1表示全施加，0表示不施加。
- Influence Filter：选择通过何种方式控制力场对粒子的影响。Layer选择生效层；List选择确定粒子系统；Layer Mask And List则通过层和指定的粒子。

## Noise模块

Noise用于为粒子添加噪声。

- Separate Axes：是否在每个轴上独立控制强度和映射。

- Strength：定义粒子生命周期内噪声对粒子的影响强度。值越高，粒子移动越快和越远。

- Frequency：可控制粒子改变行进方向的频率以及方向变化的突然程度。值越大，运动越明显。

- Scroll Speed：噪声图的滚动速度。随着时间的推移而移动噪声场可产生更不可预测和不稳定的粒子移动。值越大，粒子越不稳定。

- Damping：启用后，让Strength与Frequency成正比。

- Octaves：指定组合多少层数重叠噪声来产生最终噪声值。使用更多层可提供更丰富、更有趣的噪声，但会显著增加性能成本。

- Octave Multiplier：对于每个附加的噪声层，按此比例降低强度。

- Octave Scale：对于每个附加的噪声层，按此乘数调整频率。

- Quality：设置噪声质量。

- Remap：将最终噪声值重新映射到不同的范围。

- Remap Curve：描述最终噪声值如何变换的曲线。

- Position Amount：用于控制噪声对粒子位置影响程度的乘数。

- Rotation Amount：用于控制噪声对粒子旋转（以度/秒为单位）影响程度的乘数。

- Size Amount：用于控制噪声对粒子大小影响程度的乘数。

## Collision模块

Collision用于控制粒子如何与场景中的游戏对象碰撞。粒子碰撞分两种碰撞模式：Planes和World，其中World模式会与世界中任何带有碰撞器的对象发生碰撞。

对于Planes模式的碰撞：

- Planes：要进行碰撞的平面。
- Visualization：选择要将Scene窗口中的碰撞平面辅助图标显示为网格还是平面。
- Scale Plane：Scene窗口中的碰撞平面辅助图标的大小。

对于World模式的碰撞：

- Mode：选择3D还是2D的碰撞。
- Collision Quality：设置碰撞质量（在较低的质量水平下，粒子有时会穿过碰撞体）。High时始终使用物理系统来检测碰撞结果，最准确但最耗费性能；Medium和Low使用一组体素来缓存先前的碰撞，从而在以后的帧中更快地重用。Medium和Low之间的唯一区别是粒子系统在每帧查询物理系统的次数，Medium每帧的查询次数多于Low。Medium和Low仅适用于从不移动的静态碰撞体。
- - Collides With：碰撞层。
- - Max Collision Shapes：最大可碰撞形状数量。
- - Enable Dynamic Collision：是否响应与动态对象的碰撞。
- - Voxel Size：体素（Voxel）表示三维空间中的常规网格上的值。使用Medium或Low质量碰撞时，Unity会在网格结构中缓存碰撞。此设置控制着网格大小。较小的值可提供更高的准确性，但会占用更多内存，效率也会降低。
- Collider Force：在粒子碰撞后对物理碰撞体施力，通常用于粒子推动碰撞体。
- - Multiply by Collision Angle：启用时根据粒子与碰撞体之间的碰撞角度来缩放力的强度。斜向碰撞将比正面碰撞产生更小的力。
- - Multiply by Particle Speed：启用时根据粒子的速度来缩放力的强度。速度快的粒子产生的力更大。
- - Multiply by Particle Size：启用时根据粒子的大小来缩放力的强度。较大的粒子产生的力更大。

公共参数：

- Dampen：粒子碰撞后损失的速度比例。
- Bounce：粒子碰撞后从表面反弹的速度比例。
- Lifetime Loss：粒子碰撞后损失的总生命周期比例。
- Min/Max Kill Speed：碰撞后运动速度低/高于此速度的粒子将从系统中予以移除。
- Radius Scale：调整粒子碰撞球体的半径以使其更贴近粒子图形的可视边缘。
- Send Collision Messages：启用时可以调用附加到游戏对象上任何脚本的`OnParticleCollision`回调。
- Visualize Bounds：在Scene窗口中显示粒子的碰撞边界。

## Trigger模块

Trigger用于控制粒子与场景中的游戏对象的触发。启用时，可以调用附加到游戏对象上任何脚本的`OnParticleTrigger`回调。

- 触发列表：指定粒子可在场景中进行触发的碰撞器。
- Inside：粒子在触发器的边界内时触发。
- Outside：粒子在触发器的边界外时触发。
- Enter：粒子在进入触发器的边界时触发。
- Exit：粒子在离开触发器的边界时触发。
- - Ignore：忽略粒子，无法在`OnParticleTrigger`中访问粒子。
- - Kill：销毁粒子，无法在`OnParticleTrigger`中访问粒子。
- - Callback：触发回调，可以在`OnParticleTrigger`中访问粒子。
- Radius Scale：调整粒子碰撞球体的半径以使其更贴近粒子图形的可视边缘。
- Visualize Bounds：在Scene窗口中显示粒子的碰撞边界。

## Texture Sheet Animation模块

Texture Sheet Animation用于控制序列帧播放，有两种播放模式：Grid和Sprites，其中，Grid使用材质中的贴图而Sorutes则直接指定贴图。

对于Grid模式：

- Tiles：瓦片列数（X）和行数（Y）。
- Animation：设置播放序列帧的方式。Whole Sheet指整个序列帧作为一个动画序列。Single Row即每一行代表一个动画序列，通过Row Mode选择指定行（Custom指定行，Random是随机选择行，Mesh Index根据分配给粒子的网格索引选择一行，通常用于确保使用特定网格的粒子也要使用相同的纹理）。

对于Sprites模式：

- Sprite：指定播放的序列帧。

公共参数：

- TimeMode：选择对动画帧的采样方式。Lifetime指在粒子的生命周期内使用动画曲线对帧进行采样；Speed根据粒子的速度对帧进行采样，速度范围指定选择帧的最小和最大速度范围；FPS根据指定的每秒帧数值对帧进行采样。
- - Frame over Time：根据时间进行采样。
- - Speed Range：采样的速度范围。
- - FPS：采样帧率。
- Start Frame：允许指定粒子动画应从哪个帧开始（增加动画随机性）。
- Cycles：动画序列在粒子生命周期内重复的次数。
- Affected UV Channels：允许具体指定粒子系统影响的UV通道。

## Lights模块

Lights用于给粒子添加光照。

- Light：光源。
- Ratio：接受光照的粒子的比例。
- Random Distribution：是否使用随机分配光照。启用时，每个粒子都根据Ratio进行判断是否接受光照，否则由Ratio控制粒子接受光照的频率（如第n个粒子将接受光照）。
- Use Particle Color：是否使用粒子颜色调制光照颜色。
- Size Affects Range：粒子尺寸是否影响光源的光照范围。
- Alpha Affects Intensity：粒子Alpha是否影响光源的光照强度。
- Range Multiplier：影响光照范围的因子。
- Intensity Multiplier：影响光照强度的因子。
- Maximum Lights：最大光照。

## Trails模块

Trails用于控制粒子尾迹，有两种模式：Particles和Ribbon，其中Particles模式的每个粒子都产生一个尾迹，Ribbon模式的粒子连在一起产生类似丝带样式的尾迹。

对于Particles模式：

- Ratio：渲染尾迹的粒子的比例。
- Lifetime：尾迹的生命周期，是粒子生命周期的百分比（值介于0-1）。
- Minimum Vertex Distance：尾迹顶点之间的最小距离。值越小，顶点越多，尾迹越丝滑。
- World Space：选择尾迹的运动是否相对于世界空间。
- Die with Particles：尾迹是否随着粒子的消亡而消亡。

对于Ribbon模式：

- Ribbon Count：数量。
- Split Sub Emitter Ribbons：启用时，子发射器所属父系统的粒子共享尾迹。
- Attach Ribbons To Transform：尾迹是否连接到原点（模拟空间是局部空间是原点为父对象，世界空间是世界原点）。

公共参数：

- Texture Mode：选择贴图如何应用到粒子尾迹。Stretch模式会拉伸纹理；Tile模式重复贴图；Repeat Per Segment模式沿着轨迹重复纹理，以每个轨迹段重复一次的速率重复。Distribute Per Segment模式沿着轨迹的整个长度映射纹理一次，并假设所有顶点都是均匀间隔的。
- Size affects Width：选择粒子大小是否会影响尾迹宽度。
- Size affects Lifetime：选择粒子大小是否会影响尾迹寿命。
- Inherit Particle Color：选择粒子颜色是否会影响尾迹颜色。
- Color over Lifetime：控制尾迹颜色在粒子生命周期内的变化。
- Width over Trail：控制尾迹宽度随着其长度的变化。
- Color over Trail：控制尾迹颜色随着其长度的变化。
- Generate Lighting Data：启用时可在构建尾迹几何体时包含法线和切线。这样允许它们使用具有场景光照的材质，如使用标准着色器或自定义着色器的材质。
- Shadow Bias：阴影偏移。

## Custom Data模块

Custom Datau用于自定义要附加到粒子的自定义数据格式。

## Renderer模块

Renderer模块设置粒子的图像或网格如何着色和渲染。

- Remder Mode：设置渲染模式。

- - Billboard：朝向在Render Alignment中指定的方向渲染。
  
  - Stretched Billboard：朝向摄像机渲染并应用已设置的缩放。
  
  - - Camera Scale：根据摄影机移动拉伸粒子。将此设置为零可禁用摄影机移动拉伸。
    
    - Speed Scale：根据粒子的速度按比例拉伸粒子。将此设置为零可禁用基于速度的拉伸。
    
    - Length Scale：沿粒子速度方向根据宽度按比例拉伸粒子，使其与当前大小成比例。将其设置为零会使粒子消失，实际上长度为零。
    
    - Freeform Stretchjing：是否启用自由拉伸。启用后，正面观察时粒子不会变薄。
    
    - Rotate With Stretch：是否根据粒子拉伸的方向旋转粒子。当Freeform Stretchjing未启用时，此属性默认启用。
  
  - Horizontal Billboard：粒子与XZ平面（地面）平行渲染。
  
  - Vertical Billboard：粒子垂直XZ平面（地面）渲染，但始终面向摄像机。
  
  - Mesh：使用网格渲染。
  
  - - Mesh Distribution：指定将网格随机分配给粒子的方法。Uniform Random将网格均匀的随机分配给粒子。Non-uniform Random按自定义权重分配网格。
    
    - Meshes：网格。
    
    - Mesh Weightings：权重（仅当Mesh Distribution为Non-uniform Random时生效。）
  
  - None：不渲染粒子。通常与Trails模块组合使用以渲染尾迹。

- Normal Direction：法线方向。值为1.0时，法线指向摄影机，值为0.0时，法线朝向屏幕中心（仅当Remder Mode为四种Billboard之一时生效）。

- Material：材质。

- Trail Material：尾迹材质（仅当Trails模块启用时生效）。

- Sort Mode：粒子间排序模式。By Distance指先距离摄像机距离近的渲染在上层，旋转摄像机时不会改变粒子渲染顺序；Oldest in Front指寿命长的渲染在上层；Youngest in Front指寿命短的渲染在上层；By Depth根据粒子与摄影机近平面的距离渲染粒子，旋转摄像机时可能改变粒子渲染顺序。

- Sorting Fudge：粒子系统排序顺序的偏差。值小的渲染在上层。此设置仅影响场景中出现的整体粒子系统，它不会对系统内的单个粒子进行排序。

- Min/Max Particle Size：最小/大粒子尺寸（不管其他设置如何，表示为视口大小的分数，仅当Remder Mode为四种Billboard之一时生效）。

- Render Alignment：粒子的朝向。View指面向摄像机的视角；World指世界Z轴；Local指局部坐标；Facing指面向摄像机的位置；Velocity指面向其速度方向。

- Flip：镜像渲染粒子比例。

- Enable Mesh GPU Instancing：是否使用GPU实例化渲染粒子系统（仅当Render Mod为Mesh时生效）。

- Allow Roll：控制面向摄影机的粒子是否可以围绕摄影机的Z轴旋转。禁用此选项对于VR应用程序特别有用，因为HMD（头戴式显示器）滚动会导致粒子系统发生不希望的粒子旋转。

- Pivot：修改旋转粒子的轴心点。此值是粒子大小的乘数。

- Visualize Pivot：在Scene窗口中预览粒子轴心点。

- Masking：遮罩模式。

- Apply Active Color Space：在线性颜色空间中渲染时，系统在将粒子颜色上传到GPU之前，会从Gamma空间转换粒子颜色。

- Custom Vertex Streams：配置材质的顶点着色器中可用的粒子属性。

- Cast Shadows：是否投射阴影，其中Two Sided允许从网格的任一侧投射阴影，启用时忽视背面剔除；Shadows Only指仅阴影可见。

- Receive Shadows：指示此系统中的粒子是否可以接收来自其他源的阴影。只有不透明材质才能接收阴影。

- Shadows Bias：阴影偏移。

- Motion Vectors：是否使用运动矢量来跟踪此粒子系统的Transform组件从一帧到下一帧的每像素屏幕空间运动。Camera Motion Only指仅使用摄像机跟踪运动；Per Object Motion指使用特定通道跟踪运动；Force No Motion指不跟踪。

- Sorting Layer ID：排序图层。

- Order in Layer：在排序图层中的顺序。

- Light Probes：光照探针。

- Reflection Probes：反射探针。

# 粒子系统力场

Particle System Force Field组件用于将力应用于粒子。所有力都施加在力场的本地空间中。旋转会影响力场方向和旋转属性。

形状部分控制影响区域的形状，可选为Sphere、Hemisphere、Cylinder和Box。

- Start Range：开始范围。
- End Range：结束范围。
- Length：长度。

方向部分选择力在X/Y/Z三个轴的方向。

引力部分设置引力的影响。

- Strength：强度。
- Focus：引力的中心点。值为0将粒子吸引到形状的中心，值为1将粒子吸引到形状的外边缘。

涡流部分设置力场的涡流。

- Speed：速度。
- Attraction：强度。值为1表示最大，值为0表示没有。
- Randomness：设置一个随机的形状轴来推动周围的粒子。值为1表示最大随机性，值为0表示没有随机性。

阻力用于减少粒子的速度。

- Strength：强度。
- Multiply by Size：是否根据粒子的尺寸调整阻力。
- Muitiply by Velocity：是否根据粒子的速度调整阻力。

矢量场是预先计算好的力场，Unity无法直接制作，需要用到插件或在其他软件制作。

- Volume Texture：矢量场纹理。
- Speed：速度因子。值越大，速度越快。
- Attraction：阻力强度。值越大，阻力越大。