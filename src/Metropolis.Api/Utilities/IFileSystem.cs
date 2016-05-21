using System.Collections.Generic;

namespace Metropolis.Api.Utilities
{
    public interface IFileSystem
    {
        IEnumerable<string> GetFiles(string sourceDirectory, string dll);
        string GetFileName(string targetdll);
    }
}