namespace Metropolis.Analyzers.Toxicity
{
    public class ToxicityScore
    {
        public double Toxicity { get; set; }
        public double LinesOfCode { get; set; }
        public double ClassCoupling { get; set; }
        public double DepthOfInheritance { get; set; }
        public double NumberOfMethods { get; set; }
        public double CyclomaticComplexity { get; set; }
    }
}