using System.Collections.Generic;
using Metropolis.Api.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services.Tasks.Commands
{
    public class SlocMetricsCommand : BaseMetricsCommand
    {
        private const string SlocCommand = @"sloc {0} -d --format csv -> {1}";

        public override string MetricsType => "Sloc";
        public override string Extension => ".csv";

        public override IEnumerable<MetricsResult> Run(MetricsCommandArguments args)
        {
            var result = new MetricsResult { ParseType = ParseType.EsLint, MetricsFile = GetMetricsOutoutFile(args) };
            ExecuteProcess(SlocCommand.FormatWith(args.SourceDirectory, result.MetricsFile));
            return new[] {result};
        }
    }
}