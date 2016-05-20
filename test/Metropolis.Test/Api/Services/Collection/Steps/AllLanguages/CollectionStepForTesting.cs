using Metropolis.Api.Collection.Steps;
using Metropolis.Common.Models;

namespace Metropolis.Test.Api.Services.Collection.Steps.AllLanguages
{
    public class CollectionStepForTesting : BaseCollectionStep
    {
        private bool runBadCommand;

        public CollectionStepForTesting() : base(false)
        {
        }

        public override string MetricsType => "Test";
        public override string Extension => ".xml";
        public override ParseType ParseType => ParseType.VisualStudio;
        public override string PrepareCommand(MetricsCommandArguments args, MetricsResult result)
        {
            return runBadCommand? "this should fail" : $"dir | Out-File '{result.MetricsFile}'";
        }

        public void RunFailingCommand()
        {
            runBadCommand = true;
        }
    }
}