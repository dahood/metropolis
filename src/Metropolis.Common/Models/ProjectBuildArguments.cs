using System;

namespace Metropolis.Common.Models
{
    public class ProjectBuildArguments : IEquatable<ProjectBuildArguments>
    {
        public ProjectBuildArguments(string projectName, string projectFile, RepositorySourceType sourceType, string buildFolder)
        {
            ProjectName = projectName;
            ProjectFile = projectFile;
            SourceType = sourceType;
            BuildOutputFolder = buildFolder;
        }

        public string ProjectName { get; }

        public RepositorySourceType SourceType { get; }
        public string ProjectFile { get; }

        public string BuildOutputFolder { get; }

        public bool Equals(ProjectBuildArguments other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(ProjectName, other.ProjectName) &&
                   SourceType == other.SourceType && string.Equals(ProjectFile, other.ProjectFile) &&
                   string.Equals(BuildOutputFolder, other.BuildOutputFolder);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ProjectBuildArguments) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = ProjectName != null ? ProjectName.GetHashCode() : 0;
                hashCode = (hashCode*397) ^ (int) SourceType;
                hashCode = (hashCode*397) ^ (ProjectFile?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (BuildOutputFolder?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}