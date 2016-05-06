using Metropolis.Domain;
using Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.EsLint;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.Parsers.EsLint
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