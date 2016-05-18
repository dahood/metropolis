using Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers;
using Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Class;
using Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Member;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles
{
    public class PuppyCrawlCheckStylesClassBuilder : BaseCheckStylesClassBuilder
    {
        private static readonly ICheckStylesClassReader[] PuppyCrawlClassReaders =
        { 
           new PuppyCrawlAnonymousInnerClassLenthReader(),
            new PuppyCrawlClassDataAbstractionCouplingReader(),
            new PuppyCrawlClassFanOutComplexityReader()
        };

        private static readonly ICheckStylesMemberReader[] PuppyCrawlMemberReaders =
        {
            new PuppyCrawlComplexityReader(), new PuppyCrawlMethodLengthReader(), new PuppyCrawlNumberOfParametersReader(),
            new PuppyCrawlDefaultCaseReader(), new PuppyCrawlBooleanExpressionComplexityReader(), new PupyyCrawlNestedTryDepthReader(),
            new PupyyCrawlNestedIfDepthReader(), 
        };

        public PuppyCrawlCheckStylesClassBuilder() : base(PuppyCrawlClassReaders, PuppyCrawlMemberReaders) {}
    }
}