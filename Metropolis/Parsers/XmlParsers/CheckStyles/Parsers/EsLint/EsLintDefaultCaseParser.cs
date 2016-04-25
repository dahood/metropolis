using Metropolis.Domain;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.EsLint
{
    public class EsLintDefaultCaseParser : CheckStyleBaseParser, ICheckStylesMemberParser
    {
        public override string Source => EslintSources.DefaultCase;
        
        public void Parse(Member member, CheckStylesItem item)
        {
            member.MissingDefaultCase += 1;
        }
    }
}