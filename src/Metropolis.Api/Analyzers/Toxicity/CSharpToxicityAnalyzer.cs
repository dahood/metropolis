using Metropolis.Api.Domain;

namespace Metropolis.Api.Analyzers.Toxicity
{
    /// <summary>
    /// Developed by Richard and Jonathan while performing numerous code reviews for .NET projects
    /// </summary>
    public class CSharpToxicityAnalyzer : ToxicityAnalyzer
    {
        //class level thresholds
        private const int ThresholdLinesOfCode = 500;
        private const int ThresholdClassCoupling = 30;
        private const int ThresholdDepthOfInheritance = 3;
        private const int DepthOfInheritanceFactor = 5; // Increase the effect on toxicity when this rule is violated
        private const int ThresholdNumberOfMethods = 20;
        // method level thresholds
        private const int ThresholdMethodLength = 30;
        private const int ThresholdCyclomaticComplexity = 20; //higher for C# than Java due to LINQ

        public override ToxicityScore CalculateToxicity(Instance instanceToScore)
        {
            // Class Level Toxicity
            var linesOfCode = ComputeToxicity(instanceToScore.LinesOfCode, ThresholdLinesOfCode);
            var classCoupling = ComputeToxicity(instanceToScore.ClassCoupling, ThresholdClassCoupling);
            var depthOfInheritance = ComputeToxicity(instanceToScore.DepthOfInheritance, ThresholdDepthOfInheritance);
            var numberOfMethods = ComputeToxicity(instanceToScore.Members.Count, ThresholdNumberOfMethods);

            double cyclomaticComplexity = 0;
            double methodLength = 0;
            // Method Level Toxicity
            foreach (var method in instanceToScore.Members)
            {
                cyclomaticComplexity += ComputeToxicity(method.CylomaticComplexity, ThresholdCyclomaticComplexity);
                methodLength += ComputeToxicity(method.LinesOfCode, ThresholdMethodLength);
            }

            // Rationalize
            var score = new ToxicityScore
            {
                LinesOfCode = Rationalize(linesOfCode),
                ClassCoupling = Rationalize(classCoupling),
                DepthOfInheritance = Rationalize(depthOfInheritance*DepthOfInheritanceFactor),
                NumberOfMethods = Rationalize(numberOfMethods),
                MethodLength = Rationalize(methodLength),
                CyclomaticComplexity = Rationalize(cyclomaticComplexity)
            };


            score.Toxicity = score.LinesOfCode + score.ClassCoupling + score.DepthOfInheritance + 
                             score.NumberOfMethods + score.MethodLength + score.CyclomaticComplexity;

            return score;
        }
    }
}