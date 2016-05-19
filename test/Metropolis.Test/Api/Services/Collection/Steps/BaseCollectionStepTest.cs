using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Metropolis.Api.Collection.Steps;
using Metropolis.Common.Models;
using Metropolis.Test.TestHelpers;
using Metropolis.Test.Utilities;
using NUnit.Framework;

namespace Metropolis.Test.Api.Services.Collection.Steps
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
                ProjectName = "Test", IgnorePath = null, MetricsOutputDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}",
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

    public class CollectionStepForTesting : BaseCollectionStep
    {
        private bool runBadCommand;

        public CollectionStepForTesting() : base(false)
        {
        }

        public override string MetricsType => "Test";
        public override string Extension => ".xml";
        public override ParseType ParseType => ParseType.VisualStudio;
        public override string PrepareCommand(MetricsCommandArguments args, MetricsResult result)
        {
            return runBadCommand? "this should fail" : $"dir | Out-File '{result.MetricsFile}'";
        }

        public void RunFailingCommand()
        {
            runBadCommand = true;
        }
    }
}
