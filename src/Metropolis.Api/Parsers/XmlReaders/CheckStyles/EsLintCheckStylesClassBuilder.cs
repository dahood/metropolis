using System.Linq;
using Metropolis.Api.Parsers.XmlReaders.CheckStyles.Readers;
using Metropolis.Api.Parsers.XmlReaders.CheckStyles.Readers.EsLint;

namespace Metropolis.Api.Parsers.XmlReaders.CheckStyles
{
    public class EsLintCheckStylesClassBuilder : BaseCheckStylesClassBuilder
    {
        private static readonly ICheckStylesMemberParser[] EsLintMemberParsers =
        {
            new EsLintComplexityReader(), new EsLintNumberOfStatmentsReader(), new EsLintNumberOfParametersReader(),
            new EsLintDefaultCaseReader(), new EsLintCaseNoFallThroughReader()
        };
        public EsLintCheckStylesClassBuilder() : base(Enumerable.Empty<ICheckStylesClassParser>(), EsLintMemberParsers) { }
    }
}