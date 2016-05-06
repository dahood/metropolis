using System.Collections.Generic;
using Metropolis.Api.Core.Domain;

namespace Metropolis.Api.Core.Analyzers
{
    /// <summary>
    ///     Perform more complex parsing logic or calculations (e.g. calculating toxicity involves taking the natural log of
    ///     all the properties)
    /// </summary>
    internal interface ICodebaseAnalyzer
    {
        CodeBase Analyze(List<Class> toAnalyze);
    }
}