using System;
using System.Collections.Generic;
using Metropolis.Api.Services.Collection.Steps;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services.Collection
{
    public interface IMetricsStepFactory
    {
        ICollectionStep CommandFor(RepositorySourceType repositorySourcetype);
    }

    public class MetricsStepFactory : IMetricsStepFactory
    {
        private readonly Dictionary<RepositorySourceType, Func<ICollectionStep>> commandMap =
            new Dictionary<RepositorySourceType, Func<ICollectionStep>>
            {
                {RepositorySourceType.CSharp, () => new CSharpCollectionStep()},
                {RepositorySourceType.Java, () => new JavaCollectionStep()},
                {RepositorySourceType.ECMA, () => new EcmaCollectionStep()}
            };

        public ICollectionStep CommandFor(RepositorySourceType sourceType)
        {
            return commandMap[sourceType]();
        }
    }
}