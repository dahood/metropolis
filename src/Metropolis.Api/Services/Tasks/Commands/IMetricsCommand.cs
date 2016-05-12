using System.Collections.Generic;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services.Tasks.Commands
{
    public interface IMetricsCommand
    {
        IEnumerable<MetricsResult> Run(MetricsCommandArguments projectName);
    }
}