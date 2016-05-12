using Metropolis.Api.Core.Domain;
using Metropolis.Common.Models;

namespace Metropolis.Api.Microservices
{
    public interface IAnalysisService
    {
        CodeBase Analyze(ProjectDetails projectDetails);
    }
}