using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab1.Security
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Tag
    {
        [JsonProperty]
        public string Name { get;  set; }
        [JsonProperty]
        public Attributes Attributes { get; set; }

        public string Attribute { get; set; }
        public bool IsTagClosed { get;  private set ; }

        [JsonProperty]
        public List<Tag> ChildTag = new List<Tag>();

        public int closeTagIndex { get; set; }
        [JsonProperty]
        public List<Parameter> Parameters { get; set; }
        public bool CloseTag(string tagName) => IsTagClosed = tagName == Name;
     
        public bool ShouldSerializeAttributes()
        {
            return (Attributes is object);
        }
        public bool ShouldSerializeParameters()
        {
            return (Parameters is object);
        }

        public bool ShouldSerializeChildTag()
        {
            return (ChildTag.Count > 0);
        }

        public void RegisterChildTag(Tag tag)
        {
            if (!IsTagClosed)
                ChildTag.Add(tag);

        }

        public void RegisterParameter(List<Parameter> parameters)
        {
            if (!IsTagClosed)
                Parameters = parameters;

        }
        public Tag(string _name, Attributes _attributes)
        {
            Name = _name;
            Attributes = _attributes;
        }
        public Tag(string _name, string _value)
        {
            Name = _name;
            Attribute = _value;
        }
        public Tag(string _name)
        {
            Name = _name;
        }
    }
}
