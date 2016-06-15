using System.IO;
using FluentAssertions;
using Metropolis.Api.Analyzers;
using Metropolis.Api.Collection;
using Metropolis.Api.Collection.Steps;
using Metropolis.Api.Collection.Steps.CSharp;
using Metropolis.Api.Domain;
using Metropolis.Api.Services;
using Metropolis.Api.Utilities;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;
using Moq;
using NUnit.Framework;

namespace Metropolis.Test.Api.Services
{
    public class AnalysisServiceTest : StrictMockBaseTest
    {
        private AnalysisServices analysisServices;
        private Mock<ICollectionStepFactory> metricsTaskFactory;
        private Mock<ICodebaseService> codebaseService;
        private Mock<ICollectionStep> metricsCommand;
        private MetricsCommandArguments details;
        private Mock<IAnalyzerFactory> analyzerFactory;
        private Mock<ICodebaseAnalyzer> analyzer;
        private Mock<IFileSystem> fileSystem;
        private Mock<ILocateFxCopMetricsTool> locateFxCopMetricsTool;

        [SetUp]
        public void SetUp()
        {
            metricsTaskFactory = CreateMock<ICollectionStepFactory>();
            codebaseService = CreateMock<ICodebaseService>();
            metricsCommand = CreateMock<ICollectionStep>();
            analyzerFactory = CreateMock<IAnalyzerFactory>();
            analyzer = CreateMock<ICodebaseAnalyzer>();
            fileSystem = CreateMock<IFileSystem>();
            locateFxCopMetricsTool = CreateMock<ILocateFxCopMetricsTool>();

            details = new MetricsCommandArguments
            {
                RepositorySourceType = RepositorySourceType.Java,
                ProjectName = "test",
                SourceDirectory = @"c:\mySourceCode\JavaProject",
                MetricsOutputFolder = @"c:\myMetricsOutput"
            };

            fileSystem.Setup(x => x.CreateFolder(AnalysisServices.MetricsOutputFolder));

            analysisServices = new AnalysisServices(metricsTaskFactory.Object, codebaseService.Object, analyzerFactory.Object, fileSystem.Object, locateFxCopMetricsTool.Object);
        }

        [Test]
        public void CanAnalyzeCodeBase_WithSingleMetricsResult()
        {
            var expectedMetricsFile = $"{details.MetricsOutputFolder}\\{details.ProjectName}_CheckStyles.xml";
            var result = new MetricsResult { MetricsFile = expectedMetricsFile, ParseType = ParseType.PuppyCrawler};

            metricsTaskFactory.Setup(x => x.GetStep(RepositorySourceType.Java)).Returns(metricsCommand.Object);
            metricsCommand.Setup(x => x.Run(details)).Returns(new[] { result});

            var stringReader = new StringReader("foo");
            fileSystem.Setup(x => x.OpenFileStream(expectedMetricsFile)).Returns(stringReader);

            codebaseService.Setup(x => x.Get(stringReader, ParseType.PuppyCrawler)).Returns(CodeBase.Empty);
            analyzerFactory.Setup(x => x.For(RepositorySourceType.Java)).Returns(analyzer.Object);
            analyzer.Setup(x => x.Analyze(CodeBase.Empty().AllInstances)).Returns(CodeBase.Empty);

            var results = analysisServices.Analyze(details);
            results.Should().NotBeNull();
        }

        [Test]
        public void CanAnalyzeCodeBase_WithMultipleMetricsResults()
        {
            details.RepositorySourceType = RepositorySourceType.ECMA;

            var eslintMetricsFile = $"{details.MetricsOutputFolder}\\{details.ProjectName}_eslint.xml";
            var slocMetricsFile = $"{details.MetricsOutputFolder}\\{details.ProjectName}_sloc.csv";

            var eslintResult = new MetricsResult { MetricsFile = eslintMetricsFile, ParseType = ParseType.EsLint};
            var slocResult = new MetricsResult { MetricsFile = slocMetricsFile, ParseType = ParseType.SlocEcma};

            metricsTaskFactory.Setup(x => x.GetStep(RepositorySourceType.ECMA)).Returns(metricsCommand.Object);
            metricsCommand.Setup(x => x.Run(details)).Returns(new[] { eslintResult, slocResult});

            var stringReader = new StringReader("foo");
            fileSystem.Setup(x => x.OpenFileStream(eslintMetricsFile)).Returns(stringReader);
            fileSystem.Setup(x => x.OpenFileStream(slocMetricsFile)).Returns(stringReader);

            codebaseService.Setup(x => x.Get(stringReader, ParseType.EsLint)).Returns(CodeBase.Empty);
            codebaseService.Setup(x => x.Get(stringReader, ParseType.SlocEcma)).Returns(CodeBase.Empty);
            analyzerFactory.Setup(x => x.For(RepositorySourceType.ECMA)).Returns(analyzer.Object);
            analyzer.Setup(x => x.Analyze(CodeBase.Empty().AllInstances)).Returns(CodeBase.Empty);

            var results = analysisServices.Analyze(details);
            results.Should().NotBeNull();
        }
    }
}
