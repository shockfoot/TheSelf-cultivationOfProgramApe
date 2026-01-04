# 天空盒

天空盒是围绕整个场景的包装器，用于模拟天空材质。天空盒材质种类有6 Sided、Procedural、Cubemap三种。

## 6 Sided

Tint Color颜色。

Exposure亮度。

Rotation旋转。

六个面材质。

## Procedural

Sun太阳：None不渲染、Simple简单的、High Quality高质量

Sun Size太阳大小。

Atmosphere Thickne大气层厚度。

Sky Tint天空颜色。

Ground地面颜色。

Exposure亮度。

Render Queue渲染队列。

# 地形Terrain

Raise and lower the terrain调高/低地形高度。

Set the terrain height设置高度。

Smooth the terrain height平滑高度。

Paint the terrain texture画地形贴图。

Place Trees放置树。

Paint Details设置草等细节。

Settings for the terrain设置地形。

# 材质

材质是物质的质地，即色彩、纹理、光滑度、透明度、反射率、折射率、发光度等，实际就是Shader的实例。

Shader着色器是专门用来渲染3D图像的技术，可以使纹理以某种方式展现，实际就是一段嵌入到渲染管线中的程序，可以控制GPU运算图像效果的算法。

Texture纹理是附加到物体表面的贴图。

## 材质属性

Rendering Mode渲染模式：Opaque不透明、Cutout镂空、Fade淡入淡出、Transparent透明

Albedo基础贴图：决定物体表面纹理与颜色。

Metallic金属：使用金属特性模拟外观。

Specular镜面反射：使用镜面特性模拟外观。

Smoothness光滑度。

Normal Map法线贴图：描述物体表面凹凸程度。

Emission自发光：控制物体表面自发光颜色和贴图。

Tilling平铺：沿不同轴，纹理平铺个数。

Offset偏移：滑动纹理。

# 声音

声音Audio系统包含Audio Listener音频监听器、Audio Source音频源、Audio Reverb zone混响等。Unity支持mp3、ogg、wav、aif、mod、it、s3m、xm等格式。

声音分为2D（常用于背景音乐）、3D（有空间感，近大远小）两类。

## Audio Source属性

Audio Clip音频剪辑：需要播放的音频资源。

Output音频输出。

Mute静音。

Bypass Effects直通效果：简单打开/关闭所有音效的办法。

Bypass Listener Effects：是否忽略Listener上应用的效果。

Bypass Reverb Zones：是否忽略混响区域。

Play on Awake：是否场景启动时自动播放。

Loop循环。

Volume音量。

3D Sound Settings

Volume Rolloff声音衰减方式：Linear Rolloff线性衰减、Logarithmic Rolloff对数、Custom Rolloff自定义

# 视频

## Video Player视频播放组件属性

Source播放模式：Video Clip本地音频源、URL路径

Video Clip视频源。

Play On Awake是否场景启动时自动播放。

Wait For First Frame是否等待第一帧后播放。

Loop循环。

Playback Speed播放速度。

Render Mode渲染模式。

Renderer渲染器。

Material Property材质性质，

Audio Output Mode声音播放模式：None、Audio Source、Direction。