using FluentAssertions;
using Metropolis.Api.Analyzers;
using Metropolis.Api.Domain;
using Metropolis.Api.Services;
using Metropolis.Api.Services.Collection;
using Metropolis.Api.Services.Collection.Steps;
using Metropolis.Common.Models;
using Moq;
using NUnit.Framework;

namespace Metropolis.Test.Api.Services
{
    public class AnalysisServiceTest : StrictMockBaseTest
    {
        private AnalysisServices analysisServices;
        private Mock<IMetricsStepFactory> metricsTaskFactory;
        private Mock<ICodebaseService> codebaseService;
        private Mock<ICollectionStep> metricsCommand;
        private MetricsCommandArguments details;
        private Mock<IAnalyzerFactory> analyzerFactory;
        private Mock<ICodebaseAnalyzer> analyzer;

        [SetUp]
        public void SetUp()
        {
            metricsTaskFactory = CreateMock<IMetricsStepFactory>();
            codebaseService = CreateMock<ICodebaseService>();
            metricsCommand = CreateMock<ICollectionStep>();
            analyzerFactory = CreateMock<IAnalyzerFactory>();
            analyzer = CreateMock<ICodebaseAnalyzer>();

            details = new MetricsCommandArguments
            {
                RepositorySourceType = RepositorySourceType.Java,
                ProjectName = "test",
                SourceDirectory = @"c:\mySourceCode\JavaProject",
                MetricsOutputDirectory = @"c:\myMetricsOutput"
            };

            analysisServices = new AnalysisServices(metricsTaskFactory.Object, codebaseService.Object, analyzerFactory.Object);
        }

        [Test]
        public void CanAnalyzeCodeBase_WithSingleMetricsResult()
        {
            var expectedMetricsFile = $"{details.MetricsOutputDirectory}\\{details.ProjectName}_CheckStyles.xml";
            var result = new MetricsResult { MetricsFile = expectedMetricsFile, ParseType = ParseType.PuppyCrawler};

            metricsTaskFactory.Setup(x => x.GetStep(RepositorySourceType.Java)).Returns(metricsCommand.Object);
            metricsCommand.Setup(x => x.Run(details)).Returns(new[] { result});

            codebaseService.Setup(x => x.Get(expectedMetricsFile, ParseType.PuppyCrawler)).Returns(CodeBase.Empty);
            analyzerFactory.Setup(x => x.For(RepositorySourceType.Java)).Returns(analyzer.Object);
            analyzer.Setup(x => x.Analyze(CodeBase.Empty().AllInstances)).Returns(CodeBase.Empty);

            var results = analysisServices.Analyze(details);
            results.Should().NotBeNull();
        }

        [Test]
        public void CanAnalyzeCodeBase_WithMultipleMetricsResults()
        {
            details.RepositorySourceType = RepositorySourceType.ECMA;

            var eslintMetricsFile = $"{details.MetricsOutputDirectory}\\{details.ProjectName}_eslint.xml";
            var slocMetricsFile = $"{details.MetricsOutputDirectory}\\{details.ProjectName}_sloc.csv";

            var eslintResult = new MetricsResult { MetricsFile = eslintMetricsFile, ParseType = ParseType.EsLint};
            var slocResult = new MetricsResult { MetricsFile = slocMetricsFile, ParseType = ParseType.SlocEcma};

            metricsTaskFactory.Setup(x => x.GetStep(RepositorySourceType.ECMA)).Returns(metricsCommand.Object);
            metricsCommand.Setup(x => x.Run(details)).Returns(new[] { eslintResult, slocResult});

            codebaseService.Setup(x => x.Get(eslintMetricsFile, ParseType.EsLint)).Returns(CodeBase.Empty);
            codebaseService.Setup(x => x.Get(slocMetricsFile  , ParseType.SlocEcma)).Returns(CodeBase.Empty);
            analyzerFactory.Setup(x => x.For(RepositorySourceType.ECMA)).Returns(analyzer.Object);
            analyzer.Setup(x => x.Analyze(CodeBase.Empty().AllInstances)).Returns(CodeBase.Empty);

            var results = analysisServices.Analyze(details);
            results.Should().NotBeNull();
        }
    }
}
