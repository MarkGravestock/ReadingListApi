using System;
using System.Collections.Generic;

namespace ReadlingList.Domain
{
    public class ItemToRead
    {
        public string Id { get; set; }
        public Uri ImageUri { get; set; }
        public Uri Uri { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Tags { get; set; } 
    }
}