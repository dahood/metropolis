using System.IO;
using FluentAssertions;
using Metropolis.Api.Analyzers;
using Metropolis.Api.Collection;
using Metropolis.Api.Collection.Steps;
using Metropolis.Api.Domain;
using Metropolis.Api.IO;
using Metropolis.Api.Services;
using Metropolis.Common.Models;
using Metropolis.Common.Utilities;
using Moq;
using NUnit.Framework;

namespace Metropolis.Test.Api.Services
{
    public class AnalysisServiceTest : StrictMockBaseTest
    {
        private AnalysisServices analysisServices;
        private Mock<ICodebaseAnalyzer> analyzer;
        private Mock<IAnalyzerFactory> analyzerFactory;
        private Mock<ICodebaseService> codebaseService;
        private MetricsCommandArguments details;
        private Mock<IYamlFileDeserializer<MetricsCommandArguments>> fileDeserializer;
        private Mock<IFileSystem> fileSystem;
        private Mock<ICollectionStep> metricsCommand;
        private Mock<ICollectionStepFactory> metricsTaskFactory;
        private const string ProjectPath = "ResultsGoHere";
        private string projectFile;

        [SetUp]
        public void SetUp()
        {
            metricsTaskFactory = CreateMock<ICollectionStepFactory>();
            codebaseService = CreateMock<ICodebaseService>();
            metricsCommand = CreateMock<ICollectionStep>();
            analyzerFactory = CreateMock<IAnalyzerFactory>();
            analyzer = CreateMock<ICodebaseAnalyzer>();
            fileSystem = CreateMock<IFileSystem>();
            fileDeserializer = CreateMock<IYamlFileDeserializer<MetricsCommandArguments>>();

            details = new MetricsCommandArguments
            {
                RepositorySourceType = RepositorySourceType.Java,
                ProjectName = "test",
                SourceDirectory = @"c:\mySourceCode\JavaProject",
                MetricsOutputFolder = @"c:\myMetricsOutput",
                BuildOutputFolder = @"C:\roaming\buildoutput\test",
            };

            projectFile = $@"{ProjectPath}\{details.ProjectName}.project";
            fileSystem.Setup(x => x.MetricsOutputFolder).Returns(@"C:\roaming\metrics");
            fileSystem.Setup(x => x.CreateFolder(fileSystem.Object.MetricsOutputFolder));

            analysisServices = new AnalysisServices(metricsTaskFactory.Object, codebaseService.Object, analyzerFactory.Object, fileSystem.Object,
                fileDeserializer.Object);
        }

        [Test]
        public void CanAnalyzeCodeBase_WithSingleMetricsResult()
        {
            SetupSingleMetricAnalysis();

            var results = analysisServices.Analyze(details);
            results.Should().NotBeNull();
        }

        [Test]
        public void CanAnalyzeCodeBase_WithMultipleMetricsResults()
        {
            details.RepositorySourceType = RepositorySourceType.ECMA;

            var eslintMetricsFile = $"{details.MetricsOutputFolder}\\{details.ProjectName}_eslint.xml";
            var slocMetricsFile = $"{details.MetricsOutputFolder}\\{details.ProjectName}_sloc.csv";
            fileSystem.Setup(x => x.GetProjectBuildFolder(details.ProjectName)).Returns(details.BuildOutputFolder);
            fileSystem.Setup(x => x.CreateFolder(details.BuildOutputFolder));

            var eslintResult = new MetricsResult { MetricsFile = eslintMetricsFile, ParseType = ParseType.EsLint };
            var slocResult = new MetricsResult { MetricsFile = slocMetricsFile, ParseType = ParseType.SlocEcma };

            metricsTaskFactory.Setup(x => x.GetStep(RepositorySourceType.ECMA)).Returns(metricsCommand.Object);
            metricsCommand.Setup(x => x.Run(details)).Returns(new[] { eslintResult, slocResult });

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

        [Test]
        public void ShouldAnalyzeCodeFromExistingCommandFile()
        {
            var filename = "fileName.yml";
            fileDeserializer.Setup(x => x.Deserialize(filename)).Returns(details);

            var codeBase = SetupSingleMetricAnalysis();
            codebaseService.Setup(x => x.Save(codeBase, projectFile));

            var codebase = analysisServices.Analyze(filename, ProjectPath);
            codebase.Name.Should().Be(details.ProjectName);
        }

  
        private CodeBase SetupSingleMetricAnalysis()
        {
            var codeBase = CodeBase.Empty();

            var expectedMetricsFile = $"{details.MetricsOutputFolder}\\{details.ProjectName}_CheckStyles.xml";
            var result = new MetricsResult { MetricsFile = expectedMetricsFile, ParseType = ParseType.PuppyCrawler };
            fileSystem.Setup(x => x.GetProjectBuildFolder(details.ProjectName)).Returns(details.BuildOutputFolder);
            fileSystem.Setup(x => x.CreateFolder(details.BuildOutputFolder));

            metricsTaskFactory.Setup(x => x.GetStep(RepositorySourceType.Java)).Returns(metricsCommand.Object);
            metricsCommand.Setup(x => x.Run(details)).Returns(new[] { result });

            var stringReader = new StringReader("foo");
            fileSystem.Setup(x => x.OpenFileStream(expectedMetricsFile)).Returns(stringReader);

            codebaseService.Setup(x => x.Get(stringReader, ParseType.PuppyCrawler)).Returns(CodeBase.Empty);
            analyzerFactory.Setup(x => x.For(RepositorySourceType.Java)).Returns(analyzer.Object);
            analyzer.Setup(x => x.Analyze(codeBase.AllInstances)).Returns(codeBase);

            return codeBase;
        }

    }
}