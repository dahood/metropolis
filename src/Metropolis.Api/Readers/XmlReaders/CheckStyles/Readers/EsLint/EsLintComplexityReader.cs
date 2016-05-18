using System.Text.RegularExpressions;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.EsLint
{
    public class EsLintComplexityReader : CheckStyleBaseReader, ICheckStylesMemberReader
    {
        private readonly Regex nameRegex = new Regex("'",RegexOptions.IgnorePatternWhitespace|RegexOptions.Compiled);

        public override string Source => EslintSources.Complexity;

        public EsLintComplexityReader() : base(IntRegex)
        {
        }

        public void Read(Member member, CheckStylesItem item)
        {
            member.Name = nameRegex.Split(item.Message)[1];
            member.CylomaticComplexity = Parser.Match(item.Message).Value.AsInt();
        }
    }
}