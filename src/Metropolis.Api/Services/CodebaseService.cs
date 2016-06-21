using System.Collections.Generic;
using System.IO;
using System.Linq;
using Metropolis.Api.Build;
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
        private readonly IProjectBuildFactory builderFactory;
        private readonly IFileSystem fileSystem;
        private readonly IProjectRepository projectRepository;
        
        public CodebaseService() : this(new MetricsReaderFactory(), new ProjectRepository(), new ProjectBuildFactory(), new FileSystem())
        {
        }

        public CodebaseService(IMetricsReaderFactory readerFactory, IProjectRepository repository, IProjectBuildFactory builderFactory, IFileSystem fileSystem)
        {
            this.readerFactory = readerFactory;
            projectRepository = repository;
            this.builderFactory = builderFactory;
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
        
        public ProjectBuildResult BuildSolution(ProjectBuildArguments buildArgs)
        {
            buildArgs.BuildOutputFolder = Path.Combine(fileSystem.ProjectBuildFolder, buildArgs.ProjectName);
            return builderFactory.BuilderFor(buildArgs.SourceType).Build(buildArgs);
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

        public BuildPathsDto GetBuildPaths(string projectName)
        {
            return new BuildPathsDto
            {
                IgnoreFile = fileSystem.GetIgnoreFilePath(projectName),
                BuildOutputDirectory = fileSystem.GetProjectBuildFolder(projectName)
            };
        }

        public IEnumerable<FileDto> GetIgnoreFilesForProject(string projectName)
        {
            return fileSystem.ReadIgnoreFile(projectName)
                             .Select(each => new FileDto { Ignore = true, Name = each}).ToList();
        }

        public CodeBase Get(TextReader stream, ParseType parseType)
        {
            using (stream)
            {
                return readerFactory.GetReader(parseType).Parse(stream);
            }
        }

        public void WriteIgnoreFile(string projectName, string projectFolder, IEnumerable<FileDto> filesToIgnore)
        {
            var ingoreData = filesToIgnore.Select(x => x.Name);

            fileSystem.WriteText(Path.Combine(projectFolder, fileSystem.IgnoreFile), ingoreData);
            fileSystem.WriteText(Path.Combine(fileSystem.ProjectBuildFolder, projectName, fileSystem.IgnoreFile), ingoreData);
        }
    }
}