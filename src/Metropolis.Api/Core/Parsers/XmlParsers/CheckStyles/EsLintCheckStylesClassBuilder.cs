using System.Linq;
using Metropolis.Api.Core.Parsers.XmlParsers.CheckStyles.Parsers;
using Metropolis.Api.Core.Parsers.XmlParsers.CheckStyles.Parsers.EsLint;

namespace Metropolis.Api.Core.Parsers.XmlParsers.CheckStyles
{
    public class EsLintCheckStylesClassBuilder : BaseCheckStylesClassBuilder
    {
        private static readonly ICheckStylesMemberParser[] EsLintMemberParsers =
        {
            new EsLintComplexityParser(), new EsLintNumberOfStatmentsParser(), new EsLintNumberOfParametersParser(),
            new EsLintDefaultCaseParser(), new EsLintCaseNoFallThroughParser()
        };
        public EsLintCheckStylesClassBuilder() : base(Enumerable.Empty<ICheckStylesClassParser>(), EsLintMemberParsers) { }
    }
}