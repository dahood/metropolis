using System.Collections.Generic;
using System.IO;

namespace Metropolis.AutoComplete
{
    public class FileSuggestionProvider : FileSystemSuggestionProvider
    {
        protected override IEnumerable<FileSystemInfo> GetSuggestions(DirectoryInfo dirInfo, string filter)
        {
            var lst = new List<FileSystemInfo>(dirInfo.GetDirectories(filter));
            lst.AddRange(dirInfo.GetFiles(filter));
            return lst;
        }
    }
}