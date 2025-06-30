using UnityEditor;
using UnityEngine;
using System.IO;

namespace Liam.ScriptHeader
{
    public static class ScriptHeaderInitializer
    {
        [MenuItem("Assets/Create/Script Header/创建配置文件", priority = 2000)]
        public static void InitConfig()
        {
            string[] possiblePaths = {
                "Packages/com.liam.script-header/Resources",
                "Assets/Resources"
            };

            string assetPath = null;
            
            foreach (var path in possiblePaths)
            {
                string testPath = $"{path}/ScriptHeaderConfig.asset";
                if (File.Exists(testPath))
                {
                    EditorUtility.DisplayDialog("提示", "配置文件已存在，无需重复创建。", "确定");
                    return;
                }
                assetPath = testPath;
            }

            foreach (var path in possiblePaths)
            {
                if (!Directory.Exists(path))
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                        assetPath = $"{path}/ScriptHeaderConfig.asset";
                        break;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            if (string.IsNullOrEmpty(assetPath))
            {
                EditorUtility.DisplayDialog("错误", "无法创建配置文件目录", "确定");
                return;
            }

            var config = ScriptableObject.CreateInstance<ScriptHeaderConfig>();
            AssetDatabase.CreateAsset(config, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorUtility.DisplayDialog("成功", "ScriptHeaderConfig.asset 已创建，请到 Project Settings 中配置。", "好的");
        }
    }
}