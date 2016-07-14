using System;
using FluentAssertions;
using Metropolis.Api.Analyzers.Toxicity;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;
using NUnit.Framework;

namespace Metropolis.Test.Api.Analyzers.Toxicity
{
    public abstract class AbstractToxicityAnalyzerTest<T> where T : ToxicityAnalyzer, new()
    {
        protected T Analyzer;
        public const int ThresholdExceeded = 2;

        [SetUp]
        public void SetUp()
        {
            Analyzer = new T();
        }

        protected abstract Instance HealthyInstance { get; }
        protected abstract Instance CreateHealthyInstance(Action<Instance> initializer);
        protected abstract int ThresholdNumberOfMembers { get; }
        protected abstract int ThresholdMethodLength { get; }
        protected abstract int ThresholdCyclomaticComplexity { get; }

        [Test]
        [TestCase("fakeParam")] //TODO: this is needed until resharper fixes the issue with abstract generic tests
        public void ToxicOn_NumberOfMembers(string param)
        {
            var toAnalyse = HealthyInstance;
            (ThresholdNumberOfMembers + 1).ForEach(x => toAnalyse.WithHealthyMember<T>($"Member{x}"));

            var score = Analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(Math.Log(1));
        }

        [Test]
        [TestCase("fakeParam")] //TODO: this is needed until resharper fixes the issue with abstract generic tests
        public void Healthy_WhenEverythingIsWithinTheThresholds_NoMembers(string param)
        {
            var toAnalyse = HealthyInstance;
            var score = Analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(0);
        }

        [Test]
        [TestCase("fakeParam")] //TODO: this is needed until resharper fixes the issue with abstract generic tests
        public void ToxicOn_LinesOfCode_NoMembers(string param)
        {
            var toAnalyse = CreateHealthyInstance(x => x.LinesOfCode += ThresholdExceeded);
            var score = Analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(Math.Log(ThresholdExceeded));
        }

        [Test]
        [TestCase("fakeParam")] //TODO: this is needed until resharper fixes the issue with abstract generic tests
        public void NotToxic_WhenEverythingIsWithinTheThresholds_OneMember(string param)
        {
            var toAnalyse = HealthyInstance.WithHealthyMember<T>("ToString");
            var score = Analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(0);
        }

        [Test]
        [TestCase("fakeParam")] //TODO: this is needed until resharper fixes the issue with abstract generic tests
        public void Healthy_NumberOfMembers(string param)
        {
            var toAnalyse = HealthyInstance;
            ThresholdNumberOfMembers.ForEach(x => toAnalyse.WithHealthyMember<T>($"Member{x}"));

            var score = Analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(0);
        }

        [Test]
        [TestCase("fakeParam")] //TODO: this is needed until resharper fixes the issue with abstract generic tests
        public void NotToxic_Member_MethodLengthExceeded(string param)
        {
            var exceededMethodLength = ThresholdMethodLength + ThresholdExceeded;
            var toAnalyse = HealthyInstance.WithHealthyMember<T>("ToString", exceededMethodLength);

            var score = Analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(Math.Log(ThresholdExceeded));
        }

        [Test]
        [TestCase("fakeParam")] //TODO: this is needed until resharper fixes the issue with abstract generic tests
        public void NotToxic_Member_CylcomaticComplexityExceeded(string param)
        {
            var exceededCyclomaticComplexity = ThresholdCyclomaticComplexity + ThresholdExceeded;
            var toAnalyse = HealthyInstance.WithHealthyMember<T>("ToString", ThresholdMethodLength, exceededCyclomaticComplexity);

            var score = Analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(Math.Log(ThresholdExceeded));
        }
    }
}