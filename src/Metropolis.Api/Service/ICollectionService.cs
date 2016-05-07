using Metropolis.Api.Core.Domain;

namespace Metropolis.Api.Service
{
    public interface ICollectionService
    {
        void Save(CodeBase workspace, string fileName);
        CodeBase Load(string fileName);
        CodeBase LoadDefault();
        CodeBase ParseToxicity(string fileName, string sourceBaseDirectory);
        CodeBase ParseVisualStudioMetrics(string fileName, string sourceBaseDirectory);
    }
}
