using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Wonka.Core.Engine;
using Wonka.Core.Github;

namespace Wonka.Tests.Core.Engine
{
    public class BlogPostRetriverTests
    {
        [Test]
        public void should_retrive_blog_posts()
        {
            // arrange
            var adapter = Substitute.For<IGithubAdapter>();
            var references = new List<TreeReference>
                                 {
                                     new TreeReference { Url = "url", Path = "path", Sha = "sha"}
                                 };
            var retriever = new BlogPostRetriver(adapter);

            // act
            var posts = retriever.GetPosts(references);

            // assert
            posts.Should().NotBeNull();
        }

        [Test]
        public void should_blog_post_contain_title()
        {
            // arrange
            var adapter = Substitute.For<IGithubAdapter>();
            var references = new List<TreeReference>
                                 {
                                     new TreeReference { Url = "url", Path = "path", Sha = "sha"}
                                 };
            var retriever = new BlogPostRetriver(adapter);

            // act
            var posts = retriever.GetPosts(references);
            
            // assert
            posts.First().Title.Should().NotBeNull();
        }

        [Test]
        public void should_blog_post_contain_date()
        {
            // arrange
            var adapter = Substitute.For<IGithubAdapter>();
            var references = new List<TreeReference>
                                 {
                                     new TreeReference { Url = "url", Path = "path", Sha = "sha"}
                                 };
            var retriever = new BlogPostRetriver(adapter);

            // act
            var posts = retriever.GetPosts(references);

            // assert
            // TODO: fake test, should be fixed...
            posts.First().Date.Should().HaveYear(2012);
        }

        [Test]
        public void should_have_body()
        {
            // arrange
            var adapter = Substitute.For<IGithubAdapter>();
            var references = new List<TreeReference>
                                 {
                                     new TreeReference { Url = "url", Path = "path", Sha = "sha"}
                                 };
            var retriever = new BlogPostRetriver(adapter);

            // act
            var posts = retriever.GetPosts(references);

            // assert
            posts.First().Body.Should().NotBeNull();
        }
    }
}
