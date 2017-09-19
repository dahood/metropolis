using Metropolis.Api.Collection.Steps;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection
{
    public interface ICollectionStepFactory
    {
        ICollectionStep GetStep(RepositorySourceType repositorySourcetype);
    }
}