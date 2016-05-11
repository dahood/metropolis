using System;
using System.Collections.Generic;
using Metropolis.Api.Core.Domain;

namespace Metropolis.Api.Core.Analyzers.Toxicity
{
    public abstract class ToxicityAnalyzer : ICodebaseAnalyzer
    {
        public CodeBase Analyze(List<Instance> toAnalyze)
        {
            // TODO:  PLINQ doesn't speed this up... probably using this incorrectly
            //toAnalyze.AsParallel().ForAll(x => x.Toxicity = CalculateToxicity(x).Toxicity);
            toAnalyze.ForEach(c => c.Toxicity = CalculateToxicity(c).Toxicity);
            return new CodeBase(new CodeGraph(toAnalyze));
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
