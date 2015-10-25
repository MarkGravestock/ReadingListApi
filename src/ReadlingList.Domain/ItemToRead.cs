using System;
using System.Collections.Generic;

namespace ReadlingList.Domain
{
    public class ItemToRead
    {
        public Uri Uri { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Tags { get; set; } 
    }
}