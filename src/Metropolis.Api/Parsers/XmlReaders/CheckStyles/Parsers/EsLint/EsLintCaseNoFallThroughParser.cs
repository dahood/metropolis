using Metropolis.Api.Domain;

namespace Metropolis.Api.Parsers.XmlReaders.CheckStyles.Parsers.EsLint
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