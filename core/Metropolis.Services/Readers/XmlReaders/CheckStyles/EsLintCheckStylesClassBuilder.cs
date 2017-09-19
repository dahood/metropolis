using System.Linq;
using Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers;
using Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.EsLint;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles
{
    public class EsLintCheckStylesClassBuilder : BaseCheckStylesClassBuilder
    {
        private static readonly ICheckStylesMemberReader[] EsLintMemberReaders =
        {
            new EsLintComplexityReader(), new EsLintNumberOfParametersReader(),
            new EsLintDefaultCaseReader(), new EsLintCaseNoFallThroughReader()
        };
        public EsLintCheckStylesClassBuilder() : base(Enumerable.Empty<ICheckStylesClassReader>(), EsLintMemberReaders) { }

        protected override ICheckStylesMemberReader MethodLengthSourceType => new EsLintNumberOfStatmentsReader();
    }
}