using System.Collections.Generic;
using Wonka.Core.Github.Model;

namespace Wonka.Core.Github
{
    public interface IGithubAdapter
    {
        IList<Reference> GetAllReferences();
        Trees GetTrees(string sha);
        Blob GetBlob(string sha);
    }
}