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
        public Location PhysicalPath { get; protected set; }

        public FileHeadRevisionStatus IsInHeadRevision { get; set; }
        public HashSet<CommitEntry> VersionHistory { get; set; }
    }

    public enum FileHeadRevisionStatus { Unsure, Exists, DeletedFromHead}
}