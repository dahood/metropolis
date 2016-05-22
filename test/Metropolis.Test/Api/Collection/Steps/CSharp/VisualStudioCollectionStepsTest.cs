using System.Linq;
using FluentAssertions;
using Metropolis.Api.Collection.Steps.CSharp;
using Metropolis.Common.Models;
using Metropolis.Test.Api.Services;
using Moq;
using NUnit.Framework;

namespace Metropolis.Test.Api.Collection.Steps.CSharp
{
    [TestFixture]
    public class VisualStudioCollectionStepsTest : StrictMockBaseTest
    {
        private VisualStudioCollectionSteps step;
        private Mock<ICollectAssemblies> assemblyCollection;
        private Mock<IFxCopCollectionTask> metricsTask;

        [SetUp]
        public void SetUp()
        {
            assemblyCollection = CreateMock<ICollectAssemblies>();
            metricsTask = CreateMock<IFxCopCollectionTask>();
            step = new VisualStudioCollectionSteps(assemblyCollection.Object, metricsTask.Object);
        }

        [Test]
        public void CanCollectFxCopMetrics()
        {
            var args =new MetricsCommandArguments();
            const string assembly = "myAssembly.dll";

            assemblyCollection.Setup(x => x.GatherAssemblies(args)).Returns(new[] {assembly});
            metricsTask.Setup(x => x.Run(args, assembly)).Returns(new MetricsResult());

            var results =  step.Run(args);

            results.Should().NotBeEmpty();
            results.Count().Should().Be(1);
        }
    }
}
