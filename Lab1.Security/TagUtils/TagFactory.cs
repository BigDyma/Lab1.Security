using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1.Security
{
   public abstract class TagFactory
    {
        public static Tag CreateTagWithAttributes(TagUtilsProvider provider)
        {
            return new Tag(provider.GetTagName, new Attributes { Name=provider.GetNameOfAttribute, Value=provider.GetTagValue});
        }

        public static Tag CreateTagWithNoAttributes(TagUtilsProvider provider)
        {
            return new Tag(provider.GetTagName);
        }

        public static Tag CreateTagFromTagUtilsProvider(TagUtilsProvider provider)
        {
            if (!provider.IsValidTag())
                throw new Exception("what the hell");

            if (provider.IsTagWithAttributes)
                return CreateTagWithAttributes(provider);
            else if (provider.IsTagAnAttribute)
                return CreateTagAsAnAttribute(provider);

            return CreateTagWithNoAttributes(provider);
        }

        private static Tag CreateTagAsAnAttribute(TagUtilsProvider provider)
        {
            return new Tag(provider.GetTagName, provider.GetTagValue);

        }
    }
}
