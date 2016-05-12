using System.Collections.Generic;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services.Tasks.Commands
{
    public class CSharpMetricsCommand : IMetricsCommand
    {
        public IEnumerable<MetricsResult> Run(MetricsCommandArguments projectName)
        {
            throw new System.NotImplementedException();
        }
    }
}