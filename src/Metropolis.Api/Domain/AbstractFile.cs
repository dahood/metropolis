using System.Collections.Generic;

namespace Metropolis.Api.Domain
{
    /// <summary>
    /// A file in a version control system
    /// </summary>
    public class AbstractFile
    {
        public Location PhysicalPath { get; protected set; }
        public HashSet<CommitEntry> VersionHistory { get; set; }
    }
}