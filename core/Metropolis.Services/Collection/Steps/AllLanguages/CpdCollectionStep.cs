using System.Collections.Generic;
using Metropolis.Api.Collection.PowerShell;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;
using Microsoft.Extensions.Logging;

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
        ///     Thresholds set per language as below, or DefaultThreshold
        /// </summary>
        public const int CsharpThreshold = 75;
        public const int EcmaScriptThreshold = 75;
        public const int JavaThreshold = 50;

        /// <summary>
        ///     CPD has a token for each language it supports, along with an associated .jar file to cover specifics
        ///     like using statements in C#
        /// </summary>
        public const string CsharpToken = "cs";
        public const string EcmaScriptToken = "ecmascript";
        public const string JavaToken = "java";

        //sample commmand  
        //java -Xmx512m -cp "cpd/*" net.sourceforge.pmd.cpd.CPD --format csv --language cs --minimum-tokens 50 --files C:\dev\metropolis\src > cpd-metro.csv
        private const string CpdCommand =
            @"java -Xmx512m -cp '{0}' net.sourceforge.pmd.cpd.CPD --format csv --language {1} --minimum-tokens {2} --files '{3}' > '{4}'";
        private const string ClassPath = @"cpd/*";

        private readonly Dictionary<ParseType, string> languageTokenMap = new Dictionary<ParseType, string>
        {
            {ParseType.CpdCsharp, CsharpToken},
            {ParseType.CpdEcma, EcmaScriptToken},
            {ParseType.CpdJava, JavaToken}
        };

        private readonly Dictionary<ParseType, int> languageThresholdMap = new Dictionary<ParseType, int>
        {
            {ParseType.CpdCsharp, CsharpThreshold},
            {ParseType.CpdEcma, EcmaScriptThreshold},
            {ParseType.CpdJava, JavaThreshold}
        };

        public CpdCollectionStep(ParseType parseType) : base(new RunPowerShell())
        {
            ParseType = parseType;
        }
        public override string MetricsType => "CPD";
        public override string Extension => ".csv";
        public override ParseType ParseType { get; }

        public override string ValidateMetricResults(string fileNametoValidate)
        {
            //TODO: validate cpd output somehow...this usually works
            return string.Empty;
        }

        public override string PrepareCommand(MetricsCommandArguments args, MetricsResult result)
        {
            var tokenThreshold = GetThreshold();
            var languageToken = GetLanguageToken();
            return CpdCommand.FormatWith(LocateBinaries(ClassPath), languageToken, tokenThreshold, args.SourceDirectory, result.MetricsFile);
        }

        private string GetLanguageToken()
        {
            return languageTokenMap[ParseType];
        }

        private int GetThreshold()
        {
            return languageThresholdMap[ParseType];
        }
    }
}