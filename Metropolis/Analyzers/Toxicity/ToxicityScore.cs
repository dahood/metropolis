namespace Metropolis.Analyzers.Toxicity
{
    public class ToxicityScore
    {
        public double Toxicity { get; set; }

        //Common All
        public double LinesOfCode { get; set; }
        public double NumberOfMethods { get; set; }
        public double MethodLength { get; set; }
        public double CyclomaticComplexity { get; set; }
        public double ParameterNumber { get; set; }
        public double NestedIfDepth { get; set; }
        public double MissingSwitchDefault { get; set; }

        // Common OO (Java/C#)
        public double ClassCoupling { get; set; } //ClassFanOutComplexity by Java, VS metrics for C#

        //C#
        public double DepthOfInheritance { get; set; }

        //Java
        public double BooleanExpressionComplexity { get; set; }
        public double NestedTryDepth { get; set; }
        public double AnonInnerLength { get; set; }
        public double ClassDataAbstractionCoupling { get; set; }
        
        // ECMA / Javascript
        public double SwitchNoFallThrough { get; set; }
    }
}