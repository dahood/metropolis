using Metropolis.Extensions;
using Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers;
using Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.Parsers.PuppyCrawl.Member
{
    public class PupyyCrawlNestedIfDepthParser : CheckStyleBaseParser, ICheckStylesMemberParser
    {
        public override string Source => PuppyCrawlSources.NestedIfDepth;
        
        public void Parse(Domain.Member member, CheckStylesItem item)
        {
            member.NestedIfDepth = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}