using Metropolis.Api.Core.Domain;
using Metropolis.Common.Models;

namespace Metropolis.Api.Microservices
{
    public interface ICodebaseService
    {
        void Save(CodeBase workspace, string fileName);
        CodeBase Load(string projectFileName);
        CodeBase LoadDefault();
        CodeBase Get(string filename, ParseType parseType);
    }
}
