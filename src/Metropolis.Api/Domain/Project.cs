using System;
using System.Collections.Generic;

namespace Metropolis.Api.Domain
{
    public class Project
    {
        /// <summary>
        ///     Only support one version of project file for now - we can increment this
        ///     later to warn users that the file needs to be reloaded
        /// </summary>
        public const int SupportedVersion = 3;

        public int MetropolisFileVersion => SupportedVersion;
        public string Name { get; set; }
        public string SourceBaseDirectory { get; set; }
        public string SourceCodeLanguage { get; set; }
        public IEnumerable<SerializableClass> Classes { get; set; }
    }

    public class SerializableClass
    {
        public IEnumerable<SerializableClassVersionInfo> Meta { get; set; }
        public IEnumerable<SerializableMember> Members { get; set; }
        public IEnumerable<SerializableDuplicate> Duplicates { get; set; }
        public SerializeableCodeBag CodeBag { get; set; }

        public string Location { get; set; }
        public string Name { get; set; }
        public int NumberOfMethods { get; set; }
        public int LinesOfCode { get; set; }
        public int ClassCoupling { get; set; }
        public int CyclomaticComplexity { get; set; }
        public int DepthOfInheritance { get; set; }
        public double Toxicity { get; set; }
    }

    public class SerializableDuplicate
    {
        public string LineNumber { get; set; }
        public string LinesOfCode { get; set; }
    }

    public class SerializeableCodeBag
    {
        public string Name { get; set; }
        public string CodeBagType { get; set; }
        public string LocationPath { get; set; }
    }

    public class SerializableMember
    {
        public string Name { get; set; }
        public int LinesOfCode { get; set; }
        public int CylomaticComplexity { get; set; }
        public int ClassCoupling { get; set; }
        public int NumberOfParameters { get; set; }
        public int MissingDefaultCase { get; set; }
        public int NoFallthrough { get; set; }
    }

    public class SerializableClassVersionInfo
    {
        public string FileName { get; set; }
        public string CommitMessage { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}