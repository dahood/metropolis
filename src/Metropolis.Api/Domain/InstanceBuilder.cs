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

            return new Instance(filename, directory, CodeBagType.Directory, physicalPath);
        }
    }
}