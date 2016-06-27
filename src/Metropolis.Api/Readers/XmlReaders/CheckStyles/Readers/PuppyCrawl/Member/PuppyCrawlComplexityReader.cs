using Metropolis.Common.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Member
{
    public class PuppyCrawlComplexityReader : CheckStyleBaseReader, ICheckStylesMemberReader
    {
        public override string Source => PuppyCrawlSources.CyclomaticComplexity;

        public PuppyCrawlComplexityReader() : base(IntRegex)
        {
        }

        public void Read(Domain.Member member, CheckStylesItem item)
        {
            member.Name = $"Line: {item.Line}";
            member.CylomaticComplexity = Parser.Match(item.Message).Value.AsInt();
        }
    }
}