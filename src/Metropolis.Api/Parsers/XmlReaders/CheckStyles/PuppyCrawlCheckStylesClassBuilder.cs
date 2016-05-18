using Metropolis.Api.Parsers.XmlReaders.CheckStyles.Parsers;
using Metropolis.Api.Parsers.XmlReaders.CheckStyles.Parsers.PuppyCrawl.Class;
using Metropolis.Api.Parsers.XmlReaders.CheckStyles.Parsers.PuppyCrawl.Member;

namespace Metropolis.Api.Parsers.XmlReaders.CheckStyles
{
    public class PuppyCrawlCheckStylesClassBuilder : BaseCheckStylesClassBuilder
    {
        private static readonly ICheckStylesClassParser[] PuppyCrawlClassParsers =
        { 
           new PuppyCrawlAnonymousInnerClassLenthParser(),
            new PuppyCrawlClassDataAbstractionCouplingParser(),
            new PuppyCrawlClassFanOutComplexityParser()
        };

        private static readonly ICheckStylesMemberParser[] PuppyCrawlMemberParsers =
        {
            new PuppyCrawlComplexityParser(), new PuppyCrawlMethodLengthParser(), new PuppyCrawlNumberOfParametersParser(),
            new PuppyCrawlDefaultCaseParser(), new PuppyCrawlBooleanExpressionComplexityParser(), new PupyyCrawlNestedTryDepthParser(),
            new PupyyCrawlNestedIfDepthParser(), 
        };

        public PuppyCrawlCheckStylesClassBuilder() : base(PuppyCrawlClassParsers, PuppyCrawlMemberParsers) {}
    }
}