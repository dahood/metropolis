using System.IO;
using Metropolis.Api.Domain;
using Metropolis.Api.Persistence;
using Metropolis.Api.Utilities;

namespace Metropolis.Api.IO.AutoSave
{
    public interface IAutoSaveService
    {
        void Save(CodeBase codeBase);
    }

    public class AutoSaveService : IAutoSaveService
    {
        readonly IProjectRepository projectRepository;
        private readonly IFileSystem fileSystem;

        public AutoSaveService() : this(new ProjectRepository(), new FileSystem())
        {
        }

        public AutoSaveService(IProjectRepository projectRepository, IFileSystem fileSystem)
        {
            this.projectRepository = projectRepository;
            this.fileSystem = fileSystem;
        }

        public void Save(CodeBase codeBase)
        {
            fileSystem.EnsureDirectoriesExist(fileSystem.AutoSaveFolder);
            projectRepository.Save(codeBase, Path.Combine(fileSystem.AutoSaveFolder,NextAutoSaveName));
        }

        public string NextAutoSaveName => $"AutoSave-{Clock.Now.ToString("yyyy-M-dd-HH-mm-ss-ff")}.project";
    }
}