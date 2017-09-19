using System;

namespace Metropolis.Api.Domain
{
    /// <summary>
    ///     Path information to a file or a directory
    /// </summary>
    public class Location : IEquatable<Location>
    {
        public Location(string physcialPath)
        {
            Path = physcialPath;
        }

        public string Path { get; }

        public bool Equals(Location other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Path, other.Path);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Location) obj);
        }

        public override int GetHashCode()
        {
            return Path?.GetHashCode() ?? 0;
        }

        public static bool operator ==(Location left, Location right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Location left, Location right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return Path;
        }
    }
}