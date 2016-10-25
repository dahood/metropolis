using System;
using System.Collections.Generic;

namespace Metropolis.Api.Domain
{
    /// <summary>
    ///     A model from git on commit history
    /// </summary>
    public class VersionHistory
    {
        public string CommitHash { get; set; }
        public string AuthorName { get; set; }
        public string CommitMessage { get; set; }
        public DateTime CommitTime { get; set; }
        public IEnumerable<Location> AdditionsAndDeletions { get; set; }
    }
}