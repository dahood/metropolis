using System;
using Metropolis.Api.Analyzers.Toxicity;
using Metropolis.Api.Domain;
using NUnit.Framework;

namespace Metropolis.Test.Api.Analyzers.Toxicity
{
    [TestFixture]
    public class EcmaToxicityAnalyzerTest : AbstractToxicityAnalyzerTest<JavascriptToxicityAnalyzer>
    {
        protected override int ThresholdNumberOfMembers => JavascriptToxicityAnalyzer.ThresholdNumberOfMethods;
        protected override int ThresholdMethodLength => JavascriptToxicityAnalyzer.ThresholdMethodLength;
        protected override int ThresholdCyclomaticComplexity => JavascriptToxicityAnalyzer.ThresholdCyclomaticComplexity;
        protected override Instance HealthyInstance => AnalyzerFixture.HealthEcmaInstance;

        protected override Instance CreateHealthyInstance(Action<Instance> initializer)
        {
            return AnalyzerFixture.Initialize(AnalyzerFixture.HealthEcmaInstance, initializer);
        }


    }
}