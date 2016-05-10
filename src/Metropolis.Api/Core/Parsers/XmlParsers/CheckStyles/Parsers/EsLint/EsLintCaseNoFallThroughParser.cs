using Metropolis.Api.Core.Domain;

namespace Metropolis.Api.Core.Parsers.XmlParsers.CheckStyles.Parsers.EsLint
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