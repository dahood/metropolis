using System.Text.RegularExpressions;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;

namespace Metropolis.Api.Parsers.XmlReaders.CheckStyles.Readers.EsLint
{
    public class EsLintComplexityReader : CheckStyleBaseReader, ICheckStylesMemberParser
    {
        private readonly Regex nameRegex = new Regex("'",RegexOptions.IgnorePatternWhitespace|RegexOptions.Compiled);

        public override string Source => EslintSources.Complexity;

        public EsLintComplexityReader() : base(IntRegex)
        {
        }

        public void Parse(Member member, CheckStylesItem item)
        {
            member.Name = nameRegex.Split(item.Message)[1];
            member.CylomaticComplexity = Parser.Match(item.Message).Value.AsInt();
        }
    }
}