using Metropolis.Api.Collection.PowerShell;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;
using Microsoft.Extensions.Logging;

namespace Metropolis.Api.Collection.Steps.AllLanguages
{
    /// <summary>
    ///     Automation for Javascript/ECMA parsing
    /// </summary>
    public class SlocCollectionStep : BaseCollectionStep
    {
        private const string SlocCommand = @"{0}sloc ""{1}"" -d --format csv > ""{2}""";

        public SlocCollectionStep(ParseType parseType) : base(new RunPowerShell())
        {
            ParseType = parseType;
        }

        public override string MetricsType => "Sloc";
        public override string Extension => ".csv";
        public override ParseType ParseType { get; }

        public override string PrepareCommand(MetricsCommandArguments args, MetricsResult result)
        {
            return SlocCommand.FormatWith(GetNodeBinPath(), args.SourceDirectory, result.MetricsFile);
        }

        public override string ValidateMetricResults(string fileNametoValidate)
        {
            //TODO: validate sloc output somehow...this usually works
            return string.Empty;
        }
    }
}