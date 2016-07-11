using System;
using Metropolis.Api.Analyzers.Toxicity;
using Metropolis.Api.Domain;

namespace Metropolis.Test.Api.Analyzers.Toxicity
{
    public static class AnalyzerFixture
    {
        public static Instance HealthyCSharpInstance =>
            new Instance(CodeBag.Empty, "CSharp", new Location(@"c:\healthy.cs"))
            {
                LinesOfCode = CSharpToxicityAnalyzer.ThresholdLinesOfCode,
                ClassCoupling = CSharpToxicityAnalyzer.ThresholdClassCoupling,
                DepthOfInheritance = CSharpToxicityAnalyzer.ThresholdDepthOfInheritance,
                NumberOfMethods = CSharpToxicityAnalyzer.ThresholdNumberOfMethods
            };

        public static Instance HealthJavaInstance =>
            new Instance(CodeBag.Empty, "Java", new Location(@"c:\healthy.java"))
            {
                LinesOfCode = JavaToxicityAnalyzer.ThresholdLinesOfCode,
                NumberOfMethods = JavaToxicityAnalyzer.ThresholdNumberOfMethods,
                AnonymousInnerClassLength = JavaToxicityAnalyzer.ThresholdAnonymousInnerClassLength,
                ClassDataAbstractionCoupling = JavaToxicityAnalyzer.ThresholdClassDataAbstractionCoupling,
                ClassFanOutComplexity = JavaToxicityAnalyzer.ThresholdClassFanOutComplexity
            };

        public static Instance HealthEcmaInstance =>
            new Instance(CodeBag.Empty, "Ecma", new Location(@"c:\healthy.js"))
            {
                LinesOfCode = JavascriptToxicityAnalyzer.ThresholdLinesOfCode,
                NumberOfMethods = JavascriptToxicityAnalyzer.ThresholdNumberOfMethods
            };

        public static Instance Initialize(Instance target, Action<Instance> action)
        {
            action(target);
            return target;
        }

        public static Instance WithHealthyMember<T>(this Instance instance, string memberName, int methodLength = 0, int cyclomaticComplexity = 0) where T : ToxicityAnalyzer
        {
            if (typeof(T) == typeof(CSharpToxicityAnalyzer))
                return instance.WithHealthyCSharpMember(memberName, methodLength, cyclomaticComplexity);
            if (typeof(T) == typeof(JavaToxicityAnalyzer))
                return instance.WithHealthyJavaMember(memberName, methodLength, cyclomaticComplexity);
            if (typeof(T) == typeof(JavascriptToxicityAnalyzer))
                return instance.WithHealthyEcmaMember(memberName, methodLength, cyclomaticComplexity);

            throw new NotSupportedException($"{typeof(T).Name} is not supported");
        }
        private static Instance WithHealthyCSharpMember(this Instance instance, string memberName, int methodLength = CSharpToxicityAnalyzer.ThresholdMethodLength,
                                                                                     int cylcomaticComplexity = CSharpToxicityAnalyzer.ThresholdCyclomaticComplexity)
        {
            var member = new Member(memberName, methodLength, cylcomaticComplexity, CSharpToxicityAnalyzer.ThresholdClassCoupling);
            instance.AddMembers(new[] { member });
            return instance;
        }
        private static Instance WithHealthyEcmaMember(this Instance instance, string memberName, int methodLength = JavascriptToxicityAnalyzer.ThresholdMethodLength,
                                                                                    int cylcomaticComplexity = JavascriptToxicityAnalyzer.ThresholdCyclomaticComplexity)
        {
            var member = new Member(memberName, methodLength, cylcomaticComplexity, 0)
            {
                NumberOfParameters = JavascriptToxicityAnalyzer.ThresholdNumberOfParameters,
                NestedIfDepth = JavascriptToxicityAnalyzer.ThresholdNestedIfDepth,
                MissingDefaultCase = JavascriptToxicityAnalyzer.ThresholdMissingDefaultCase,
                NoFallthrough = JavascriptToxicityAnalyzer.ThresholdNoFallthrough
            };
            instance.AddMembers(new[] { member });
            return instance;
        }

        private static Instance WithHealthyJavaMember(this Instance instance, string memberName, int methodLength = CSharpToxicityAnalyzer.ThresholdMethodLength,
                                                                                     int cylcomaticComplexity = CSharpToxicityAnalyzer.ThresholdCyclomaticComplexity)
        {
            var member = new Member(memberName, methodLength, cylcomaticComplexity, 0)
            {
                MissingDefaultCase = JavaToxicityAnalyzer.ThresholdDefaultCase,
                BooleanExpressionComplexity = JavaToxicityAnalyzer.ThresholdBooleanComplexity,
                NestedIfDepth = JavaToxicityAnalyzer.ThresholdNestedIfDepth,
                NestedTryDepth = JavaToxicityAnalyzer.ThresholdNestedTryDepth,
                NumberOfParameters = JavaToxicityAnalyzer.ThresholdParameterNumber
            };
            instance.AddMembers(new[] { member });
            return instance;
        }
    }
}