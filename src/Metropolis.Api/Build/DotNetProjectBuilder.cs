using Metropolis.Api.Collection.PowerShell;
using Metropolis.Api.IO;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Build
{
    public class DotNetProjectBuilder : IProjectBuilder
    {
        public const string MsBuildCommand = @"&'{0}' '{1}' /p:OutDir='{2}'";
        public const string MsBuildCleanCommand = @"&'{0}' '{1}' /t:clean";
        private readonly IUserPreferences userPreferences;
        private readonly IRunPowerShell powershell;
        private readonly IFileSystem fileSystem;

        public DotNetProjectBuilder() : this(new UserPreferences(),  new RunPowerShell(), new FileSystem())
        {
        }

        public DotNetProjectBuilder(IUserPreferences userPreferences,IRunPowerShell runPowerShell, IFileSystem fileSystem)
        {
            this.userPreferences = userPreferences;
            powershell = runPowerShell;
            this.fileSystem = fileSystem;
        }

        public ProjectBuildResult Build(ProjectBuildArguments buildArgs)
        {
            fileSystem.CleanFolder(buildArgs.BuildOutputFolder);
            powershell.Invoke(GetBuildCommand(buildArgs));
            return CreateProjectBuildResult(buildArgs);
        }

        public void CleanProject(string projectFile)
        {
            powershell.Invoke(GetBuildCleanCommand(projectFile));
        }

        private string GetBuildCleanCommand(string projectFile)
        {
            return MsBuildCleanCommand.FormatWith(userPreferences.MsBuildPath, projectFile);
        }

        private string GetBuildCommand(ProjectBuildArguments buildArgs)
        {
            return MsBuildCommand.FormatWith(userPreferences.MsBuildPath, buildArgs.ProjectFile, buildArgs.BuildOutputFolder);
        }

        private ProjectBuildResult CreateProjectBuildResult(ProjectBuildArguments buildArgs)
        {
            return new ProjectBuildResult
            {
                BuildFolder = buildArgs.BuildOutputFolder,
                Artifacts = fileSystem.FindAllBinaries(buildArgs.BuildOutputFolder)
            };
        }
    }
}