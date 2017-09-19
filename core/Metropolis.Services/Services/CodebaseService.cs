using System.Collections.Generic;
using System.IO;
using System.Linq;
using Metropolis.Api.Domain;
using Metropolis.Api.IO;
using Metropolis.Api.Persistence;
using Metropolis.Api.Readers;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services
{
    public class CodebaseService : ICodebaseService
    {
        private readonly IMetricsReaderFactory readerFactory;
        private readonly IFileSystem fileSystem;
        private readonly IProjectRepository projectRepository;
        
        public CodebaseService() : this(new MetricsReaderFactory(), new ProjectRepository(), new FileSystem())
        {
        }

        public CodebaseService(IMetricsReaderFactory readerFactory, IProjectRepository repository, IFileSystem fileSystem)
        {
            this.readerFactory = readerFactory;
            projectRepository = repository;
            this.fileSystem = fileSystem;
        }
        string ICodebaseService.ProjectBuildFolder => fileSystem.ProjectBuildFolder;

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
        
        public IEnumerable<FileDto> GetIgnoreFilesForProject(string projectName)
        {
            return fileSystem.ReadIgnoreFile(projectName)
                             .Select(each => new FileDto { Ignore = true, Name = each}).ToList();
        }

        public FileContentsResult GetFileContents(string physicalFilePath)
        {
            return new FileContentsResult {FileName = Path.GetFileName(physicalFilePath), Data = fileSystem.ReadFile(physicalFilePath)};
        }

        public CodeBase Get(TextReader stream, ParseType parseType)
        {
            using (stream)
            {
                var r = readerFactory.GetReader(parseType);
                return r.Parse(stream);
            }
        }

        public void WriteIgnoreFile(string projectFolder, IEnumerable<FileDto> filesToIgnore)
        {
            var ignoreData = filesToIgnore.Select(x => x.Name).ToList();
            fileSystem.WriteText(Path.Combine(projectFolder, fileSystem.IgnoreFile), ignoreData);
        }
    }
}