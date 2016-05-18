using Metropolis.Api.Extensions;

namespace Metropolis.Api.Parsers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Member
{
    public class PuppyCrawlMethodLengthReader : CheckStyleBaseReader, ICheckStylesMemberParser
    {
        public override string Source => PuppyCrawlSources.MethodLength;
        
        public void Parse(Domain.Member member, CheckStylesItem item)
        {
            member.LinesOfCode = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}