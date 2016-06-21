using System.Collections.Generic;
using System.Linq;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.CSharp
{
    public class VisualStudioCollectionStep : ICollectionStep
    {
        private readonly ICollectAssemblies assemblyCollection;
        private readonly IFxCopCollectionTask metricsTask;

        public VisualStudioCollectionStep() : this(new CollectAssemblies(), new FxCopCollectionTask())
        {
        }

        public VisualStudioCollectionStep(ICollectAssemblies assemblyCollection, IFxCopCollectionTask metricsTask)
        {
            this.assemblyCollection = assemblyCollection;
            this.metricsTask = metricsTask;
        }
        
        public IEnumerable<MetricsResult> Run(MetricsCommandArguments args)
        {
            var results = new List<MetricsResult>();
            assemblyCollection.GatherAssemblies(args).AsParallel().ForAll(x => results.Add(metricsTask.Run(args, x)));
            return results;
        }
    }
}