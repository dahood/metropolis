using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Metropolis.Api.Collection.Steps;
using NUnit.Framework;

namespace Metropolis.Test.Api.Collection.Steps
{
    public abstract class BaseCompositeCollectionStepTest<T> where T : CompositeCollectionStep, new()
    {
        private T step;

        protected abstract IEnumerable<ICollectionStep> ExpectedSteps { get; }

        [Test]
        public void ShouldContainSteps()
        {
            step = new T();

            var missingSteps = StepsMissing;
            missingSteps.Should().BeEmpty("Missing steps: " + string.Join(",", missingSteps.GetType().Name));

            var extraSteps = ExtraSteps;
            missingSteps.Should().BeEmpty("Extra steps: " + string.Join(",", extraSteps.GetType().Name));
        }

        public List<ICollectionStep> ExtraSteps => GetExceptions(step.Commands, ExpectedSteps);
        private List<ICollectionStep> StepsMissing => GetExceptions(ExpectedSteps, step.Commands);

        private List<ICollectionStep> GetExceptions(IEnumerable<ICollectionStep> target, IEnumerable<ICollectionStep> toCompare)
        {
            return (from t in target
                     join right in toCompare
                         on t.GetType() equals right.GetType() into match
                     from expected in match.DefaultIfEmpty()
                     where expected == null
                     select t).ToList();
        }

    }
}