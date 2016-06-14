namespace Metropolis.Api.Collection.Steps.CSharp
{
    public interface IBuildEnvironment
    {
        string FxCopMetricsToolPath { get; }
        string MsBuildPath { get; }
    }
}
