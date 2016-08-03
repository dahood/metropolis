using System.Collections.Generic;
using System.IO;
using Metropolis.Api.Build;
using Metropolis.Api.IO;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.CSharp
{
    public class VisualStudioRebuildStep : ICollectionStep
    {
        private readonly IProjectBuilder projectBuilder;
        private readonly IFileSystem fileSystem;

        public VisualStudioRebuildStep() : this(new DotNetProjectBuilder(),new FileSystem())
        {
        }

        public VisualStudioRebuildStep(IProjectBuilder projectBuilder, FileSystem fileSystem)
        {
            this.projectBuilder = projectBuilder;
            this.fileSystem = fileSystem;
        }

        public IEnumerable<MetricsResult> Run(MetricsCommandArguments args)
        {
            var buildArgs = new ProjectBuildArguments(args.ProjectName, args.ProjectFile, args.RepositorySourceType, args.BuildOutputFolder);
            projectBuilder.Build(buildArgs);
            CopyIgnoreFileToSandboxFolder(args);
            return MetricsResult.Empty();
        }

        private void CopyIgnoreFileToSandboxFolder(MetricsCommandArguments args)
        {
            var source = Path.Combine(args.ProjectFolder, Path.GetFileName(args.IgnoreFile));
            fileSystem.Copy(source, args.IgnoreFile);
        }

        public string ValidateMetricResults(string fileNametoValidate)
        {
            //TODO: validate msbuild output - but there is no file here so does this make sense???
            return string.Empty;
        }
    }
}