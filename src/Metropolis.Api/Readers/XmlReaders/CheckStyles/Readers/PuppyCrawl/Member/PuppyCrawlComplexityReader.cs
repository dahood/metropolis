using Metropolis.Api.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Member
{
    public class PuppyCrawlComplexityReader : CheckStyleBaseReader, ICheckStylesMemberReader
    {
        public override string Source => PuppyCrawlSources.FanOutComplexity;

        public PuppyCrawlComplexityReader() : base(IntRegex)
        {
        }

        public void Parse(Domain.Member member, CheckStylesItem item)
        {
            member.Name = $"{item.Line}-{item.Column}";
            member.CylomaticComplexity = Parser.Match(item.Message).Value.AsInt();
        }
    }
}