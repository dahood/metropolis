using System;
using Metropolis.Api.Analyzers.Toxicity;
using Metropolis.Api.Domain;

namespace Metropolis.Test.Api.Analyzers.Toxicity
{
    public static class AnalyzerFixture
    {
        public static Instance HappyCSharpInstance =>
            new Instance(CodeBag.Empty, "CSharp", new Location(@"c:\csharp.cs"))
            {
                LinesOfCode = CSharpToxicityAnalyzer.ThresholdLinesOfCode,
                ClassCoupling = CSharpToxicityAnalyzer.ThresholdClassCoupling,
                DepthOfInheritance = CSharpToxicityAnalyzer.ThresholdDepthOfInheritance,
                NumberOfMethods = CSharpToxicityAnalyzer.ThresholdNumberOfMethods
            };

        public static Instance Create(Instance target, Action<Instance> action)
        {
            action(target);
            return target;
        }
    }
}