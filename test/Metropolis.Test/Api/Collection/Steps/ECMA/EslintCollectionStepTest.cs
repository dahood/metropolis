using FluentAssertions;
using Metropolis.Api.Collection.Steps;
using Metropolis.Api.Collection.Steps.ECMA;
using Metropolis.Common.Models;
using NUnit.Framework;

namespace Metropolis.Test.Api.Collection.Steps.ECMA
{
    [TestFixture]
    public class EslintCollectionStepTest : CollectionBaseTest
    {
        private EsLintCollectionStep step;

        [SetUp]
        public void BeforeEachTest()
        {
            step = new EsLintCollectionStep();
        }
        
        [Test]
        public void HasCorrectSettings()
        {
            step.MetricsType.Should().Be("Eslint");
            step.Extension.Should().Be(".xml");
            step.ParseType.Should().Be(ParseType.EsLint);
        }

        [Test]
        public void CanParseCommand_WithIgnoreFile()
        {
            var expected = $"{NodeModulesPath}eslint -c '{BaseCollectionStep.LocateSettings("default.eslintrc.json")}' '{Args.SourceDirectory}'"+
                           $" --no-eslintrc -o '{Result.MetricsFile}' -f checkstyle" +
                           $"  --ignore-path '{Args.IgnoreFile}'";

            var command = step.PrepareCommand(Args, Result);

            command.Should().Be(expected);
        }

        [Test]
        public void CanParseCommand_NoIgnoreFile()
        {
            Args.IgnoreFile = string.Empty; //no ignore path in this example

            var expected = $"{NodeModulesPath}eslint -c '{BaseCollectionStep.LocateSettings("default.eslintrc.json")}' '{Args.SourceDirectory}'"+
                           $" --no-eslintrc -o '{Result.MetricsFile}' -f checkstyle ";

            var command = step.PrepareCommand(Args, Result);

            command.Should().Be(expected);
        }

        [Test]
        public void ShouldAddJsx_WithReact()
        {
            Args.IgnoreFile = string.Empty; //no ignore path in this example
            Args.EcmaScriptDialect = EslintPasringOptions.REACT;

            var expected = $"{NodeModulesPath}eslint -c '{BaseCollectionStep.LocateSettings("react.eslintrc.json")}' '{Args.SourceDirectory}'" +
                           $" --no-eslintrc -o '{Result.MetricsFile}' -f checkstyle  --ext .js,.jsx";

            var command = step.PrepareCommand(Args, Result);

            command.Should().Be(expected);
        }

        [Test]
        public void ShouldPickupBabelReactConfig_WithBabelReact()
        {
            Args.IgnoreFile = string.Empty; //no ignore path in this example
            Args.EcmaScriptDialect = EslintPasringOptions.BABEL_REACT;

            var expected = $"{NodeModulesPath}eslint -c '{BaseCollectionStep.LocateSettings("babel_react.eslintrc.json")}' '{Args.SourceDirectory}'" +
                           $" --no-eslintrc -o '{Result.MetricsFile}' -f checkstyle  --ext .js,.jsx";

            var command = step.PrepareCommand(Args, Result);

            command.Should().Be(expected);
        }
    }
}