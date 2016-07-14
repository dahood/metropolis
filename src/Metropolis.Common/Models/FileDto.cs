using System;

namespace Metropolis.Common.Models
{
    public class FileDto : IEquatable<FileDto>
    {
        public bool Ignore { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }

        public bool Equals(FileDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Ignore == other.Ignore && string.Equals(Name, other.Name) && string.Equals(FullPath, other.FullPath);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FileDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Ignore.GetHashCode();
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (FullPath != null ? FullPath.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}