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
                EditorGUILayout.HelpBox("ScriptHeaderConfig 未找到。请使用菜单 Tools > Script Header > 初始化配置文件", MessageType.Warning);
                return;
            }

            settings.Update();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("注释配置", EditorStyles.boldLabel);

            DrawField("author", "作者", "includeAuthor");
            DrawField("company", "公司", "includeCompany");
            DrawField("email", "邮箱", "includeEmail");
            DrawField("location", "地点", "includeLocation");
            DrawField("defaultDescription", "描述", "includeDescription");
            DrawField("copyright", "版权信息", "includeCopyright");

            settings.ApplyModifiedProperties();
        }

        private void DrawField(string fieldName, string label, string toggleName)
        {
            SerializedProperty toggleProp = settings.FindProperty(toggleName);
            SerializedProperty fieldProp = settings.FindProperty(fieldName);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            toggleProp.boolValue = EditorGUILayout.ToggleLeft($"启用 {label}", toggleProp.boolValue, EditorStyles.boldLabel);
            if (toggleProp.boolValue)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(fieldProp, new GUIContent(label));
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(4);
        }

        [SettingsProvider]
        public static SettingsProvider CreateScriptHeaderSettings()
        {
            return new ScriptHeaderSettingsProvider("Project/Script Header", SettingsScope.Project);
        }
    }
}