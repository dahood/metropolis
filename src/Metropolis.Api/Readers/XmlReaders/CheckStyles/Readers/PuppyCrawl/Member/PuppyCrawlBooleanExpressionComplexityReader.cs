using Metropolis.Api.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Member
{
    public class PuppyCrawlBooleanExpressionComplexityReader : CheckStyleBaseReader, ICheckStylesMemberReader
    {
        public override string Source => PuppyCrawlSources.BooleanExpressionComplexity;
        
        public void Parse(Domain.Member member, CheckStylesItem item)
        {
            member.BooleanExpressionComplexity = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}