using System.Collections.Generic;
using System.Linq;
using Metropolis.Api.IO;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.CSharp
{
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
            var assembliesToIgnore = filesystem.ReadIgnoreFile(args.IgnoreFile);
            var toDoList =  filesystem.GetFiles(args.BuildOutputFolder, "*.dll")
                .Union(filesystem.GetFiles(args.BuildOutputFolder, "*.exe"))
                .Where(file => !assembliesToIgnore.Any(file.EndsWith)).ToList();
            return toDoList;
        } 
    }
}