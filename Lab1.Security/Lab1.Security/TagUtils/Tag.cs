using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab1.Security
{
    public class Tag
    {
        public string Name { get;  set; }
        public Attributes Attributes { get; set; }
        public bool IsTagClosed { get;  private set ; }

        public List<Tag> ChildTag = new List<Tag>();

        public int closeTagIndex { get; set; }
        public List<Parameter> Parameters { get; set; }
        public bool CloseTag(string tagName) => IsTagClosed = tagName == Name;
     

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
        public Tag(string _name)
        {
            Name = _name;
        }
    }
}
