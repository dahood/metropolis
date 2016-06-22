using System.Collections.Generic;
using Metropolis.Api.Build;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.CSharp
{
    public class DotProjectCleanStep : ICollectionStep
    {
        private readonly IProjectBuilder projectBuilder;

        public DotProjectCleanStep() : this(new DotNetProjectBuilder())
        {
        }

        public DotProjectCleanStep(DotNetProjectBuilder projectBuilder)
        {
            this.projectBuilder = projectBuilder;
        }

        public IEnumerable<MetricsResult> Run(MetricsCommandArguments args)
        {
            projectBuilder.CleanProject(args.ProjectFile);
            return MetricsResult.Empty();
        }
    }
}