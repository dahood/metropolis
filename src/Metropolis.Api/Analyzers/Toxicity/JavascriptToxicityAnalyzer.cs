using Metropolis.Api.Domain;

namespace Metropolis.Api.Analyzers.Toxicity
{
    /// <summary>
    /// Developed by Greg and Jonathan when reviewing AngularJS codebases
    /// </summary>
    public class JavascriptToxicityAnalyzer : ToxicityAnalyzer
    {
        // Ecma file level thresholds
        public const int ThresholdLinesOfCode = 500;
        public const int ThresholdNumberOfMethods = 20;
        // Function level thresholds
        public const int ThresholdMethodLength = 30;
        public const int ThresholdCyclomaticComplexity = 10;
        public const int ThresholdNumberOfParameters = 5;
        public const int ThresholdNestedIfDepth = 2;
        public const int ThresholdMissingDefaultCase = 0;
        public const int ThresholdNoFallthrough = 0;

        public override ToxicityScore CalculateToxicity(Instance instanceToScore)
        {
            // Class Level Toxicity
            var linesOfCode = ComputeToxicity(instanceToScore.LinesOfCode, ThresholdLinesOfCode);
            var numberOfMethods = ComputeToxicity(instanceToScore.Members.Count, ThresholdNumberOfMethods);
            var methodLength = 0d;
            var numberOfParameters = 0d;
            var nestedIfDepth = 0d;
            var missingDefaultCase = 0d;
            var noFallThrough = 0d;

            double cyclomaticComplexity = 0;
            // Method Level Toxicity
            foreach (var method in instanceToScore.Members)
            {
                cyclomaticComplexity += ComputeToxicity(method.CylomaticComplexity, ThresholdCyclomaticComplexity);
                methodLength += ComputeToxicity(method.LinesOfCode, ThresholdMethodLength);
                numberOfParameters += ComputeToxicity(method.NumberOfParameters, ThresholdNumberOfParameters);
                nestedIfDepth += ComputeToxicity(method.NestedIfDepth, ThresholdNestedIfDepth);
                missingDefaultCase += ComputeToxicity(method.MissingDefaultCase, ThresholdMissingDefaultCase);
                noFallThrough += ComputeToxicity(method.NoFallthrough, ThresholdNoFallthrough);
            }

            // Rationalize
            var score = new ToxicityScore
            {
                LinesOfCode = Rationalize(linesOfCode),
                NumberOfMethods = Rationalize(numberOfMethods),
                MethodLength = Rationalize(methodLength),
                CyclomaticComplexity = Rationalize(cyclomaticComplexity),
                NumberOfParameters =  Rationalize(numberOfParameters),
                NestedIfDepth = Rationalize(nestedIfDepth),
                MissingDefaultCase = Rationalize(missingDefaultCase),
                SwitchNoFallThrough = Rationalize(noFallThrough)
            };

            score.Toxicity = score.LinesOfCode + score.NumberOfMethods +
                             score.CyclomaticComplexity + score.NumberOfParameters + score.MethodLength +
                             score.NestedIfDepth + score.MissingDefaultCase + score.SwitchNoFallThrough;

            return score;
        }

    }
}