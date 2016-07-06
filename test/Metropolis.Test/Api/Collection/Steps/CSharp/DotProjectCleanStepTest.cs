using FluentAssertions;
using Metropolis.Api.Build;
using Metropolis.Api.Collection.Steps.CSharp;
using Metropolis.Common.Models;
using Metropolis.Test.Api.Services;
using Moq;
using NUnit.Framework;

namespace Metropolis.Test.Api.Collection.Steps.CSharp
{
    [TestFixture]
    public class DotProjectCleanStepTest : StrictMockBaseTest
    {
        DotProjectCleanStep step;
        private Mock<IProjectBuilder> projectBuilder;

        [SetUp]
        public void SetUp()
        {
            projectBuilder = CreateMock<IProjectBuilder>();
            step = new DotProjectCleanStep(projectBuilder.Object);
        }

        [Test]
        public void ShouldCleanProject()
        {
            var args = new MetricsCommandArguments {ProjectFile = @"c:\TestProj.sln"};
            projectBuilder.Setup(x => x.CleanProject(args.ProjectFile));
            var results = step.Run(args);
            results.Should().BeEquivalentTo(MetricsResult.Empty());
        }
    }
}
