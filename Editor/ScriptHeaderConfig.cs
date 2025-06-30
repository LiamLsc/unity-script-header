using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System;
using System.Linq;

namespace Liam.ScriptHeader
{
    [System.Serializable]
    public struct HeaderField
    {
        public bool enable;
        public string key;
        public string value;

        public string GetParsedValue(string scriptName)
        {
            return TemplateParser.Parse(value, scriptName);
        }
    }

    public static class TemplateParser
    {
        private static readonly List<ITemplateHandler> _handlers = new List<ITemplateHandler>();

        static TemplateParser()
        {
            RegisterHandler(new FilenameTemplateHandler());
            RegisterHandler(new FilenameNoExtTemplateHandler());
            RegisterHandler(new AuthorTemplateHandler());
            RegisterHandler(new DateTemplateHandler());
            RegisterHandler(new TimeTemplateHandler());
            RegisterHandler(new DatetimeTemplateHandler());
            RegisterHandler(new YearTemplateHandler());
            RegisterHandler(new ProjectNameTemplateHandler());
            RegisterHandler(new UnityVersionTemplateHandler());
            RegisterHandler(new UserDomainTemplateHandler());
            RegisterHandler(new UnityPlatformTemplateHandler());
            RegisterHandler(new ScriptPathTemplateHandler());
            RegisterHandler(new GuidTemplateHandler());
            RegisterHandler(new AppVersionTemplateHandler());
        }

        public static void RegisterHandler(ITemplateHandler handler)
        {
            if (_handlers.All(h => h.Tag != handler.Tag))
            {
                _handlers.Add(handler);
            }
        }

        public static string Parse(string template, string scriptName)
        {
            if (string.IsNullOrEmpty(template))
                return template;

            string result = template;
            foreach (var handler in _handlers)
            {
                if (result.Contains(handler.Tag))
                {
                    result = result.Replace(handler.Tag, handler.Process(scriptName));
                }
            }
            return result;
        }

        public static string[] GetAvailableTemplates()
        {
            return _handlers.Select(h => h.Tag).ToArray();
        }
    }

    public class ScriptHeaderConfig : ScriptableObject
    {
        public bool enableFeature = true;
        public List<HeaderField> headerFields = new List<HeaderField>
        {
            new HeaderField { enable = true, key = "FileName", value = "#FILENAME#" },
            new HeaderField { enable = true, key = "Date", value = "#DATETIME#" },
            new HeaderField { enable = true, key = "Author", value = "#AUTHOR#" },
            new HeaderField { enable = true, key = "Email", value = "123456789@163.com" },
            new HeaderField { enable = true, key = "Description", value = "" }
        };

        public static ScriptHeaderConfig Instance
        {
            get
            {
                var config = AssetDatabase.LoadAssetAtPath<ScriptHeaderConfig>(
                    "Packages/com.liam.script-header/Resources/ScriptHeaderConfig.asset");

                config ??= Resources.Load<ScriptHeaderConfig>("ScriptHeaderConfig");

                if (config == null)
                    Debug.LogWarning("ScriptHeaderConfig 未找到，请点击右键Assets > Create > Script Header > 创建配置文件");

                return config;
            }
        }
    }

    public interface ITemplateHandler
    {
        string Tag { get; }
        string Process(string scriptName);
    }

    public abstract class BaseTemplateHandler : ITemplateHandler
    {
        public abstract string Tag { get; }
        public abstract string Process(string scriptName);
    }


    #region  Default Config
    public class FilenameTemplateHandler : BaseTemplateHandler
    {
        public override string Tag => "#FILENAME#";
        public override string Process(string scriptName) => scriptName + ".cs";
    }

    public class FilenameNoExtTemplateHandler : BaseTemplateHandler
    {
        public override string Tag => "#FILENAME_NOEXT#";
        public override string Process(string scriptName) => scriptName;
    }

    public class AuthorTemplateHandler : BaseTemplateHandler
    {
        public override string Tag => "#AUTHOR#";
        public override string Process(string scriptName) => Environment.UserName;

    }

    public class DateTemplateHandler : BaseTemplateHandler
    {
        public override string Tag => "#DATE#";
        public override string Process(string scriptName) => DateTime.Now.ToString("yyyy-MM-dd");
    }

    public class TimeTemplateHandler : BaseTemplateHandler
    {
        public override string Tag => "#TIME#";
        public override string Process(string scriptName) => DateTime.Now.ToString("HH:mm:ss");
    }

    public class DatetimeTemplateHandler : BaseTemplateHandler
    {
        public override string Tag => "#DATETIME#";
        public override string Process(string scriptName) => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public class YearTemplateHandler : BaseTemplateHandler
    {
        public override string Tag => "#YEAR#";
        public override string Process(string scriptName) => DateTime.Now.Year.ToString();
    }

    public class ProjectNameTemplateHandler : BaseTemplateHandler
    {
        public override string Tag => "#PROJECTNAME#";
        public override string Process(string scriptName) => Application.productName;
    }

    public class UnityVersionTemplateHandler : BaseTemplateHandler
    {
        public override string Tag => "#UNITY_VERSION#";
        public override string Process(string scriptName) => Application.unityVersion;
    }

    public class UserDomainTemplateHandler : BaseTemplateHandler
    {
        public override string Tag => "#USERDOMAIN#";
        public override string Process(string scriptName) => Environment.UserDomainName;
    }

    public class UnityPlatformTemplateHandler : BaseTemplateHandler
    {
        public override string Tag => "#UNITY_PLATFORM#";
        public override string Process(string scriptName) =>
            EditorUserBuildSettings.activeBuildTarget.ToString();
    }

    public class ScriptPathTemplateHandler : BaseTemplateHandler
    {
        public override string Tag => "#SCRIPT_PATH#";
        public override string Process(string scriptName) =>
            AssetDatabase.GetAssetPath(Selection.activeObject);
    }

    public class GuidTemplateHandler : BaseTemplateHandler
    {
        public override string Tag => "#GUID#";
        public override string Process(string scriptName) =>
            AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(Selection.activeObject));
    }

    public class AppVersionTemplateHandler : BaseTemplateHandler
    {
        public override string Tag => "#APP_VERSION#";
        public override string Process(string scriptName) =>
            PlayerSettings.bundleVersion;
    }
    #endregion

}