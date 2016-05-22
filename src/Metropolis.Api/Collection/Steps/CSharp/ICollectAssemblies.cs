using System.Collections.Generic;
using System.IO;
using System.Linq;
using Metropolis.Api.Utilities;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.CSharp
{
    public interface ICollectAssemblies
    {
        IEnumerable<string> GatherAssemblies(MetricsCommandArguments args);
    }

    public class CollectAssemblies : ICollectAssemblies
    {
        private readonly IFileSystem filesystem;

        public CollectAssemblies() : this(new FileSystem())
        {
        }

        public CollectAssemblies(IFileSystem filesystem)
        {
            this.filesystem = filesystem;
        }

        public IEnumerable<string> GatherAssemblies(MetricsCommandArguments args)
        {
            var assembliesToIgnore = ReadIgnoreFile(args.IgnoreFile);
            var toDoList =  filesystem.GetFiles(args.SourceDirectory, "*.dll")
                                      .Where(file => !assembliesToIgnore.Any(file.EndsWith)).ToList();
            return toDoList;
        } 

        private static IEnumerable<string> ReadIgnoreFile(string ignoreFile)
        {
            if (ignoreFile.IsEmpty() | !File.Exists(ignoreFile)) return Enumerable.Empty<string>();
            return File.ReadAllLines(ignoreFile);
        }
    }
}
