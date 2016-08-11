using FluentAssertions;
using Metropolis.Api.Collection.Steps.AllLanguages;
using Metropolis.Common.Models;
using Metropolis.Test.Utilities;
using NUnit.Framework;

namespace Metropolis.Test.Api.Collection.Steps.AllLanguages
{
    [TestFixture]
    public class CpdCollectionStepTest : CollectionBaseTest
    {

        [Test]
        public void HasCorrectSettings()
        {
            var step = new CpdCollectionStep(ParseType.CpdJava);
            step.Extension.Should().Be(".csv");
            step.MetricsType.Should().Be("CPD");
            step.ParseType.Should().Be(ParseType.CpdJava);
        }

        [Test]
        public void PrepareCommand_CSharp()
        {
            RunTestFor(ParseType.CpdCsharp, RepositorySourceType.CSharp, CpdCollectionStep.CsharpToken, CpdCollectionStep.CsharpThreshold);
        }

        [Test]
        public void PrepareCommand_Java()
        {
            RunTestFor(ParseType.CpdJava, RepositorySourceType.Java, CpdCollectionStep.JavaToken, CpdCollectionStep.JavaThreshold);
        }

        [Test]
        public void PrepareCommand_EMCA()
        {
            RunTestFor(ParseType.CpdEcma,RepositorySourceType.ECMA, CpdCollectionStep.EcmaScriptToken, CpdCollectionStep.EcmaScriptThreshold);
        }

        private void RunTestFor(ParseType parseType, RepositorySourceType srcType, string languageToken, int languageThreshold)
        {
            var step = new CpdCollectionStep(parseType);

            Args.RepositorySourceType = srcType;
            var command = step.PrepareCommand(Args, Result);

            command.Should().NotBeEmpty();
            command.ShouldContainText("net.sourceforge.pmd.cpd.CPD")
                   .ShouldContainText("--format csv")
                   .ShouldContainText($"--language {languageToken}")
                   .ShouldContainText($"--minimum-tokens {languageThreshold}")
                   .ShouldContainText($"--files '{Args.SourceDirectory}'")
                   .ShouldContainText($"> '{Result.MetricsFile}'");

            step.ValidateMetricResults("afile").Should().BeEmpty();
        }
    }
}
