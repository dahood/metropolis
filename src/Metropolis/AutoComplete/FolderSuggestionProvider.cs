using System.Collections.Generic;
using System.IO;

namespace Metropolis.AutoComplete
{
    public class FolderSuggestionProvider : FileSystemSuggestionProvider
    {
        protected override IEnumerable<FileSystemInfo> GetSuggestions(DirectoryInfo dirInfo, string filter)
        {
            return dirInfo.Exists ? dirInfo.GetDirectories(filter) : new FileSystemInfo[0];
        }
    }
}