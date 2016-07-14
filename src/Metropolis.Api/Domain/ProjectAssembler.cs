using System.Collections.Generic;
using System.Linq;

namespace Metropolis.Api.Domain
{
    public class ProjectAssembler
    {
        public static Project Assemble(CodeBase codeBase)
        {
            return new Project
            {
                Name = codeBase.Name,
                SourceBaseDirectory = codeBase.SourceBaseDirectory,
                SourceCodeLanguage = codeBase.SourceType.ToString(),
                ProjectFile = codeBase.ProjectFile,
                ProjectFolder = codeBase.ProjectFolder,
                IgnoreFile = codeBase.IgnoreFile,
                Classes = Assemble(codeBase.Graph.AllInstances)
            };
        }

        public static Project Assemble(CodeGraph graph)
        {
            return new Project { Classes = Assemble(graph.AllInstances) };
        }

        private static IEnumerable<SerializableClass> Assemble(IEnumerable<Instance> classes)
        {
            return classes.Select(Assemble);
        }
        private static SerializableClass Assemble(Instance src)
        {
            return new SerializableClass
            {
                Name = src.Name,
                Location = src.PhysicalPath.Path,
                CodeBag = new SerializeableCodeBag
                {
                    Name = src.CodeBag.Name,
                    CodeBagType = src.CodeBag.Type.ToString(),
                    LocationPath = src.CodeBag.Path
                },
                NumberOfMethods = src.NumberOfMethods,
                ClassCoupling = src.ClassCoupling,
                CyclomaticComplexity = src.CyclomaticComplexity,
                DepthOfInheritance = src.DepthOfInheritance,
                Toxicity = src.Toxicity,
                LinesOfCode = src.LinesOfCode,
                Meta = src.Meta.Select(Assemble),
                Members = src.Members.Select(Assemble),
                Duplicates = src.Duplicates.Select(Assemble)
            };
        }

        private static SerializableClassVersionInfo Assemble(InstanceVersionInfo src)
        {
            return new SerializableClassVersionInfo
            {
                FileName = src.FileName,
                CommitMessage = src.CommitMessage,
                TimeStamp = src.TimeStamp
            };
        }

        private static SerializableMember Assemble(Member src)
        {
            return new SerializableMember
            {
                LinesOfCode = src.LinesOfCode,
                CylomaticComplexity = src.CylomaticComplexity,
                Name = src.Name,
                ClassCoupling = src.ClassCoupling,
                MissingDefaultCase = src.MissingDefaultCase,
                NoFallthrough = src.NoFallthrough,
                NumberOfParameters = src.NumberOfParameters
            };
        }

        private static SerializableDuplicate Assemble(Duplicate src)
        {
            return new SerializableDuplicate
            {
                LinesOfCode = src.LinesOfCode.ToString(),
                LineNumber = src.LineNumber.ToString(),
                Location = src.Location.ToString(),
                CopyCats = Assemble(src.CopyCats)
            };
        }

        private static IEnumerable<SerializableDuplicate> Assemble(IEnumerable<Duplicate> copyCats)
        {
            return copyCats.ToList().ConvertAll(x => new SerializableDuplicate
            {
                LinesOfCode = x.LinesOfCode.ToString(),
                LineNumber = x.LineNumber.ToString(),
                Location = x.Location.ToString()
            }).ToArray();
        }
    }
}