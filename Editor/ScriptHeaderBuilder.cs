using System;

namespace Liam.ScriptHeader
{
    public static class ScriptHeaderBuilder
    {
        public static string GenerateHeader(string scriptName)
        {
            var config = ScriptHeaderConfig.Instance;
            if (config == null) return string.Empty;

            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string header = "// ======================================================\n";
            header += $"// - FileName:      {scriptName}.cs\n";
            header += $"// - Date:          {time}\n";

            if (config.includeAuthor)
                header += $"// - Author:        {config.author}\n";
            if (config.includeCompany)
                header += $"// - Company:       {config.company}\n";
            if (config.includeEmail)
                header += $"// - Email:         {config.email}\n";
            if (config.includeLocation)
                header += $"// - Location:      {config.location}\n";
            if (config.includeDescription)
                header += $"// - Description:   {config.defaultDescription}\n";
            if (config.includeCopyright)
                header += $"// - Copyright:     {config.copyright}\n";


            header += "// ======================================================\n\n";
            return header;
        }
    }
}