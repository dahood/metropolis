using Metropolis.Domain;

namespace Metropolis.Analyzers.Toxicity
{
    public class JavaToxicityScore : ToxicityScore
    {
        public JavaToxicityScore(Class classToScore) : base(classToScore)
        {
            // no overrides/differences yet
            // eventually need to modify Toxicity Score to allow for Erik other metrics - NOP, Boolean switch, ... can always ADD more metrics
            // because natural log should help us
        }
    }
}
