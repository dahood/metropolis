namespace Metropolis.Api.Domain
{
    /// <summary>
    ///     Anything that isn't an executable unit of code. Build files, docker files, configuration and binary files are
    ///     examples
    /// </summary>
    public class ArtifactFile : AbstractFile
    {
        public ArtifactFileType Type { get; set; }

        public CommitEntry CommitEntry { get; set; }

        // and ConfigutationFiles like web.config or web.xml or docker container files
        // but BuildFiles like .xml for ANT or package.json for npm packages
        // and BinaryAssets  like assets like png, 3dmodels, and videos
        // and OtherCodeFiles random non-binary text files (e.g documentation, or anything else that doesn't fit in the above catagories
    }

    public enum ArtifactFileType
    {
        BuildFile,
        DeploymentFile,
        ConfigurationFile,
        BinaryAsset,
        Miscellaneous
    }
}