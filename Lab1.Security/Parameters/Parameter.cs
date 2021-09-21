using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1.Security
{
    public class Parameter
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Parameter(string name, string description)
        {
            Name = name;
            Description = description;
        }

        
    }
}
