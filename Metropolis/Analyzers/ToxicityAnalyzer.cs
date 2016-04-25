using System;
using System.Collections.Generic;
using Metropolis.Domain;
using Metropolis.Analyzers.Toxicity;

namespace Metropolis.Analyzers
{
    public abstract class ToxicityAnalyzer : ICodebaseAnalyzer
    {
        public CodeBase Analyze(List<Class> toAnalyze)
        {
            foreach (var c in toAnalyze)
            {
                c.Toxicity = CalculateToxicity(c).Toxicity;
            }
            return new CodeBase(new CodeGraph(toAnalyze));
        }

        public abstract ToxicityScore CalculateToxicity(Class classToScore);

        protected static double ComputeToxicity(int measure, int threshold)
        {
            var difference = measure - threshold;
            return Math.Max(difference, 0);
        }

        protected static double Rationalize(double number)
        {
            return number > 0 ? Math.Log(number) : 0d;
        }
    }
}
