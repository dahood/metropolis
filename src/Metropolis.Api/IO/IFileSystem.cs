using System.Collections.Generic;
using System.IO;
using Metropolis.Common.Models;

namespace Metropolis.Api.IO
{
    public interface IFileSystem
    {
        IEnumerable<string> GetFiles(string sourceDirectory, string filter);
        string GetFileName(string fileName);
        void CreateFolder(string targetFolder);
        IEnumerable<DriveInfo> AllDrives { get; }
        bool FileExists(string potentialPath);
        void CleanFolder(string buildOutputFolder);
        IEnumerable<FileDto> FindAllBinaries(string buildOutputFolder);
        TextReader OpenFileStream(string fileName);
        void WriteText(string path, IEnumerable<string> data);
        string ProjectBuildFolder { get; }
        string IgnoreFile { get; }
        string GetIgnoreFilePath(string projectName);
        string GetProjectBuildFolder(string projectName);
    }
}