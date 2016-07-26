using Metropolis.Api.Domain;
using Metropolis.Common.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.EsLint
{
    public class EsLintNumberOfStatmentsReader : CheckStyleBaseReader, ICheckStylesMemberReader
    {
        private const int EsLintMethodFudgeFactor = 2;

        public override string Source => EslintSources.MemberNumberOfStatements;

        public EsLintNumberOfStatmentsReader() : base("[()]")
        {
        }

        public void Read(Member member, CheckStylesItem item)
        {
            var linesOfCode = Parser.Split(item.Message)[1].AsInt();
            member.StartLine = item.Line;
            member.EndLine = item.Line + linesOfCode + EsLintMethodFudgeFactor;
            member.LinesOfCode = linesOfCode;
        }
    }
}