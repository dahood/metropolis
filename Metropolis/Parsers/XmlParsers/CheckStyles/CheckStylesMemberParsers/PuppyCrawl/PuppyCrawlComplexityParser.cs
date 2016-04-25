using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl
{
    public class PuppyCrawlComplexityParser : MemberParserBase
    {
        public override string Source => "com.puppycrawl.tools.checkstyle.checks.metrics.ClassFanOutComplexityCheck";

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