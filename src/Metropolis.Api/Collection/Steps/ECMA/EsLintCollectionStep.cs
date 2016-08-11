using System;
using System.IO;
using Metropolis.Api.Collection.PowerShell;
using Metropolis.Api.IO;
using Metropolis.Common.Models;
using Metropolis.Common.Extensions;

namespace Metropolis.Api.Collection.Steps.ECMA
{
    public class EsLintCollectionStep : BaseCollectionStep
    {
        private readonly IFileSystem fileSystem;
        private const string EsLintCommand = @"{0}eslint -c '{1}' '{2}\**' -o '{3}' -f checkstyle";
        private const string IgnorePathPart = " --ignore-path '{0}'";

        private static readonly Tuple<string,string> ParsingErrorKeyword =  new Tuple<string, string>("Parsing error: The keyword",
            "Eslint: Keyword Missing like 'module' for ECMA6.");
        private static readonly Tuple<string, string> ParsingErrorCharacter = new Tuple<string, string>("Parsing error: Unexpected character", 
            "Eslint: unexpected chracter like 'apos' for ECMA6."); 

        public override string MetricsType => "Eslint";
        public override string Extension => ".xml";
        public override ParseType ParseType => ParseType.EsLint;

        public EsLintCollectionStep() : this(new RunPowerShell(), new FileSystem())
        {
        }

        private EsLintCollectionStep(IRunPowerShell runPowerShell, IFileSystem fileSystem) : base(runPowerShell)
        {
            this.fileSystem = fileSystem;
        }

        public override string ValidateMetricResults(string fileNametoValidate)
        {
            string validateMetricResults = string.Empty;
            using (var filestream = fileSystem.OpenFileStreamReader(fileNametoValidate))
            {
                using (var reader = new StreamReader(filestream))
                {
                    var contents = reader.ReadToEnd();
                    validateMetricResults += Validate(contents, ParsingErrorKeyword);
                    validateMetricResults += Validate(contents, ParsingErrorCharacter);
                }
            }
            
            return validateMetricResults;
        }

        private static string Validate(string contents, Tuple<string, string> parsingErrorKeyword)
        {
            if (contents.Contains(parsingErrorKeyword.Item1))
            {
                return parsingErrorKeyword.Item2 + Environment.NewLine;
            }
            return string.Empty;
        }

        public override string PrepareCommand(MetricsCommandArguments args, MetricsResult result)
        {
            var eslintConfigFile = GetEcmaDialect(args.EcmaScriptDialect);

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
            if (eslintPasringOptions == EslintPasringOptions.ECMA6)
                fileName = "ecma6.eslintrc.json";
            return fileName;
        }
    }
}