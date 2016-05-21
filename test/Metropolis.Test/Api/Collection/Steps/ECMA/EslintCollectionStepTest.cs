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
            var expected = $"eslint -c '{AppDomain.CurrentDomain.BaseDirectory}.eslintrc.json' '{Args.SourceDirectory}\\**' "+
                           $"-o '{Result.MetricsFile}' -f checkstyle" +
                           $" --ignore-path '{Args.IgnorePath}'";

            var command = step.PrepareCommand(Args, Result);

            command.Should().Be(expected);
        }

        [Test]
        public void CanParseCommand_NoIgnoreFile()
        {
            Args.IgnorePath = string.Empty; //no ignore path in this example

            var expected = $"eslint -c '{AppDomain.CurrentDomain.BaseDirectory}.eslintrc.json' '{Args.SourceDirectory}\\**' "+
                           $"-o '{Result.MetricsFile}' -f checkstyle";

            var command = step.PrepareCommand(Args, Result);

            command.Should().Be(expected);
        }
    }
}