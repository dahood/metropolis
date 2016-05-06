using System;
using System.IO;
using System.Reflection;
using Metropolis.Api.Core.Domain;
using Newtonsoft.Json;

namespace Metropolis.Api.Core.Persistence
{
    public class ProjectRepository
    {
        public void Save(CodeBase codebase, string fileName = "C:\\dev\\sample.project")
        {
            var project = ProjectAssembler.Assemble(codebase.Graph);
            project.Name = codebase.Name;
            project.SourceCodeLanguage = codebase.SourceType.ToString();
            var json = JsonConvert.SerializeObject(project);
            File.WriteAllText(fileName, json);
        }

        public CodeBase Load(string fileName)
        {
            VerifyVersionNumber(fileName);
            return CreateCodeBase(File.ReadAllText(fileName));
        }

        private static void VerifyVersionNumber(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
            {
                byte[] versionHeader = new byte[100];
                stream.Read(versionHeader, 0, 100);
                var results = System.Text.Encoding.UTF8.GetString(versionHeader, 0, versionHeader.Length);
                if (!results.Contains($"\"MetropolisFileVersion\":{Project.SupportedVersion}"))
                    throw new ApplicationException("Version of Metropolis project you are loading is out of date");
            }
        }

        private static CodeBase CreateCodeBase(string json)
        {
            var project = JsonConvert.DeserializeObject<Project>(json);
            var sourceType = (RepositorySourceType) Enum.Parse(typeof(RepositorySourceType), project.SourceCodeLanguage);
            return new CodeBase(project.Name, ProjectAssembler.Disassemble(project), sourceType);
        }

        public CodeBase LoadDefault()
        {
            const string resourceName = "Metropolis.Api.entity-framework-6.project";
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null) throw new ApplicationException(resourceName+" not found");
                using (var reader = new StreamReader(stream))
                {
                    return CreateCodeBase(reader.ReadToEnd());
                }
            }
        }
    }
}