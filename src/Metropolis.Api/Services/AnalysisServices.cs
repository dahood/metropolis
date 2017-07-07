using System.IO;
using Metropolis.Api.Analyzers;
using Metropolis.Api.Collection;
using Metropolis.Api.Domain;
using Metropolis.Api.IO;
using Metropolis.Common.Models;
using Metropolis.Common.Utilities;

namespace Metropolis.Api.Services
{
    public class AnalysisServices : IAnalysisService
    {
        private readonly IAnalyzerFactory analyzerFactory;
        private readonly ICodebaseService codebaseService;
        private readonly ICollectionStepFactory collectionStepFactory;
        private readonly IYamlFileDeserializer<MetricsCommandArguments> fileDeserializer;
        private readonly IFileSystem fileSystem;

        public AnalysisServices() : this(new CollectionStepFactory(), new CodebaseService(), new AnalyzerFactory(), new FileSystem(),
            new YamlFileDeserializer<MetricsCommandArguments>())
        { }

        public AnalysisServices(ICollectionStepFactory collectionStepFactory, ICodebaseService codebaseService, IAnalyzerFactory analyzerFactory,
            IFileSystem fileSystem, IYamlFileDeserializer<MetricsCommandArguments> fileDeserializer)
        {
            this.collectionStepFactory = collectionStepFactory;
            this.codebaseService = codebaseService;
            this.analyzerFactory = analyzerFactory;
            this.fileSystem = fileSystem;
            this.fileDeserializer = fileDeserializer;
            fileSystem.CreateFolder(fileSystem.MetricsOutputFolder);
        }

        public CodeBase Analyze(MetricsCommandArguments details)
        {
            var command = collectionStepFactory.GetStep(details.RepositorySourceType);
            details.MetricsOutputFolder = fileSystem.MetricsOutputFolder;
            details.BuildOutputFolder = fileSystem.GetProjectBuildFolder(details.ProjectName);
            fileSystem.CreateFolder(details.BuildOutputFolder);

            var metricsResults = command.Run(details);

            var codeBase = CodeBase.Empty();
            foreach (var x in metricsResults)
            {
                var filename = x.MetricsFile;
                var cb = codebaseService.Get(fileSystem.OpenFileStream(filename), x.ParseType);
                codeBase.Enrich(new CodeGraph(cb.AllInstances));
            }

            var codebase = analyzerFactory.For(details.RepositorySourceType).Analyze(codeBase.AllInstances);
            codebase.SourceType = details.RepositorySourceType;
            codebase.Name = details.ProjectName;

            return codebase;
        }

        public CodeBase Analyze(string configFile, string projectFileLocation)
        {
            var commandArgument = fileDeserializer.Deserialize(configFile);
            var codeBase = Analyze(commandArgument);

            var projectResultFileName = $"{commandArgument.ProjectName}.project";
            var projectResultFile = Path.Combine(projectFileLocation, projectResultFileName);
            codebaseService.Save(codeBase, projectResultFile);

            return codeBase;
        }
    }
}