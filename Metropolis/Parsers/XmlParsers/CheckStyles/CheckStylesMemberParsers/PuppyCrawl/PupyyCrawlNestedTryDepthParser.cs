using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl
{
    public class PupyyCrawlNestedTryDepthParser : MemberParserBase
    {
        public override string Source => "com.puppycrawl.tools.checkstyle.checks.coding.NestedTryDepthCheck";
        
        public override void Parse(Member member, CheckStylesItem item)
        {
            member.NestedTryDepth = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}