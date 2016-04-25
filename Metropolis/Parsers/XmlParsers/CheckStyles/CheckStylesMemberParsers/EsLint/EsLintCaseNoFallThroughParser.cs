using Metropolis.Domain;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.EsLint
{
    public class EsLintCaseNoFallThroughParser : MemberParserBase
    {
        public override string Source => "eslint.rules.no-fallthrough";
        public override void Parse(Member member, CheckStylesItem item)
        {
            member.NoFallthrough += 1;
        }
    }
}