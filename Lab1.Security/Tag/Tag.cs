using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab1.Security
{
    public class Tag
    {

        public string Name { get;  set; }
        public string Value { get ;  set ; }

        public bool IsTagClosed { get;  private set ; }

        public List<Tag> ChildTag = new List<Tag>();

        public List<Parameter> Parameters { get; set; }
        public void CloseTag(string tagName) => IsTagClosed = tagName == Name;
     

        public void RegisterChildTag(Tag tag)
        {
            if (!IsTagClosed)
                ChildTag.Add(tag);
            else
                throw new Exception("Don't joke with me!");
        }

        public void RegisterParameter(List<Parameter> parameters)
        {
            if (!IsTagClosed)
                Parameters = parameters;
            else
                throw new Exception("You naughty-naughty!");
        }
        public Tag(string _name, string _value)
        {
            Name = _name;
            Value = _value;
        }
        public Tag(string _name)
        {
            Name = _name;
        }
    }
}
