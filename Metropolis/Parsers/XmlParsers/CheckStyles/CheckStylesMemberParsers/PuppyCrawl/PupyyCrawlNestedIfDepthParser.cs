using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl
{
    public class PupyyCrawlNestedIfDepthParser : MemberParserBase
    {
        public override string Source => PuppyCrawlSources.NestedIfDepth;
        
        public override void Parse(Member member, CheckStylesItem item)
        {
            member.NestedIfDepth = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}