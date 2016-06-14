using System.Collections.Generic;
using Metropolis.Common.Models;

namespace Metropolis.Api.Build
{
    public interface IProjectBuilder
    {
        ProjectBuildResult Build(ProjectBuildArguments buildArgs);
    }
}