using Metropolis.Api.Domain;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.EsLint
{
    public class EsLintCaseNoFallThroughReader : CheckStyleBaseReader, ICheckStylesMemberReader
    {
        public override string Source => EslintSources.CaseNoFallThrough;
        public void Parse(Member member, CheckStylesItem item)
        {
            member.NoFallthrough += 1;
        }
    }
}