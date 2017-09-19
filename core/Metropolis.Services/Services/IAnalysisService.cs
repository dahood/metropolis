using Metropolis.Common.Models;
using Metropolis.Api.Domain;

namespace Metropolis.Api.Services
{
    public interface IAnalysisServiceCore
    {
        CodeBase Analyze(MetricsCommandArguments details);
    }
}