namespace Metropolis.Api.Domain
{

    /// <summary>
    /// Represents the type of CodeBag (e.g. Namespace for C#, Package for Java, Directory for JavsScript)
    /// </summary>
    public enum CodeBagType
    {
        Namespace,
        Package,
        Directory
    }
}