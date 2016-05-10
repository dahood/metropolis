using Metropolis.Api.Core.Domain;
using Metropolis.Api.Extensions;

namespace Metropolis.Api.Core.Parsers.XmlParsers.CheckStyles.Parsers.EsLint
{
    public class EsLintNumberOfParametersParser : CheckStyleBaseParser, ICheckStylesMemberParser
    {
        public override string Source => EslintSources.NumberOfParameters;
        
        public void Parse(Member member, CheckStylesItem item)
        {
            member.NumberOfParameters = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}