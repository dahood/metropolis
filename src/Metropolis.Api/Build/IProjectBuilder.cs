using Metropolis.Common.Models;

namespace Metropolis.Api.Build
{
    public interface IProjectBuilder
    {
        void Build(ProjectBuildArguments buildArgs);
    }
}