using System.Collections.Generic;
using System.Linq;
using Metropolis.Domain;

namespace Metropolis.Parsers.CsvParsers
{
    //Data Aquisition
    public class ToxicityParser : CsvClassParser<ToxicityCsvLineItem, ToxicityCsvLineItemClassMap>
    {
        public ToxicityParser(bool hasHeaderRecord = true) : base(hasHeaderRecord)
        {
        }

        protected override CodeBase ParseLines(IEnumerable<ToxicityCsvLineItem> lines, string sourceBaseDirectory)
        {
            return new CodeBase(
                new CodeGraph(
                lines.Select(
                    line => new Class(line.Namespace, line.Type, line.NumberOfMethods, line.LinesOfCode, line.Toxicity)).ToList()));
        }
    }
}