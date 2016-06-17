using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public string ProjectBuildFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Metropolis","Build");
        public string IgnoreFile => ".metropolisignore";
        public string GetIgnoreFilePath(string projectName)
        {
            return Path.Combine(ProjectBuildFolder, projectName, IgnoreFile);
        }

        public string GetProjectBuildFolder(string projectName)
        {
            return Path.Combine(ProjectBuildFolder, projectName);
        }

        public bool FileExists(string potentialPath)
        {
            return File.Exists(potentialPath);
        }
        public void CleanFolder(string folder)
        {
            CreateFolder(folder);                                                                   //ensure it exists
            var directory = new DirectoryInfo(folder);
            foreach (var file in directory.GetFiles()) file.Delete();                               //clean all files
            foreach (var subDirectory in directory.GetDirectories()) subDirectory.Delete(true);     //clean all subfolders
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