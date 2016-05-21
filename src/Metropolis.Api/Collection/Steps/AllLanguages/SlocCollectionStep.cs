using Metropolis.Api.Collection.PowerShell;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.AllLanguages
{
    /// <summary>
    ///     Automation for Javascript/ECMA parsing
    /// </summary>
    public class SlocCollectionStep : BaseCollectionStep
    {
        private const string SlocCommand = @"sloc '{0}' -d --format csv -> '{1}'";

        public SlocCollectionStep(ParseType parseType) : base(new RunPowerShell(), true)
        {
            ParseType = parseType;
        }

        public override string MetricsType => "Sloc";
        public override string Extension => ".csv";
        public override ParseType ParseType { get; }

        public override string PrepareCommand(MetricsCommandArguments args, MetricsResult result)
        {
            return SlocCommand.FormatWith(args.SourceDirectory, result.MetricsFile);
        }
    }
}