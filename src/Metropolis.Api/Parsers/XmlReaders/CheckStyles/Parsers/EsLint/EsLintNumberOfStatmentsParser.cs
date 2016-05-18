using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;

namespace Metropolis.Api.Parsers.XmlReaders.CheckStyles.Parsers.EsLint
{
    public class EsLintNumberOfStatmentsParser : CheckStyleBaseParser, ICheckStylesMemberParser
    {
        public override string Source => EslintSources.MemberNumberOfStatements;

        public EsLintNumberOfStatmentsParser() : base("[()]")
        {
        }

        public void Parse(Member member, CheckStylesItem item)
        {
            member.LinesOfCode = Parser.Split(item.Message)[1].AsInt();
        }
    }
}