using Metropolis.Api.Domain;
using Metropolis.Common.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.EsLint
{
    public class EsLintNumberOfParametersReader : CheckStyleBaseReader, ICheckStylesMemberReader
    {
        public override string Source => EslintSources.NumberOfParameters;
        
        public void Read(Member member, CheckStylesItem item)
        {
            member.NumberOfParameters = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}