namespace Metropolis.Api.Collection.Steps.CSharp
{
    public interface IDotNetEnvironment
    {
        string FxCopMetricsToolPath { get; }
        string MsBuildPath { get; }
    }
}
