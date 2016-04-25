using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.EsLint
{
    public class EsLintNumberOfParametersParser : MemberParserBase
    {
        public override string Source => EslintSources.NumberOfParameters;
        
        public override void Parse(Member member, CheckStylesItem item)
        {
            member.NumberOfParameters = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}