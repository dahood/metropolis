using Metropolis.Api.Collection.PowerShell;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.AllLanguages
{
    /// <summary>
    ///     Copy Paste Detector (cpd) - find duplicates for most files
    /// </summary>
    public class CpdCollectionStep : BaseCollectionStep
    {
        //sample commmand
        // java -cp {0} net.sourceforge.pmd.cpd.CPD --format csv --language {1} --minimum-tokens {2} --files {3} > {4}
        //-minimum-tokens 100 --language cs --format csv --files c:\dev\metropolis > results.csv
        // NEED ALL JARS...
        // pmd*.jar
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
            return CpdCommand.FormatWith(GetNodeBinPath(), args.SourceDirectory, result.MetricsFile);
        }
    }
}