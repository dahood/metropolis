using Metropolis.Api.Extensions;

namespace Metropolis.Api.Parsers.XmlParsers.CheckStyles.Parsers.PuppyCrawl.Class
{
    public class PuppyCrawlClassFanOutComplexityParser : CheckStyleBaseParser, ICheckStylesClassParser
    {
        public override string Source => PuppyCrawlSources.ClassFanOutComplexity;
        
        public void Parse(Domain.Instance type, CheckStylesItem item)
        {
            type.ClassFanOutComplexity= IntParser.Match(item.Message).Value.AsInt();
        }
    }
}