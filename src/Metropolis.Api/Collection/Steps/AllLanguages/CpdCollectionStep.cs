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
        ///     Thresholds set per language as below, or DefaultThreshold
        /// </summary>
        private const int CsharpThreshold = 75;
        private const int EcmaScriptThreshold = 75;
        private const int JavaThreshold = 75;
        private const int DefaultThreshold = 75;

        /// <summary>
        ///     CPD has a token for each language it supports, along with an associated .jar file to cover specifics
        ///     like using statements in C#
        /// </summary>
        private const string CsharpToken = "cs";
        private const string EcmaScriptToken = "javascript";
        private const string JavaToken = "java";
        private const string DefaultToken = "java";

        //sample commmand  
        //TODO: CSharp has problems with CPD because it scans the obj/Debug folder and finds tons 
        // of duplicates!!! --exclude "obj\**"
        
        //java -cp "cpd/*" net.sourceforge.pmd.cpd.CPD --format csv --language cs --minimum-tokens 50 --files C:\dev\metropolis\src --exclude "obj\**" > cpd-metro.csv
        //java -cp "cpd/*" net.sourceforge.pmd.cpd.CPD --format csv --language java --minimum-tokens 50 --files C:\Dev\disruptor\src > cpd-disruptor.csv
        //java -cp "cpd/*" net.sourceforge.pmd.cpd.CPD --format csv --language javascript --minimum-tokens 50 --files C:\Dev\d3\src > cpd-d3.csv
        private const string CpdCommand =
            @"java -cp {0} net.sourceforge.pmd.cpd.CPD --format csv --language {1} --minimum-tokens {2} --files {3} > {4}";
        private const string ClassPath = @"cpd/*";

        public CpdCollectionStep(ParseType parseType) : base(new RunPowerShell())
        {
            ParseType = parseType;
        }
        public override string MetricsType => "CPD";
        public override string Extension => ".csv";
        public override ParseType ParseType { get; }

        public override string PrepareCommand(MetricsCommandArguments args, MetricsResult result)
        {
            var tokenThreshold = GetThreshold();
            var languageToken = GetLanguageToken();
            return CpdCommand.FormatWith(LocateBinaries(ClassPath), languageToken, tokenThreshold, args.SourceDirectory, result.MetricsFile);
        }

        private string GetLanguageToken()
        {
            //TODO this would be better as a Dictionary<ParseType, string>
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
            //TODO this would be better as a Dictionary<ParseType, int>
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