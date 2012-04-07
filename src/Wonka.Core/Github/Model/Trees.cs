using System.Collections.Generic;

namespace Wonka.Core.Github.Model
{
    public class Trees
    {
        public string Sha { get; set; }
        public string Url { get; set; }
        public IList<Item> Tree { get; set; }
    }
}