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
        private const string EsLintCommand = @"{0}eslint -c '{1}' '{2}' --no-eslintrc -o '{3}' -f checkstyle {4}";
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

            var cmd = EsLintCommand.FormatWith(GetNodeBinPath(), 
            LocateSettings(eslintConfigFile) , args.SourceDirectory, result.MetricsFile,
                GetFileExtensions(args.EcmaScriptDialect));

            if (args.IgnoreFile.IsNotEmpty())
                cmd = string.Concat(cmd, IgnorePathPart.FormatWith(args.IgnoreFile));

            return cmd;
        }

        private static string GetFileExtensions(EslintPasringOptions eslintPasringOptions)
        {
            // if it is react, babel_react handle js and jsx files, otherwise just don't override the ext param, and let eslint do its default
            return eslintPasringOptions == EslintPasringOptions.REACT  || eslintPasringOptions == EslintPasringOptions.BABEL_REACT ? " --ext .js,.jsx" : "";
        }

        private static string GetEcmaDialect(EslintPasringOptions eslintPasringOptions)
        {
            var fileName = string.Empty;
            if (eslintPasringOptions == EslintPasringOptions.DEFAULT)
                fileName = "default.eslintrc.json";
            if (eslintPasringOptions == EslintPasringOptions.ECMA6)
                fileName = "ecma6.eslintrc.json";
            if (eslintPasringOptions == EslintPasringOptions.REACT)
                fileName = "react.eslintrc.json";
            if (eslintPasringOptions == EslintPasringOptions.BABEL_REACT)
                fileName = "babel_react.eslintrc.json";
            if (eslintPasringOptions == EslintPasringOptions.NODE)
                fileName = "node.eslintrc.json";
            return fileName;
        }
    }
}