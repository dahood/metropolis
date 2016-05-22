using System;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.CSharp
{
    public interface IFxCopCollectionTask
    {
        MetricsResult Run(MetricsCommandArguments args, string each);
    }
}