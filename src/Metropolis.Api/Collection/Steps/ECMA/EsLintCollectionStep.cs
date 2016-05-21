using System;
using Metropolis.Api.Collection.PowerShell;
using Metropolis.Common.Models;
using Metropolis.Common.Extensions;

namespace Metropolis.Api.Collection.Steps.ECMA
{
    public class EsLintCollectionStep : BaseCollectionStep
    {
        private const string EsLintCommand = @"eslint -c '{0}.eslintrc.json' '{1}\**' -o '{2}' -f checkstyle";
        private const string IgnorePathPart = " --ignore-path '{0}'";

        public override string MetricsType => "Eslint";
        public override string Extension => ".xml";
        public override ParseType ParseType => ParseType.EsLint;

        public EsLintCollectionStep() : base(new RunPowerShell(), true)
        {
        }

        public override string PrepareCommand(MetricsCommandArguments args, MetricsResult result)
        {
            var cmd = EsLintCommand.FormatWith(AppDomain.CurrentDomain.BaseDirectory, args.SourceDirectory, result.MetricsFile);

            if (args.IgnoreFile.IsNotEmpty())
                cmd = string.Concat(cmd, IgnorePathPart.FormatWith(args.IgnoreFile));

            return cmd;
        }
    }
}