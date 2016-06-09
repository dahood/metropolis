using Metropolis.Api.Collection.PowerShell;
using Metropolis.Common.Models;
using Metropolis.Common.Extensions;

namespace Metropolis.Api.Collection.Steps.ECMA
{
    public class EsLintCollectionStep : BaseCollectionStep
    {
        private const string EsLintCommand = @"{0}eslint -c '{1}' '{2}\**' -o '{3}' -f checkstyle";
        private const string IgnorePathPart = " --ignore-path '{0}'";

        public override string MetricsType => "Eslint";
        public override string Extension => ".xml";
        public override ParseType ParseType => ParseType.EsLint;

        public EsLintCollectionStep() : base(new RunPowerShell())
        {
        }

        public override string PrepareCommand(MetricsCommandArguments args, MetricsResult result)
        {
            var eslintConfigFile = GetEcmaDialect(args.Dialect);

            var cmd = EsLintCommand.FormatWith(GetNodeBinPath(), LocateSettings(eslintConfigFile), args.SourceDirectory, result.MetricsFile);

            if (args.IgnoreFile.IsNotEmpty())
                cmd = string.Concat(cmd, IgnorePathPart.FormatWith(args.IgnoreFile));

            return cmd;
        }

        private static string GetEcmaDialect(EslintPasringOptions eslintPasringOptions)
        {
            var fileName = string.Empty;
            if (eslintPasringOptions == EslintPasringOptions.DEFAULT)
                fileName = "default.eslintrc.json";
            if (eslintPasringOptions == EslintPasringOptions.MODULE)
                fileName = "module.eslintrc.json";
            if (eslintPasringOptions == EslintPasringOptions.ECMA6)
                fileName = "ecma6.eslintrc.json";
            return fileName;
        }
    }
}