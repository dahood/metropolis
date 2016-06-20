using System;
using System.IO;
using Metropolis.Api.Analyzers;
using Metropolis.Api.Collection;
using Metropolis.Api.Domain;
using Metropolis.Api.IO;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services
{
    public class AnalysisServices : IAnalysisService
    {
        private readonly IAnalyzerFactory analyzerFactory;
        private readonly ICodebaseService codebaseService;
        private readonly ICollectionStepFactory collectionStepFactory;
        private readonly IFileSystem fileSystem;
        private readonly IUserPreferences userPreferences;

        public AnalysisServices()
            : this(new CollectionStepFactory(), new CodebaseService(), new AnalyzerFactory(), new FileSystem(), new UserPreferences())
        {
        }

        public AnalysisServices(ICollectionStepFactory collectionStepFactory, ICodebaseService codebaseService, IAnalyzerFactory analyzerFactory,
            IFileSystem fileSystem, IUserPreferences userPreferences)
        {
            this.collectionStepFactory = collectionStepFactory;
            this.codebaseService = codebaseService;
            this.analyzerFactory = analyzerFactory;
            this.fileSystem = fileSystem;
            this.userPreferences = userPreferences;
            fileSystem.CreateFolder(MetricsOutputFolder);
        }

        public static string MetricsOutputFolder
            => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Metropolis.Metrics");

        public string FxCopMetricsPath => userPreferences.FxCopPath;

        string IAnalysisService.MetricsOutputFolder => MetricsOutputFolder;

        public CodeBase Analyze(MetricsCommandArguments details)
        {
            var command = collectionStepFactory.GetStep(details.RepositorySourceType);
            details.MetricsOutputFolder = MetricsOutputFolder;
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
    }
}