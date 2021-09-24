using Lab1.Security.Parameters;
using Newtonsoft.Json;
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

        public string ResultJSON { get => JsonConvert.SerializeObject(Result);  }

        private Tag Result => ReccursiveParsing();
        private string[] Content { get; set; }

        private int ContentLength => Content.Length;
        public AuditFile(string _path)
        {
            Path = _path;
            Content = File.ReadAllLines(Path);

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
                    if (ParametersUtils.IsValidParameterAsNonClosedString(line) && line.Trim().Last() != '"')
                    {
                        ParseOpenStringParameter(ref item, ref line);
                    }
                    parameters.Add(ParametersUtils.CreateParameterFromLineForClosedStrings(line));
                    n = item;
                }
                else if (TagUtilsProvider.IsValidTag(line) || TagUtilsProvider.IsValidClosingTag(line))
                    break;
            }

            return parameters;
        }

        private void ParseOpenStringParameter(ref int item, ref string line)
        {
            int localIndex = item;
            for (var openLine = localIndex +1; openLine < Content.Length; openLine++)
            {
               
                line += Content[openLine];

                item = openLine;

                if (Content[openLine].EndsWith("\""))
                    break;


            }
        }

        private Tag SearchForParameters(ref int n, Tag recoveryTag)
        {
            if (recoveryTag.Name == "report")
            { }
            for (int i = n+1; i < ContentLength; i++)
            {
                var line = Content[i];
                var index = i;
                if (TagUtilsProvider.IsValidTag(line))
                    return recoveryTag;
                if (ParametersUtils.IsValidParameter(line))
                {
                    recoveryTag.RegisterParameter(ParseParameters(ref index));
                    n = index;
                    break;
                }

            }
            return recoveryTag;
        }

        private Tag ParseThisLineAsTag(ref int n, Tag recoveryTag)
        {
            for (int i = n; i < ContentLength; i++)
            {
                var line = Content[i];
                var index = i;
                if (TagUtilsProvider.IsValidTag(line))
                {
                    TagUtilsProvider provider = new TagUtilsProvider(line);
                    n = index;
                    return TagFactory.CreateTagFromTagUtilsProvider(provider);
                }
            }
            return recoveryTag;
        }

        private Tag ReccursiveParsing(int n = 0, Tag recoveryTag = null)
        {
            if (n == ContentLength)
            {
                return null;
            }


            TreatItLikeATag(n, recoveryTag, out int localIndex, out Tag localTag);

            if (!localTag.IsTagClosed)
            {
                for (int nestedIndex = localIndex; nestedIndex < ContentLength && !localTag.IsTagClosed;)
                {
                    

                    if (!localTag.IsTagClosed)
                    {
                        var line = Content[localIndex];
                        Tag ChildTag = ReccursiveParsing(localIndex + 1, localTag);
                        localIndex = ChildTag.closeTagIndex;
                        nestedIndex = localIndex;
                        localTag.RegisterChildTag(ChildTag);

                        localTag = SearchForClosingTag(ref localIndex, localTag);
                    }
                }
            }
           return localTag; 
        }

        private void TreatItLikeATag(int n, Tag recoveryTag, out int localIndex, out Tag localTag)
        {
            localIndex = n;

            localTag = ParseThisLineAsTag(ref localIndex, recoveryTag);
            localTag = SearchForParameters(ref localIndex, localTag);
            localTag = SearchForClosingTag(ref localIndex, localTag);
        }

        private Tag SearchForClosingTag(ref int n, Tag recoveryTag)
        {
            for (int i = n+1; i < ContentLength; i++)
            {
                var line = Content[i];
                if (TagUtilsProvider.IsValidTag(line) || ParametersUtils.IsValidParameter(line))
                {
                    return recoveryTag;
                }
                else if (TagUtilsProvider.IsValidClosingTag(line))
                {
                    TagUtilsProvider closingTag = new TagUtilsProvider(line);

                    if (recoveryTag.CloseTag(closingTag.GetClosingTagName))
                    {
                        recoveryTag.closeTagIndex = i;
                        n = i;
                        return recoveryTag;
                    }
                    else
                        throw new Exception("si za huinea");

                    
                }
            }
            return recoveryTag;
        }
    }
}
