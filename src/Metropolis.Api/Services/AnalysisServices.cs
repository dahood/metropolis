using Metropolis.Api.Analyzers;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;
using Metropolis.Api.Services.Collection;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services
{
    public class AnalysisServices : IAnalysisService
    {
        private readonly IMetricsStepFactory metricsStepFactory;
        private readonly IAnalyzerFactory analyzerFactory;
        private readonly ICodebaseService codebaseService;
        
        public AnalysisServices() : this(new MetricsStepFactory(), new CodebaseService(), new AnalyzerFactory())
        {
        }

        public AnalysisServices(IMetricsStepFactory metricsStepFactory, ICodebaseService codebaseService, IAnalyzerFactory analyzerFactory)
        {
            this.metricsStepFactory = metricsStepFactory;
            this.codebaseService = codebaseService;
            this.analyzerFactory = analyzerFactory;
        }

        public CodeBase Analyze(MetricsCommandArguments details)
        {
            var command = metricsStepFactory.CommandFor(details.RepositorySourceType);
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
