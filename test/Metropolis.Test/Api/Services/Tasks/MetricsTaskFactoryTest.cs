using FluentAssertions;
using Metropolis.Api.Services.Tasks;
using Metropolis.Api.Services.Tasks.Commands;
using Metropolis.Common.Models;
using NUnit.Framework;

namespace Metropolis.Test.Api.Services.Tasks
{
    [TestFixture]

    public class MetricsTaskFactoryTest
    {
        private MetricsTaskFactory factory;

        [SetUp]
        public void SetUp()
        {
            factory = new MetricsTaskFactory();
        }

        [Test]
        public void CanGetCSharpCommand()
        {
            factory.CommandFor(RepositorySourceType.CSharp)
                   .Should().NotBeNull()
                   .And.BeAssignableTo<CSharpMetricsCommand>();
        }

        [Test]
        public void CanGetJavaCommand()
        {
            factory.CommandFor(RepositorySourceType.Java)
                   .Should().NotBeNull()
                   .And.BeAssignableTo<JavaMetricsCommand>();
        }

        [Test]
        public void CanGetEcmaCommand()
        {
            factory.CommandFor(RepositorySourceType.ECMA)
                   .Should().NotBeNull()
                   .And.BeAssignableTo<EcmaMetricsCommand>();
        }
    }
}
