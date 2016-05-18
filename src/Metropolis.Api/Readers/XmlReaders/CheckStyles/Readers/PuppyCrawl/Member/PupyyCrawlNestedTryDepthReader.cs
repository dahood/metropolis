using Metropolis.Api.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Member
{
    public class PupyyCrawlNestedTryDepthReader : CheckStyleBaseReader, ICheckStylesMemberReader
    {
        public override string Source => PuppyCrawlSources.NestedTryDepth;
        
        public void Parse(Domain.Member member, CheckStylesItem item)
        {
            member.NestedTryDepth = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}