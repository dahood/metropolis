using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Metropolis.Api.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Domain
{
    public static class ProjectDisassembler
    {
        public static CodeBase Disassemble(Project project)
        {
            return new CodeBase(new CodeGraph(Disassemble(project.Classes)))
            {
                RunDate = DateTime.ParseExact(project.RunDate, ProjectAssembler.DateTimeFormat, CultureInfo.InvariantCulture),
                Name = project.Name,
                SourceBaseDirectory = project.SourceBaseDirectory,
                SourceType = project.SourceCodeLanguage.ToEnumExact<RepositorySourceType>(),
                ProjectFile = project.ProjectFile,
                ProjectFolder = project.ProjectFolder,
                IgnoreFile = project.IgnoreFile
            };
        }

        private static IEnumerable<Instance> Disassemble(IEnumerable<SerializableClass> classes)
        {
            return classes.Select(Disassemble);
        }

        private static Instance Disassemble(SerializableClass src)
        {
            var disassemble = new Instance(
                new CodeBag(src.CodeBag.Name, src.CodeBag.CodeBagType.ToEnumExact<CodeBagType>(),
                    src.CodeBag.LocationPath), src.Name, new Location(src.Location))
            {
                LinesOfCode = src.LinesOfCode,
                NumberOfMethods = src.NumberOfMethods,
                ClassCoupling = src.ClassCoupling,
                CyclomaticComplexity = src.CyclomaticComplexity,
                DepthOfInheritance = src.DepthOfInheritance,
                Toxicity = src.Toxicity,
                History = Disassemble(src.History),
                Members = src.Members.Select(Disassemble).ToList()
            };

            disassemble.Duplicates.AddRange(src.Duplicates.Select(Disassemble).ToList());

            return disassemble;
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

        private static Duplicate Disassemble(SerializableDuplicate src)
        {
            return new Duplicate(int.Parse(src.LinesOfCode), int.Parse(src.LineNumber), new Location(src.Location), Disassemble(src.CopyCats));
        }

        private static Duplicate[] Disassemble(IEnumerable<SerializableDuplicate> copyCats)
        {
            return
                copyCats.ToList()
                    .ConvertAll(x => new Duplicate(int.Parse(x.LinesOfCode), int.Parse(x.LineNumber), new Location(x.Location)))
                    .ToArray();
        }

        private static VersionHistory Disassemble(SerializableVersionHistory src)
        {
            //TODO: fix this also
            //use the src luke!
            return new VersionHistory();
        }
    }
}