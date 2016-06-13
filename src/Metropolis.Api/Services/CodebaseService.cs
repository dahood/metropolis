using Metropolis.Api.Build;
using Metropolis.Api.Domain;
using Metropolis.Api.Persistence;
using Metropolis.Api.Readers;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services
{
    public class CodebaseService : ICodebaseService
    {
        private readonly IMetricsReaderFactory readerFactory;
        private readonly IProjectBuildFactory builderFactory;
        private readonly IProjectRepository projectRepository;
        
        public CodebaseService() : this(new MetricsReaderFactory(), new ProjectRepository(), new ProjectBuildFactory())
        {
        }

        public CodebaseService(IMetricsReaderFactory readerFactory, IProjectRepository repository, IProjectBuildFactory builderFactory)
        {
            this.readerFactory = readerFactory;
            projectRepository = repository;
            this.builderFactory = builderFactory;
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
        
        public void BuildSolution(ProjectBuildArguments buildArgs)
        {
            builderFactory.BuilderFor(buildArgs.SourceType).Build(buildArgs);
        }

        public CodeBase GetVisualStudioMetrics(string fileName)
        {
            var result = Get(fileName, ParseType.VisualStudio);
            result.SourceType = RepositorySourceType.CSharp;
            return result;
        }

        public CodeBase Get(string filename, ParseType parseType)
        {
            return readerFactory.GetReader(parseType).Parse(filename);
        }
    }
}