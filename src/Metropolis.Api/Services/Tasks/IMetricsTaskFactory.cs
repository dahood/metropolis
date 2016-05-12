using System;
using System.Collections.Generic;
using Metropolis.Api.Services.Tasks.Commands;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services.Tasks
{
    public interface IMetricsTaskFactory
    {
        IMetricsCommand CommandFor(RepositorySourceType repositorySourcetype);
    }

    public class MetricsTaskFactory : IMetricsTaskFactory
    {
        private readonly Dictionary<RepositorySourceType, Func<IMetricsCommand>> commandMap = 
            new Dictionary<RepositorySourceType, Func<IMetricsCommand>>
            {
                {RepositorySourceType.CSharp, () => new CSharpMetricsCommand() },
                {RepositorySourceType.Java, () => new JavaMetricsCommand() },
                {RepositorySourceType.ECMA, () => new EcmaMetricsCommand() }
            };

        public IMetricsCommand CommandFor(RepositorySourceType sourceType)
        {
            return commandMap[sourceType]();
        }
    }
}