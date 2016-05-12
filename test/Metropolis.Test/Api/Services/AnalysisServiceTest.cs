﻿using FluentAssertions;
using Metropolis.Api.Core.Domain;
using Metropolis.Api.Microservices;
using Metropolis.Api.Microservices.Tasks;
using Metropolis.Api.Microservices.Tasks.Commands;
using Metropolis.Common.Models;
using Moq;
using NUnit.Framework;

namespace Metropolis.Test.Api.Services
{
    public class AnalysisServiceTest : StrictMockBaseTest
    {
        private AnalysisServices analysisServices;
        private Mock<IMetricsTaskFactory> metricsTaskFactory;
        private Mock<ICodebaseService> codebaseService;
        private Mock<IMetricsCommand> metricsCommand;
        private ProjectDetails details;

        [SetUp]
        public void SetUp()
        {
            metricsTaskFactory = CreateMock<IMetricsTaskFactory>();
            codebaseService = CreateMock<ICodebaseService>();
            metricsCommand = CreateMock<IMetricsCommand>();

            details = new ProjectDetails
            {
                RepositorySourcetype = RepositorySourceType.Java,
                ProjectName = "test",
                SourceDirectory = @"c:\mySourceCode\JavaProject",
                MetricsOutputDirectory = @"c:\myMetricsOutput"
            };

            analysisServices = new AnalysisServices(metricsTaskFactory.Object, codebaseService.Object);
        }

        [Test]
        public void CanAnalyzeCodeBase_WithSingleMetricsResult()
        {
            var expectedMetricsFile = $"{details.MetricsOutputDirectory}\\{details.ProjectName}_CheckStyles.xml";
            var result = new MetricsResult { MetricsFile = expectedMetricsFile, ParseType = ParseType.PuppyCrawler};

            metricsTaskFactory.Setup(x => x.CommandFor(RepositorySourceType.Java)).Returns(metricsCommand.Object);
            metricsCommand.Setup(x => x.Run(details.ProjectName, details.SourceDirectory, details.MetricsOutputDirectory))
                          .Returns(new[] { result});

            codebaseService.Setup(x => x.Get(expectedMetricsFile, ParseType.PuppyCrawler)).Returns(CodeBase.Empty);

            var results = analysisServices.Analyze(details);
            results.Should().NotBeNull();
        }

        [Test]
        public void CanAnalyzeCodeBase_WithMultipleMetricsResults()
        {
            details.RepositorySourcetype = RepositorySourceType.ECMA;

            var eslintMetricsFile = $"{details.MetricsOutputDirectory}\\{details.ProjectName}_eslint.xml";
            var slocMetricsFile = $"{details.MetricsOutputDirectory}\\{details.ProjectName}_sloc.csv";

            var eslintResult = new MetricsResult { MetricsFile = eslintMetricsFile, ParseType = ParseType.EsLint};
            var slocResult = new MetricsResult { MetricsFile = slocMetricsFile, ParseType = ParseType.SlocJavaScript};

            metricsTaskFactory.Setup(x => x.CommandFor(RepositorySourceType.ECMA)).Returns(metricsCommand.Object);
            metricsCommand.Setup(x => x.Run(details.ProjectName, details.SourceDirectory, details.MetricsOutputDirectory))
                          .Returns(new[] { eslintResult, slocResult});

            codebaseService.Setup(x => x.Get(eslintMetricsFile, ParseType.EsLint)).Returns(CodeBase.Empty);
            codebaseService.Setup(x => x.Get(slocMetricsFile  , ParseType.SlocJavaScript)).Returns(CodeBase.Empty);

            var results = analysisServices.Analyze(details);
            results.Should().NotBeNull();
        }
    }
}
