using System;

namespace Liam.ScriptHeader
{
    public static class ScriptHeaderBuilder
    {
        public static string GenerateHeader(string scriptName)
        {
            var config = ScriptHeaderConfig.Instance;
            if (config == null) return string.Empty;

            string header = "// ======================================================\n";
            
            foreach (var field in config.headerFields)
            {
                if (!field.enable) continue;
                
                string value = field.GetParsedValue(scriptName);
                header += $"// - {field.key}:".PadRight(15) + $"{value}\n";
            }
            
            header += "// ======================================================\n\n";
            return header;
        }
    }
}