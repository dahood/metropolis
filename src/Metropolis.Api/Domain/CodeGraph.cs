using System.Collections.Generic;
using System.Linq;

namespace Metropolis.Api.Domain
{
    /// <summary>
    /// A graph to represent a version controlled source code system
    /// Instances make up all the executable code units
    /// Artifacts make up all the other configuration, deployment, build system, and binary assets
    /// CodeBags are furthur containers provided like file directories or package/namespace classification for OO languages
    /// </summary>
    public class CodeGraph
    {
        private readonly Dictionary<Location, Instance> instanceMap = new Dictionary<Location, Instance>();
        //TODO: make this work... not sure how this merges with the CodeGraph
        //private readonly Dictionary<Location, ArtifactFile> artifactMap = new Dictionary<Location, ArtifactFile>();

        private readonly HashSet<CodeBag> codebags = new HashSet<CodeBag>();

        public CodeGraph(IEnumerable<Instance> classes)
        {
            Initialize(classes);
        }

        public List<Instance> AllInstances => instanceMap.Values.ToList();

        public List<CodeBag> AllNamespaces => codebags.ToList();
        public int Count => instanceMap.Count;

        public int MaxLinesOfCode => instanceMap.Values.Count != 0 ? instanceMap.Values.Max(c => c.LinesOfCode) : 0; 

        public int MinLinesOfCode => instanceMap.Values.Count != 0 ? instanceMap.Values.Min(c => c.LinesOfCode) : 0;

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
            if (instanceMap.TryGetValue(src.PhysicalPath, out c))
            {
                c.Apply(src);
            }
            else
            {
                instanceMap.Add(src.PhysicalPath, src);
                codebags.Add(src.CodeBag);
            }
        }
    }
}