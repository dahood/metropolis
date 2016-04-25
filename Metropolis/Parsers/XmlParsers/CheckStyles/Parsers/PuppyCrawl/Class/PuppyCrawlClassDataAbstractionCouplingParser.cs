using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl
{
    public class PuppyCrawlClassDataAbstractionCouplingParser : CheckStyleBaseParser, ICheckStylesClassParser
    {
        public override string Source => PuppyCrawlSources.ClassDataAbstractionCoupling;
        
        public void Parse(Class type, CheckStylesItem item)
        {
            type.ClassDataAbstractionCoupling = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}