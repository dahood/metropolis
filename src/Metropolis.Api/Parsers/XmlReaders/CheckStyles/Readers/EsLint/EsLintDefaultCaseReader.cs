using Metropolis.Api.Domain;

namespace Metropolis.Api.Parsers.XmlReaders.CheckStyles.Readers.EsLint
{
    public class EsLintDefaultCaseReader : CheckStyleBaseReader, ICheckStylesMemberParser
    {
        public override string Source => EslintSources.DefaultCase;
        
        public void Parse(Member member, CheckStylesItem item)
        {
            member.MissingDefaultCase += 1;
        }
    }
}