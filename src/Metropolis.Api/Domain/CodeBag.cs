namespace Metropolis.Api.Domain
{
    /// <summary>
    ///     Container for code instances. Each has a CodeBagType
    ///     (e.g Java = Package, C# = Namespace, JavaScript = Directory, ECMA 5 = Module)
    ///     Other candidate names were... CodeLibrary, CodeSack, KittyLitter
    /// </summary>
    public class CodeBag
    {
        private readonly Location path;

        public CodeBag(string name, CodeBagType type, string physcialPath)
        {
            Name = name;
            Type = type;
            path = new Location(physcialPath);
        }

        public string Name { get; }
        public string Path => path.Path;
        public CodeBagType Type { get; }
    }
}