using Metropolis.Api.Core.Domain;
using Metropolis.Api.Core.Parsers.CsvParsers;
using Metropolis.Api.Core.Persistence;

namespace Metropolis.Api.Service
{
    public class MetropolisApi : IMetropolisApi
    {
        private readonly ProjectRepository projectRepository = new ProjectRepository();

        public void Save(CodeBase workspace, string fileName)
        {
            projectRepository.Save(workspace, fileName);
        }

        public CodeBase Load(string fileName)
        {
            return projectRepository.Load(fileName);
        }

        public CodeBase LoadDefault()
        {
            return projectRepository.LoadDefault();
        }

        public CodeBase ParseToxicity(string fileName, string sourceBaseDirectory)
        {
            var result = new ToxicityParser().Parse(fileName, sourceBaseDirectory);
            result.SourceType = RepositorySourceType.CSharp;
            return result;
        }

        public CodeBase ParseVisualStudioMetrics(string fileName, string sourceBaseDirectory)
        {
            var result = new VisualStudioMetricsParser().Parse(fileName, sourceBaseDirectory);
            result.SourceType = RepositorySourceType.CSharp;
            return result;
        }
    }
}