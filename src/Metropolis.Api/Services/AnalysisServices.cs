using Metropolis.Api.Analyzers;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;
using Metropolis.Api.Services.Tasks;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services
{
    public class AnalysisServices : IAnalysisService
    {
        private readonly IMetricsTaskFactory metricsTaskFactory;
        private readonly IAnalyzerFactory analyzerFactory;
        private readonly ICodebaseService codebaseService;
        
        public AnalysisServices() : this(new MetricsTaskFactory(), new CodebaseService(), new AnalyzerFactory())
        {
        }

        public AnalysisServices(IMetricsTaskFactory metricsTaskFactory, ICodebaseService codebaseService, IAnalyzerFactory analyzerFactory)
        {
            this.metricsTaskFactory = metricsTaskFactory;
            this.codebaseService = codebaseService;
            this.analyzerFactory = analyzerFactory;
        }

        public CodeBase Analyze(MetricsCommandArguments details)
        {
            var command = metricsTaskFactory.CommandFor(details.RepositorySourceType);
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
