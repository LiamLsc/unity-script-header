using UnityEngine;

namespace Liam.ScriptHeader
{
    public class ScriptHeaderConfig : ScriptableObject
    {
        public string author = "YourName";
        public bool includeAuthor = true;
        public string company = "YourCompany";
        public bool includeCompany = false;
        public string email = "your@email.com";
        public bool includeEmail = false;
        public string location = "ShangHai, China";
        public bool includeLocation = false;
        public string defaultDescription = "Please fill in the script function description";
        public bool includeDescription = true;
        public string copyright = "© 2025 YourCompany. All Rights Reserved.";
        public bool includeCopyright = false;

        public static ScriptHeaderConfig Instance
        {
            get
            {
                var config = Resources.Load<ScriptHeaderConfig>("ScriptHeaderConfig");
                if (config == null)
                {
                    Debug.LogWarning("ScriptHeaderConfig 未找到，请点击菜单 Tools > Script Header > 初始化配置文件");
                }
                return config;
            }
        }
    }
}