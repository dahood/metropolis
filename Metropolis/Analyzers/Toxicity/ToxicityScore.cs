using System;
using Metropolis.Domain;

namespace Metropolis.Analyzers.Toxicity
{
    public abstract class ToxicityScore
    {
        //class level thresholds
        private const int ThresholdLinesOfCode = 500;
        private const int ThresholdClassCoupling = 30;
        private const int ThresholdDepthOfInheritance = 3;
        private const int DepthOfInheritanceFactor = 5; // Increase the effect on toxicity when this rule is violated
        private const int ThresholdNumberOfMethods = 20;
        // method level thresholds
        private const int ThresholdMethodLength = 30;
        private readonly int thresholdCyclomaticComplexity;

        private readonly Class classToScore;
        private double linesOfCode;
        private double classCoupling;
        private double depthOfInheritance;
        private double numberOfMethods;
        private double cyclomaticComplexity;

        protected ToxicityScore(Class classToScore, int thresholdCyclomaticComplexity = 10)
        {
            this.classToScore = classToScore;
            this.thresholdCyclomaticComplexity = thresholdCyclomaticComplexity;
            Calculate();
        }

        public double Toxicity => LinesOfCode + ClassCoupling + DepthOfInheritance + NumberOfMethods + CyclomaticComplexity;
        public double LinesOfCode => Rationalize(linesOfCode);
        public double ClassCoupling => Rationalize(classCoupling);
        public double DepthOfInheritance => Rationalize(depthOfInheritance * DepthOfInheritanceFactor);
        public double NumberOfMethods => Rationalize(numberOfMethods);
        public double CyclomaticComplexity => Rationalize(cyclomaticComplexity);

        private void Calculate()
        {
            linesOfCode = ComputeToxicity(classToScore.LinesOfCode, ThresholdLinesOfCode);
            classCoupling = ComputeToxicity(classToScore.ClassCoupling, ThresholdClassCoupling);
            depthOfInheritance = ComputeToxicity(classToScore.DepthOfInheritance, ThresholdDepthOfInheritance);
            numberOfMethods = ComputeToxicity(classToScore.Members.Count, ThresholdNumberOfMethods);

            // Method Level Toxicity
            foreach (var method in classToScore.Members)
            {
                cyclomaticComplexity += ComputeToxicity(method.CylomaticComplexity, thresholdCyclomaticComplexity);
                linesOfCode += ComputeToxicity(method.LinesOfCode, ThresholdMethodLength);
            }
        }
        private static double ComputeToxicity(int measure, int threshold)
        {
            var difference = measure - threshold;
            return Math.Max(difference, 0);
        }

        private static double Rationalize(double number)
        {
            return number > 0 ? Math.Log(number) : 0d;
        }
    }
}