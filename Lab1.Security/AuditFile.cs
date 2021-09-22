using Lab1.Security.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab1.Security
{
    public class AuditFile
    {
        public string Path { get; set; } 

        public Tag GlobalTag { get; set; }

        private string[] Content { get; set; }

        private int ContentLength => Content.Length;
        public AuditFile(string _path)
        {
            Path = _path;
            Content = File.ReadAllLines(Path);

        }

        public void Parse()
        {
            GlobalTag = ReccursiveParsing();
        }

        private List<Parameter> ParseParameters( ref int n)
        {
            var initialIndex = n;
            List<Parameter> parameters = new List<Parameter>();
            for (var item = initialIndex; item < Content.Length; item++)
            {
                var line = Content[item];

                if (ParametersUtils.IsValidParameterAsClosedString(line) || ParametersUtils.IsValidParameterAsType(line) || ParametersUtils.IsValidParameterAsNonClosedString(line))
                {
                    if (ParametersUtils.IsValidParameterAsNonClosedString(line))
                    {
                        ParseOpenStringParameter(ref item, ref line);
                    }
                    parameters.Add(ParametersUtils.CreateParameterFromLineForClosedStrings(line));
                    n = item;
                }
                else break;
            }

            return parameters;
        }

        private void ParseOpenStringParameter(ref int item, ref string line)
        {
            int localIndex = item;
            for (var openLine = localIndex; item <= Content.Length; item++)
            {
                var lastChar = Content[openLine].Trim().Last();
                if (lastChar == '"')
                    break;

                line += Content[openLine];

                item = openLine;
            }
        }
        private Tag ParseNextTag(ref int n, Tag recoveryTag)
        {
            for (int i = n; i < ContentLength; i++)
            {
                var line = Content[i];
                var index = i;
                if (TagUtilsProvider.IsValidTag(line))
                {
                    TagUtilsProvider provider = new TagUtilsProvider(line);
                    n = index+1;
                    return TagFactory.CreateTagFromTagUtilsProvider(provider);
                }
                if (ParametersUtils.IsValidParameter(line))
                {
                    recoveryTag.RegisterParameter(ParseParameters(ref index));
                    n = index +1;
                    break;
                }
               
            }
            return recoveryTag;
        }

        private Tag ReccursiveParsing(int n = 0, Tag recoveryTag = null)
        {
            if (n == ContentLength)
                return null;
            Tag localTag = ParseInDepth(n, recoveryTag); 

            return localTag;
        }

        private Tag ParseInDepth(int n, Tag recoveryTag)
        {
            Tag localTag = null;
            for (int localIndex = n; localIndex < ContentLength;)
            {
                var line = Content[localIndex];
                var index = localIndex;

                if (TagUtilsProvider.IsValidClosingTag(line))
                {
                    TagUtilsProvider closingTag = new TagUtilsProvider(line);

                    recoveryTag.CloseTag(closingTag.GetClosingTagName);
                    recoveryTag.closeTagIndex = localIndex;
                    return recoveryTag;
                }

                
                localTag = ParseNextTag(ref localIndex, recoveryTag);

                line = Content[localIndex];
                index = localIndex;

                localTag.RegisterChildTag(ReccursiveParsing(index, localTag));
                localIndex = localTag.closeTagIndex + 1;
            }

            return localTag;
        }
    }
}
