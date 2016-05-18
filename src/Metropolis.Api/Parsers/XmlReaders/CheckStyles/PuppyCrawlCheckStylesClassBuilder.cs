using Metropolis.Api.Parsers.XmlReaders.CheckStyles.Parsers;
using Metropolis.Api.Parsers.XmlReaders.CheckStyles.Parsers.PuppyCrawl.Class;
using Metropolis.Api.Parsers.XmlReaders.CheckStyles.Parsers.PuppyCrawl.Member;

namespace Metropolis.Api.Parsers.XmlReaders.CheckStyles
{
    public class PuppyCrawlCheckStylesClassBuilder : BaseCheckStylesClassBuilder
    {
        private static readonly ICheckStylesClassParser[] PuppyCrawlClassParsers =
        { 
           new PuppyCrawlAnonymousInnerClassLenthReader(),
            new PuppyCrawlClassDataAbstractionCouplingReader(),
            new PuppyCrawlClassFanOutComplexityReader()
        };

        private static readonly ICheckStylesMemberParser[] PuppyCrawlMemberParsers =
        {
            new PuppyCrawlComplexityReader(), new PuppyCrawlMethodLengthReader(), new PuppyCrawlNumberOfParametersReader(),
            new PuppyCrawlDefaultCaseReader(), new PuppyCrawlBooleanExpressionComplexityReader(), new PupyyCrawlNestedTryDepthReader(),
            new PupyyCrawlNestedIfDepthReader(), 
        };

        public PuppyCrawlCheckStylesClassBuilder() : base(PuppyCrawlClassParsers, PuppyCrawlMemberParsers) {}
    }
}