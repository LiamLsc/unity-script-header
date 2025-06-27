# 📝 Unity Script Header 注释生成器

一个轻量、可配置的 Unity 编辑器工具，在创建 `.cs` 脚本时自动插入统一格式的注释头信息。

支持作者、邮箱、公司、地点、版权、描述等字段，所有信息都可通过 Project Settings 界面配置并动态开关。

![project setting.png](https://img.picui.cn/free/2025/06/27/685e087e73937.png)

![script.png](https://img.picui.cn/free/2025/06/27/685e087e1340b.png)


## 📦 安装方式（推荐 Git URL）

### Git URL安装

你可以通过 Unity Package Manager 直接从 GitHub 安装本工具：
1. 打开 Unity 项目
2. 进入 `Window > Package Manager`
3. 点击左上角 “+” → `Add package from Git URL`
4. 输入以下地址：`https://github.com/LiamLsc/unity-script-header.git`

### 本地安装
1. GitHub > Releases > 下载到本地并解压
2. 进入 `Window > Package Manager`
3. 点击左上角 “+” → `Add package from disk`
4. 选择本地文件夹下的`package.json`文件

## 🎮 使用方法

🧱 配置注释字段
点击菜单：`Edit → Project Settings → Script Header`
```
设置字段和开关：
- 脚本名（自动）          - 日期（自动）
- 作者（Author）          - 邮箱（Email）
- 公司（Company）         - 地点（Location）
- 描述（Description）     - 版权信息（Copyright）
- [✓] 每项启用开关
```

🧪 测试：创建脚本
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



## ⚙️ 功能特性

✅ 新建 C# 脚本时自动添加注释头
✅ 注释字段完全可配置（作者、邮箱、公司、版权等）
✅ 每个字段支持独立开关
✅ 支持通过 Project Settings 进行实时设置
✅ 防止重复插入注释
✅ 使用命名空间封装，避免命名冲突
✅ 完全符合 Unity UPM 包标准




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

## ✨ 未来计划（TODO）

 - 支持其他脚本类型（如 Shader、EditorWindow）

 - 提供模板字段变量（如自动获取命名空间）

 - 自定义模板格式配置
