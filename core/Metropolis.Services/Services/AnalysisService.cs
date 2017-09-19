using Microsoft.Extensions.Logging;
using Metropolis.Common.Models;
using Metropolis.Common.Utilities;
using Metropolis.Api.IO;
using Metropolis.Api.Collection;
using Metropolis.Api.Domain;
using Metropolis.Api.Analyzers;
using System.IO;


namespace Metropolis.Api.Services
{
    public class AnalysisServicesCore : IAnalysisServiceCore
    {
        private readonly IYamlFileDeserializer<MetricsCommandArguments> fileDeserializer;
        private readonly IFileSystem fileSystem;
        private readonly ICodebaseService codebaseService;
        private readonly ICollectionStepFactory collectionStepFactory;
        private readonly ILogger Logger;
        private readonly IAnalyzerFactory analyzerFactory;
        public AnalysisServicesCore( 
        IYamlFileDeserializer<MetricsCommandArguments> fileDeserializer,
        IFileSystem fileSystem,
        ICollectionStepFactory collectionStepFactory,
        ICodebaseService codebaseService,
        IAnalyzerFactory analyzerFactory)
        {
            this.fileDeserializer = fileDeserializer;
            this.fileSystem = fileSystem;
            this.collectionStepFactory = collectionStepFactory;
            this.codebaseService = codebaseService;
            this.Logger = LogManager.GetCurrentClassLogger("AnalysisServices");
            this.analyzerFactory = analyzerFactory;
        }

        public AnalysisServicesCore(): this(
            new YamlFileDeserializer<MetricsCommandArguments>(),
            new FileSystem(),
            new CollectionStepFactory(),
            new CodebaseService(),
            new AnalyzerFactory()) {}
        public CodeBase Analyze(string configFile, string projectFileLocation)
        {
            var commandArgument = fileDeserializer.Deserialize(configFile);
            var codebase = Analyze(commandArgument);

            var projectResultFileName = $"{commandArgument.ProjectName}.project";
            var projectResultFile = Path.Combine(projectFileLocation, projectResultFileName);
            codebaseService.Save(codebase, projectResultFile);

            return codebase;
        }

        public CodeBase Analyze(MetricsCommandArguments details)
        {
            details.MetricsOutputFolder = fileSystem.MetricsOutputFolder;
            details.BuildOutputFolder = fileSystem.GetProjectBuildFolder(details.ProjectName);
            fileSystem.CreateFolder(details.BuildOutputFolder);
            var command = collectionStepFactory.GetStep(details.RepositorySourceType);
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
            return codeBase;
        }

    }
}
