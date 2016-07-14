using System;
using System.IO;
using System.Reflection;
using Metropolis.Api.Domain;
using Newtonsoft.Json;

namespace Metropolis.Api.Persistence
{
    public class ProjectRepository : IProjectRepository
    {
        public void Save(CodeBase codebase, string fileName)
        {
            var project = Get(codebase);
            var json = JsonConvert.SerializeObject(project);
            File.WriteAllText(fileName, json);
        }

        public CodeBase Load(string fileName)
        {
            VerifyVersionNumber(fileName);
            return CreateCodeBase(File.ReadAllText(fileName));
        }

        public static Project Get(CodeBase codebase)
        {
            return ProjectAssembler.Assemble(codebase);
        }

        public static CodeBase Get(Project project)
        {
            return ProjectDisassembler.Disassemble(project);
        }

        public CodeBase LoadDefault()
        {
            const string resourceName = "Metropolis.Api.hibernate-core.project";
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null) throw new ApplicationException(resourceName + " not found");
                using (var reader = new StreamReader(stream))
                {
                    return CreateCodeBase(reader.ReadToEnd());
                }
            }
        }

        private static void VerifyVersionNumber(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
            {
                var versionHeader = new byte[100];
                stream.Read(versionHeader, 0, 100);
                var results = System.Text.Encoding.UTF8.GetString(versionHeader, 0, versionHeader.Length);
                if (!results.Contains($"\"MetropolisFileVersion\":{Project.SupportedVersion}"))
                    throw new ApplicationException("Version of Metropolis project you are loading is out of date");
            }
        }

        private static CodeBase CreateCodeBase(string json)
        {
            return Get(JsonConvert.DeserializeObject<Project>(json));
        }
    }
}