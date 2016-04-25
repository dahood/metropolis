using Metropolis.Domain;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.EsLint
{
    public class EsLintCaseNoFallThroughParser : CheckStyleBaseParser, ICheckStylesMemberParser
    {
        public override string Source => EslintSources.CaseNoFallThrough;
        public void Parse(Member member, CheckStylesItem item)
        {
            member.NoFallthrough += 1;
        }
    }
}