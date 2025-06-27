using UnityEditor;
using UnityEngine;
using System.IO;

namespace Liam.ScriptHeader
{
    public static class ScriptHeaderInitializer
    {
        [MenuItem("Tools/Script Header/初始化配置文件")]
        public static void InitConfig()
        {
            //string folderPath = "Packages/com.liam.script-header/Resources";
            string folderPath = "Assets/Resources";
            string assetPath = $"{folderPath}/ScriptHeaderConfig.asset";

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            if (File.Exists(assetPath))
            {
                EditorUtility.DisplayDialog("提示", "配置文件已存在，无需重复创建。", "确定");
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