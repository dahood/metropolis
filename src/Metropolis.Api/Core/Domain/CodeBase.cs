using System.Collections.Generic;
using System.Linq;

namespace Metropolis.Api.Core.Domain
{
    public class CodeBase
    {
        public string Name { get; set; }
        public string SourceBaseDirectory { get; set; }
        public RepositorySourceType SourceType { get; set; }
        public CodeGraph Graph { get; }
        public List<Class> AllClasses => Graph.AllClasses;

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
            Enrich(enricher.AllClasses);
        }

        public void Enrich(IEnumerable<Class> classes)
        {
            foreach (var c in classes)
            {
                Graph.Apply(c);
            }
        }

        public int NumberOfTypes => Graph.Count;

        public int LinesOfCode => AllClasses.Sum(c => c.LinesOfCode);

        public double AverageToxicity()
        {
            double sum = AllClasses.Sum(c => c.Toxicity);
            return sum / NumberOfTypes;
        }

        public Dictionary<string, IEnumerable<Class>> ByNamespace()
        {
            return Graph.AllNamespaces.ToDictionary(ns => ns, ns => AllClasses.Where(x => x.NameSpace == ns));
        }

        public static CodeBase Empty()
        {
            return new CodeBase(new CodeGraph(new Class[0]));
        }
    }
}