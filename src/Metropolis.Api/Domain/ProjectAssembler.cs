using System.Collections.Generic;
using System.Linq;

namespace Metropolis.Api.Domain
{
    public class ProjectAssembler
    {
        public const string DateTimeFormat = "yyyy-M-dd-HH-mm-ss-ff";
        public static Project Assemble(CodeBase codeBase)
        {
            return new Project
            {
                RunDate = codeBase.RunDate.ToString(DateTimeFormat),
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
                History = Assemble(src.History),
                CodeBag = new SerializeableCodeBag
                { 
                    Name = src.CodeBag.Name,
                    CodeBagType = src.CodeBag.Type.ToString(),
                    LocationPath = src.CodeBag.Path
                },
                //metrics
                NumberOfMethods = src.NumberOfMethods,
                ClassCoupling = src.ClassCoupling,
                CyclomaticComplexity = src.CyclomaticComplexity,
                DepthOfInheritance = src.DepthOfInheritance,
                Toxicity = src.Toxicity,
                LinesOfCode = src.LinesOfCode,
                //attributes
                Members = src.Members.Select(Assemble),
                Duplicates = src.Duplicates.Select(Assemble)
            };
        }

        private static SerializableVersionHistory Assemble(VersionHistory src)
        {
            //TODO: fix this!!!
            return new SerializableVersionHistory();
               
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