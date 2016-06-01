namespace Metropolis.Api.Domain
{
    /// <summary>
    ///     Path information to a file or a directory
    /// </summary>
    public class Location
    {
        public Location(string physcialPath)
        {
            Path = physcialPath;
        }

        public string Path { get; private set; }
    }
}