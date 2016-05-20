using Metropolis.Common.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Member
{
    public class PupyyCrawlNestedTryDepthReader : CheckStyleBaseReader, ICheckStylesMemberReader
    {
        public override string Source => PuppyCrawlSources.NestedTryDepth;
        
        public void Read(Domain.Member member, CheckStylesItem item)
        {
            member.NestedTryDepth = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}