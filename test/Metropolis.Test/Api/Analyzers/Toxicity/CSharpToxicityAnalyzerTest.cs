using System;
using FluentAssertions;
using Metropolis.Api.Analyzers.Toxicity;
using Metropolis.Api.Domain;
using NUnit.Framework;

namespace Metropolis.Test.Api.Analyzers.Toxicity
{
    [TestFixture]
    public class CSharpToxicityAnalyzerTest : AbstractToxicityAnalyzerTest<CSharpToxicityAnalyzer>
    {    
        protected override int ThresholdNumberOfMembers => CSharpToxicityAnalyzer.ThresholdNumberOfMethods;
        protected override int ThresholdCyclomaticComplexity => CSharpToxicityAnalyzer.ThresholdCyclomaticComplexity;
        protected override int ThresholdMethodLength => CSharpToxicityAnalyzer.ThresholdMethodLength;

        protected override Instance HealthyInstance => AnalyzerFixture.HealthyCSharpInstance;

        protected override Instance CreateHealthyInstance(Action<Instance> initializer)
        {
            return AnalyzerFixture.Initialize(AnalyzerFixture.HealthyCSharpInstance, initializer);
        }
        
        [Test]
        public void ToxicOn_DepthOfInheritance_NoMembers()
        {
            var toAnalyse = CreateHealthyInstance(x => x.DepthOfInheritance += ThresholdExceeded);
            var score = Analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(Math.Log(ThresholdExceeded * CSharpToxicityAnalyzer.DepthOfInheritanceFactor));
        }

        [Test]
        public void ToxicOn_ClassCoupling_NoMembers()
        {
            var toAnalyse = CreateHealthyInstance(x => x.ClassCoupling += ThresholdExceeded);
            var score = Analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(Math.Log(ThresholdExceeded));
        }

    }
}
