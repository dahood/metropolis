using Metropolis.Domain;

namespace Metropolis.Analyzers.Toxicity
{
    public class JavaToxicityAnalyzer : ToxicityAnalyzer
    {
        //class level thresholds
        private const int ThresholdLinesOfCode = 500;
        private const int ThresholdNumberOfMethods = 20;
        // method level thresholds
        private const int ThresholdMethodLength = 30;
        private const int thresholdCyclomaticComplexity = 15;

        public override ToxicityScore CalculateToxicity(Class classToScore)
        {
            // Class Level Toxicity
            var linesOfCode = ComputeToxicity(classToScore.LinesOfCode, ThresholdLinesOfCode);
            var numberOfMethods = ComputeToxicity(classToScore.Members.Count, ThresholdNumberOfMethods);

            double cyclomaticComplexity = 0;
            double methodLength = 0;
            // Method Level Toxicity
            foreach (var method in classToScore.Members)
            {
                cyclomaticComplexity += ComputeToxicity(method.CylomaticComplexity, thresholdCyclomaticComplexity);
                methodLength += ComputeToxicity(method.LinesOfCode, ThresholdMethodLength);
            }

            // Rationalize
            var score = new ToxicityScore();
            score.LinesOfCode = Rationalize(linesOfCode);
            score.MethodLength = Rationalize(methodLength);
            score.NumberOfMethods = Rationalize(numberOfMethods);
            score.CyclomaticComplexity = Rationalize(cyclomaticComplexity);

            score.Toxicity = score.LinesOfCode + score.ClassCoupling +
                             score.DepthOfInheritance + score.NumberOfMethods +
                             score.CyclomaticComplexity;

            return score;
        }
    }
}
