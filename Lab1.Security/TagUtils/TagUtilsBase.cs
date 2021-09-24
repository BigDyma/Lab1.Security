using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1.Security
{
    public abstract class TagUtilsBase
    {
        protected int IndexOfOpeningTag { get => Line.IndexOf("<") +1; }
        protected int IndexOfClosingTagNoAttributes { get => Line.IndexOf(">");   }
        protected int IndexOfClosingTagWithAttributes { get => Line.IndexOf("\">"); }

        protected int IndexOfStartingAttributeName { get => Line.IndexOf(" "); }

        protected int IndexOfMiddleTag { get => Line.IndexOf(":"); }

        protected int IndexOfEndAttributeName { get => IndexOfMiddleTag ; }
        protected int IndexOfStartingAttributesValue { get => IndexOfMiddleTag +2; }

        protected int IndexOfEndName { get => IndexOfStartingAttributeName; }

        protected int IndexOfOpeningClosingTag { get => IndexOfOpeningTag + 1; }
        public bool IsTagWithAttributes => IndexOfClosingTagWithAttributes > 0 && IndexOfStartingAttributeName > 0;

        public bool IsTagAnAttribute => IndexOfClosingTagWithAttributes > 0;
        protected string Line { get; set; }
        public TagUtilsBase(string line)
        {
            Line = line.Trim() ;
        }
    }
}
