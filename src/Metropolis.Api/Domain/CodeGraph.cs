using System.Collections.Generic;
using System.Linq;

namespace Metropolis.Api.Domain
{
    public class CodeGraph
    {
        private readonly Dictionary<string, Instance> map = new Dictionary<string, Instance>();
        private readonly HashSet<CodeBag> codebags = new HashSet<CodeBag>();

        public CodeGraph(IEnumerable<Instance> classes)
        {
            Initialize(classes);
        }

        public List<Instance> AllInstances => map.Values.ToList();

        public List<CodeBag> AllNamespaces => codebags.ToList();
        public int Count => map.Count;

        public int MaxLinesOfCode => map.Values.Count != 0 ? map.Values.Max(c => c.LinesOfCode) : 0; 

        public int MinLinesOfCode => map.Values.Count != 0 ? map.Values.Min(c => c.LinesOfCode) : 0;

        private void Initialize(IEnumerable<Instance> list)
        {
            foreach (var type in list)
            {
                Apply(type);
            }
        }

        public void Apply(Instance src)
        {
            Instance c;
            if (map.TryGetValue(src.QualifiedName, out c))
            {
                c.AddMeta(src.Meta);
                c.Apply(src);
            }
            else
            {
                map.Add(src.QualifiedName, src);
                codebags.Add(src.CodeBag);
            }
        }

    }
}