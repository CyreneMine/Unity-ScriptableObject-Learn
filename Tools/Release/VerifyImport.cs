// Copy into an isolated validation project's Assets/Editor after importing the release.
using System;
using System.IO;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public static class VerifyImport
{
    [Serializable]
    private class Result
    {
        public string unityVersion;
        public string sourceSha256;
        public bool importedSourceMatches;
        public bool configuredAssetLoaded;
        public bool configuredValuesMatch;
        public bool configuredInstanceCached;
        public bool missingAssetCreatesRuntimeInstance;
        public bool fallbackInstanceCached;
        public bool genericTypesIsolated;
        public bool passed;
    }

    private static void Require(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }

    public static void Run()
    {
        try
        {
            string[] args = Environment.GetCommandLineArgs();
            string expectedHash = args[Array.IndexOf(args, "-expectedSourceHash") + 1];
            string hash;
            using (var sha = SHA256.Create())
                hash = BitConverter.ToString(sha.ComputeHash(File.ReadAllBytes(
                    "Assets/SingleScriptableObject/SingleScriptableObject.cs")))
                    .Replace("-", "").ToLowerInvariant();
            var result = new Result { unityVersion = Application.unityVersion, sourceSha256 = hash };
            result.importedSourceMatches = hash == expectedHash;
            Require(result.importedSourceMatches, "Imported script differs from release source.");

            Directory.CreateDirectory("Assets/Resources/ScriptableObject");
            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
            var configured = ScriptableObject.CreateInstance<SSOConfiguredProbe>();
            configured.attack = 37;
            configured.defense = 19;
            AssetDatabase.CreateAsset(configured, "Assets/Resources/ScriptableObject/SSOConfiguredProbe.asset");
            AssetDatabase.SaveAssets();
            var loaded = SSOConfiguredProbe.Instance;
            result.configuredAssetLoaded = ReferenceEquals(loaded, configured) && EditorUtility.IsPersistent(loaded);
            result.configuredValuesMatch = loaded.attack == 37 && loaded.defense == 19;
            result.configuredInstanceCached = ReferenceEquals(loaded, SSOConfiguredProbe.Instance);

            Require(Resources.Load<SSOFallbackProbe>("ScriptableObject/SSOFallbackProbe") == null,
                "Fallback test unexpectedly has an existing resource.");
            var fallback = SSOFallbackProbe.Instance;
            result.missingAssetCreatesRuntimeInstance = fallback != null &&
                !EditorUtility.IsPersistent(fallback) && fallback.value == 23;
            result.fallbackInstanceCached = ReferenceEquals(fallback, SSOFallbackProbe.Instance);
            result.genericTypesIsolated = !ReferenceEquals(loaded, fallback);
            result.passed = result.importedSourceMatches && result.configuredAssetLoaded &&
                result.configuredValuesMatch && result.configuredInstanceCached &&
                result.missingAssetCreatesRuntimeInstance && result.fallbackInstanceCached &&
                result.genericTypesIsolated;
            File.WriteAllText("verification-result.json", JsonUtility.ToJson(result, true));
            Require(result.passed, "SingleScriptableObject verification failed.");
            UnityEngine.Object.DestroyImmediate(fallback);
            Debug.Log("SINGLE_SCRIPTABLE_OBJECT_RELEASE_VERIFIED");
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
            EditorApplication.Exit(1);
        }
    }
}
