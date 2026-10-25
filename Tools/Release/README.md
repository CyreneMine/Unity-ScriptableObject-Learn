# 发布包重建与 Unity 导入验证

1. 在仓库根目录运行 `python Tools/Release/build_release.py`。
2. 附件生成于被 Git 忽略的 `ReleaseArtifacts/1.0.0/`，脚本检查归档条目、ZIP 完整性及源码字节一致性。
3. 创建独立临时 Unity 工程，使用 Unity `6000.3.10f1`，先通过 `-importPackage` 导入生成的 unitypackage，并等待导入与编译成功退出。
4. 将 `VerifyImport.cs` 和 `VerificationProbes/` 下的两个探针脚本复制到该临时工程的 `Assets/Editor/`。
5. 在原工程中计算源脚本 SHA256，以参数 `-expectedSourceHash` 传入，然后启动临时工程：

```text
Unity.exe -batchmode -nographics -quit -projectPath <临时工程绝对路径> -executeMethod VerifyImport.Run -expectedSourceHash <源脚本SHA256> -logFile <日志绝对路径>
```

6. 核对退出码为 0、日志无编译错误，且临时工程根目录的 `verification-result.json` 中 `passed` 为 `true`。
7. 将报告复制到 `ReleaseArtifacts/1.0.0/verification-result.json` 后，再运行打包脚本生成包含报告的校验和列表。

验证代码仅用于临时工程，不导入课程工程或发布附件。不要把正在打开的工程同时交给批处理 Unity，也不要把临时工程的 `Library/` 或日志提交到仓库。

首次发布在 Windows + Unity `6000.3.10f1` 的全新临时工程中验证，结果归档在 `Docs/Releases/verification-1.0.0.json`；其他 Unity 版本与平台未实际验证。
