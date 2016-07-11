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
        public void Healthy_WhenEverythingIsWithinTheThresholds_NoMembers()
        {
            var toAnalyse = HealthyInstance;
            var score = Analyzer.CalculateToxicity(toAnalyse);
            score.Toxicity.Should().Be(0);
        }

    }
}