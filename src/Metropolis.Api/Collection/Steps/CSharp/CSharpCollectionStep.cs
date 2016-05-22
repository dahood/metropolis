using System.Collections.Generic;
using System.Linq;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.CSharp
{
    public class VisualStudioPowerToolsFxCopCollectionStep : ICollectionStep
    {
        private readonly ICollectAssemblies assemblyCollection;
        private readonly ICSharpMetricsTask metricsTask;

        public VisualStudioPowerToolsFxCopCollectionStep() : this(new CollectAssemblies(), new CSharpMetricsTask())
        {
        }

        public VisualStudioPowerToolsFxCopCollectionStep(ICollectAssemblies assemblyCollection, ICSharpMetricsTask metricsTask)
        {
            this.assemblyCollection = assemblyCollection;
            this.metricsTask = metricsTask;
        }
        
        public IEnumerable<MetricsResult> Run(MetricsCommandArguments args)
        {
            return assemblyCollection.GatherAssemblies(args).Select(each => metricsTask.Run(args, each)).ToList();
        }
    }
}