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

        public string[] Content { get; set; }

        public int RecursionStartIndex { get; set; }
        public int ContentLength => Content.Length;
        public AuditFile(string _path)
        {
            Path = _path;
            Content = File.ReadAllLines(Path);

        }
        private void SeedFirstTag()
        {

            foreach (var item in Content.Select((value, index) => new { value, index }))
            {
                var line = item.value;
                var index = item.index;
                if (TagUtilsProvider.IsValidTag(item.value))
                {
                    TagUtilsProvider provider = new TagUtilsProvider(item.value);

                    GlobalTag = CreateTagFromTagUtilsProvider(provider);
                    RecursionStartIndex = item.index;
                    break;
                }
            }
        }
        public void Parse()
        {
            SeedFirstTag();

            GlobalTag.RegisterChildTag(ReccursiveParsing(RecursionStartIndex));
        }

        public Tag ParseTag(string line)
        {

            if (TagUtilsProvider.IsValidTag(line))
            {
                TagUtilsProvider provider = new TagUtilsProvider(line);

                return CreateTagFromTagUtilsProvider(provider);
            }
            else throw new Exception("something bad is happening, run!");
        }

        // @TODO move this to utils
        private Tag CreateTagWithAttributes(TagUtilsProvider provider)
        {
             return new Tag(provider.GetTagName, provider.GetTagValue);
        }

        private Tag CreateTagWithNoAttributes(TagUtilsProvider provider)
        {
            return new Tag(provider.GetTagName);
        }

        private Tag CreateTagFromTagUtilsProvider(TagUtilsProvider provider)
        {
            if (!provider.IsValidTag())
                throw new Exception("what the hell");

            if (provider.IsTagWithAttributes)
              return  CreateTagWithAttributes(provider);

            return CreateTagWithNoAttributes(provider);
        }

        public List<Parameter> ParseParameters( ref int n)
        {
            List<Parameter> parameters = new List<Parameter>();
            foreach (var item in Content.Skip(n).ToArray().Select((value, index) => new { value, index }))
            {
                var line = item.value;
                n = item.index;

                if (ParametersUtils.IsValidParameterAsClosedString(line) || ParametersUtils.IsValidParameterAsType(line))
                {
                     parameters.Add(ParametersUtils.CreateParameterFromLineForClosedStrings(line));
                }
                else if (ParametersUtils.IsValidParameterAsNonClosedString(line))
                {
                    // @TODO finish this
                    parameters.Add(ParametersUtils.CreateParameterFromLineForNonClosedString(line));
                }
            }

            return parameters;
        }

        public Tag ReccursiveParsing(int n)
        {
            if (n == ContentLength - 1)
                return null;

            

            Tag localTag = ParseTag(Content[n]);
            foreach (var item in Content.Skip(n).ToArray().Select((value, index) => new  { value, index }))
            {
                var line = item.value;
                var index = item.index;

                if (ParametersUtils.IsValidParameter(line))
                {
                    localTag.RegisterParameter(ParseParameters(ref index));
                }
                //@TODO check if it's closing tag

               localTag.RegisterChildTag(ReccursiveParsing(index));    

            }
            return localTag;
        }
    }
}
