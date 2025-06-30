using UnityEditor;
using UnityEngine;

namespace Liam.ScriptHeader
{
    public class ScriptHeaderSettingsProvider : SettingsProvider
    {
        private SerializedObject settings;

        public ScriptHeaderSettingsProvider(string path, SettingsScope scope = SettingsScope.Project)
            : base(path, scope) { }

        public override void OnActivate(string searchContext, UnityEngine.UIElements.VisualElement rootElement)
        {
            var config = ScriptHeaderConfig.Instance;
            if (config != null)
            {
                settings = new SerializedObject(config);
            }
        }

        public override void OnGUI(string searchContext)
        {
            if (settings == null)
            {
                EditorGUILayout.HelpBox("ScriptHeaderConfig 未找到，请点击 Assets > Create > Script Header > 创建配置文件", MessageType.Warning);
                return;
            }

            settings.Update();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("全局设置", EditorStyles.boldLabel);
            var enableProp = settings.FindProperty("enableFeature");
            EditorGUILayout.PropertyField(enableProp, new GUIContent("注释功能"));
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("注释字段配置", EditorStyles.boldLabel);
            
            SerializedProperty fieldsProp = settings.FindProperty("headerFields");
            EditorGUILayout.PropertyField(fieldsProp, true);
            
            settings.ApplyModifiedProperties();
        }

        

        [SettingsProvider]
        public static SettingsProvider CreateScriptHeaderSettings()
        {
            return new ScriptHeaderSettingsProvider("Project/Script Header", SettingsScope.Project);
        }
    }
}