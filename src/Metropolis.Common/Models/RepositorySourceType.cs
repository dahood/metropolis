namespace Metropolis.Common.Models
{
    /// <summary>
    /// This is the source type of the code you are analyzing. Many things depend on knowing this like:
    /// - Toxicity analyzer to use
    /// - What you name your CodeBags (e.g. Package = Java, Namespace = C#, Directories = Javascript
    /// - What you call your Instances (e.g. Class, File, Module)
    /// </summary>
    public enum RepositorySourceType { CSharp, Java, ECMA  }
}

