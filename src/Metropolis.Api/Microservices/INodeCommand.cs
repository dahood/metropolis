namespace Metropolis.Api.Microservices
{
    public interface INodeCommand
    {
        void Run(string sourceDirectory, string metricsOutputDirectory);
    }
}