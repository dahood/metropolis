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
            string fileName = "default.eslintrc.json";
            if (args.Setting == EcmaLanguageSetting.ECMA3)
                fileName = "ecma3.eslintrc.json";
            if (args.Setting == EcmaLanguageSetting.ECMA5)
                fileName = "ecma5.eslintrc.json";

            var cmd = EsLintCommand.FormatWith(GetNodeBinPath(), LocateSettings(fileName), args.SourceDirectory, result.MetricsFile);

            if (args.IgnoreFile.IsNotEmpty())
                cmd = string.Concat(cmd, IgnorePathPart.FormatWith(args.IgnoreFile));

            return cmd;
        }
    }
}