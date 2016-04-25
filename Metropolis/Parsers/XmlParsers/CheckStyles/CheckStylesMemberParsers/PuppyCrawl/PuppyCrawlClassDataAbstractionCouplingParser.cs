using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl
{
    public class PuppyCrawlClassDataAbstractionCouplingParser : MemberParserBase
    {
        public override string Source => "com.puppycrawl.tools.checkstyle.checks.metrics.ClassDataAbstractionCouplingCheck";
        
        public override void Parse(Member member, CheckStylesItem item)
        {
            member.ClassDataAbstractionCoupling= IntParser.Match(item.Message).Value.AsInt();
        }
    }
}