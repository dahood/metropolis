using System.Collections.Generic;
using Metropolis.Common.Models;

namespace Metropolis.Api.Microservices.Tasks.Commands
{
    public interface IMetricsCommand
    {
        IEnumerable<MetricsResult> Run(MetricsCommandArguments projectName);
    }
}