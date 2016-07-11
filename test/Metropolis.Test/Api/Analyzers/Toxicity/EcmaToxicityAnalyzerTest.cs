using System;
using FluentAssertions;
using Metropolis.Api.Analyzers.Toxicity;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;
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

        [Test]
        public void Healthy_NumberOfMembers()
        {
            var toAnalyse = HealthyInstance;
            ThresholdNumberOfMembers.ForEach(x => toAnalyse.WithHealthyMember<JavascriptToxicityAnalyzer>($"Member{x}"));

            var score = Analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(0);
        }

        [Test]
        public void ToxicOn_NumberOfMembers()
        {
            var toAnalyse = HealthyInstance;
            (ThresholdNumberOfMembers + 1).ForEach(x => toAnalyse.WithHealthyMember<JavascriptToxicityAnalyzer>($"Member{x}"));

            var score = Analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(Math.Log(1));
        }

        [Test]
        public void ToxicOn_LinesOfCode_NoMembers()
        {
            var toAnalyse = CreateHealthyInstance(x => x.LinesOfCode += ThresholdExceeded);
            var score = Analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(Math.Log(ThresholdExceeded));
        }

        [Test]
        public void NotToxic_WhenEverythingIsWithinTheThresholds_OneMember()
        {
            var toAnalyse = HealthyInstance.WithHealthyMember<JavascriptToxicityAnalyzer>("ToString");
            var score = Analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(0);
        }

        [Test]
        public void NotToxic_Member_MethodLengthExceeded()
        {
            var exceededMethodLength = ThresholdMethodLength + ThresholdExceeded;
            var toAnalyse = HealthyInstance.WithHealthyMember<JavascriptToxicityAnalyzer>("ToString", exceededMethodLength);

            var score = Analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(Math.Log(ThresholdExceeded));
        }

        [Test]
        public void NotToxic_Member_CylcomaticComplexityExceeded()
        {
            var exceededCyclomaticComplexity = ThresholdCyclomaticComplexity + ThresholdExceeded;
            var toAnalyse = HealthyInstance.WithHealthyMember<JavascriptToxicityAnalyzer>("ToString", ThresholdMethodLength, exceededCyclomaticComplexity);

            var score = Analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(Math.Log(ThresholdExceeded));
        }
    }
}