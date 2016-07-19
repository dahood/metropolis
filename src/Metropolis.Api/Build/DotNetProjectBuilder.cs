using System;
using Metropolis.Api.Collection.PowerShell;
using Metropolis.Api.IO;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;
using NLog;

namespace Metropolis.Api.Build
{
    public class DotNetProjectBuilder : IProjectBuilder
    {
        public const string MsBuildCommand = @"&'{0}' '{1}' /t:Rebuild /p:OutDir='{2}' /maxcpucount";
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IFileSystem fileSystem;
        private readonly IRunPowerShell powershell;
        private readonly IUserPreferences userPreferences;

        public DotNetProjectBuilder() : this(new UserPreferences(), new RunPowerShell(), new FileSystem())
        {
        }

        public DotNetProjectBuilder(IUserPreferences userPreferences, IRunPowerShell runPowerShell, IFileSystem fileSystem)
        {
            this.userPreferences = userPreferences;
            powershell = runPowerShell;
            this.fileSystem = fileSystem;
        }

        public ProjectBuildResult Build(ProjectBuildArguments buildArgs)
        {
            try
            {
                Logger.Info($"Cleaning build folder: {buildArgs.BuildOutputFolder}");
                fileSystem.CleanFolder(buildArgs.BuildOutputFolder);
                var buildCommand = GetBuildCommand(buildArgs);
                Logger.Info($"MSBuild - building with this command: {buildCommand}");
                powershell.Invoke(buildCommand);
                return CreateProjectBuildResult(buildArgs);
            }
            catch (Exception e)
            {
                Logger.Error(e, "Error occurred trying to exeucte an external process");
                throw new ApplicationException("Error occurred trying to exeucte an external process", e);
            }
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