# 📝 Unity Script Header

一个轻量、可配置的 Unity 编辑器工具，在创建 `.cs` 脚本时自动插入统一格式的注释头信息。

支持作者、邮箱、公司、地点、版权、描述等字段，所有信息都可通过 Project Settings 界面配置并动态开关。

支持团队协作，各自配置，互不影响，SCM默认忽略不上传。


![QQ20250711-112044.png](https://cdn.picui.cn/vip/2025/07/11/68708331e797d.png)

![QQ20250711-112112.png](https://cdn.picui.cn/vip/2025/07/11/68708331db539.png)


## 📦 安装方式（推荐 Git URL）

### Git URL安装

你可以通过 Unity Package Manager 直接从 GitHub 安装本工具：

1. 打开 Unity 项目
2. 进入 `Window > Package Manager`
3. 点击左上角 “+” → `Add package from Git URL`
4. 输入以下地址：

    - 国际版
    `https://github.com/LiamLsc/unity-script-header.git`
    - 国内版
    `https://gitee.com/Liam_Lee/unity-script-header.git`

### 本地安装

1. GitHub > Releases > 下载到本地并解压
2. 进入 `Window > Package Manager`
3. 点击左上角 “+” → `Add package from disk`
4. 选择本地文件夹下的`package.json`文件

## 🎮 使用方法

### 🧱 配置标准通用字段
- 点击菜单：`Edit → Project Settings → Script Header`
- 点击 `+` 按钮添加自定义模板
- 如：描述，玩家控制脚本

#### 支持自定义关键字模板，默认支持以下关键字，可手动扩展
| 字段 | 含义说明 | 示例输出 |
| --- | --- | --- |
| #FILENAME# | 脚本文件名称（含扩展名） | PlayerController.cs |
| #FILENAME_NOEXT# | 脚本文件名称（不含扩展名） | PlayerController |
| #AUTHOR# | 作者（来自配置项或系统用户名） | Liam |
| #DATE# | 创建日期 | 2025-06-28 |
| #TIME# | 创建时间 | 14:30:01 |
| #DATETIME# | 创建时间戳（日期+时间） | 2025-06-28 14:30:01 |
| #YEAR# | 当前年份 | 2025 |
| #PROJECTNAME# | 当前 Unity 项目名称（ProjectSettings.asset 读取） | MyAwesomeGame |
| #UNITY_VERSION# | 当前 Unity 编辑器版本 | 2022.3.12f1 |
| #USERDOMAIN# | 当前用户域（Windows） | DESKTOP-XXXX |
| #UNITY_PLATFORM# | 当前构建目标平台（EditorUserBuildSettings） | Android / StandaloneWindows64 |
| #SCRIPT_PATH# | 相对路径（Assets 下，需要选中对象） | Assets/Scripts/Player/PlayerController.cs |
| #GUID# | 脚本文件的 GUID（meta 中提取，需要选中对象） | bcf3a6b0a7334c74a9f1d1234567890a |
| #APP_VERSION# | 获取Unity内应用的版本号 | 1.0.0 |

#### 支持多个关键字模板组合，默认文本类型

```
#FILENAME_NOEXT# - #AUTHOR# - #DATE#
```

会被解析为：

```
PlayerController - Liam - 2025-06-28
```

#### 🧪 测试：创建脚本
通过右键菜单 Create > C# Script 创建脚本文件，即可自动插入如下格式：

```C#
// ======================================================
// 文件名: PlayerController.cs
// 创建时间: 2025-06-26 16:52:12
// 创建者: Liam
// 邮箱: liam@example.com
// 公司: YourCompany
// 地点: Shanghai, China
// 版权: © 2025 YourCompany
// 描述: 控制玩家行为逻辑
// ======================================================
```

### ✅ 自定义扩展关键字模板方法

``
遵循开闭原则(OCP)
``

1. 创建新模板处理器类继承BaseTemplateHandler
2. 在TemplateParser的静态构造函数中注册

```csharp
public class UnityVersionTemplateHandler : BaseTemplateHandler
{
    public override string Tag => "#UNITYVERSION#";
    public override string Process(string scriptName) => Application.unityVersion;
}

// 在TemplateParser的静态构造函数中添加：
RegisterHandler(new UnityVersionTemplateHandler());
```

## 🎈 支持版本

- Unity 2022 ~ 6000 
- 其他版本尚未测试

## ⚙️ 功能特性

✅ 新建 C# 脚本时自动添加注释头

✅ 注释字段完全可配置（作者、邮箱、公司、版权等）

✅ 每个字段支持独立开关

✅ 支持通过 Project Settings 进行实时设置

✅ 防止重复插入注释

✅ 使用命名空间封装，避免命名冲突

✅ 完全符合 Unity UPM 包标准

✅ 提供模板字段变量

✅ 自定义模板格式配置

✅ 直观的配置界面

## 📁 项目结构

```
Root/                      
├── package.json                   
├── README.md                       
├── CHANGELOG.md                    
├── LICENSE                         
├── Editor/
│   ├── AssetCreationProcessor.cs
│   ├── ScriptHeaderBuilder.cs
│   ├── ScriptHeaderConfig.cs
│   ├── ScriptHeaderSettingsProvider.cs
│   └── ScriptHeaderInitializer.cs
├── Resources/
│   └── ScriptHeaderConfig.asset    
```

## 🧾 许可证 License

本项目使用 MIT License，可自由用于商业和个人项目。

© 2025 Liam. 保留署名权。

## 🙋 联系方式与反馈

如有建议、问题或 bug，请提交 Issue 或创建 Pull Request。

## 🤓 常见问题

1. 为什么没有xxx字段？

    - 请参考自定义扩展关键字模板方法


2. 自定义扩展模板时，Tag名称为什么不能使用#开头？
   
    - 为了避免与默认关键字冲突，Tag名称不能以#开头.
    - 如：#UNITYVERSION# 会被解析为 UNITYVERSION，而不是 #UNITYVERSION#
    - 如：##UNITYVERSION# 会被解析为 #UNITYVERSION#，而不是 UNITYVERSION

3. 配置文件被误删了怎么办？

    - 可以在`Assets > Create > Script Header > 创建配置文件`
    默认创建在`Resources`文件夹内
    
    - 如果项目需要多人协作，请手动将配置文件移动至`Packages > Script Header > Resources` 文件夹下即可

4. 配置文件不生效怎么办？
    - 检查配置文件是否为可用状态，如果不可用可以删除配置文件并按照`步骤3`重新创建即可

## ✨ 未来计划（TODO）

- 支持其他脚本类型（如 Shader、EditorWindow）

- 支持读取不同的配置文件（多个配置文件）

