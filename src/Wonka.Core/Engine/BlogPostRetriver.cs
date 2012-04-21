using System;
using System.Linq;
using System.Collections.Generic;
using Wonka.Core.Engine.Model;
using Wonka.Core.Github;
using Wonka.Core.Github.Model;

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
            return references.Select(ToBlogPost);
        }

        private BlogPost ToBlogPost(TreeReference b)
        {
            var blob = _adapter.GetBlob(b.Sha);
            return new BlobToBlogPost().Process(blob);
        }
    }
}