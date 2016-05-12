using System;
using System.Collections.Generic;
using System.IO;
using Metropolis.Api.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services.Tasks.Commands
{
    public class EcmaMetricsCommand : IMetricsCommand
    {
        private const string EsLintCommand = @"eslint  -c {0}\.eslintrc.json {1}\**  -o {2} -f checkstyle --ignore-path {3}\.eslintignore";

        public IEnumerable<MetricsResult> Run(MetricsCommandArguments args)
        {
            var workingDirectory = Environment.CurrentDirectory;
            var cmd = EsLintCommand.FormatWith(workingDirectory, args.SourceDirectory, GetMetricsOutoutFile(args), args.IgnorePath);
                 
            throw new System.NotImplementedException();
        }

        private static string GetMetricsOutoutFile(MetricsCommandArguments args)
        {
            var fileName = $"{args.ProjectName}_Ecma_checkStyles.csv";
            return Path.Combine(args.MetricsOutputDirectory, fileName);
        }
    }
}