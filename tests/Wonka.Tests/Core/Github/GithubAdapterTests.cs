using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Wonka.Core.Github;

namespace Wonka.Tests.Core.Github
{
    [TestFixture]
    public class GithubAdapterTests
    {
        [Test]
        public void should_be_at_least_one_reference()
        {
            // arrange
            var adapter = new GithubAdapter("alexanderbeletsky", "blog.beletsky.net");

            // act
            var references = adapter.GetAllReferences();

            // assert
            references.Count.Should().BeGreaterOrEqualTo(1);
        }

        [Test]
        public void should_contain_ref_and_object_and_url_fields()
        {
            // arrange
            var adapter = new GithubAdapter("alexanderbeletsky", "blog.beletsky.net");

            // act
            var references = adapter.GetAllReferences();

            // assert
            references.First().Ref.Should().NotBeEmpty();
            references.First().Url.Should().NotBeEmpty();
            references.First().Object.Should().NotBeNull();

        }

        [Test]
        public void should_object_contain_sha_key()
        {
            // arrange
            var adapter = new GithubAdapter("alexanderbeletsky", "blog.beletsky.net");

            // act
            var references = adapter.GetAllReferences();

            // assert
            references.First().Object.Sha.Should().NotBeEmpty();
        }

        [Test]
        public void should_trees_contain_tree()
        {
            // arrange
            var adapter = new GithubAdapter("alexanderbeletsky", "blog.beletsky.net");

            // act
            var references = adapter.GetAllReferences();
            var tree = adapter.GetTrees(references.First().Object.Sha);

            // assert
            tree.Tree.Should().NotBeNull();
        }

        [Test]
        public void should_contain_at_least_one_item_in_tree()
        {
            // arrange
            var adapter = new GithubAdapter("alexanderbeletsky", "blog.beletsky.net");

            // act
            var references = adapter.GetAllReferences();
            var tree = adapter.GetTrees(references.First().Object.Sha);

            // assert
            tree.Tree.Count.Should().BeGreaterOrEqualTo(1);
        }

        [Test]
        public void should_tree_contain_path_and_url_and_sha()
        {
            // arrange
            var adapter = new GithubAdapter("alexanderbeletsky", "blog.beletsky.net");

            // act
            var references = adapter.GetAllReferences();
            var tree = adapter.GetTrees(references.First().Object.Sha);

            // assert
            tree.Tree.First().Path.Should().NotBeEmpty();
            tree.Tree.First().Url.Should().NotBeEmpty();
            tree.Tree.First().Sha.Should().NotBeEmpty();
        }

        [Test]
        public void should_blob_contain_content_field()
        {
            // arrange
            var adapter = new GithubAdapter("alexanderbeletsky", "blog.beletsky.net");

            // act
            var references = adapter.GetAllReferences();
            var trees = adapter.GetTrees(references.First().Object.Sha);
            var blob = adapter.GetBlob(trees.Tree.First().Sha);

            // assert
            blob.Content.Should().NotBeEmpty();
        }
    }
}
