# FFmpet

FFmpeg是一个开源的跨平台音视频处理框架，涵盖了录制、转换、流化等一系列音视频处理功能，支持诸如AVI、MP4、MOV、FLV、WMV、MPEG、MKV等海量音视频格式，并且能在Windows、Mac OS、Linux等多种操作系统上稳定运行。

FFmpeg最初由法国计算机程序员Fabrice Bellard于2000年创建，其中“FF”代表“Fast Forward”，意思是快进，“mpeg”则是流行的视频压缩标准MPEG，即运动图像专家组。后来由Michael Niedermayer接手并持续开发，众多来自MPlayer项目的开发者也参与其中，为FFmpeg添砖加瓦，使其逐渐成长为功能完备的强大工具。

## 安装与环境配置

### Windows

1. 于FFmpeg官网（<https://ffmpeg.org/>）-> Download -> Windows -> Windows buildings by BtbN下载对应程序，并解压到指定文件夹；
2. 将bin文件夹的完整路径添加到环境变量中系统变量的Path里；
3. 在命令提示符窗口输入“ffmpeg -version”检查FFmpeg是否配置完成。

## 命令行操作

FFmpeg命令行的基本形式为：`ffmpeg [全局参数] {[输入文件参数] -i 输入文件地址} ... {[输出文件参数] 输出文件地址} ...`。

- 全局参数影响整个程序运行。
- 输入文件参数指定输入源特性。
- 输出文件参数则决定输出结果。

各参数作用如下：

- `-i`：定位输入源。有多个时，多次使用`-i`依次指定。
- `-preset`：指定视频质量，可以跟ultrafast、superfast、veryfast、faster、fast、medium（默认）、slow、slower、veryslow。此参数允许在编码速度和文件大小之间做取舍，编码越快，文件越大。
- `-crf`：即Constant Rate Factor，设定画面质量，取值0（无损压缩）-51（最差质量）。此参数允许在图像质量和文件大小之间做权衡，数值越小，质量越高，19-28为常用区间。
- `-c:v`：指定视频编码器。
- `-c:a`：指定音频编码器。
- `-vf`：指定视频过滤器（Video Filter）。视频过滤器可以对视频图像进行变换（修改尺寸、裁剪、旋转等）、添加滤镜等。可以指定一系列过滤器，前一个过滤器的输出将直接作为下一个过滤器的输入。
- `-af`：指定音频过滤器（Audio Filter）。
- `-vn`：删除视频轨。
- `-an`：删除音频轨。
- `-sn`：删除字幕。
- `-dn`：删除数据流。
- `-ss`：指定要剪切片段的起始位置，可以使用"hh:mm:ss"或秒数格式。`-ss`在`-i`之前，通过关键帧可以快速定位，但起始位置可能会有偏差，适合剪切长视频。
- `-t`：指定片段时长。
- `-to`：指定片段结束位置。
- `-f concat`：在`-i`之前，指定输入文件是视频列表，用于合并视频。此时，输入文件的内容为“file 'name.extension' （另起一行）file 'name.extension'”。
- `-c copy`：直接复制流而不重新编码，能在格式转换时保留原始质量并大幅提速。

## 示例

`ffmpeg -i Test.mkv test.mp4`命令是将输入文件Test从mkv格式转换为mp4格式，输出文件名为test。

`ffmpeg -i Test.mkv -c:v libx264 -c:a aac -crf 23 test.mp4`命令是将输入文件Test从mkv格式转换为mp4格式，输出文件名为test，视频编码器使用libx264，音频编码器使用AAC，图像质量为23。

`ffmpeg -i Test.mkv -ss 00:00:10 -t 20 -c copy test.mp4`命令是将输入文件Test从第10秒开始，无损剪切长度为20秒，输出格式为mp4的文件test。

`ffmpeg -f concat -i video.txt -c copy test.mp4`命令是按输入文本内的顺序无损合并视频，输出格式为mp4的文件test。

`ffmpeg Test.mkv -vn -c:a aac test.mp3`命令是将输入的音频提取为mp3格式的文件test。
