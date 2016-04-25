using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl
{
    public class PuppyCrawlMethodLengthParser : CheckStyleBaseParser, ICheckStylesMemberParser
    {
        public override string Source => PuppyCrawlSources.MethodLength;
        
        public void Parse(Member member, CheckStylesItem item)
        {
            member.LinesOfCode = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}