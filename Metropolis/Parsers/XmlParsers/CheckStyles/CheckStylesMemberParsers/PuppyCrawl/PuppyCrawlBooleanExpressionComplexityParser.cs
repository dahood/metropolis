using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl
{
    public class PuppyCrawlBooleanExpressionComplexityParser : MemberParserBase
    {
        public override string Source => "com.puppycrawl.tools.checkstyle.checks.metrics.BooleanExpressionComplexityCheck";
        
        public override void Parse(Member member, CheckStylesItem item)
        {
            member.BooleanExpressionComplexity = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}