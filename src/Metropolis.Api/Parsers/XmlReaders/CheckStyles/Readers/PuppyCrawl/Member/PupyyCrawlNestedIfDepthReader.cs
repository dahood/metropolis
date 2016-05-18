using Metropolis.Api.Extensions;

namespace Metropolis.Api.Parsers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Member
{
    public class PupyyCrawlNestedIfDepthReader : CheckStyleBaseReader, ICheckStylesMemberParser
    {
        public override string Source => PuppyCrawlSources.NestedIfDepth;
        
        public void Parse(Domain.Member member, CheckStylesItem item)
        {
            member.NestedIfDepth = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}