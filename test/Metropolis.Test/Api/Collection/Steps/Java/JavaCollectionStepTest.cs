using System.Collections.Generic;
using Metropolis.Api.Collection.Steps;
using Metropolis.Api.Collection.Steps.AllLanguages;
using Metropolis.Api.Collection.Steps.Java;
using Metropolis.Common.Models;
using NUnit.Framework;

namespace Metropolis.Test.Api.Collection.Steps.Java
{
    [TestFixture]
    public class JavaCollectionStepTest : BaseCompositeCollectionStepTest<JavaCollectionStep>
    {
        protected override IEnumerable<ICollectionStep> ExpectedSteps =>
            new ICollectionStep[] { new PuppyCrawlerCheckstyleCollectionStep(), new SlocCollectionStep(ParseType.SlocJava),
                                    new CpdCollectionStep(ParseType.CpdJava)};
    }
}