using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl
{
    public class PuppyCrawlAnonymousInnerClassLenthParser : MemberParserBase
    {
        public override string Source => "com.puppycrawl.tools.checkstyle.checks.sizes.AnonInnerLengthCheck";
        
        public override void Parse(Member member, CheckStylesItem item)
        {
            member.AnonymousInnerClassLenth= IntParser.Match(item.Message).Value.AsInt();
        }
    }
}