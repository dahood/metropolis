using System;
using Metropolis.Common.Models;

namespace Metropolis.Api.Build
{
    public interface IProjectBuildFactory
    {
        IProjectBuilder BuilderFor(RepositorySourceType sourceType);
    }

    public class ProjectBuildFactory : IProjectBuildFactory
    {
        public IProjectBuilder BuilderFor(RepositorySourceType sourceType)
        {
            if (sourceType == RepositorySourceType.CSharp)
                return new DotNetProjectBuilder();
            throw new NotSupportedException($"Currently there is not builder for {sourceType}");
        }
    }
}
