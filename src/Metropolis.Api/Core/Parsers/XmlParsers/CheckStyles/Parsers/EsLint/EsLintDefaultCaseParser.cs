using Metropolis.Api.Core.Domain;

namespace Metropolis.Api.Core.Parsers.XmlParsers.CheckStyles.Parsers.EsLint
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