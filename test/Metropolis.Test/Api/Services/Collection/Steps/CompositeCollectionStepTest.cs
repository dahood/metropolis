using System.Linq;
using FluentAssertions;
using Metropolis.Api.Collection.Steps;
using Metropolis.Common.Models;
using Moq;
using NUnit.Framework;

namespace Metropolis.Test.Api.Services.Collection.Steps
{
    [TestFixture]
    public class CompositeCollectionStepTest : StrictMockBaseTest
    {
        CompositeCollectionStep compositeStep;
        private Mock<ICollectionStep> step1;
        private Mock<ICollectionStep> step2;

        [SetUp]
        public void SetUp()
        {
            step1 = CreateMock<ICollectionStep>();
            step2 = CreateMock<ICollectionStep>();

        }

        [Test]
        public void Should_Run_Both_Steps_In_Parallel()
        {
            var args = new MetricsCommandArguments();

            step1.Setup(x => x.Run(args)).Returns(new[] {new MetricsResult {MetricsFile = "one"}});
            step2.Setup(x => x.Run(args)).Returns(new[] {new MetricsResult {MetricsFile = "two"}});

            compositeStep = new CompositeCollectionStep(new[] { step1.Object, step2.Object });
            var results = compositeStep.Run(args);

            results.Should().NotBeNull();
            results.Count().Should().Be(2);
        }
        [Test]
        public void Should_Run_Both_Steps()
        {
            var args = new MetricsCommandArguments();

            step1.Setup(x => x.Run(args)).Returns(new[] {new MetricsResult {MetricsFile = "one"}});
            step2.Setup(x => x.Run(args)).Returns(new[] {new MetricsResult {MetricsFile = "two"}});

            compositeStep = new CompositeCollectionStep(new[] { step1.Object, step2.Object }, false);
            var results = compositeStep.Run(args);

            results.Should().NotBeNull();
            results.Count().Should().Be(2);
        }
    }
}
