namespace Metropolis.Api.IO
{
    public interface IBuildEnvironment
    {
        string FxCopMetricsToolPath { get; }
        string MsBuildPath { get; }
    }
}
