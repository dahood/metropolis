using System.Collections.Generic;
using System.Linq;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps
{
    public class CompositeCollectionStep : ICollectionStep
    {
        public readonly IEnumerable<ICollectionStep> Commands;
        private readonly bool runParallel;

        public CompositeCollectionStep(IEnumerable<ICollectionStep> commands, bool runParallel = true)
        {
            Commands = commands;
            this.runParallel = runParallel;
        }

        public IEnumerable<MetricsResult> Run(MetricsCommandArguments args)
        {
            return runParallel ? RunInParallel(args) : Commands.SelectMany(each => each.Run(args));
        }

        private IEnumerable<MetricsResult> RunInParallel(MetricsCommandArguments args)
        {
            var results = new List<MetricsResult>();
            Commands.AsParallel().ForAll(x => results.AddRange(x.Run(args)));
            return results;
        }
    }
}