using Metropolis.Domain;
using Metropolis.Extensions;
using Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers;
using Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.EsLint;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.Parsers.EsLint
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