using System;
using System.Collections.Generic;
using Metropolis.Api.Collection.Steps;
/*using Metropolis.Api.Collection.Steps.CSharp;*/
using Metropolis.Api.Collection.Steps.ECMA;
using Metropolis.Api.Collection.Steps.Java;
using Metropolis.Common.Models;
using Metropolis.Api.Collection;
using Metropolis.Api.Collection.Steps.AllLanguages;

using Microsoft.Extensions.Logging;

namespace Metropolis.Api.Collection
{
    public class CollectionStepFactory : ICollectionStepFactory
    {
        public CollectionStepFactory() 
        {
            commandMap  =
            new Dictionary<RepositorySourceType, Func<ICollectionStep>>
            {
                {RepositorySourceType.CSharp, () =>  null},
                {RepositorySourceType.Java, () => new JavaCollectionStep()},
                {RepositorySourceType.ECMA, () => new EcmaCollectionStep()}
            };
        }
        private readonly Dictionary<RepositorySourceType, Func<ICollectionStep>> commandMap;

        public ICollectionStep GetStep(RepositorySourceType sourceType)
        {
            return commandMap[sourceType]();
        }
    }
}