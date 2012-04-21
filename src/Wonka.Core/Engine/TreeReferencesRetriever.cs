using System.Collections.Generic;
using System.Linq;
using Wonka.Core.Github;

namespace Wonka.Core.Engine
{
    public class TreeReferencesRetriever
    {
        private readonly IGithubAdapter _adapter;

        public TreeReferencesRetriever(IGithubAdapter adapter)
        {
            _adapter = adapter;
        }

        public IEnumerable<TreeReference> ForAll()
        {
            var reference = _adapter.GetAllReferences().First();
            var trees = _adapter.GetTrees(reference.Object.Sha);

            return trees.Tree.Select(i => new TreeReference {Url = i.Url, Sha = i.Sha, Path = i.Path});
        }

        public IEnumerable<TreeReference> ForPathContains(string contains)
        {
            return ForAll().Where(r => r.Path.Contains(contains));
        }
    }
}