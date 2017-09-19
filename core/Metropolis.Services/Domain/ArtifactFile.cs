namespace Metropolis.Api.Domain
{
    /// <summary>
    ///     Anything that isn't an executable unit of code. Build files, docker files, configuration and binary files are
    ///     examples
    /// </summary>
    public abstract class ArtifactFile : AbstractFile
    {
        protected ArtifactFile(Location path) : base(path)
        {
        }

        public ArtifactFileType Type { get; set; }
    }

    /// <summary>
    ///     ConfigutationFiles like web.config or web.xml or docker container files
    ///     BuildFiles like .xml for ANT or package.json for npm packages
    ///     BinaryAssets like assets like png, 3dmodels, and videos
    ///     Miscellaneous are random non-binary text files (e.g documentation, or anything else) 
    /// </summary>
    public enum ArtifactFileType
    {
        BuildFile,
        DeploymentFile,
        ConfigurationFile,
        BinaryAsset,
        Miscellaneous
    }
}