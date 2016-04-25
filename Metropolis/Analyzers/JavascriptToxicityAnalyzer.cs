using System.Collections.Generic;
using Metropolis.Analyzers.Toxicity;
using Metropolis.Domain;

namespace Metropolis.Analyzers
{
    class JavascriptToxicityAnalyzer : ICodebaseAnalyzer
    {
        public CodeBase Analyze(List<Class> toAnalyze)
        {
            foreach (var c in toAnalyze)
            {
                c.Toxicity = new JavascriptToxicityScore(c).Toxicity;
            }
            return new CodeBase(new CodeGraph(toAnalyze));
        }
    }
}