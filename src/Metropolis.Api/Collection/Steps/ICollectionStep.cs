using System.Collections.Generic;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps
{
    public interface ICollectionStep
    {
        IEnumerable<MetricsResult> Run(MetricsCommandArguments args);
        /// <summary>
        /// returns error message if not valid, otherwise string.Empty
        /// </summary>
        /// <param name="fileNametoValidate"></param>
        /// <returns></returns>
        string ValidateMetricResults(string fileNametoValidate);
    }
}