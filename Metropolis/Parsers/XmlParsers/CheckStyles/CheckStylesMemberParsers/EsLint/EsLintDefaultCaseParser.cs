using Metropolis.Domain;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.EsLint
{
    public class EsLintDefaultCaseParser : MemberParserBase
    {
        public override string Source => EslintSources.DefaultCase;
        
        public override void Parse(Member member, CheckStylesItem item)
        {
            member.MissingDefaultCase += 1;
        }
    }
}