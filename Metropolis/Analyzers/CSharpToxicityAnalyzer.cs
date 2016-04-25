using System.Collections.Generic;
using Metropolis.Analyzers.Toxicity;
using Metropolis.Domain;

namespace Metropolis.Analyzers
{
    public class CSharpToxicityAnalyzer : ICodebaseAnalyzer
    {
        public CodeBase Analyze(List<Class> toAnalyze)
        {
            foreach (var c in toAnalyze)
            {
                c.Toxicity = new CsharpToxicityScore(c).Toxicity;
            }
            return new CodeBase(new CodeGraph(toAnalyze));
        }
    }
}