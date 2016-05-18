using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.EsLint
{
    public class EsLintNumberOfStatmentsReader : CheckStyleBaseReader, ICheckStylesMemberReader
    {
        public override string Source => EslintSources.MemberNumberOfStatements;

        public EsLintNumberOfStatmentsReader() : base("[()]")
        {
        }

        public void Read(Member member, CheckStylesItem item)
        {
            member.LinesOfCode = Parser.Split(item.Message)[1].AsInt();
        }
    }
}