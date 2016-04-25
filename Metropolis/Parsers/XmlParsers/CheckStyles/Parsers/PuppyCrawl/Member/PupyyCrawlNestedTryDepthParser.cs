using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl
{
    public class PupyyCrawlNestedTryDepthParser : CheckStyleBaseParser, ICheckStylesMemberParser
    {
        public override string Source => PuppyCrawlSources.NestedTryDepth;
        
        public void Parse(Member member, CheckStylesItem item)
        {
            member.NestedTryDepth = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}