using Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers;
using Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl;

namespace Metropolis.Parsers.XmlParsers.CheckStyles
{
    public class PuppyCrawlCheckStylesClassBuilder : BaseCheckStylesClassBuilder
    {
        private static readonly ICheckStylesMemberParser[] MemberParsers =
        {
            new PuppyCrawlComplexityParser(), new PuppyCrawlLinesOfCodeParser(), new PuppyCrawlNumberOfParametersParser(),
            new PuppyCrawlDefaultCaseParser(), new PuppyCrawlBooleanExpressionComplexityParser(), new PupyyCrawlNestedTryDepthParser(),
            new PupyyCrawlNestedIfDepthParser(), new PuppyCrawlAnonymousInnerClassLenthParser(),
            new PuppyCrawlClassDataAbstractionCouplingParser()
        };

        public PuppyCrawlCheckStylesClassBuilder() : base(MemberParsers)
        {
        }

        public PuppyCrawlCheckStylesClassBuilder(params ICheckStylesMemberParser[] parsers) : base(parsers)
        {
        }
    }
}