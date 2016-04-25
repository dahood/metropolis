using Metropolis.Domain;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.EsLint
{
    public class EsLintCaseNoFallThroughParser : MemberParserBase
    {
        public override string Source => EslintSources.CaseNoFallThrough;
        public override void Parse(Member member, CheckStylesItem item)
        {
            member.NoFallthrough += 1;
        }
    }
}