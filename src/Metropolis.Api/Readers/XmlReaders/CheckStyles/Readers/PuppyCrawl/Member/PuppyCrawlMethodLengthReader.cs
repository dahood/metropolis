using Metropolis.Common.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Member
{
    public class PuppyCrawlMethodLengthReader : CheckStyleBaseReader, ICheckStylesMemberReader
    {
        public override string Source => PuppyCrawlSources.MethodLength;
        
        public void Read(Domain.Member member, CheckStylesItem item)
        {
            var linesOfCode = IntParser.Match(item.Message).Value.AsInt();
            member.EndLine = member.StartLine + linesOfCode;
            member.LinesOfCode = linesOfCode;
        }
    }
}