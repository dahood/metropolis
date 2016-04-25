using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl
{
    public class PuppyCrawlBooleanExpressionComplexityParser : MemberParserBase
    {
        public override string Source => PuppyCrawlSources.BooleanExpressionComplexity;
        
        public override void Parse(Member member, CheckStylesItem item)
        {
            member.BooleanExpressionComplexity = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}