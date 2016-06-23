using System.Linq;

namespace Metropolis.Api.Domain
{
    public class InstanceBuilder
    {
        public static Instance Build(string physicalPath)
        {
            var filename = string.Join(".", physicalPath.Split('\\').Last().Split('.').ToList());
            var parts = physicalPath.Split('\\').ToList();
            parts.RemoveRange(parts.Count - 1, 1);
            var directory = string.Join("\\", parts);

            var instance = new Instance(filename, directory, CodeBagType.Directory, physicalPath);

            return instance;
        }

        public static Instance Build(string nspace, string name, int numberOfMethods, int linesOfCode, int complexity, int depthOfInheritance, int coupling,
            string physicalFile = null)
        {
            return new Instance(nspace, name, numberOfMethods, linesOfCode, complexity, depthOfInheritance, coupling)
                        {PhysicalPath = physicalFile};
        }
    }
}