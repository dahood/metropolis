using Metropolis.Domain;

namespace Metropolis.Analyzers.Toxicity
{
    public class JavascriptToxicityAnalyzer : ToxicityAnalyzer
    {
        // Ecma file level thresholds
        private const int ThresholdLinesOfCode = 500;
        private const int ThresholdNumberOfMethods = 20;
        // Function level thresholds
        private const int ThresholdMethodLength = 30;
        private const int ThresholdCyclomaticComplexity = 10;
        private const int ThresholdNestedIfDepthy = 2;
        private const int ThresholdNestedTryDepth = 1;

        public override ToxicityScore CalculateToxicity(Class classToScore)
        {
            // Class Level Toxicity
            var linesOfCode = ComputeToxicity(classToScore.LinesOfCode, ThresholdLinesOfCode);
            var numberOfMethods = ComputeToxicity(classToScore.Members.Count, ThresholdNumberOfMethods);

            double cyclomaticComplexity = 0;
            // Method Level Toxicity
            foreach (var method in classToScore.Members)
            {
                cyclomaticComplexity += ComputeToxicity(method.CylomaticComplexity, ThresholdCyclomaticComplexity);
                linesOfCode += ComputeToxicity(method.LinesOfCode, ThresholdMethodLength);
            }

            // Rationalize
            var score = new ToxicityScore
            {
                LinesOfCode = Rationalize(linesOfCode),
                NumberOfMethods = Rationalize(numberOfMethods),
                CyclomaticComplexity = Rationalize(cyclomaticComplexity)
            };

            score.Toxicity = score.LinesOfCode + score.NumberOfMethods +
                             score.CyclomaticComplexity;

            return score;
        }
    }
}