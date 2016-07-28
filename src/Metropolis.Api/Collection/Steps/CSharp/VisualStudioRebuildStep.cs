using System.Collections.Generic;
using Metropolis.Api.Build;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.CSharp
{
    public class VisualStudioRebuildStep : ICollectionStep
    {
        private readonly IProjectBuilder projectBuilder;

        public VisualStudioRebuildStep() : this(new DotNetProjectBuilder())
        {
        }

        public VisualStudioRebuildStep(IProjectBuilder projectBuilder)
        {
            this.projectBuilder = projectBuilder;
        }

        public IEnumerable<MetricsResult> Run(MetricsCommandArguments args)
        {
            var buildArgs = new ProjectBuildArguments(args.ProjectName, args.ProjectFile, args.RepositorySourceType, args.BuildOutputFolder);
            projectBuilder.Build(buildArgs);
            return MetricsResult.Empty();
        }

        public string ValidateMetricResults(string fileNametoValidate)
        {
            //TODO: validate msbuild output - but there is no file here so does this make sense???
            return string.Empty;
        }
    }
}