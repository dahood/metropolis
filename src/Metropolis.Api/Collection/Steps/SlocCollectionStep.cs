using Metropolis.Api.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps
{
    /// <summary>
    ///     Automation for Javascript/ECMA parsing
    /// </summary>
    public class SlocCollectionStep : BaseCollectionStep
    {
        private const string SlocCommand = @"sloc '{0}' -d --format csv -> '{1}'";

        public SlocCollectionStep(ParseType parseType) : base(true)
        {
            ParseType = parseType;
        }

        protected override string MetricsType => "Sloc";
        protected override string Extension => ".csv";
        protected override ParseType ParseType { get; }

        protected override string PrepareCommand(MetricsCommandArguments args, MetricsResult result)
        {
            return SlocCommand.FormatWith(args.SourceDirectory, result.MetricsFile);
        }
    }
}