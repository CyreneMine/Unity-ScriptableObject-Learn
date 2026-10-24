# Unity ScriptableObject 学习项目

用于学习 Unity ScriptableObject 的基础概念、资源创建、配置引用和运行时数据边界，持续记录练习与复盘。

- GitHub：[CyreneMine/Unity-ScriptableObject-Learn](https://github.com/CyreneMine/Unity-ScriptableObject-Learn)
- Unity：`6000.3.10f1`，以 `ProjectSettings/ProjectVersion.txt` 为准。
- 课程名称、教程链接和实际章节目录：待补充。
- 当前阶段：工程与学习文档初始化，课程练习未开始。

## 当前工程

截至 2026-10-24，磁盘检查确认：

- 已有 `Assets/`、`Packages/` 和 `ProjectSettings/`。
- 场景入口为 `Assets/Scenes/SampleScene.unity`，包含初始相机与方向光。
- 已有 `Assets/InputSystem_Actions.inputactions`。
- `Assets/` 下尚无 C# 脚本或 ScriptableObject 数据资源。
- Build Settings 场景列表为空，未进行构建验证。
- 本次只建立学习文档和 Git 管理配置；未执行 Play Mode 验证。

## 学习目标

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
