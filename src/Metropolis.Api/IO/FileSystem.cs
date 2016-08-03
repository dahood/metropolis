using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Metropolis.Api.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.IO
{
    public class FileSystem : IFileSystem
    {
        public IEnumerable<string> GetFiles(string sourceDirectory, string filter)
        {
            return Directory.GetFiles(sourceDirectory, filter);
        }

        public string GetFileName(string targetdll)
        {
            return Path.GetFileName(targetdll);
        }

        public void CreateFolder(string folder)
        {
            if (Directory.Exists(folder)) return;
            Directory.CreateDirectory(folder);
        }

        IEnumerable<DriveInfo> IFileSystem.AllDrives => DriveInfo.GetDrives();
        public string ProjectBuildFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Metropolis", "Build");
        public string AutoSaveFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Metropolis", "Autosave");
        public string ScreenShotFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Metropolis", "Screenshots");

        public string MetricsOutputFolder
            => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Metropolis", "Metrics");

        public string IgnoreFile => ".metropolisignore";

        public string GetIgnoreFilePath(string projectName)
        {
            return projectName == null ? string.Empty : Path.Combine(ProjectBuildFolder, projectName, IgnoreFile);
        }

        public string GetProjectBuildFolder(string projectName)
        {
            return projectName == null ? string.Empty : Path.Combine(ProjectBuildFolder, projectName);
        }

        public IEnumerable<string> ReadIgnoreFile(string ignoreFile)
        {
            return File.Exists(ignoreFile) ? File.ReadAllLines(ignoreFile) : Enumerable.Empty<string>();
        }

        public string ReadFile(string physicalFilePath)
        {
            return FileExists(physicalFilePath) ? File.ReadAllText(physicalFilePath) : string.Empty;
        }

        public void EnsureDirectoriesExist(params string[] paths)
        {
            paths.ForEach(target =>
            {
                if (Directory.Exists(target)) return;
                Directory.CreateDirectory(target);
            });
        }

        public void CreateMetropolisSpecialFolders()
        {
            EnsureDirectoriesExist(AutoSaveFolder, ScreenShotFolder);
        }

        public IEnumerable<FileInfo> GetAutloadProjects()
        {
            return Directory.GetFiles(AutoSaveFolder, "AutoSave*.project")
                .Select(x => new FileInfo(x)).OrderByDescending(fi => fi.LastWriteTime);
        }
        
        public string GetProjectIgnoreFile(string projectFolder)
        {
            return Path.Combine(projectFolder, IgnoreFile);
        }

        public bool FileExists(string potentialPath)
        {
            return File.Exists(potentialPath);
        }

        public void CleanFolder(string folder)
        {
            CreateFolder(folder); //ensure it exists
            var directory = new DirectoryInfo(folder);
            foreach (var file in directory.GetFiles()) file.Delete(); //clean all files
            foreach (var subDirectory in directory.GetDirectories()) subDirectory.Delete(true); //clean all subfolders
        }

        public IEnumerable<FileDto> FindAllBinaries(string folder)
        {
            var directory = new DirectoryInfo(folder);
            if (!directory.Exists) return new FileDto[0];

            return (from f in directory.GetFiles()
                where f.Name.EndsWith(".dll") || f.Name.EndsWith(".exe")
                select new FileDto {Name = f.Name, FullPath = f.FullName}).ToList();
        }

        public TextReader OpenFileStream(string fileName)
        {
            if (!File.Exists(fileName)) throw new ApplicationException($"File does not exist: {fileName}");
            return File.OpenText(fileName);
        }

        public void WriteText(string path, IEnumerable<string> data)
        {
            File.WriteAllLines(path, data);
        }
    }
}