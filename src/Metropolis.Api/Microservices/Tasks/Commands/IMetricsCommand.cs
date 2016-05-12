using System.Collections.Generic;
using Metropolis.Common.Models;

namespace Metropolis.Api.Microservices.Tasks.Commands
{
    public interface IMetricsCommand
    {
        IEnumerable<MetricsResult> Run(string projectName, string sourceDirectory, string metricsOutputDirectory);
    }
}