using System;
using Metropolis.Utilities;

namespace Metropolis.Domain
{
    public class ClassVersionInfo
    {
        public ClassVersionInfo(string fileName, string commitMessage)
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