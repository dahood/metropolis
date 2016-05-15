using System;
using System.Collections.Generic;
using Metropolis.Api.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services.Tasks.Commands
{
    public class EsLintMetricsCommand : BaseMetricsCommand
    {
        private const string EsLintCommand = @"eslint  -c '{0}\.eslintrc.json' '{1}\**'  -o '{2}' -f checkstyle --ignore-path '{3}'";

        public override string MetricsType => "Eslint";
        public override string Extension => ".xml";

        public override IEnumerable<MetricsResult> Run(MetricsCommandArguments args)
        {
            var result = new MetricsResult {ParseType = ParseType.EsLint, MetricsFile = GetMetricsOutoutFile(args)};
            var cmd = EsLintCommand.FormatWith(Environment.CurrentDirectory, args.SourceDirectory, result.MetricsFile, args.IgnorePath);
            SaveAndExecuteCommand(args, cmd);
            return new[] {result};
        }
    }
}