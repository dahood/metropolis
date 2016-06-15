using System.IO;
using Metropolis.Api.Domain;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services
{
    public interface ICodebaseService
    {
        void Save(CodeBase workspace, string fileName);
        CodeBase Load(string projectFileName);
        CodeBase LoadDefault();
        CodeBase Get(TextReader stream, ParseType parseType);
        CodeBase GetToxicity(TextReader openFileStream);
        CodeBase GetVisualStudioMetrics(TextReader openFileStream);
    }
}
