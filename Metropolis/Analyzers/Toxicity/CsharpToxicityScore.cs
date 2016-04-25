using Metropolis.Domain;

namespace Metropolis.Analyzers.Toxicity
{
    public class CsharpToxicityScore : ToxicityScore
    {
        public CsharpToxicityScore(Class classToScore) : base(classToScore, 20)
        {
        }

    }
}
