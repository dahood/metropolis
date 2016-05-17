using System;
using Metropolis.Api.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services.Tasks.Commands
{
    public class EsLintMetricsCommand : BaseMetricsCommand
    {
        private const string EsLintCommand = @"eslint -c '{0}.eslintrc.json' '{1}\**' -o '{2}' -f checkstyle";
        private const string IgnorePathPart = "  --ignore - path '{0}'";

        public override string MetricsType => "Eslint";
        public override string Extension => ".xml";

        public EsLintMetricsCommand() : base(true)
        {
        }

        protected override string PrepareCommand(MetricsCommandArguments args, MetricsResult result)
        {
            var cmd = EsLintCommand.FormatWith(AppDomain.CurrentDomain.BaseDirectory, args.SourceDirectory, result.MetricsFile);

            if (args.IgnorePath.IsNotEmpty())
                cmd = string.Concat(cmd, IgnorePathPart.FormatWith(args.IgnorePath));

            return cmd;
        }

        protected override MetricsResult MetricResultFor(MetricsCommandArguments args)
        {
            return new MetricsResult {ParseType = ParseType.EsLint, MetricsFile = GetMetricsOutoutFile(args)};
        }
    }
}