using Metropolis.Api.Extensions;

namespace Metropolis.Api.Core.Parsers.XmlParsers.CheckStyles.Parsers.PuppyCrawl.Member
{
    public class PuppyCrawlBooleanExpressionComplexityParser : CheckStyleBaseParser, ICheckStylesMemberParser
    {
        public override string Source => PuppyCrawlSources.BooleanExpressionComplexity;
        
        public void Parse(Domain.Member member, CheckStylesItem item)
        {
            member.BooleanExpressionComplexity = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}