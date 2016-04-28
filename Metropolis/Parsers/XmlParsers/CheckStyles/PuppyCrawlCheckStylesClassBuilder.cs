using Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl;
using Metropolis.Parsers.XmlParsers.CheckStyles.Parsers;
using Metropolis.Parsers.XmlParsers.CheckStyles.Parsers.PuppyCrawl.Class;
using Metropolis.Parsers.XmlParsers.CheckStyles.Parsers.PuppyCrawl.Member;

namespace Metropolis.Parsers.XmlParsers.CheckStyles
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