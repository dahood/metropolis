namespace Metropolis.Api.Domain
{
    public interface IInstance
    {
        string Name { get; }
        Location PhysicalPath { get; }
        int NumberOfMethods { get; }
        int LinesOfCode { get; }
        int DepthOfInheritance { get; }
        int CyclomaticComplexity { get; }
        int ClassCoupling { get;  }
        int AnonymousInnerClassLength { get; }
        int ClassFanOutComplexity { get; }
        int ClassDataAbstractionCoupling { get; }
        double Toxicity { get;  }
        int DuplicateLines { get; }
        double DuplicatePercentage { get; }
    }
}