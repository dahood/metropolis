using FluentAssertions;
using Metropolis.Api.Collection.PowerShell;
using Metropolis.Api.Collection.Steps;
using Metropolis.Api.Collection.Steps.Java;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;
using Moq;
using NUnit.Framework;

namespace Metropolis.Test.Api.Collection.Steps.Java
{
    [TestFixture]
    public class PuppyCrawlerCheckstyleCollectionStepTest : CollectionBaseTest
    {
        private PuppyCrawlerCheckstyleCollectionStep step;
        private Mock<IRunPowerShell> powerShell;
        private string expectedCommand;

        [SetUp]
        public void BeforeEachTest()
        {
            powerShell = CreateMock<IRunPowerShell>();
            step = new PuppyCrawlerCheckstyleCollectionStep(powerShell.Object);

            expectedCommand = PuppyCrawlerCheckstyleCollectionStep.CheckstyleCommand
                                              .FormatWith(BaseCollectionStep.LocateBinaries("checkstyle-6.18-all.jar"), 
                                                          BaseCollectionStep.LocateSettings("metropolis_checkstyle_metrics.xml"),
                                                          Result.MetricsFile, Args.SourceDirectory);
        }

        [Test]
        public void HasCorrectSettings()
        {
            step.Extension.Should().Be(".xml");
            step.MetricsType.Should().Be("Java Checkstyle");
            step.ParseType.Should().Be(ParseType.PuppyCrawler);
        }

        [Test]
        public void CanParseCommand()
        {
            var command = step.PrepareCommand(Args, Result);
            command.Should().Be(expectedCommand);
        }
    }
}
