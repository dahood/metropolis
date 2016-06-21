using System;
using System.Collections.Specialized;

namespace Metropolis.Common.Models
{
    public class ProjectBuildArguments : IEquatable<ProjectBuildArguments>
    {
        public string ProjectName { get; set; }
        public string ProjectFolder { get; set; }
        public RepositorySourceType SourceType { get; set; }
        public string ProjetFile { get; set; }
        
        public string BuildOutputFolder { get; set; }

        public bool Equals(ProjectBuildArguments other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(ProjectName, other.ProjectName) && string.Equals(ProjectFolder, other.ProjectFolder) &&
                   SourceType == other.SourceType && string.Equals(ProjetFile, other.ProjetFile) &&
                   string.Equals(BuildOutputFolder, other.BuildOutputFolder);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ProjectBuildArguments) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (ProjectName != null ? ProjectName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (ProjectFolder != null ? ProjectFolder.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (int) SourceType;
                hashCode = (hashCode*397) ^ (ProjetFile != null ? ProjetFile.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (BuildOutputFolder != null ? BuildOutputFolder.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
