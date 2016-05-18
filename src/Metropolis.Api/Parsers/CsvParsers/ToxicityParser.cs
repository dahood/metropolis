using System.Collections.Generic;
using System.Linq;
using Metropolis.Api.Domain;

namespace Metropolis.Api.Parsers.CsvParsers
{
    //Data Aquisition
    public class ToxicityParser : CsvInstanceReader<ToxicityCsvLineItem, ToxicityCsvLineItemClassMap>
    {
        public ToxicityParser(bool hasHeaderRecord = true) : base(hasHeaderRecord)
        {
        }

        protected override CodeBase ParseLines(IEnumerable<ToxicityCsvLineItem> lines)
        {
            return new CodeBase(
                new CodeGraph(
                lines.Select(
                    line => new Instance(line.Namespace, line.Type, line.NumberOfMethods, line.LinesOfCode, line.Toxicity)).ToList()));
        }
    }
}