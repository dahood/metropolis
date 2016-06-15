using System.Collections.Generic;
using System.IO;

namespace Metropolis.Api.Utilities
{
    public interface IFileSystem
    {
        IEnumerable<string> GetFiles(string sourceDirectory, string dll);
        string GetFileName(string targetdll);
        void CreateFolder(string metricsFolder);
        IEnumerable<DriveInfo> AllDrives { get; }
        bool FileExists(string potentialPath);
        TextReader OpenFileStream(string fileName);
    }
}