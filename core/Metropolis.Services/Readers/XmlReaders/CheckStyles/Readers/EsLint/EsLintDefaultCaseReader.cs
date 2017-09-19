using Metropolis.Api.Domain;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.EsLint
{
    public class EsLintDefaultCaseReader : CheckStyleBaseReader, ICheckStylesMemberReader
    {
        public override string Source => EslintSources.DefaultCase;
        
        public void Read(Member member, CheckStylesItem item)
        {
            member.MissingDefaultCase += 1;
        }
    }
}