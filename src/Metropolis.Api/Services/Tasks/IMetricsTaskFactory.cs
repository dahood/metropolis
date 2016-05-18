using System;
using System.Collections.Generic;
using Metropolis.Api.Services.Tasks.Commands;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services.Tasks
{
    public interface IMetricsTaskFactory
    {
        ICollectionStep CommandFor(RepositorySourceType repositorySourcetype);
    }

    public class MetricsTaskFactory : IMetricsTaskFactory
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