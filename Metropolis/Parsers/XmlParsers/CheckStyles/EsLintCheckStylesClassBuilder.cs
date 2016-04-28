using System.Linq;
using Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.EsLint;
using Metropolis.Parsers.XmlParsers.CheckStyles.Parsers;
using Metropolis.Parsers.XmlParsers.CheckStyles.Parsers.EsLint;

namespace Metropolis.Parsers.XmlParsers.CheckStyles
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