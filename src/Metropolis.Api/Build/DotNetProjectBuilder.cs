using Metropolis.Api.Collection.PowerShell;
using Metropolis.Api.Collection.Steps.CSharp;
using Metropolis.Api.Utilities;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Build
{
    public class DotNetProjectBuilder : IProjectBuilder
    {
        private const string MsBuildCommand = @"&'{0}' '{1}' /p:OutDir='{2}'";
        private readonly IDotNetEnvironment dotNetEnvironment;
        private readonly IRunPowerShell powershell;
        private readonly IFileSystem fileSystem;

        public DotNetProjectBuilder() : this(new DotNetEnvironment(),  new RunPowerShell(), new FileSystem())
        {
        }

        private DotNetProjectBuilder(IDotNetEnvironment dotNetEnvironment,IRunPowerShell runPowerShell, IFileSystem fileSystem)
        {
            this.dotNetEnvironment = dotNetEnvironment;
            powershell = runPowerShell;
            this.fileSystem = fileSystem;
        }

        public ProjectBuildResult Build(ProjectBuildArguments buildArgs)
        {
            fileSystem.CleanFolder(buildArgs.BuildOutputFolder);
            var buildCommand = MsBuildCommand.FormatWith(dotNetEnvironment.MsBuildPath, buildArgs.ProjetFile, buildArgs.BuildOutputFolder);
            powershell.Invoke(buildCommand);
            return new ProjectBuildResult
            {
                BuildFolder = buildArgs.BuildOutputFolder,
                Artifacts = fileSystem.FindAllBinaries(buildArgs.BuildOutputFolder)
            };
        }
    }
}