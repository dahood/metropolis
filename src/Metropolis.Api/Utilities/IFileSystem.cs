using System.Collections.Generic;
using System.IO;
using Metropolis.Common.Models;

namespace Metropolis.Api.Utilities
{
    public interface IFileSystem
    {
        IEnumerable<string> GetFiles(string sourceDirectory, string dll);
        string GetFileName(string targetdll);
        void CreateFolder(string metricsFolder);
        IEnumerable<DriveInfo> AllDrives { get; }
        bool FileExists(string potentialPath);
        void CleanFolder(string buildOutputFolder);
        IEnumerable<FileDto> FindAllBinaries(string buildOutputFolder);
    }
}