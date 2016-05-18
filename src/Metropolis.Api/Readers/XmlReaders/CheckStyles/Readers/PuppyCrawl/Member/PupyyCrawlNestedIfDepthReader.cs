using Metropolis.Api.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Member
{
    public class PupyyCrawlNestedIfDepthReader : CheckStyleBaseReader, ICheckStylesMemberReader
    {
        public override string Source => PuppyCrawlSources.NestedIfDepth;
        
        public void Read(Domain.Member member, CheckStylesItem item)
        {
            member.NestedIfDepth = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}