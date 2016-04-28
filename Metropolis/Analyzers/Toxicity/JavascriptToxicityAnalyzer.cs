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
        private const int ThresholdNumberOfParameters = 5;
        private const int ThresholdNestedIfDepth = 2;
        private const int ThresholdNestedTryDepth = 1;
        private const int ThresholdMissingDefaulCase = 0;
        private const int ThresholdNoFallthrough = 0;

        public override ToxicityScore CalculateToxicity(Class classToScore)
        {
            // Class Level Toxicity
            var linesOfCode = ComputeToxicity(classToScore.LinesOfCode, ThresholdLinesOfCode);
            var numberOfMethods = ComputeToxicity(classToScore.Members.Count, ThresholdNumberOfMethods);
            var numberOfParameters = 0d;
            var nestedIfDepth = 0d;
            var nestedTryDepth = 0d;
            var missingDefaultCase = 0d;
            var noFallThrough = 0d;


            double cyclomaticComplexity = 0;
            // Method Level Toxicity
            foreach (var method in classToScore.Members)
            {
                cyclomaticComplexity += ComputeToxicity(method.CylomaticComplexity, ThresholdCyclomaticComplexity);
                linesOfCode += ComputeToxicity(method.LinesOfCode, ThresholdMethodLength);
                numberOfParameters += ComputeToxicity(method.NumberOfParameters, ThresholdNumberOfParameters);
                nestedIfDepth += ComputeToxicity(method.NestedIfDepth, ThresholdNestedIfDepth);
                nestedTryDepth += ComputeToxicity(method.NestedTryDepth, ThresholdNestedTryDepth);
                missingDefaultCase += ComputeToxicity(method.MissingDefaultCase, ThresholdMissingDefaulCase);
                noFallThrough += ComputeToxicity(method.NoFallthrough, ThresholdNoFallthrough);
            }

            // Rationalize
            var score = new ToxicityScore
            {
                LinesOfCode = Rationalize(linesOfCode),
                NumberOfMethods = Rationalize(numberOfMethods),
                CyclomaticComplexity = Rationalize(cyclomaticComplexity),
                NumberOfParameters =  Rationalize(numberOfParameters),
                NestedIfDepth = Rationalize(nestedIfDepth),
                NestedTryDepth = Rationalize(nestedTryDepth),
                MissingDefaultCase = Rationalize(missingDefaultCase),
                SwitchNoFallThrough = Rationalize(noFallThrough)
            };

            score.Toxicity = score.LinesOfCode + score.NumberOfMethods +
                             score.CyclomaticComplexity + score.NumberOfParameters +
                             score.NestedIfDepth + score.NestedTryDepth +
                             score.MissingDefaultCase + score.SwitchNoFallThrough;

            return score;
        }

    }
}