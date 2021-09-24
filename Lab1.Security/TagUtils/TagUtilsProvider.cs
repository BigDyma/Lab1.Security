using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1.Security
{
    public class TagUtilsProvider : TagUtilsBase
    {
        public TagUtilsProvider(string line):base(line)
        {

        }

        private string GetNameForTagWithAttributes => Line[IndexOfOpeningTag..IndexOfEndName];

        private string GetNameForTagAsAttribute => Line[IndexOfOpeningTag..IndexOfMiddleTag];

        private string GetNameForTagNoAttributes => Line[IndexOfOpeningTag..IndexOfClosingTagNoAttributes];

        public string GetTagName
        {
            get
            {
                if (IndexOfEndName >= 0 && IndexOfMiddleTag > 0)
                    return GetNameForTagWithAttributes;
                else if (IndexOfMiddleTag > 0)
                    return GetNameForTagAsAttribute;
                else
                    return GetNameForTagNoAttributes;
            }
        }



        public string GetNameOfAttribute => Line[IndexOfStartingAttributeName..IndexOfEndAttributeName];

        public string GetClosingTagName => Line[IndexOfOpeningClosingTag..IndexOfClosingTagNoAttributes];
        public string GetTagValue => Line[IndexOfStartingAttributesValue..IndexOfClosingTagWithAttributes];

        public bool IsValidTag() => GetTagName != string.Empty;
        public static bool IsValidTag(string line) => RegexHelpers.ValidateRegex(line, RegexHelpers.TagRegex);

        public static bool IsValidClosingTag(string line) => RegexHelpers.ValidateRegex(line, RegexHelpers.CloseTagRegex);

        public static bool isClosingTag(string line) => (line.IndexOf('/') > -1) && IsValidClosingTag(line);



    }
}
