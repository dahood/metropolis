using Metropolis.Domain;

namespace Metropolis.Analyzers.Toxicity
{
    /// <summary>
    /// Mostly taken from Erik Doernenberg's 2008 toxicity reloaded article 
    /// http://erik.doernenburg.com/2008/11/how-toxic-is-your-code/
    /// </summary>
    public class JavaToxicityAnalyzer : ToxicityAnalyzer
    {
        //class level thresholds
        private const int ThresholdLinesOfCode = 500;
        private const int ThresholdNumberOfMethods = 20;
        private const int ThresholdAnonymousInnerClassLength = 35;
        private const int ThresholdClassDataAbstractionCoupling = 10;
        private const int ThresholdClassFanOutComplexity = 30;

        // method level thresholds
        private const int ThresholdMethodLength = 30;
        private const int thresholdCyclomaticComplexity = 15;
        private const int ThresholdDefaultCase = 0; //not acceptable to miss a default: - could cause unexpected bug
        private const int ThresholdBooleanComplexity = 3;
        private const int ThresholdNestedIfDepth = 3;
        private const int ThresholdNestedTryDepth = 2;
        private const int ThresholdParameterNumber = 6;


        public override ToxicityScore CalculateToxicity(Class classToScore)
        {
            // Class Level Toxicity
            var linesOfCode = ComputeToxicity(classToScore.LinesOfCode, ThresholdLinesOfCode);
            var numberOfMethods = ComputeToxicity(classToScore.Members.Count, ThresholdNumberOfMethods);
            var innerClassAnonymous = ComputeToxicity(classToScore.AnonymousInnerClassLenth, ThresholdAnonymousInnerClassLength);
            var classDataAbstractionCoupling = ComputeToxicity(classToScore.ClassDataAbstractionCoupling, ThresholdClassDataAbstractionCoupling);
            var classFanOutComplexity = ComputeToxicity(classToScore.ClassCoupling, ThresholdClassFanOutComplexity);

            // Method Level Toxicity
            double cyclomaticComplexity = 0;
            double methodLength = 0;
            double missingDefaultCase = 0;
            double booleanComplexity = 0;
            double nestedIfDepth = 0;
            double nestedTryDepth = 0;
            double parameterNumber = 0;
            
            foreach (var method in classToScore.Members)
            {
                cyclomaticComplexity += ComputeToxicity(method.CylomaticComplexity, thresholdCyclomaticComplexity);
                methodLength += ComputeToxicity(method.LinesOfCode, ThresholdMethodLength);
                missingDefaultCase += ComputeToxicity(method.MissingDefaultCase, ThresholdDefaultCase);
                booleanComplexity += ComputeToxicity(method.BooleanExpressionComplexity, ThresholdBooleanComplexity);
                nestedIfDepth += ComputeToxicity(method.NestedIfDepth, ThresholdNestedIfDepth);
                nestedTryDepth += ComputeToxicity(method.NestedTryDepth, ThresholdNestedTryDepth);
                parameterNumber += ComputeToxicity(method.NumberOfParameters, ThresholdParameterNumber);
            }

            // Rationalize
            var score = new ToxicityScore();
            // class level
            score.LinesOfCode = Rationalize(linesOfCode);
            score.NumberOfMethods = Rationalize(numberOfMethods);
            score.AnonInnerLength = Rationalize(innerClassAnonymous);
            // TODO: break this out to two distinct measures?
            score.ClassCoupling = Rationalize(classDataAbstractionCoupling + classFanOutComplexity);

            // method level
            score.MethodLength = Rationalize(methodLength);
            score.CyclomaticComplexity = Rationalize(cyclomaticComplexity);
            score.MissingSwitchDefault = Rationalize(missingDefaultCase);
            score.BooleanExpressionComplexity = Rationalize(booleanComplexity);

            score.Toxicity = score.LinesOfCode + score.NumberOfMethods + score.ClassCoupling + score.AnonInnerLength +
                             score.MethodLength + score.CyclomaticComplexity + score.MissingSwitchDefault + score.BooleanExpressionComplexity;

            return score;
        }
    }
}
