# 特殊文件夹

Unity保留了一些特定文件夹名称用于特殊用途。

- Assets：存放Unity项目使用的资产。Assets文件夹中的资产会在Project窗口中显示。
- Editor：存放编辑器脚本，**不参与构建**。可以有多个，位置不同会影响编译顺序，此文件夹下的Mono脚本无法挂载到游戏对象上。
- Editor Default Resources：使用`EditorGUITUtility.Load()`时从此文件夹中加载资源，**不参与构建**。只能有一个且必须在Assets根目录下。
- Gizmos：使用`Gizmos`相关API时从此文件夹中加载资源。只能有一个且必须在Assets根目录下。
- Resources：可以使用`Resources.Load()`在运行时通过路径和名称从此文件夹中加载资源，资源**永远被构建到包中存放资源的archive中，只读，不能动态修改**。可以有多个，在Editor下的可以通过编辑器脚本加载，但在构建时会被移除。
- Standard Assets：导入标准资产包（Standard Asset Package）时会将相关资产放在此文件夹下。只能有一个且必须在Assets根目录下，最先编译，通常存放内置资源，会被导出到Assembly-CSharp-firstpass中。
- StreamingAssets：存放的文件会被拷贝到包中（移动和网页版会被嵌入到包中），不会修改和压缩（**只读**，占空间），其中的**脚本不编译**，只能有一个且必须在Assets根目录下。可以通过`Application.streamingAssetsPath`访问，具体路径因平台而异：大多数平台（Unity编辑器、Windows、Linux为`Application.dataPath/StreamingAssets`，macOS为`Application.dataPath/Resources/Data/StreamingAssets`，iOS为`Application.dataPath/Raw`，Androidde使用压缩的APK/JAR文件中的文件`jar:file://Application.dataPath!/assets`。
- Plugins：存放插件，参与构建，只能有一个且必须在Assets根目录下。
- Android Asset Packs：Unity将任何以.androidpack结尾的文件夹看作Android资产包。
- Android Library Projects：Unity将任何以.androidlib结尾的文件夹看作AAndroid库项目。
- 隐藏文件夹：Unity会忽视隐藏文件夹、以`.`开头、以`_`结尾、以`cvs`为名或以`.tmp`为扩展名的文件或文件夹。在这些文件夹中的资源不会被导入，脚本不会被编译。也不会出现在Project窗口中。

# 导入

创建Unity项目时，Unity会在项目下创建以下子文件夹：

- Temp
- Library
- Assets：
- ProjectSettings
- Logs
- Packages

可以将外部资产通过导入或复制添加到Unity项目中。Unity支持的资产类型都有对应的导入设置，这些设置会影响资产的显示或行为。在开发跨平台项目时可以覆盖“默认”设置，并根据每个平台分配不同的导入设置。

在Unity中修改文件，Unity不会修改源文件。如果修改资产的导入设置或更改Assets文件夹中的源文件，Unity会自动检测并更新资产。在Unity中移动或重命名资产更加安全，Unity会自动移动或重命名相应的.meta文件。

Unity读取并处理添加到Assets文件夹中的任何文件，并将文件内容转换为内部游戏数据。资产文件本身保持不变，内部数据存储在Library文件夹中。这些数据是资产数据库（Asset Database）的一部分。不需要手动更改Library文件夹，这意味着不应将Library文件夹包含在版本控制之下。如果项目没有在Unity中打开，可以安全地删除Library文件夹，因为Unity可以在下次启动项目时从Assets和ProjectSettings文件夹中重新生成所有数据。

在某些情况下，Unity可能会在导入单个资产文件时创建多个资产，如切割后的2D精灵图。

默认情况下，Unity在编辑器主进程中依次导入资产。然而，Unity也支持某些类型资产的并行导入（使用多个进程同时导入资产），这比默认的顺序导入更快。若要启用并行导入，可在Edit > Project Settings > Editor下的Asset Pipeline部分下启用Parallel Import。

Unity的并行导入功能仅支持特定类型的资产（图片和模型）。使用`AssetDatabase.Import()`、`AssetDatabase.Refresh()`或`AssetDatabase.CreateAsset()`以导入、刷新或创建纹理或模型时也会遵守此设置。在并行导入脚本时可能会导致意外问题，比如在单独进程导入期间修改代码中的静态变量，则主线程中可能不会检测到该修改。因此，为导入而编写的代码始终都应该是自包含的、确定性的，并且不应该更改其运行的上下文，如不应该更改编辑器设置或在磁盘上创建新资产。

在Edit > Project Settings > Editor下的Asset Pipeline下有三个用于特定项目的控制导入流程的设置：

- Desired Import Worker Count：并行导入需要的最佳进程数量。可以在Preferences > Asset Pipeline中设置Import Worker Count %以设置此属性的默认值（系统逻辑核心数的百分比）。
- Standby Import Worker Count：要保留的最小进程数量，即使其处于空闲状态。Unity会关闭闲置的多余进程以释放系统资源。
- Idle Import Worker Shutdown Delay：关闭闲置进程之前等待的时间（秒）。

# 支持资产类型

Unity通过内置的导入程序支持许多不同类型的资产，它们大多为基本资产类型，包括：

- 3D模型：3D模型文件包含多种类型的资产，如网格（Mesh）、动画（Animation）、材质（Material）、纹理（Texture）等。对于模型资产，Unity支持FBX，SketchUp、SpeedTree等格式。
- 图形：Unity将图像文件导入为纹理。Unity支持最常见的图像文件类型，如BMP、TIF、TGA、JPG和PSD等。如果将分层的PSD文件保存在Assets文件夹中，Unity会将它们作为合并导入。
- 音频：Unity支持多种音频文件格式。通常最好导入未压缩的音频文件格式，如WAV或AIFF，因为在导入过程中Unity会应用导入设置中指定的压缩设置。
- 文本：Unity可以从文本文件导入数据从而允许存储和使用外部数据，如TXT、HTML、XML、JSON、CSV、BYTES等。文本资产与生成中的所有其他资产一样进行序列化。发布游戏时不包含物理文本文件。文本资产不用于在运行时生成文本文件。
- 插件和代码：可以将插件资产（如DLL文件）放入Unity项目中以扩展游戏或应用程序的功能。Unity还支持汇编定义，以帮助创建和组织脚本程序集。
- 原生资产：Unity编辑器原生的资源类型，如动画、曲线（Curve）、渐变（Gtadient）、遮罩（Mask）、材质、预设（Presrt），不包括场景（Scene）、预制体（Prefab）和程序集（Assembly）。

# 自定义导入程序

Unity支持自定义资产导入程序，允许导入Unity本身不支持的文件格式。可以通过抽象类`ScriptedImporter`并应用`ScriptedImporter`属性创建自定义导入程序，这会注册此程序以处理一个或多个文件扩展名。当Asset Pipeline检测到与已注册文件扩展名匹配的文件更新时，会调用自定义导入程序的`OnImportAsset()`。**自定义导入程序无法处理已由Unity本机处理的文件扩展名。**可以通过`ScriptedImporterEditor`类并使用`CustomEditor`属性对其进行装饰来告知其用于何种类型的导入程序，从而实现自定义导入设置编辑器。

资产导入程序（包括您编写的任何脚本导入程序）应生成一致的（确定性）结果，这意味着它们应始终从相同的输入和依赖项生成相同的输出。为了验证导入程序是否属于这种情况，Asset Database有两种方法检查项目中资产导入结果的一致性：

- 在编辑器中手动重新导入一或多个资产：手动导入资源会导致Unity检查新的导入结果是否与之前的缓存导入结果匹配。要开始手动重新导入，右键单击资产并从上下文菜单中选择重新导入Reimport。然后，Unity会检查导入结果是否一致。重新导入根资源时，Unity还会重新导入并对其子资源执行一致性检查。
- 使用命令行参数打开编辑器：`-consistencyCheck`将告诉编辑器在启动时对项目中的所有资产和导入程序执行一致性检查，默认执行本地（Local）检查。`-consistencyCheckSourceMode value`设置执行一致性检查时要检查的源，值可为本地`local`或缓存服务器`cacheserver`。`local`表示强制在本地重新导入所有资产，并执行一致性检查，`cacheserver`要求缓存服务器提供资产的元数据并比较本地结果是否与缓存服务器上的结果匹配，而不重新导入所有资产。

一致性检查仅检查可缓存的资产导入。因此，如果禁用导入程序的缓存，则该导入的一致性检查也将禁用。如果检测到不一致的结果，Unity会在控制台窗口提供有关问题的详细信息，如哪个资产导致一致性检查失败、该资产的GUID、资产导入结果的内容的哈希值、与之前的导入结果的差异。如果缓存服务器不可用，一致性检查器将在编辑器日志中打印出警告。如果尚未为项目设置缓存服务器，则会打印`ConsistencyChecker - Cacheserver is not enabled`。如果Unity无法连接到缓存服务器，则会打印`ConsistencyChecker - Not connected to accelerator/cacheserver`。

可以使用位于Unity编辑器安装中Data/Tools中的binary2text工具来检查Library文件夹的内容，以准确查看导入程序生成的内容。

# 资产元数据

在Unity导入资产时，会存储和管理有关资产的其他数据，如应使用哪些导入设置来导入资产、使用此资产的资产等。以下为Unity导入资产的工作流程：

1. 为该资产分配唯一标识。
2. 为该资产创建元数据文件。
3. 处理该资产。

Unity通过此GUID检查Assets文件夹中的内容，且用于标识资产的引用关系以保证Unity移动或重命名资源时不会造成引用混乱。

Unity为每个资产文件和文件夹创建元数据文件。当Unity为资产创建元数据文件时会将资产GUID写入该文件，并将元数据文件与资产文件存储在相同位置。元数据文件还包含在Inspector窗口显示的该资产的导入设置等。如果更改导入设置，Unity会将这些新设置保存到资产随附的元数据文件中并重新导入资产、更新Library文件夹中的数据。如果在Unity中移动或重命名资产，Unity还会自动移动或重命名相应的元数据文件。

对于空文件夹，如果Unity检测到一个空文件夹没有对应的元数据文件但该文件夹之前具有元数据文件时，Unity会认为该元数据文件通过版本控制系统（Verson Control System，VCS）被删除，因此会删除此空文件夹。如果Unity检测到一个新的文件夹的元数据文件而该文件夹不存在时，Unity会认为该元数据文件是通过VCS创建的，因此会在本地创建相应的空文件夹。

默认Project窗口不显示元数据文件，可在Editor > Project Settings > Version Control下设置元数据文件可见性。

# 资产数据库

对于大多数类型的资源，Unit将资产源文件中的数据转换为可在游戏或实时应用程序中使用的格式，这些转换后的文件以及与之关联的数据存储在资产数据库（Asset Database）中。转换过程是必需的，因为大多数文件格式都经过优化以节省存储空间，而在游戏或实时应用程序中，资产数据需要采用可供硬件（如CPU、图形或音频硬件）立即使用的格式。通过`AssetDatabase`API，可以访问资产并控制或自定义导入过程。

Asset Database跟踪每个资产的所有依赖项（如源文件、导入设置、目标平台等），并缓存所有资产数据。如果修改了已导入的资产源文件或更改了其依赖项，缓存的数据将变为无效，Unity必须重新导入并更新数据。

Unity始终可以从资产源文件及其依赖项重新创建这些数据，因此这些数据可视为预先计算数据的缓存，从而在使用时节省时间。Unity默认使用本地缓存，即导入的资产数据缓存在本地计算机上项目文件夹下的Library文件夹中。在使用VCS时应忽略Library文件夹。

Unity在Library文件夹中维护两个数据库文件，它们一起被称为Asset Database。这两个数据库跟踪有关资产源文件（Source Asset）和导入处理后的数据（Artifact）。

Source Asset Database包含有关资产源文件的元数据（如上次修改日期、文件内容的哈希值、GUID等），Unity使用这些元数据来确定资产源文件是否被修改，从而确定是否应重新导入资产文件。

Artifact Database包含有关资产源文件的导入结果（如依赖项、Artifact元数据和Artifact文件列表等）。每个Artifact文件名都是GUID，没有扩展名。Unity将这些文件存储在以GUID前两个字符为名的子文件夹下。可以在Import Activity窗口查看各资产生成的Artifact文件和详细信息（导入时间、大小等）。Import Activity窗口可以通过两种方式打开：Window > Analysis > Import Activity或者选中资产后在Project或Inspector窗口右键选择Import Activity Window。

Unity通常会在将资产拖入项目时自动导入，但也可以使用`AssetDatabase.ImportAsset()`在脚本控制下导入。

Unity仅在需要时加载资产，可以使用`AssetDatabase.LoadAssetAtPath()`、`AssetDatabase.LoadMainAssetAtPath()`、`AssetDatabase.LoadAllAssetRepresentationsAtPath()`和`AssetDatabase.LoadAllAssetsAtPath()`从脚本加载和访问资产。

由于Unity会保留有关资产文件的元数据，因此切勿使用Windows文件系统创建、移动或删除它们。相反，可以使用`AssetDatabase.Contains()`、`AssetDatabase.CreateAsset()`、`AssetDatabase.CreateFolder()`、`AssetDatabase.RenameAsset()`、`AssetDatabase.CopyAsset()`、`AssetDatabase.MoveAsset()`、`AssetDatabase.MoveAssetToTrash()`和`AssetDatabase.DeleteAsset()`。

切换平台可能会导致Unity重新导入资产。使用Asset Database V2时，平台是Asset Database中缓存数据的一部分。这意味着在不同平台上，资产数据将存储为单独的缓存数据。因此，首次使用项目中尚未使用的平台时，将重新导入这些资产，但重新导入的新数据不会覆盖旧平台的缓存数据，即当随后切换回旧平台时，这些资产的数据都已缓存并可供使用从而使切换速度更快。

## 更新资产数据库

Unity会在以下情况下更新Asset Database：

- 编辑器重新获得焦点（如果在Preferences中启用了Auto-Refresh）。
- 通过Assets > Refresh菜单选择更新。
- 在脚本中通过`AssetDatabase.Refresh()`更新。
- 其他一些Asset Database API会触发`AssetDatabase.Refresh()`如`AssetDatabase.CreateAsset()`和`AssetDatabase.ImportAsset()`。

Unity在Asset Database更新期间执行以下步骤，某些步骤可能导致更新过程重新启动（如在导入FBX模型时从模型中提取纹理）：

1. 查找对资产文件的更改：扫描Assets和Packages文件夹以检查自上次扫描以来是否添加、修改或删除了任何文件，并将更改收集到一个列表中以便下一步处理。
2. 更新Asset Database：收集文件列表后，会获取已添加或修改文件的文件哈希值，通过这些文件的GUID更新Asset Database，并删除其检测到的已删除文件。
3. 依赖跟踪：Asset Database跟踪两种类型的资产依赖关系：静态依赖和动态依赖。如果资产的任何依赖发生更改，Unity会触发该资产的重新导入。
4. 导入和编译与代码相关的文件（如DLL、ASMDEF、ASMREF、RSP和CS等）：在已更改或添加的文件列表中，Unity收集与代码相关的文件，并将其发送到脚本编译管道（Script Compilation Pipeline）。编译器从项目中的脚本文件和程序集文件生成程序集。
5. 如果未从脚本调用`AssetDatabase.Refresh()`将重新加载域（Domain）：如果Unity检测到任何脚本更改，它会重新加载域。这样做是因为可能已经创建新的脚本导入程序，并且它们的逻辑可能会影响刷新队列中资产的导入结果。此步骤将重新启动`AssetDatabase.Refresh()`以确保任何新的脚本化导入程序生效。
6. 对导入的代码相关文件的所有资产进行后处理。
7. 导入与代码无关的资产并对其进行后处理：先处理所有内置导入程序，然后在单独阶段处理所有自定义脚本导入程序。在导入过程中会发生一系列回调。
8. 热重载（Hot Load）资产：在热重新加载过程中，存储在不可序列化变量中的所有数据都会丢失。
9. 完成上述步骤后，Asset Database更新就完成了。Artifact Database会更新相关信息并在磁盘上生成数据。

**静态依赖**是导入程序所依赖的值、设置或属性（如资产名、与资产管理的导入程序的ID、导入程序版本、目标构建平台等）。静态依赖在导入资产之前是已知的，并且不受导入过程中导入程序行为的影响。如果资产的静态依赖发生更改，Unity会重新导入该资产。**动态依赖**是在导入过程中发现的资产之间的依赖关系。Unity将资产的动态依赖存储在Asset Import Context中。

## 自定义资产数据库工作流

`AssetDatabase`类有大量方法，允许以与Unity Editor本身完全相同的方式访问资产并对其执行操作。使用`AssetDatabase`类可以自定义资产管道（Asset Pipeline）并创建使用自己的脚本访问、加载、创建和操作资产的工具以扩展编辑器的工作方式。`AssetDatabase`不能在独立构建项目中使用。

从脚本的角度来看，Unity认为的“资产”与在Project窗口中看到的略有不同。Assets文件夹中的文件是资产的源文件，它们在概念上与Unity编辑器使用的资产对象不同。当Unity导入资产文件时，它会对其进行处理并生成导入结果：从`UnityEngine.Object`派生的可序列化C#对象。在导入过程中创建的元数据文件包含资产的导入设置与GUID，GUID允许Unity将资产源文件与Artifact Database中的数据连接起来。因此，在Unity中通过脚本访问的资产就是这些导入的结果。

Unity自己创建的某些类型的资源文件，如预制体、场景、脚本化资产和材质等，其源文件中已经包含序列化数据，因此Unity生成和缓存的Artifact数据与源文件非常相似。

资产文件可以包含多个可序列化对象，为了支持`AssetDatabase`提供的API，每个对象都可以被视为“资产”。当创建仅包含单个资产（如材质）的资产文件时，主资产始终是该资产。对于包含多个可序列化资产对象的资产，除非使用`SetMainObject()`另行指定，否则主资产始终是添加到文件中的第一个资产。主资产以为的资产对象被视为子资产（包括组件）。

脚本总是在所有其他常规资产之前导入和编译，因为编辑器需要知道项目中是否存在自定义资产后处理程序或导入程序。这样可以确保编辑器在导入其余非脚本资源时使用任何新的或更改的后处理程序或导入程序。但是，在导入过程中触发的回调会导致：

- 对于第一次导入的资产，访问资产时会返回`null`。
- 对于已经导入过的资产，访问资产时会返回旧的资产数据（如果在域重新加载前进行了修改）。

因此，使用`AssetDatabase`类编写资产导入程序、预处理程序和后处理程序时，应注意资产的导入顺序是不确定的，除非使用`ScriptedImporter.GatherDependenciesFromSourceFile()`。

## 批处理

可以使用批处理来减少在代码中对资产进行更改时所花费的时间和处理量。如果在代码中对多个资产进行更改（如复制或移动），Asset Database默认依次处理每个更改，并在转到下一行代码之前对资产执行完整的刷新过程。使用批处理，可以减少花费的时间和触发的回调数量。

通过`AssetDatabase.StartAssetEditing()`和`AssetDatabase.StopAssetEditing()`进行批处理。`AssetDatabase.StartAssetEditing()`会告诉Asset Database用户正在编辑资产以使Asset Database进入暂停状态且忽视对资产的任何更改。`AssetDatabase.StopAssetEditing()`可以启用Asset Database，然后Asset Database会处理在`AssetDatabase.StartAssetEditing()`和`AssetDatabase.StopAssetEditing()`之间所做的更改。

上述两API可以嵌套使用，但需要确保调用次数相同。这是因为二者依靠函数递增和递减计数器，而不是一个简单的布尔值来控制Asset Database（防止过早的启用Asset Database）。调用`AssetDatabase.StartAssetEditing()`会增加计数，调用`AssetDatabase.StopAssetEditing()`则会减少计数。计数器为零时，Asset Database才会被启用。当然，计数器小于零时会报错。因此，最好使用`try-finally`来确保发生任何错误时都可以调用到`AssetDatabase.StopAssetEditing()`。

# 预设

预设（Preset）允许将组件、资产或Project Setting窗口的属性配置保存为预设文件（.preset），然后可以使用此预设文件将相同的设置应用于其他同类资产。应用预设文件仅将预设的属性复制到资产中而不会将预设文件链接到该资产，因此对预设文件所做的更改不会影响已经应用预设文件的资产。可以在Inspector窗口中右击预设文件的属性以选择是否禁用该属性。可以在Inspector窗口或Edit > Project Setting > Preset Manager为指定资产设置默认预设。

可以使用脚本应用预设，但类必须继承`UnityEngine.Monobehaviour`、`UnityEngine.ScriptableObject`或`UnityEngine.ScriptableImporter`。在编辑器脚本中，使用`ObjectFactory`类创建新的游戏对象、组件或资产时会自动应用默认预设而不需要在脚本中手动设置。