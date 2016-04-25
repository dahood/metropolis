using Metropolis.Analyzers.Toxicity;
using Metropolis.Domain;

namespace Metropolis.Analyzers
{
    public class JavaToxicityAnalyzer : ToxicityAnalyzer
    {
        //class level thresholds
        private const int ThresholdLinesOfCode = 500;
        private const int ThresholdClassCoupling = 30;
        private const int ThresholdDepthOfInheritance = 3;
        private const int DepthOfInheritanceFactor = 5; // Increase the effect on toxicity when this rule is violated
        private const int ThresholdNumberOfMethods = 20;
        // method level thresholds
        private const int ThresholdMethodLength = 30;
        private const int thresholdCyclomaticComplexity = 15;

        public override ToxicityScore CalculateToxicity(Class classToScore)
        {
            //TODO: replace with CheckStyle data sources...there are more than C#

            // Class Level Toxicity
            var linesOfCode = ComputeToxicity(classToScore.LinesOfCode, ThresholdLinesOfCode);
            var classCoupling = ComputeToxicity(classToScore.ClassCoupling, ThresholdClassCoupling);
            var depthOfInheritance = ComputeToxicity(classToScore.DepthOfInheritance, ThresholdDepthOfInheritance);
            var numberOfMethods = ComputeToxicity(classToScore.Members.Count, ThresholdNumberOfMethods);

            double cyclomaticComplexity = 0;
            // Method Level Toxicity
            foreach (var method in classToScore.Members)
            {
                cyclomaticComplexity += ComputeToxicity(method.CylomaticComplexity, thresholdCyclomaticComplexity);
                linesOfCode += ComputeToxicity(method.LinesOfCode, ThresholdMethodLength);
            }

            // Rationalize
            var score = new ToxicityScore();
            score.LinesOfCode = Rationalize(linesOfCode);
            score.ClassCoupling = Rationalize(classCoupling);
            score.DepthOfInheritance = Rationalize(depthOfInheritance * DepthOfInheritanceFactor);
            score.NumberOfMethods = Rationalize(numberOfMethods);
            score.CyclomaticComplexity = Rationalize(cyclomaticComplexity);

            score.Toxicity = score.LinesOfCode + score.ClassCoupling +
                             score.DepthOfInheritance + score.NumberOfMethods +
                             score.CyclomaticComplexity;

            return score;
        }
    }
}
