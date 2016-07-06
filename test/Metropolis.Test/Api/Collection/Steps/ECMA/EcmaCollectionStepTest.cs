using System.Collections.Generic;
using Metropolis.Api.Collection.Steps;
using Metropolis.Api.Collection.Steps.AllLanguages;
using Metropolis.Api.Collection.Steps.ECMA;
using Metropolis.Common.Models;
using NUnit.Framework;

namespace Metropolis.Test.Api.Collection.Steps.ECMA
{
    [TestFixture]
    public class EcmaCollectionStepTest : BaseCompositeCollectionStepTest<EcmaCollectionStep>
    {
        protected override IEnumerable<ICollectionStep> ExpectedSteps =>
            new ICollectionStep[] { new EsLintCollectionStep(), new SlocCollectionStep(ParseType.SlocEcma), new CpdCollectionStep(ParseType.CpdEcma) };
    }
}
