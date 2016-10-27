using System;
using System.Collections.Generic;

namespace Metropolis.Api.Domain
{
    /// <summary>
    ///     A model from git on commit history
    /// </summary>
    public class CommitEntry
    {
        public string CommitHash { get; set; } // 12ad430
        public string AuthorName { get; set; }
        public string CommitMessage { get; set; }
        public DateTime CommitTime { get; set; }
        public List<CommitDetail> AdditionsAndDeletions { get; set; } = new List<CommitDetail>();
    }
}