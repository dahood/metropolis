using FluentAssertions;
using Metropolis.Api.Collection;
using Metropolis.Api.Collection.Steps.CSharp;
using Metropolis.Api.Collection.Steps.ECMA;
using Metropolis.Api.Collection.Steps.Java;
using Metropolis.Common.Models;
using NUnit.Framework;

namespace Metropolis.Test.Api.Collection
{
    [TestFixture]

    public class MetricsStepFactoryTest
    {
        private CollectionStepFactory factory;

        [SetUp]
        public void SetUp()
        {
            factory = new CollectionStepFactory();
        }

        [Test]
        public void CanGetCSharpCommand()
        {
            factory.GetStep(RepositorySourceType.CSharp)
                   .Should().NotBeNull()
                   .And.BeAssignableTo<VisualStudioCollectionSteps>();
        }

        [Test]
        public void CanGetJavaCommand()
        {
            factory.GetStep(RepositorySourceType.Java)
                   .Should().NotBeNull()
                   .And.BeAssignableTo<PuppyCrawlerCheckstyleCollectionStep>();
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
