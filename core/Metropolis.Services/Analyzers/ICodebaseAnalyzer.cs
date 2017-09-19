using System.Collections.Generic;
using Metropolis.Api.Domain;

namespace Metropolis.Api.Analyzers
{
    /// <summary>
    ///     Perform more complex parsing logic or calculations (e.g. calculating toxicity involves taking the natural log of
    ///     all the properties)
    /// </summary>
    public interface ICodebaseAnalyzer
    {
        CodeBase Analyze(List<Instance> toAnalyze);
    }
}