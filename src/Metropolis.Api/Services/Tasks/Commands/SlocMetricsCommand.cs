using System;
using Metropolis.Api.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services.Tasks.Commands
{
    public class SlocMetricsCommand : BaseMetricsCommand
    {
        private const string SlocCommand = @"sloc '{0}' -d --format csv -> '{1}'";

        public override string MetricsType => "Sloc";
        public override string Extension => ".csv";

        public SlocMetricsCommand() : base(true)
        {
        }

        protected override string PrepareCommand(MetricsCommandArguments args, MetricsResult result)
        {
            return SlocCommand.FormatWith(args.SourceDirectory, result.MetricsFile);
        }

        protected override MetricsResult MetricResultFor(MetricsCommandArguments args)
        {
            return new MetricsResult {ParseType = GetSLocType(args), MetricsFile = GetMetricsOutoutFile(args)};
        }

        private static ParseType GetSLocType(MetricsCommandArguments args)
        {
            switch (args.RepositorySourcetype)
            {
                case RepositorySourceType.ECMA:
                    return ParseType.SlocEcma;
                case RepositorySourceType.CSharp:
                    return ParseType.SlocCSharp;
                case RepositorySourceType.Java:
                    return ParseType.SlocJava;
                default:
                    throw new ApplicationException($"{args.RepositorySourcetype} is not a known metrics type");
            }
        }
    }
}