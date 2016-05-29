using System.Collections;
using System.Collections.Generic;
using System.IO;
using WpfControls;

namespace Metropolis.AutoComplete
{
    public abstract class FileSystemSuggestionProvider : ISuggestionProvider
    {
        public IEnumerable GetSuggestions(string filter)
        {
            if (string.IsNullOrEmpty(filter) || filter.Length < 3 || filter[1] != ':')
            {
                return null;
            }

            var dirFilter = "*";
            var dirPath = filter;
            if (filter.EndsWith("\\"))
                return GetSuggestions(new DirectoryInfo(dirPath), dirFilter);

            var index = filter.LastIndexOf("\\");
            dirPath = filter.Substring(0, index + 1);
            dirFilter = filter.Substring(index + 1) + "*";

            return GetSuggestions(new DirectoryInfo(dirPath), dirFilter);
        }

        protected abstract IEnumerable<FileSystemInfo> GetSuggestions(DirectoryInfo dirInfo, string filter);
    }
}
