using Metropolis.Api.Domain;

namespace Metropolis.Api.Persistence
{
    public interface IProjectRepository
    {
        void Save(CodeBase codebase, string fileName);
        CodeBase Load(string fileName);
        CodeBase LoadDefault();
    }
}