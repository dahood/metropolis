using Metropolis.Api.Extensions;

namespace Metropolis.Api.Core.Parsers.XmlParsers.CheckStyles.Parsers.PuppyCrawl.Member
{
    public class PupyyCrawlNestedTryDepthParser : CheckStyleBaseParser, ICheckStylesMemberParser
    {
        public override string Source => PuppyCrawlSources.NestedTryDepth;
        
        public void Parse(Domain.Member member, CheckStylesItem item)
        {
            member.NestedTryDepth = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}