using Metropolis.Api.Domain;

namespace Metropolis.Api.Parsers.XmlParsers.CheckStyles.Parsers.EsLint
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