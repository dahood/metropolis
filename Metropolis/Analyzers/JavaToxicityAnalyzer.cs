using System.Collections.Generic;
using Metropolis.Analyzers.Toxicity;
using Metropolis.Domain;

namespace Metropolis.Analyzers
{
    class JavaToxicityAnalyzer : ICodebaseAnalyzer
    {
        public CodeBase Analyze(List<Class> toAnalyze)
        {
            foreach (var c in toAnalyze)
            {
                c.Toxicity = new JavaToxicityScore(c).Toxicity;
            }
            return new CodeBase(new CodeGraph(toAnalyze));
        }
    }
}
