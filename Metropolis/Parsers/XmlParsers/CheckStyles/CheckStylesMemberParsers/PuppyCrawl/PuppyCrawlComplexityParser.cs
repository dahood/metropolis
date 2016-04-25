using Metropolis.Domain;
using Metropolis.Extensions;
using Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.EsLint;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl
{
    public class PuppyCrawlComplexityParser : MemberParserBase
    {
        public override string Source => PuppyCrawlSources.FanOutComplexity;

        public PuppyCrawlComplexityParser() : base(IntRegex)
        {
        }

        public override void Parse(Member member, CheckStylesItem item)
        {
            member.Name = $"{item.Line}-{item.Column}";
            member.CylomaticComplexity = Parser.Match(item.Message).Value.AsInt();
        }
    }
}