using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Metropolis.Api.Domain
{
    /// <summary>
    /// A graph to represent a version controlled source code system
    /// Instances make up all the executable code units (e.g. java class)
    /// Artifacts make up all the other configuration, deployment, build system, and binary assets
    /// CodeBags are furthur containers provided like file directories or package/namespace classification for OO languages
    /// </summary>
    public class CodeGraph
    {
        private readonly Dictionary<Location, Instance> instanceMap = new Dictionary<Location, Instance>();
        private bool baseDirecotrySet = false;
        private string baseDirectory = String.Empty;
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
                if (!baseDirecotrySet)
                    ExtractBaseDirectory(type);
            }
        }

        private void ExtractBaseDirectory(Instance type)
        {
            if (baseDirectory == null)
                baseDirectory = type.PhysicalPath.Path;

            if (!baseDirecotrySet && instanceMap.Count > 1)
            {
                var firstInstance = instanceMap.First().Value.PhysicalPath.Path;
                var secondInstance = type.PhysicalPath.Path;
                //TODO: not the most efficient way of doing this, but it works for now
                for (var i = 0; i < secondInstance.Length; i++)
                {
                    if (firstInstance.StartsWith(secondInstance.Substring(0, i)))
                        baseDirectory = secondInstance.Substring(0, i);
                    else
                        break;
                }
                baseDirecotrySet = true;
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

        public Instance FindInstanceWithRelativePath(Location src, bool useWindowsDirectory)
        {
            Instance instance;
            var path = useWindowsDirectory ? src.Path.Replace("/","\\") : src.Path;
            Location fullPathLocation = new Location(Path.Combine(baseDirectory, path));
            instanceMap.TryGetValue(fullPathLocation, out instance);
            return instance;
        }
    }
}