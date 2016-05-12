using Metropolis.Common.Models;

namespace Metropolis.Api.Microservices
{
    public interface INodeCommandFactory
    {
        INodeCommand CommandFor(RepositorySourceType repositorySourcetype);
    }

    public class NodeCommandFactory : INodeCommandFactory
    {
        public INodeCommand CommandFor(RepositorySourceType repositorySourcetype)
        {
            throw new System.NotImplementedException();
        }
    }
}