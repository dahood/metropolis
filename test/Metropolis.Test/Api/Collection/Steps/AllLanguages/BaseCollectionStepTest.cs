using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Metropolis.Common.Models;
using Metropolis.Test.TestHelpers;
using Metropolis.Test.Utilities;
using NUnit.Framework;

namespace Metropolis.Test.Api.Collection.Steps.AllLanguages
{
    [TestFixture]
    public class BaseCollectionStepTest 
    {
        private CollectionStepForTesting step;
        private MetricsCommandArguments args;
        private IEnumerable<MetricsResult> results;
        private string expectedCommandFile;

        [SetUp]
        public void BeforeEachTest()
        {
            step = new CollectionStepForTesting();
            
            args = new MetricsCommandArguments
            {
                ProjectName = "Test", IgnoreFile = null, MetricsOutputDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}",
                RepositorySourceType  = RepositorySourceType.CSharp, SourceDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}"
            };
            expectedCommandFile = $"{args.MetricsOutputDirectory}\\{args.ProjectName}_{step.MetricsType}_command.ps1";

            expectedCommandFile.RemoveFileIfExists();
        }

        [TearDown]
        public void TearDown()
        {
            expectedCommandFile.RemoveFileIfExists();
        }

        [Test]
        public void CanRunStep()
        {
            results = step.Run(args);

            Validate.Begin().IsNotNull(results, "results are not null").Check()
                            .IsEqual(results.Count(), 1, "Has one result").Check();

            var result = results.First();

            result.MetricsFile.Should().NotBeNullOrEmpty();
            result.ParseType.Should().Be(ParseType.VisualStudio);

            File.Exists(expectedCommandFile).Should().BeTrue();
            File.Exists(result.MetricsFile).Should().BeTrue();
        }

        [Test]
        public void Reports_Bad_Command()
        {
            step.RunFailingCommand();
            Assert.Throws<ApplicationException>(() => results = step.Run(args));
        }
    }
}
