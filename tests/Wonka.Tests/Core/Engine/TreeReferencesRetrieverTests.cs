using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Wonka.Core.Github;
using Wonka.Core.Github.Model;
using Object = Wonka.Core.Github.Model.Object;

namespace Wonka.Tests.Engine
{
    [TestFixture]
    public class TreeReferencesRetrieverTests
    {
        private IGithubAdapter _adapter;
        private TreeReferencesRetriever _treeReferencesRetriever;

        [SetUp]
        public void Setup()
        {
            _adapter = Substitute.For<IGithubAdapter>();
            _treeReferencesRetriever = new TreeReferencesRetriever(_adapter);
        }
        
        [Test]
        public void should_retrive_the_list_of_post()
        {
            // arrange
            var references = new List<Reference>
                                 {
                                     new Reference
                                         {
                                             Ref = "/head/master", 
                                             Url = "url", 
                                             Object = new Object { Sha = "sha" }
                                         }
                                 };
            _adapter.GetAllReferences().Returns(references);

            var trees = new Trees
                                 {
                                     Sha = "sha",
                                     Url = "tree/url",
                                     Tree = new List<Item>
                                                {
                                                    new Item {Path = "/sub/index.html"}
                                                }
                                 };
            _adapter.GetTrees("sha").Returns(trees);

            // act
            var treeReferences = _treeReferencesRetriever.ForAll();

            // assert
            treeReferences.Count().Should().Be(1);
        }

        [Test]
        public void should_retrive_references_with_mathching_pattern()
        {
            // arrange
            var references = new List<Reference>
                                 {
                                     new Reference
                                         {
                                             Ref = "/head/master", 
                                             Url = "url", 
                                             Object = new Object { Sha = "sha" }
                                         }
                                 };
            _adapter.GetAllReferences().Returns(references);

            var trees = new Trees
                            {
                                Sha = "sha",
                                Url = "tree/url",
                                Tree = new List<Item>
                                           {
                                               new Item {Path = "/sub/index.html"},
                                               new Item {Path = "/sub1/index.html"},
                                               new Item {Path = "/bub/index.cshtml"}
                                           }
                            };
            _adapter.GetTrees("sha").Returns(trees);

            // act
            var treeReferences = _treeReferencesRetriever.ForPathContains(".html");

            // assert
            treeReferences.Count().Should().Be(2);
        }
    }

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

    public class TreeReference
    {
        public string Path { get; set; }
        public string Url { get; set; }
        public string Sha { get; set; }
    }
}
