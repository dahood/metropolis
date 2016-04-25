using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.EsLint
{
    public class EsLintNumberOfStatmentsParser : MemberParserBase
    {
        public override string Source => "eslint.rules.max-statements";
        public EsLintNumberOfStatmentsParser() : base("[()]")
        {
        }

        public override void Parse(Member member, CheckStylesItem item)
        {
            member.LinesOfCode = Parser.Split(item.Message)[1].AsInt();
        }
    }
}