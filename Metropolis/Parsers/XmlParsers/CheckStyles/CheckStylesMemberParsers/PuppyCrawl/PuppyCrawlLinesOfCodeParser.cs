using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl
{
    public class PuppyCrawlLinesOfCodeParser : MemberParserBase
    {
        public override string Source => PuppyCrawlSources.LinesOfCode;
        
        public override void Parse(Member member, CheckStylesItem item)
        {
            member.LinesOfCode = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}