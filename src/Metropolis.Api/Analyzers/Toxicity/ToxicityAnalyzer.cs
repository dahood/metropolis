using System;
using System.Collections.Generic;
using Metropolis.Api.Domain;
using Metropolis.Api.Utilities;

namespace Metropolis.Api.Analyzers.Toxicity
{
    public abstract class ToxicityAnalyzer : ICodebaseAnalyzer
    {
        public CodeBase Analyze(List<Instance> toAnalyze)
        {
            toAnalyze.ForEach(c => c.Toxicity = CalculateToxicity(c).Toxicity);
            return new CodeBase(new CodeGraph(toAnalyze))
            {
                RunDate = Clock.Now
            };
        }

        public abstract ToxicityScore CalculateToxicity(Instance instanceToScore);

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
