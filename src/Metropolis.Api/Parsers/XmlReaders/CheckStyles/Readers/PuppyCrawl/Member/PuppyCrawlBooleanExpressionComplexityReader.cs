using Metropolis.Api.Extensions;

namespace Metropolis.Api.Parsers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Member
{
    public class PuppyCrawlBooleanExpressionComplexityReader : CheckStyleBaseReader, ICheckStylesMemberParser
    {
        public override string Source => PuppyCrawlSources.BooleanExpressionComplexity;
        
        public void Parse(Domain.Member member, CheckStylesItem item)
        {
            member.BooleanExpressionComplexity = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}