# gRPC

gRPC是一种与语言无关的高性能远程过程调用（RPC）框架。在gRPC中，客户端应用程序可以直接调用服务器应用程序上的方法，就好像它是本地对象一样，从而更轻松地创建分布式应用程序和服务。与许多RPC系统一样，gRPC基于定义服务的思想，即定义可以远程调用的方法。服务器端实现此接口并运行gRPC服务器来处理客户端调用。客户端有一个存根（在某些语言中），它提供与服务器相同的方法。

gRPC的主要优点是：

- 现代高性能轻量级RPC框架。
- 协定优先API开发，默认使用Protobuf，允许与语言无关的实现。
- 可用于多种语言的工具，以生成强类型服务器和客户端。
- 支持客户端、服务器和双向流式处理调用。
- 使用Protobuf二进制序列化减少对网络的使用。

这些优点使 gRPC 适用于：

- 效率至关重要的轻量级微服务。
- 需要多种语言用于开发的Polyglot系统。
- 需要处理流式处理请求或响应的点对点实时服务。

# .Net使用GRPC

## 支持

需要为Visual Studio添加ASP.NET和Web开发工作模块，新建项目时可以选中Grpc模板。该项目默认安装Grpc.AspNetCore包（包括Google.Protobuf、Grpc.AspNetCore.Server.ClientFactory、Grpc.Tools）。

``` xml
<PackageReference Include="Grpc.AspNetCore" Version="2.32.0" />
```

客户端应该直接引用Google.Protobuf、Grpc.Tools和Grpc.Net.Client。其中，Grpc.Tools在运行时不需要，因此依赖项可以标记为`PrivateAssets = "All"`

``` xml
<PackageReference Include="Google.Protobuf" Version="3.18.0" />
<PackageReference Include="Grpc.Net.Client" Version="2.52.0" />
<PackageReference Include="Grpc.Tools" Version="2.40.0">
  <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
  <PrivateAssets>all</PrivateAssets>
</PackageReference>
```

## .proto文件生成C#文件

.proto文件需要添加到项目中才能生成C#文件。`Include`中需要.proto文件相对于项目根目录的完整路径。

``` xml
<ItemGroup>
  <Protobuf Include="Protos\greet.proto" />
</ItemGroup>
```

> 注意：.proto文件中`improt`其他文件时不允许使用相对路径，导入路径应从根目录开始，否则可能会抛出`Import ".proto" was not found or had errors.`异常而无法编译。

需要Grpc.Tools工具包才能从.proto文件生成.cs文件。生成的文件在每次生成解决方案时按需生成，默认存放在项目输出目录（即obj文件夹相应路径）中。

默认情况下，.proto文件将生成具体的客户端类和抽象的服务端基类，可以在添加.proto文件使用`GrpcServices`来限制C#文件的生成。

- `Both`：默认值。
- `Server`：仅生成服务端文件。
- `Client`：仅生成客户端文件。
- `None`：都不生成。

对于服务端，生成的服务端基类包含.proto文件中所含有的使用gRPC调用的定义。自定义派生类以实现服务端基类即可完成该服务具体表现。

同样地，对于客户端，生成的客户端类包含.proto文件中所含有的使用gRPC调用的方法，可以直接进行调用。