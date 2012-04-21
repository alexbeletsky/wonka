using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Machine.Specifications;
using NSubstitute;
using Wonka.Core.Engine;
using Wonka.Core.Engine.Model;
using Wonka.Core.Github;
using Wonka.Core.Github.Model;

// ReSharper disable UnusedMember.Local
// ReSharper disable NotAccessedField.Local

namespace Wonka.Tests.Core.Engine
{
    public class blog_posts_retriever_tests
    {
        internal static IGithubAdapter adapter;
        internal static IList<TreeReference> references;
        internal static BlogPostRetriver retriver;
        internal static IEnumerable<BlogPost> result;

        private Establish context = () =>
                                        {
                                            adapter = Substitute.For<IGithubAdapter>();
                                            retriver = new BlogPostRetriver(adapter);
                                        };
    }


    [Subject(typeof (BlogPostRetriver))]
    public class when_processing_html_content : blog_posts_retriever_tests
    {
        private Establish setup = () =>
                                      {
                                          adapter.GetBlob("sha").Returns(new Blob
                                                                             {
                                                                                 Encoding = "utf-8",
                                                                                 Content =
                                                                                     "<title>Good evening</title><body></body>"
                                                                             });

                                          references = new List<TreeReference>
                                                           {
                                                               new TreeReference
                                                                   {Url = "url", Path = "path", Sha = "sha"}
                                                           };
                                      };

        private Because of = () => { result = retriver.GetPosts(references); };

        private It should_create_new_blog_post = () => result.First().Should().NotBeNull();

        private It should_have_initialized_title = () => result.First().Title.Should().Be("Good evening");

        private It should_have_initialized_body = () => result.First().Body.Should().Be("");
    }

    //public class BlogPostRetriverTests
    //{
    //    [Test]
    //    public void should_retrive_blog_posts()
    //    {
    //        // arrange
    //        var adapter = Substitute.For<IGithubAdapter>();
    //        var references = new List<TreeReference>
    //                             {
    //                                 new TreeReference { Url = "url", Path = "path", Sha = "sha"}
    //                             };
    //        var retriever = new BlogPostRetriver(adapter);

    //        // act
    //        var posts = retriever.GetPosts(references);

    //        // assert
    //        posts.Should().NotBeNull();
    //    }

    //    [Test]
    //    public void should_blog_post_contain_title()
    //    {
    //        // arrange
    //        var adapter = Substitute.For<IGithubAdapter>();
    //        var references = new List<TreeReference>
    //                             {
    //                                 new TreeReference { Url = "url", Path = "path", Sha = "sha"}
    //                             };
    //        var retriever = new BlogPostRetriver(adapter);

    //        // act
    //        var posts = retriever.GetPosts(references);

    //        // assert
    //        posts.First().Title.Should().NotBeNull();
    //    }

    //    [Test]
    //    public void should_title_be_extracted_from_blog_content()
    //    {
    //        // arrange
    //        var adapter = Substitute.For<IGithubAdapter>();
    //        adapter.GetBlob("sha").Returns(new Blob { Encoding = "utf-8", Content = "<title>Good evening</title><body></body>"});
    //        var references = new List<TreeReference>
    //                             {
    //                                 new TreeReference { Url = "url", Path = "path", Sha = "sha"}
    //                             };
    //        var retriever = new BlogPostRetriver(adapter);

    //        // act
    //        var posts = retriever.GetPosts(references);

    //        // assert            
    //        posts.First().Title.Should().Be("Good evening");
    //    }

    //[Test]
    //public void should_blog_post_contain_date()
    //{
    //    // arrange
    //    var adapter = Substitute.For<IGithubAdapter>();
    //    var references = new List<TreeReference>
    //                         {
    //                             new TreeReference { Url = "url", Path = "path", Sha = "sha"}
    //                         };
    //    var retriever = new BlogPostRetriver(adapter);

    //    // act
    //    var posts = retriever.GetPosts(references);

    //    // assert
    //    // TODO: fake test, should be fixed...
    //    posts.First().Date.Should().HaveYear(2012);
    //}

    //[Test]
    //public void should_have_body()
    //{
    //    // arrange
    //    var adapter = Substitute.For<IGithubAdapter>();
    //    var references = new List<TreeReference>
    //                         {
    //                             new TreeReference { Url = "url", Path = "path", Sha = "sha"}
    //                         };
    //    var retriever = new BlogPostRetriver(adapter);

    //    // act
    //    var posts = retriever.GetPosts(references);

    //    // assert
    //    posts.First().Body.Should().NotBeNull();
    //}
}