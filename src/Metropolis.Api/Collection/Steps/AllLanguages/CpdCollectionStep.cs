using Metropolis.Api.Collection.PowerShell;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.AllLanguages
{
    /// <summary>
    ///     Copy Paste Detector (cpd) - find duplicates for most files
    ///     TODO: Requires Java, just like checkstyles does
    ///     Source: http://pmd.sourceforge.net/pmd-4.3.0/cpd.html
    /// </summary>
    public class CpdCollectionStep : BaseCollectionStep
    {
        /// <summary>
        ///     Thresholds set per language as below, or DEFAULT
        /// </summary>
        public const int CsharpThreshold = 75;

        public const int EcmaScriptThreshold = 50;
        public const int JavaThreshold = 50;
        public const int DefaultThreshold = 75;

        /// <summary>
        ///     CPD has a token for each language it supports, along with an associated .jar file to cover specifics
        ///     like using statements in C#
        /// </summary>
        private const string CsharpToken = "cs";

        private const string EcmaScriptToken = "javascript";
        private const string JavaToken = "java";
        private const string DefaultToken = "java";

        //sample commmand
        // java -cp {0} net.sourceforge.pmd.cpd.CPD --format csv --language {1} --minimum-tokens {2} --files {3} > {4}
        //-minimum-tokens 100 --language cs --format csv --files c:\dev\metropolis > results.csv

        private const string CpdCommand =
            @"java -cp {0} net.sourceforge.pmd.cpd.CPD --format csv --language {1} --minimum-tokens {2} --files {3} > {4}";

        public CpdCollectionStep(ParseType parseType) : base(new RunPowerShell())
        {
            ParseType = parseType;
        }
        public override string MetricsType => "cpd";
        public override string Extension => ".csv";
        public override ParseType ParseType { get; }

        public override string PrepareCommand(MetricsCommandArguments args, MetricsResult result)
        {
            var tokenThreshold = GetThreshold();
            var languageToken = GetLanguageToken();
            return CpdCommand.FormatWith(LocateBinaries("pmd-*.jar"), languageToken, tokenThreshold, args.SourceDirectory, result.MetricsFile);
        }

        private string GetLanguageToken()
        {
            switch (ParseType)
            {
                case ParseType.CpdCsharp:
                    return CsharpToken;
                case ParseType.CpdEcma:
                    return EcmaScriptToken;
                case ParseType.CpdJava:
                    return JavaToken;
                default:
                    return DefaultToken;
            }
        }

        private int GetThreshold()
        {
            switch (ParseType)
            {
                case ParseType.CpdCsharp:
                    return CsharpThreshold;
                case ParseType.CpdEcma:
                    return EcmaScriptThreshold;
                case ParseType.CpdJava:
                    return JavaThreshold;
                default:
                    return DefaultThreshold;
            }
        }
    }
}