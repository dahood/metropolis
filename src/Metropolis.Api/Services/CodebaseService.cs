using System.IO;
using Metropolis.Api.Domain;
using Metropolis.Api.Persistence;
using Metropolis.Api.Readers;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services
{
    public class CodebaseService : ICodebaseService
    {
        private readonly IMetricsReaderFactory readerFactory;
        private readonly IProjectRepository projectRepository;
        
        public CodebaseService() : this(new MetricsReaderFactory(), new ProjectRepository())
        {
        }

        public CodebaseService(IMetricsReaderFactory readerFactory, IProjectRepository repository)
        {
            this.readerFactory = readerFactory;
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

        public CodeBase GetToxicity(TextReader openFileStream)
        {
            using (openFileStream)
            {
                var result = Get(openFileStream, ParseType.RichardToxicity);
                result.SourceType = RepositorySourceType.CSharp;
                return result;
            }
        }

        public CodeBase GetVisualStudioMetrics(TextReader openFileStream)
        {
            using (openFileStream)
            {
                var result = Get(openFileStream, ParseType.VisualStudio);
                result.SourceType = RepositorySourceType.CSharp;
                return result;
            }
        }

        public CodeBase Get(TextReader stream, ParseType parseType)
        {
            using (stream)
            {
                return readerFactory.GetReader(parseType).Parse(stream);
            }
        }
    }
}