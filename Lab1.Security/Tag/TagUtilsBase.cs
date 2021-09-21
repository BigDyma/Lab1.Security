using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1.Security
{
    public class TagUtilsBase
    {
        protected int IndexOfOpeningTag { get => Line.IndexOf("<"); }
        protected int IndexOfClosingTagNoAttributes { get => Line.LastIndexOf(">");   }
        protected int IndexOfClosingTagWithAttributes { get => Line.LastIndexOf("\">"); }

        protected int IndexOfStartingAttributesValue { get => Line.IndexOf(":\""); }

        public bool IsTagWithAttributes => IndexOfClosingTagWithAttributes > 0;
        protected string Line { get; set; }
        public TagUtilsBase(string line)
        {
            Line = line;
        }
    }
}
