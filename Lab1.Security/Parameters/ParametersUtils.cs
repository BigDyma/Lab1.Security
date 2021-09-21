using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1.Security.Parameters
{
    public static class ParametersUtils
    {
        public static bool IsValidParameterAsType(string line) => RegexHelpers.ValidateRegex(line, RegexHelpers.ParametersTypeRegex);

        public static bool IsValidParameterAsClosedString(string line) => RegexHelpers.ValidateRegex(line, RegexHelpers.ParametersInfoOneLineRegex);

        public static bool IsValidParameterAsNonClosedString(string line) => RegexHelpers.ValidateRegex(line, RegexHelpers.ParametersInfoNotClosedQuoteRegex);


        public static bool IsValidParameter(string line) => IsValidParameterAsClosedString(line) || IsValidParameterAsNonClosedString(line) || IsValidParameterAsType(line);

        public static Parameter CreateParameterFromLineForClosedStrings(string line)
        {
            var temp = line.Split(':');
            if (temp is null)
                return null;

            return new Parameter(temp[0].Trim(), temp[1].Trim());
        }

        internal static Parameter CreateParameterFromLineForNonClosedString(string line)
        {
            throw new NotImplementedException();
        }
    }
}
