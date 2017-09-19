using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Metropolis.Api.Collection.Steps.ECMA;
using Moq;
using Microsoft.Extensions.Logging;
using Metropolis.Common.Models;
using Metropolis.Test.Api.Collection.Steps;
using Metropolis.Api.Collection.Steps;

namespace Metropolis.Test.Api.Collection.Steps.ECMA
{
    [TestClass]
    public class EslintCollectionStepTest : CollectionBaseTest
    {
        private EsLintCollectionStep step;
        

        [TestInitialize]
        public void Setup()
        {
            
            step = new EsLintCollectionStep();
        }
        
        [TestMethod]
        public void HasCorrectSettings()
        {
            step.MetricsType.Should().Be("Eslint");
            step.Extension.Should().Be(".xml");
            step.ParseType.Should().Be(ParseType.EsLint);
        }

        [TestMethod]
        public void CanParseCommand_WithIgnoreFile()
        {
            var expected = $"{NodeModulesPath}eslint -c '{BaseCollectionStep.LocateSettings("default.eslintrc.json")}' '{Args.SourceDirectory}'"+
                           $" --no-eslintrc -o '{Result.MetricsFile}' -f checkstyle" +
                           $"  --ignore-path '{Args.IgnoreFile}'";

            var command = step.PrepareCommand(Args, Result);

            command.Should().Be(expected);
        }
    }
}