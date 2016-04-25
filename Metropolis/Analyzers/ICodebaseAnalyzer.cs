using System.Collections.Generic;
using Metropolis.Domain;

namespace Metropolis.Analyzers
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