using System;
using FluentAssertions;
using Metropolis.Api.Analyzers.Toxicity;
using NUnit.Framework;

namespace Metropolis.Test.Api.Analyzers.Toxicity
{
    [TestFixture]
    public class CSharpToxicityAnalyzerTest
    {
        private CSharpToxicityAnalyzer analyzer;
        const int ThresholdExceeded = 2;

        [SetUp]
        public void SetUp()
        {
            analyzer = new CSharpToxicityAnalyzer();
        }

        [Test]
        public void NotToxicWhenEverythingIsWithinTheThresholds_NoMembers()
        {
            var toAnalyse = AnalyzerFixture.HappyCSharpInstance;
            var score = analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(0);
        }

        [Test]
        public void ToxicOn_LinesOfCode_NoMembers()
        {
            var toAnalyse = AnalyzerFixture.Create(AnalyzerFixture.HappyCSharpInstance, x => x.LinesOfCode += ThresholdExceeded);
            var score = analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(Math.Log(ThresholdExceeded));
        }

        [Test]
        public void ToxicOn_ClassCoupling_NoMembers()
        {
            var toAnalyse = AnalyzerFixture.Create(AnalyzerFixture.HappyCSharpInstance, x => x.ClassCoupling += ThresholdExceeded);
            var score = analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(Math.Log(ThresholdExceeded));
        }

        [Test]
        public void ToxicOn_DepthOfInheritance_NoMembers()
        {
            var toAnalyse = AnalyzerFixture.Create(AnalyzerFixture.HappyCSharpInstance, x => x.DepthOfInheritance += ThresholdExceeded);
            var score = analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(Math.Log(ThresholdExceeded * CSharpToxicityAnalyzer.DepthOfInheritanceFactor));
        }
    }
}
