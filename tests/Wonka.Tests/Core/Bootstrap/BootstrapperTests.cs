using System.IO;
using NUnit.Framework;
using FluentAssertions;
using Wonka.Core.Bootstrap;

namespace Wonka.Tests.Core.Bootstrap
{
    [TestFixture]
    public class BootstrapperTests
    {
        private Configuration _configuration;

        [SetUp]
        public void Setup()
        {
            _configuration = new Configuration
                                 {
                                     Database = "tests.s3db"
                                 };

            if (File.Exists(_configuration.Database))
            {
                File.Delete(_configuration.Database);
            }
        }

        [Test]
        public void should_create_initial_database()
        {
            // arrange
            var bootstrapper = new Bootstrapper(_configuration);

            // act
            bootstrapper.CreateDb();

            // assert
            File.Exists(_configuration.Database).Should().BeTrue();
        }
    }
}
