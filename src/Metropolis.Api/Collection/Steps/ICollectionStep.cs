using System.Collections.Generic;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps
{
    public interface ICollectionStep
    {
        IEnumerable<MetricsResult> Run(MetricsCommandArguments args);
    }
}