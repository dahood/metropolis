namespace Metropolis.Api.Analyzers.Toxicity
{
    public class ToxicityScore
    {
        public double Toxicity { get; set; }

        //Common 
        public double NumberOfMethods { get; set; }
        public double MethodLength { get; set; }
        public double CyclomaticComplexity { get; set; }

        // Mostly Common (2 or more)
        public double LinesOfCode { get; set; } // Class (Java/C#) or File (ECMA)
        public double ParameterNumber { get; set; } // C# doesn't use this
        public double NestedIfDepth { get; set; } // Java & ECMA only
        public double MissingDefaultCase { get; set; } // Java & ECMA only
        public double ClassCoupling { get; set; } //ClassFanOutComplexity by Java, VS metrics for C#, Not used by ECMA

        //C#
        public double DepthOfInheritance { get; set; }

        //Java
        public double BooleanExpressionComplexity { get; set; }
        public double NestedTryDepth { get; set; }
        public double AnonInnerLength { get; set; }
        public double ClassDataAbstractionCoupling { get; set; }
        
        // ECMA / Javascript
        public double SwitchNoFallThrough { get; set; }
        public double ClassFanOutComplexity { get; set; }
        public double NumberOfParameters { get; set; }
    }
}