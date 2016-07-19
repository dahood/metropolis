using System.Collections.Generic;
using Metropolis.Api.Build;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.CSharp
{
    public class DotNetRebuildStep : ICollectionStep
    {
        private readonly IProjectBuilder projectBuilder;

        public DotNetRebuildStep() : this(new DotNetProjectBuilder())
        {
        }

        public DotNetRebuildStep(IProjectBuilder projectBuilder)
        {
            this.projectBuilder = projectBuilder;
        }

        public IEnumerable<MetricsResult> Run(MetricsCommandArguments args)
        {
            var buildArgs = new ProjectBuildArguments(args.ProjectName, args.ProjectFile, args.RepositorySourceType, args.BuildOutputFolder);
            projectBuilder.Build(buildArgs);
            return MetricsResult.Empty();
        }
    }
}