using Metropolis.Api.Collection.PowerShell;
using Metropolis.Api.Collection.Steps.CSharp;
using Metropolis.Api.IO;
using Metropolis.Api.Utilities;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Build
{
    public class DotNetProjectBuilder : IProjectBuilder
    {
        private const string MsBuildCommand = @"&'{0}' '{1}' /p:OutDir='{2}'";
        private readonly IBuildEnvironment buildEnvironment;
        private readonly IRunPowerShell powershell;
        private readonly IFileSystem fileSystem;

        public DotNetProjectBuilder() : this(new BuildEnvironment(),  new RunPowerShell(), new FileSystem())
        {
        }

        private DotNetProjectBuilder(IBuildEnvironment buildEnvironment,IRunPowerShell runPowerShell, IFileSystem fileSystem)
        {
            this.buildEnvironment = buildEnvironment;
            powershell = runPowerShell;
            this.fileSystem = fileSystem;
        }

        public ProjectBuildResult Build(ProjectBuildArguments buildArgs)
        {
            fileSystem.CleanFolder(buildArgs.BuildOutputFolder);
            var buildCommand = MsBuildCommand.FormatWith(buildEnvironment.MsBuildPath, buildArgs.ProjetFile, buildArgs.BuildOutputFolder);
            powershell.Invoke(buildCommand);
            return new ProjectBuildResult
            {
                BuildFolder = buildArgs.BuildOutputFolder,
                Artifacts = fileSystem.FindAllBinaries(buildArgs.BuildOutputFolder)
            };
        }
    }
}