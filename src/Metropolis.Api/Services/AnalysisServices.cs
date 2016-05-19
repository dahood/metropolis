using Metropolis.Api.Analyzers;
using Metropolis.Api.Collection;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services
{
    public class AnalysisServices : IAnalysisService
    {
        private readonly ICollectionStepFactory collectionStepFactory;
        private readonly IAnalyzerFactory analyzerFactory;
        private readonly ICodebaseService codebaseService;
        
        public AnalysisServices() : this(new CollectionStepFactory(), new CodebaseService(), new AnalyzerFactory())
        {
        }

        public AnalysisServices(ICollectionStepFactory collectionStepFactory, ICodebaseService codebaseService, IAnalyzerFactory analyzerFactory)
        {
            this.collectionStepFactory = collectionStepFactory;
            this.codebaseService = codebaseService;
            this.analyzerFactory = analyzerFactory;
        }

        public CodeBase Analyze(MetricsCommandArguments details)
        {
            var command = collectionStepFactory.GetStep(details.RepositorySourceType);
            var metrics = command.Run(details);

            var codeBase = CodeBase.Empty();
            metrics.ForEach(x =>
            {
                var cb = codebaseService.Get(x.MetricsFile, x.ParseType);
                codeBase.Enrich(new CodeGraph(cb.AllInstances));
            });

            return analyzerFactory.For(details.RepositorySourceType).Analyze(codeBase.AllInstances);
        }
    }
}
