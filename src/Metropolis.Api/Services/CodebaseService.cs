using Metropolis.Api.Domain;
using Metropolis.Api.Persistence;
using Metropolis.Api.Readers;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services
{
    public class CodebaseService : ICodebaseService
    {
        private readonly IMetricsParserFactory parserFactory;
        private readonly IProjectRepository projectRepository;
        
        public CodebaseService() : this(new MetricsParserFactory(), new ProjectRepository())
        {
        }

        public CodebaseService(IMetricsParserFactory parserFactory, IProjectRepository repository)
        {
            this.parserFactory = parserFactory;
            projectRepository = repository;
        }

        public void Save(CodeBase workspace, string fileName)
        {
            projectRepository.Save(workspace, fileName);
        }

        public CodeBase Load(string projectFileName)
        {
            return projectRepository.Load(projectFileName);
        }

        public CodeBase LoadDefault()
        {
            return projectRepository.LoadDefault();
        }

        public CodeBase GetToxicity(string fileName)
        {
            var result = Get(fileName, ParseType.RichardToxicity);
            result.SourceType = RepositorySourceType.CSharp;
            return result;
        }

        public CodeBase GetVisualStudioMetrics(string fileName)
        {
            var result = Get(fileName, ParseType.VisualStudio);
            result.SourceType = RepositorySourceType.CSharp;
            return result;
        }

        public CodeBase Get(string filename, ParseType parseType)
        {
            return parserFactory.ParserFor(parseType).Parse(filename);
        }
    }
}