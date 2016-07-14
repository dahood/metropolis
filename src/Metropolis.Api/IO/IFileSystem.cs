using System.Collections.Generic;
using System.IO;
using Metropolis.Common.Models;

namespace Metropolis.Api.IO
{
    public interface IFileSystem
    {
        IEnumerable<DriveInfo> AllDrives { get; }
        string ProjectBuildFolder { get; }
        string AutoSaveFolder { get; }
        string IgnoreFile { get; }
        string MetricsOutputFolder { get; }
        string ScreenShotFolder { get; }
        IEnumerable<string> GetFiles(string sourceDirectory, string filter);
        string GetFileName(string fileName);
        void CreateFolder(string targetFolder);
        bool FileExists(string potentialPath);
        void CleanFolder(string buildOutputFolder);
        IEnumerable<FileDto> FindAllBinaries(string buildOutputFolder);
        TextReader OpenFileStream(string fileName);
        void WriteText(string path, IEnumerable<string> data);
        string GetIgnoreFilePath(string projectName);
        string GetProjectBuildFolder(string projectName);
        IEnumerable<string> ReadIgnoreFile(string projectName);
        string ReadFile(string physicalFilePath);
        void EnsureDirectoriesExist(params string[] autoSaveFolder);
        void CreateMetropolisSpecialFolders();
        IEnumerable<FileInfo> GetAutloadProjects();
    }
}