using System.Collections.Generic;
using System.Linq;
using Metropolis.Domain;

namespace Metropolis.Parsers.CsvParsers
{
    public class MetricsReloadedClassParser : CsvClassParser<MetricsReloadedClassLineItem, MetricsReloadedClassMap>
    {
        public MetricsReloadedClassParser() : base(hasHeaderRecord: true)
        {
        }

        protected override CodeBase ParseLines(IEnumerable<MetricsReloadedClassLineItem> lines)
        {
            var classes = lines.Select(x => new Class(x.Namespace, x.Type, 0, x.LinesOfCode, 0 , x.DepthOfInheritance, x.ClassCoupling)).ToList();
            return new CodeBase(new CodeGraph(classes));
        }
    }
}