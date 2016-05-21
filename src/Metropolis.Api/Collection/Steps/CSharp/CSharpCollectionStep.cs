using System.Collections.Generic;
using System.Linq;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.CSharp
{
    public class CSharpVisualStudioCollectionStep : ICollectionStep
    {
        private readonly ICollectAssemblies assemblyCollection;
        private readonly ICSharpMetricsTask metricsTask;

        public CSharpVisualStudioCollectionStep() : this(new CollectAssemblies(), new CSharpMetricsTask())
        {
        }

        public CSharpVisualStudioCollectionStep(ICollectAssemblies assemblyCollection, ICSharpMetricsTask metricsTask)
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