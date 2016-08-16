namespace Metropolis.Api.Domain.Delta
{
    public class InstanceDelta : IInstance
    {
        public IInstance Original { get; set; }
        public IInstance Target { get; set; }

        public InstanceDelta(IInstance original, IInstance target)
        {
            Original = original;
            Target = target??Instance.CreateEmpty(original);
        }
        public string Name => Original.Name;
        public Location PhysicalPath => Original.PhysicalPath;
        public int NumberOfMethods => Target.NumberOfMethods - Original.NumberOfMethods;
        public int LinesOfCode => Target.LinesOfCode - Original.LinesOfCode;
        public int DepthOfInheritance => Target.DepthOfInheritance - Original.DepthOfInheritance;
        public int CyclomaticComplexity => Target.CyclomaticComplexity - Original.CyclomaticComplexity;
        public int ClassCoupling => Target.ClassCoupling - Original.ClassCoupling;
        public int AnonymousInnerClassLength => Target.AnonymousInnerClassLength - Original.AnonymousInnerClassLength;
        public int ClassFanOutComplexity => Target.ClassFanOutComplexity - Original.ClassFanOutComplexity;
        public int ClassDataAbstractionCoupling => Target.ClassDataAbstractionCoupling - Original.ClassDataAbstractionCoupling;
        public double Toxicity => Target.Toxicity - Original.Toxicity;
        public int DuplicateLines => Target.DuplicateLines - Original.DuplicateLines;
        public double DuplicatePercentage => Target.DuplicatePercentage - Original.DuplicatePercentage;
    }
}
