using System.Collections.Specialized;

namespace Metropolis.Common.Models
{
    public class ProjectBuildArguments
    {
        public string ProjectName { get; set; }
        public RepositorySourceType SourceType { get; set; }
        public string ProjetFile { get; set; }
        public string BuildOutputFolder { get; set; }
    }
}
