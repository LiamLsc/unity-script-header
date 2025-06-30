using UnityEditor;
using System.IO;

namespace Liam.ScriptHeader
{
    public class AssetCreationProcessor : UnityEditor.AssetModificationProcessor
    {
        public static void OnWillCreateAsset(string assetPath)
        {
            assetPath = assetPath.Replace(".meta", "");
            
            if (!assetPath.EndsWith(".cs")) return;

            var config = ScriptHeaderConfig.Instance;
            if (config == null || !config.enableFeature) return; 

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), assetPath);
            string scriptName = Path.GetFileNameWithoutExtension(assetPath);

            EditorApplication.delayCall += () =>
            {
                if (!File.Exists(fullPath)) return;

                string fileText = File.ReadAllText(fullPath);
                if (fileText.Contains("// ===")) return; 

                string[] lines = File.ReadAllLines(fullPath);
                string header = ScriptHeaderBuilder.GenerateHeader(scriptName);
                File.WriteAllText(fullPath, header + string.Join("\n", lines));

                AssetDatabase.Refresh();
            };
        }
    }
}