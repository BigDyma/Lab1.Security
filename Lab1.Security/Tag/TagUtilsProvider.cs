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

        public string GetTagName => Line[IndexOfOpeningTag..IndexOfClosingTagNoAttributes];

        public string GetTagValue => Line[IndexOfStartingAttributesValue..IndexOfClosingTagWithAttributes];

        public bool IsValidTag() => GetTagName != string.Empty;
        public static bool IsValidTag(string line) => RegexHelpers.ValidateRegex(line, RegexHelpers.TagRegex);


        public static bool isClosingTag(string line) => (line.IndexOf('/') > -1);

    }
}
