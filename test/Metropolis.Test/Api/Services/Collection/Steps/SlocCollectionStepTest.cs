using Metropolis.Api.Services.Collection.Steps;
using Metropolis.Common.Models;
using NUnit.Framework;

namespace Metropolis.Test.Api.Services.Collection.Steps
{
    [TestFixture]
    public class SlocCollectionStepTest
    {
        private SlocStepTestDouble step;

        [SetUp]
        public void SetUp()
        {
            step = new SlocStepTestDouble(ParseType.EsLint);
        }
        
    }

    public class SlocStepTestDouble : SlocCollectionStep
    {
        public SlocStepTestDouble(ParseType parseType) : base(parseType)
        {
        }

        public string TestPrepareCommand(MetricsCommandArguments args, MetricsResult result)
        {
            return PrepareCommand(args, result);
        }
    }
}
