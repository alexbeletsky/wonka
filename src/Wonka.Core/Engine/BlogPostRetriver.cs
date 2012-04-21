using System;
using System.Collections.Generic;
using Wonka.Core.Engine.Model;
using Wonka.Core.Github;

namespace Wonka.Core.Engine
{
    public class BlogPostRetriver
    {
        private readonly IGithubAdapter _adapter;

        public BlogPostRetriver(IGithubAdapter adapter)
        {
            _adapter = adapter;
        }

        public IEnumerable<BlogPost> GetPosts(IEnumerable<TreeReference> references)
        {
            return new List<BlogPost> {new BlogPost {Title = "some", Date = DateTime.Now, Body = "xxx"}};
        }
    }
}