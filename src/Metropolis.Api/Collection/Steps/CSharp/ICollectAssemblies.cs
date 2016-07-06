using System.Collections.Generic;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.CSharp
{
    public interface ICollectAssemblies
    {
        IEnumerable<string> GatherAssemblies(MetricsCommandArguments args);
    }
}
