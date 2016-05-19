using System;
using System.Collections.Generic;
using Metropolis.Api.Collection.Steps;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection
{
    public class CollectionStepFactory : ICollectionStepFactory
    {
        private readonly Dictionary<RepositorySourceType, Func<ICollectionStep>> commandMap =
            new Dictionary<RepositorySourceType, Func<ICollectionStep>>
            {
                {RepositorySourceType.CSharp, () => new CSharpCollectionStep()},
                {RepositorySourceType.Java, () => new JavaCollectionStep()},
                {RepositorySourceType.ECMA, () => new EcmaCollectionStep()}
            };

        public ICollectionStep GetStep(RepositorySourceType sourceType)
        {
            return commandMap[sourceType]();
        }
    }
}