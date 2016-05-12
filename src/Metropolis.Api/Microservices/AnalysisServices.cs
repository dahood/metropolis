using Metropolis.Api.Core.Domain;
using Metropolis.Api.Extensions;
using Metropolis.Api.Microservices.Tasks;
using Metropolis.Common.Models;

namespace Metropolis.Api.Microservices
{
    public class AnalysisServices : IAnalysisService
    {
        private readonly IMetricsTaskFactory metricsTaskFactory;
        private readonly ICodebaseService codebaseService;
        
        public AnalysisServices() : this(new MetricsTaskFactory(), new CodebaseService())
        {
        }

        public AnalysisServices(IMetricsTaskFactory metricsTaskFactory, ICodebaseService codebaseService)
        {
            this.metricsTaskFactory = metricsTaskFactory;
            this.codebaseService = codebaseService;
        }

        public CodeBase Analyze(ProjectDetails details)
        {
            var command = metricsTaskFactory.CommandFor(details.RepositorySourcetype);
            var metrics = command.Run(details.ProjectName, details.SourceDirectory, details.MetricsOutputDirectory);

            var codeBase = CodeBase.Empty();
            metrics.ForEach(x =>
            {
                var cb = codebaseService.Get(x.MetricsFile, x.ParseType);
                codeBase.Enrich(new CodeGraph(cb.AllInstances));
            });
            return codeBase;
        }
    }
}
