using FluentAssertions;
using Metropolis.Api.Collection.Steps.AllLanguages;
using Metropolis.Common.Models;
using NUnit.Framework;

namespace Metropolis.Test.Api.Collection.Steps.AllLanguages
{
    [TestFixture]
    public class SlocCollectionStepTest : CollectionBaseTest
    {
        private SlocCollectionStep step;      

        [SetUp]
        public void BeforeEachTest()
        {
            step = new SlocCollectionStep(ParseType.EsLint);
        }

        [Test]
        public void HasCorrectSettings()
        {
            step.Extension.Should().Be(".csv");
            step.MetricsType.Should().Be("Sloc");
            step.ParseType.Should().Be(ParseType.EsLint);
        }

        [Test]
        public void CanParseCommand()
        {
            string expected = $@"{NodeModulesPath}sloc '{Args.SourceDirectory}' -d --format csv -> '{Result.MetricsFile}'";
            var command = step.PrepareCommand(Args, Result);

            command.Should().Be(expected);
            step.ValidateMetricResults("afile").Should().BeEmpty();
        }
    }
}
