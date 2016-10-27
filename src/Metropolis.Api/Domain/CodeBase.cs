using System;
using System.Collections.Generic;
using System.Linq;
using Metropolis.Common.Models;

namespace Metropolis.Api.Domain
{
    public class CodeBase : IEquatable<CodeBase>
    {
        public DateTime RunDate { get; set; }
        public string Name { get; set; }
        public string SourceBaseDirectory { get; set; }
        public string ProjectFile { get; set; }
        public string ProjectFolder { get; set; }
        public string IgnoreFile { get; set; }
        public RepositorySourceType SourceType { get; set; }
        public CodeGraph Graph { get; }
        public List<Instance> AllInstances => Graph.AllInstances;

        public List<CommitEntry> Commits { get; set; }

        public CodeBase(CodeGraph graph) : this(string.Empty, graph)
        {
        }

        public CodeBase(string name, CodeGraph graph, RepositorySourceType sourceType = RepositorySourceType.CSharp)
        {
            Name = name;
            Graph = graph;
            SourceType = sourceType;
        }

        public void Enrich(CodeGraph enricher)
        {
            Enrich(enricher.AllInstances);
        }

        public void Enrich(IEnumerable<Instance> instances)
        {
            foreach (var c in instances)
            {
                Graph.Apply(c);
            }
        }

        public int InstanceCount() => Graph.Count;

        public double AverageToxicity()
        {
            return AbsoluteToxicity() / InstanceCount();
        }
        public double AbsoluteToxicity()
        {
            return AllInstances.Sum(c => c.Toxicity);
        }

        public int LinesOfCode() => AllInstances.Sum(c => c.LinesOfCode);

        public double Density()
        {
            return (double) LinesOfCode() / InstanceCount();
        }

        public double Duplicates()
        {
            if (LinesOfCode() == 0) return 0;
            return (double) Graph.AllInstances.Sum(x => x.DuplicateLines)/ LinesOfCode();
        }

        public Dictionary<CodeBag, IEnumerable<Instance>> ByNamespace()
        {
            return Graph.AllNamespaces.ToDictionary(ns => ns, ns => AllInstances.Where(x => x.CodeBag.Equals(ns)));
        }

        public static CodeBase Empty()
        {
            return new CodeBase(new CodeGraph(new Instance[0]));
        }

        public bool Equals(CodeBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && string.Equals(SourceBaseDirectory, other.SourceBaseDirectory) &&
                   string.Equals(ProjectFile, other.ProjectFile) && string.Equals(ProjectFolder, other.ProjectFolder) &&
                   string.Equals(IgnoreFile, other.IgnoreFile) && SourceType == other.SourceType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CodeBase) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (SourceBaseDirectory != null ? SourceBaseDirectory.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (ProjectFile != null ? ProjectFile.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (ProjectFolder != null ? ProjectFolder.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (IgnoreFile != null ? IgnoreFile.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (int) SourceType;
                return hashCode;
            }
        }
    }
}