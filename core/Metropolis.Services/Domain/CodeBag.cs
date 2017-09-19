namespace Metropolis.Api.Domain
{
    /// <summary>
    ///     Container for code instances. Each has a CodeBagType
    ///     (e.g Java = Package, C# = Namespace, JavaScript = Directory, ECMA 5 = Module)
    ///     Other candidate names were... CodeLibrary, CodeSack, KittyLitter
    /// </summary>
    public class CodeBag
    {
        public static CodeBag Empty = new CodeBag(string.Empty, CodeBagType.Empty, string.Empty);

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


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((CodeBag) obj);
        }

        private bool Equals(CodeBag other)
        {
            return string.Equals(Name, other.Name);
        }


        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? 0;
        }
    }
}