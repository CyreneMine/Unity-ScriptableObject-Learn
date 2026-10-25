# Unity ScriptableObject 学习项目

用于学习 Unity ScriptableObject 的基础概念、资源创建、配置引用和运行时数据边界，持续记录练习与复盘。

## 下载复用单例基类

最后的 `SingleScriptableObject<T>` 已整理为独立发布包：

- [v1.0.0 下载页面](https://github.com/CyreneMine/Unity-ScriptableObject-Learn/releases/tag/v1.0.0)：下载 unitypackage 或源码 ZIP，任选一种导入。
- [使用说明与最小示例](Docs/SingleScriptableObject.md)：资源放在 `Assets/Resources/ScriptableObject/`，文件名与类型名一致，通过 `类型.Instance` 访问。
- [实际验证报告](Docs/Releases/verification-1.0.0.json)：全新 Unity `6000.3.10f1` 工程导入、编译与加载/兜底/缓存检查。

基类只依赖 `UnityEngine`，发布包不含课程场景、存档或第三方资源。保留原始实现；不自动保存玩家数据。

## 课程信息

- GitHub：[CyreneMine/Unity-ScriptableObject-Learn](https://github.com/CyreneMine/Unity-ScriptableObject-Learn)
- Unity：`6000.3.10f1`，以 `ProjectSettings/ProjectVersion.txt` 为准。
- 课程：Unity进阶之ScriptableObject（按本地目录名记录）。
- 本地视频目录：`E:\1aUnity教程\Unity进阶之ScriptableObject`。
- 已核对 14 个编号视频，完整目录见 [学习进度](LearningProgress.md)；线上链接待补充。
- 当前阶段：**课程 14P 全部学完**（用户于 2026-10-25 确认），进入总结与按需复习。各项练习范围和验证来源见学习进度。

## 当前工程

截至 2026-10-25，课程完成时的磁盘检查及此前复盘记录：

- 已有 `Assets/`、`Packages/` 和 `ProjectSettings/`。
- 场景入口为 `Assets/Scenes/SampleScene.unity`，已包含设置面板、两个 Toggle、两个 Slider、Canvas 与 EventSystem。
- 已有 `Assets/InputSystem_Actions.inputactions`。
- `Assets/Scripts/Lesson01/MyData.cs` 声明音乐、音效的开关与音量；创建时读取 JSON，`SaveData()` 写入相同路径。
- 已有 `Assets/Resources/SettingData.asset`，四项配置已保存，脚本 GUID 匹配。
- `Assets/Scripts/Lesson02/Lesson02.cs` 保留第 5P 实现，旧组件已从当前面板移除。
- `Assets/Scripts/Lesson03/Lesson03.cs` 创建运行时实例、初始化 UI 并监听四项回写，空格键触发保存；四个场景控件引用齐全。
- 新增 `Assets/Scripts/Lesson05/RoleInfo.cs` 与 `Assets/Resources/RoleInfo.asset`，使用角色数据列表配置 3 条角色信息，脚本 GUID 匹配；`Lesson05.cs` 仍为空模板。
- `Assets/Scripts/Lesson08/SingleScriptableObject.cs` 提供泛型静态入口，优先加载 `Resources/ScriptableObject/类型名`，失败时创建运行时实例。
- `TestData.cs` 及 `Resources/ScriptableObject/TestData.asset` 构成示例，配置攻击 `2`、防御 `3`；`Lesson08.cs` 读取字段并调用 `Wound()`。用户已修正加载路径，代码与资源匹配。
- 已只读检查本地 `MusicData.json`，四项设置确已写入文件；存档留在 `Application.persistentDataPath`，不复制进仓库。
- Build Settings 场景列表为空，未进行构建验证。
- 第 5P 的用户测试反馈及第 7P 的概念演示结论保留；第 9P 手动存档读写范围检查通过。Agent 未亲自执行编译、Play Mode、重启或构建；未修改用户代码或场景。
- 发布包另行完成了临时工程的 Unity 编译和访问验证，上述课程验证范围不等于发布包验证范围；详见报告。

## 初始学习目标（保留为学习规划）

以下为建议方向，后续按实际教程调整，不代表已完成或老师的章节安排。

- 理解 ScriptableObject 的用途及其与组件、普通 C# 对象的区别。
- 学习定义数据类型、创建资源，并通过 Inspector 配置字段。
- 在实际练习中读取配置，检查资源与消费端之间的引用。
- 理解共享资源、独立配置和运行状态的区别。
- 验证运行时修改、再次运行和必要时构建后的行为。
- 形成一个经过验证的可复用小练习，并完成学习总结。

## 文档入口

| 文件 | 用途 |
| --- | --- |
| [AGENTS.md](AGENTS.md) | 导师式协作、修改边界、复盘与 Git 规则 |
| [LearningProgress.md](LearningProgress.md) | 当前进度、验证记录和最小下一步 |
| [课程总结](Notes/14-课程总结.md) | 实际产物、关键误区与课程完成范围 |
| [Notes/README.md](Notes/README.md) | 笔记归档方式和复盘模板 |

## 打开工程

1. 使用 Unity Hub 将仓库根目录添加为项目。
2. 使用 `6000.3.10f1` 打开，等待依赖解析和资源导入。
3. 打开 `Assets/Scenes/SampleScene.unity`。
4. 根据实际教程开始第一节练习，并在复盘后更新学习进度。

上述步骤是操作指引，本次未验证重新导入或运行结果。

## 协作与版本管理

- 以解释、检查和复盘为主；修改代码、场景、资源及工程设置需要明确授权。
- 每完成一节或一个阶段，更新学习记录；关键验证未完成时保留待验证状态。
- 提交与推送按需授权，默认中文提交信息。
- 提交 Unity 源资源及 `.meta`、包清单和工程设置。
- 忽略 Unity 缓存、日志、用户设置及 IDE 生成文件。
- 项目尚未声明开源许可证，第三方内容需单独确认使用与分发权限。
