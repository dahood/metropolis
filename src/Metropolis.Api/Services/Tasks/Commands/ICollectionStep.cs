using System.Collections.Generic;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services.Tasks.Commands
{
    public interface ICollectionStep
    {
        IEnumerable<MetricsResult> Run(MetricsCommandArguments args);
    }
}