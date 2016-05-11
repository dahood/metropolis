using System;
using Metropolis.Api.Utilities;

namespace Metropolis.Api.Core.Domain
{
    public class InstanceVersionInfo
    {
        public InstanceVersionInfo(string fileName, string commitMessage)
        {
            FileName = fileName;
            CommitMessage = commitMessage;
            TimeStamp = Clock.Now;
        }

        public string FileName { get; private set; }
        public string CommitMessage { get; private set; }
        public DateTime TimeStamp { get; private set; }
    }
}