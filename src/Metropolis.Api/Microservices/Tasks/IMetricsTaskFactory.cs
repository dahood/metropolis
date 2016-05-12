using Metropolis.Api.Microservices.Tasks.Commands;
using Metropolis.Common.Models;

namespace Metropolis.Api.Microservices.Tasks
{
    public interface IMetricsTaskFactory
    {
        IMetricsCommand CommandFor(RepositorySourceType repositorySourcetype);
    }

    public class MetricsTaskFactory : IMetricsTaskFactory
    {
        public IMetricsCommand CommandFor(RepositorySourceType repositorySourcetype)
        {
            throw new System.NotImplementedException();
        }
    }
}