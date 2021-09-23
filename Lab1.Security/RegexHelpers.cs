using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab1.Security
{
    public static class RegexHelpers
    {
        public static string TagRegex = "^\\s*<([A-Za-z\\-\\\\_\\s]+([:]?\"[A-Za-z\\\\-\\\\_\\s]+\")?)>$";

        public static string CloseTagRegex = "^\\s*</([A-Za-z\\-\\\\_\\s]+([//:]?\"[A-Za-z\\\\-\\\\_\\s]+\")?)>$";

        public static string ParametersInfoOneLineRegex = "^\\s+[A-Za-z0-9\\-\\\\_]+\\s+:\\s+(\".+\\n?)\"$";

        public static string ParametersInfoNotClosedQuoteRegex = "^\\s+[A-Za-z0-9\\-\\\\_]+\\s+:\\s+(\".+\\n?)$";

        public static string ParametersTypeRegex = "^\\s+[A-Za-z0-9\\-\\\\_]+\\s+:\\s+([A-Z\\\\_]+)$";

        public static string ComentRegex = "^\\s*?#";
        public static bool ValidateRegex(string line, string regex)
        {
            Match match = Regex.Match(line, regex, RegexOptions.IgnoreCase);

            return (line != string.Empty && match.Success);
        }
    }
}
