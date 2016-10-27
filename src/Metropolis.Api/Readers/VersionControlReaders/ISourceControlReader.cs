using System.IO;
using Metropolis.Api.Domain;

namespace Metropolis.Api.Readers.VersionControlReaders
{
    /// <summary>
    /// For source control systems to read in history from commands like:
    /// git log --numstat > logfile.log
    /// 
    /// Requires that you pass a valid CodeBase, since version control doesn't include information about the Instances or Artifacts themselves
    /// </summary>
    public interface ISourceControlReader
    {
        CodeBase Parse(TextReader textReader, CodeBase codeBase);
    }
}