using System.Collections.Generic;
using System.Linq;

namespace Metropolis.Api.Core.Domain
{
    public class ProjectAssembler
    {
        public static Project Assemble(CodeGraph graph)
        {
            return new Project {Classes = Assemble(graph.AllInstances)};
        }

        public static CodeGraph Disassemble(Project project)
        {
            return new CodeGraph(Disassemble(project.Classes));
        }

        private static IEnumerable<Instance> Disassemble(IEnumerable<SerializableClass> classes)
        {
            return classes.Select(Disassemble);
        }

        private static Instance Disassemble(SerializableClass src)
        {
            return new Instance(src.NameSpace, src.Name)
            {
                LinesOfCode = src.LinesOfCode,
                NumberOfMethods = src.NumberOfMethods,
                ClassCoupling = src.ClassCoupling,
                CyclomaticComplexity = src.CyclomaticComplexity,
                DepthOfInheritance = src.DepthOfInheritance,
                Toxicity = src.Toxicity,
                Meta = src.Meta.Select(Disassemble),
                Members = src.Members.Select(Disassemble).ToList()
            };
        }

        private static Member Disassemble(SerializableMember src)
        {
            return new Member(src.Name, src.LinesOfCode, src.CylomaticComplexity, src.ClassCoupling)
            {
                MissingDefaultCase = src.MissingDefaultCase,
                NoFallthrough = src.NoFallthrough,
                NumberOfParameters = src.NumberOfParameters
            };
        }

        private static InstanceVersionInfo Disassemble(SerializableClassVersionInfo i)
        {
            return new InstanceVersionInfo(i.FileName, i.CommitMessage);
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
                NameSpace = src.NameSpace,
                NumberOfMethods = src.NumberOfMethods,
                ClassCoupling = src.ClassCoupling,
                CyclomaticComplexity = src.CyclomaticComplexity,
                DepthOfInheritance =  src.DepthOfInheritance,
                Toxicity = src.Toxicity,
                LinesOfCode = src.LinesOfCode,
                Meta = src.Meta.Select(Assemble),
                Members = src.Members.Select(Assemble)
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
    }
}