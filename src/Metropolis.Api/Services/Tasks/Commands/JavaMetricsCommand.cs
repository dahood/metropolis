using System.Collections.Generic;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services.Tasks.Commands
{
    public class JavaMetricsCommand : IMetricsCommand
    {
        public IEnumerable<MetricsResult> Run(MetricsCommandArguments projectName)
        {
            throw new System.NotImplementedException();
        }
    }
}