using System;
using Metropolis.Api.Analyzers.Toxicity;
using Metropolis.Api.Domain;
using NUnit.Framework;

namespace Metropolis.Test.Api.Analyzers.Toxicity
{
    [TestFixture]
    public class JavaToxicityAnalyzerTest : AbstractToxicityAnalyzerTest<JavaToxicityAnalyzer>
    {
        protected override int ThresholdNumberOfMembers => JavaToxicityAnalyzer.ThresholdNumberOfMethods;
        protected override int ThresholdCyclomaticComplexity => JavaToxicityAnalyzer.ThresholdCyclomaticComplexity;
        protected override int ThresholdMethodLength => JavaToxicityAnalyzer.ThresholdMethodLength;

        protected override Instance HealthyInstance => AnalyzerFixture.HealthJavaInstance;

        protected override Instance CreateHealthyInstance(Action<Instance> initializer)
        {
            return AnalyzerFixture.Initialize(AnalyzerFixture.HealthJavaInstance, initializer);
        }
        
    }
}