using Metropolis.Api.Domain;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services
{
    public interface IAnalysisService
    {
        CodeBase Analyze(MetricsCommandArguments metricsCommandArguments);
    }
}