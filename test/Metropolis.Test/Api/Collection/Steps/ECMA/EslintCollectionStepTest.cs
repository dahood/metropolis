using System;
using FluentAssertions;
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
            var expected = $"{NodeModulesPath}eslint -c '{AppDomain.CurrentDomain.BaseDirectory}.eslintrc.json' '{Args.SourceDirectory}\\**' "+
                           $"-o '{Result.MetricsFile}' -f checkstyle" +
                           $" --ignore-path '{Args.IgnoreFile}'";

            var command = step.PrepareCommand(Args, Result);

            command.Should().Be(expected);
        }

        [Test]
        public void CanParseCommand_NoIgnoreFile()
        {
            Args.IgnoreFile = string.Empty; //no ignore path in this example

            var expected = $"{NodeModulesPath}eslint -c '{AppDomain.CurrentDomain.BaseDirectory}.eslintrc.json' '{Args.SourceDirectory}\\**' "+
                           $"-o '{Result.MetricsFile}' -f checkstyle";

            var command = step.PrepareCommand(Args, Result);

            command.Should().Be(expected);
        }
    }
}