using FluentAssertions;
using Metropolis.Api.Collection;
using Metropolis.Api.Collection.Steps;
using Metropolis.Common.Models;
using NUnit.Framework;

namespace Metropolis.Test.Api.Services.Collection
{
    [TestFixture]

    public class MetricsStepFactoryTest
    {
        private MetricsStepFactory factory;

        [SetUp]
        public void SetUp()
        {
            factory = new MetricsStepFactory();
        }

        [Test]
        public void CanGetCSharpCommand()
        {
            factory.GetStep(RepositorySourceType.CSharp)
                   .Should().NotBeNull()
                   .And.BeAssignableTo<CSharpCollectionStep>();
        }

        [Test]
        public void CanGetJavaCommand()
        {
            factory.GetStep(RepositorySourceType.Java)
                   .Should().NotBeNull()
                   .And.BeAssignableTo<JavaCollectionStep>();
        }

        [Test]
        public void CanGetEcmaCommand()
        {
            factory.GetStep(RepositorySourceType.ECMA)
                   .Should().NotBeNull()
                   .And.BeAssignableTo<EcmaCollectionStep>();
        }
    }
}
