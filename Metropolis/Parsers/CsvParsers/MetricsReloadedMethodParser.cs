using System.Collections.Generic;
using System.Linq;
using Metropolis.Domain;

namespace Metropolis.Parsers.CsvParsers
{
    public class MetricsReloadedMethodParser : CsvClassParser<MetricsReloadedMethodLineItem, MetricsReloadedMethodMap>
    {
        public MetricsReloadedMethodParser() : base(hasHeaderRecord: true)
        {            
        }

        protected override CodeBase ParseLines(IEnumerable<MetricsReloadedMethodLineItem> lines)
        {
            var organizedIntoClasses = lines.GroupBy(x => new {NameSpace = x.Namespace, Class = x.Type});

            var classes =
                organizedIntoClasses.Select(
                    each =>
                    {
                        var members = each.Select(m => new Member(m.Member, m.LinesOfCode, m.CyclomaticComplexity, 0));
                        return new Class(each.Key.NameSpace, each.Key.Class, members);
                    }) .ToList();

            return new CodeBase(new CodeGraph(classes));
        }
    }
}
