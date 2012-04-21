using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Wonka.Core.Engine;
using Wonka.Core.Github;
using Wonka.Core.Github.Model;

namespace Wonka.Tests.Core.Engine
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
}
