using System.Collections.Generic;
using System.Linq;

namespace Metropolis.Domain
{
    public class CodeGraph
    {
        private readonly Dictionary<string, Class> map = new Dictionary<string, Class>();
        private readonly HashSet<string> namespaces = new HashSet<string>();

        public CodeGraph(IEnumerable<Class> classes)
        {
            Initialize(classes);
        }

        public List<Class> AllClasses => map.Values.ToList();

        public List<string> AllNamespaces => namespaces.ToList();
        public int Count => map.Count;

        public int MaxLinesOfCode => map.Values.Count != 0 ? map.Values.Max(c => c.LinesOfCode) : 0; 

        public int MinLinesOfCode => map.Values.Count != 0 ? map.Values.Min(c => c.LinesOfCode) : 0;

        public void Initialize(IEnumerable<Class> list)
        {
            foreach (var type in list)
            {
                Apply(type);
            }
        }

        public void Apply(Class src)
        {
            Class c;
            if (map.TryGetValue(src.QualifiedName, out c))
            {
                c.AddMeta(src.Meta);
                c.Apply(src);
            }
            else
            {
                map.Add(src.QualifiedName, src);
                namespaces.Add(src.NameSpace);
            }
        }

    }
}