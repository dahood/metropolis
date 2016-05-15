using System;
using System.Collections.Generic;
using Metropolis.Api.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services.Tasks.Commands
{
    public class SlocMetricsCommand : BaseMetricsCommand
    {
        private const string SlocCommand = @"sloc '{0}' -d --format csv -> '{1}'";

        public override string MetricsType => "Sloc";
        public override string Extension => ".csv";

        public override IEnumerable<MetricsResult> Run(MetricsCommandArguments args)
        {
            var result = new MetricsResult { ParseType = GetSLocType(args), MetricsFile = GetMetricsOutoutFile(args) };
            var slocCommand = SlocCommand.FormatWith(args.SourceDirectory, result.MetricsFile);
            SaveAndExecuteCommand(args, slocCommand);
            return new[] {result};
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