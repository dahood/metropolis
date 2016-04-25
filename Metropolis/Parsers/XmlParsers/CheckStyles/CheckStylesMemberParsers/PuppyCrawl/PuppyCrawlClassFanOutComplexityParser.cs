using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl
{
    public class PuppyCrawlClassFanOutComplexityParser : MemberParserBase
    {
        public override string Source => "com.puppycrawl.tools.checkstyle.checks.metrics.ClassFanOutComplexityCheck";
        
        public override void Parse(Member member, CheckStylesItem item)
        {
            member.ClassFanOutComplexity= IntParser.Match(item.Message).Value.AsInt();
        }
    }
}