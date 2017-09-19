using System.Collections.Generic;

namespace Metropolis.Common.Models
{
    public class ProjectBuildResult
    {
        public string BuildFolder { get; set; }
        public IEnumerable<FileDto> Artifacts { get; set; }
    }
}