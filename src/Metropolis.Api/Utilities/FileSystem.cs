using System.Collections.Generic;
using System.IO;

namespace Metropolis.Api.Utilities
{
    public class FileSystem : IFileSystem
    {
        public IEnumerable<string> GetFiles(string sourceDirectory, string filter)
        {
            return Directory.GetFiles(sourceDirectory, filter);
        }

        public string GetFileName(string targetdll)
        {
            return Path.GetFileName(targetdll);
        }
    }
}