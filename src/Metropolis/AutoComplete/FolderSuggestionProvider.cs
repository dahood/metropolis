using System.Collections.Generic;
using System.IO;
using WpfControls;

namespace Metropolis.AutoComplete
{
    public class FolderSuggestionProvider : FileSystemSuggestionProvider, ISuggestionProvider
    {
        protected override IEnumerable<FileSystemInfo> GetSuggestions(DirectoryInfo dirInfo, string filter)
        {
            return dirInfo.GetDirectories(filter);
        }
    }
}