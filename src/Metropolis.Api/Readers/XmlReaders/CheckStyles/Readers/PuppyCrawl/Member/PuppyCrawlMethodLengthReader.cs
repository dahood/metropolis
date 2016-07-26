using Metropolis.Common.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Member
{
    public class PuppyCrawlMethodLengthReader : CheckStyleBaseReader, ICheckStylesMemberReader
    {
        private const int PuppyCrawlMethodFudgeFactor = 2;

        public override string Source => PuppyCrawlSources.MethodLength;
        
        public void Read(Domain.Member member, CheckStylesItem item)
        {
            var linesOfCode = IntParser.Match(item.Message).Value.AsInt();
            member.StartLine = item.Line;
            member.EndLine = member.StartLine + linesOfCode + PuppyCrawlMethodFudgeFactor;
            member.LinesOfCode = linesOfCode;
        }
    }
}