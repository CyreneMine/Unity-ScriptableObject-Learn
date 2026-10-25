# SingleScriptableObject v1.0.0

一个基于课程练习整理的 Unity ScriptableObject 泛型访问基类。只依赖 `UnityEngine`，保留原有类名与实现。

## 下载与导入

下载地址：<https://github.com/CyreneMine/Unity-ScriptableObject-Learn/releases/tag/v1.0.0>。

- 推荐下载 `SingleScriptableObject-1.0.0.unitypackage`，通过 `Assets > Import Package > Custom Package` 导入。
- 也可下载同名 ZIP，将其中 `Assets/SingleScriptableObject` 文件夹复制到自己项目的 `Assets` 下。
- 两种文件包含相同基类，选择一种导入；项目已有同名基类时不要再添加第二份。
- 导入内容只有基类、元数据及本说明，不包含课程场景、设置存档、示例角色或第三方资源。

导入后脚本位置：`Assets/SingleScriptableObject/SingleScriptableObject.cs`。

## 最小使用示例

创建 `EnemyConfig.cs`，文件名与类名一致：

```csharp
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Config/Enemy")]
public class EnemyConfig : SingleScriptableObject<EnemyConfig>
{
    public int attack = 10;
    public int defense = 5;
}
```

1. 新建 `Assets/Resources/ScriptableObject` 文件夹。
2. 通过 `Create > Config > Enemy` 创建资源，命名为 `EnemyConfig.asset`，放入上述文件夹。
3. 在 Inspector 中配置字段，通过 `EnemyConfig.Instance.attack` 读取。

```csharp
Debug.Log(EnemyConfig.Instance.attack);
```

每个子类使用自身作为泛型参数，例如 `EnemyConfig : SingleScriptableObject<EnemyConfig>`。

## 访问规则

首次访问 `T.Instance`：

1. 先用 `Resources.Load<T>("ScriptableObject/" + typeof(T).Name)` 加载资源。
2. 若未找到，使用 `CreateInstance<T>()` 创建运行时实例。
3. 缓存结果，后续访问复用同一对象。不同泛型类型各自拥有缓存。

资源文件名必须与类型名相同，不带命名空间；路径必须包含 `Resources` 之后的 `ScriptableObject/` 子目录。

加载失败会得到新实例的初始值，不会抛出“找不到资源”的专门提示。如果配置值意外为默认值，先检查路径与文件名。

## 使用边界

- 这是共享数据入口，不会使每个角色获得独立状态；多个使用者访问同一种类型时共享该实例。
- 找不到资源时创建的对象不会自动生成 `.asset`，也不自动保存玩家修改。
- JSON 存档、Addressables、线程同步、运行时副本管理均不在此基类范围内。
- 需要在 Unity 主线程访问。编辑器关闭 Domain Reload 时，静态缓存可能跨 Play Mode 保留，本版本没有专门的重置机制。
- 全局类名与课程原版一致，没有命名空间。项目中只保留一份此基类。
- 本发布未改变课程源码逻辑，仅整理打包、使用说明与导入验证。

## 验证与复现

发布附件的 Unity 导入及行为检查结果见同一 Release 的 `verification-result.json`。

仓库内重建附件：

```text
python Tools/Release/build_release.py
```

源码来自仓库 `Assets/Scripts/Lesson08/SingleScriptableObject.cs`，发布包保留其 `.meta` GUID；ZIP 和 unitypackage 内的脚本字节与源文件相同。`SHA256SUMS.txt` 用于核对附件完整性。
