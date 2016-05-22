using System.Collections.Generic;
using System.Linq;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.CSharp
{
    public class VisualStudioCollectionSteps : ICollectionStep
    {
        private readonly ICollectAssemblies assemblyCollection;
        private readonly IFxCopCollectionTask metricsTask;

        public VisualStudioCollectionSteps() : this(new CollectAssemblies(), new FxCopCollectionTask())
        {
        }

        public VisualStudioCollectionSteps(ICollectAssemblies assemblyCollection, IFxCopCollectionTask metricsTask)
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