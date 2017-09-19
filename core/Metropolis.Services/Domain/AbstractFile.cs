using System.Collections.Generic;

namespace Metropolis.Api.Domain
{
    /// <summary>
    /// A file in a version control system
    /// </summary>
    public class AbstractFile
    {
        protected AbstractFile(Location path)
        {
            PhysicalPath = path;
        }
        public Location PhysicalPath { get; private set; }

        public HashSet<string> VersionHistory { get; set; } = new HashSet<string>(); // 12ad430 
    }
}