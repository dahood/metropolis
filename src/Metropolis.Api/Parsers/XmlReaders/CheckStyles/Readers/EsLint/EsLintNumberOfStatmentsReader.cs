using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;

namespace Metropolis.Api.Parsers.XmlReaders.CheckStyles.Readers.EsLint
{
    public class EsLintNumberOfStatmentsReader : CheckStyleBaseReader, ICheckStylesMemberParser
    {
        public override string Source => EslintSources.MemberNumberOfStatements;

        public EsLintNumberOfStatmentsReader() : base("[()]")
        {
        }

        public void Parse(Member member, CheckStylesItem item)
        {
            member.LinesOfCode = Parser.Split(item.Message)[1].AsInt();
        }
    }
}