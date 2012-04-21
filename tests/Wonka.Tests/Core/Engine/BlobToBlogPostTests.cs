using System;
using System.Text;
using FluentAssertions;
using Machine.Specifications;
using Wonka.Core.Engine;
using Wonka.Core.Engine.Model;
using Wonka.Core.Github.Model;

// ReSharper disable UnusedMember.Local

namespace Wonka.Tests.Core.Engine
{
    public class blob_to_blog_post_tests
    {
        internal static BlobToBlogPost proc;
        internal static BlogPost result;

        private Establish context = () =>
                                        {
                                            proc = new BlobToBlogPost();
                                        };
    }

    [Subject(typeof(BlobToBlogPost))]
    public class when_content_is_utf_8_and_html : blob_to_blog_post_tests
    {
        static Blob blob = new Blob
            {
                Encoding = "utf-8",
                Content = 
                    "<html !doctype><head><title>I'm cool post!</title></head><body><p>Hello World</p></body>"
            };

        Because of = () => result = proc.Process(blob);

        It should_create_blog_post = () => result.Should().NotBeNull();

        It should_blog_post_contain_title = () => result.Title.Should().Be("I'm cool post!");

        It should_blog_post_contain_body_as_html = () => result.Body.Should().Be("<p>Hello World</p>");
    }

    [Subject(typeof(BlobToBlogPost))]
    public class when_content_is_base64_and_html : blob_to_blog_post_tests
    {
        static Blob blob = new Blob
        {
            Encoding = "base64",
            Content = Convert.ToBase64String(
                            Encoding.UTF8.GetBytes("<html !doctype><head><title>I'm cool post!</title></head><body><p>Hello World</p></body>"))
        };

        Because of = () => result = proc.Process(blob);

        It should_create_blog_post = () => result.Should().NotBeNull();

        It should_blog_post_contain_title = () => result.Title.Should().Be("I'm cool post!");

        It should_blog_post_contain_body_as_html = () => result.Body.Should().Be("<p>Hello World</p>");        
    }
}
