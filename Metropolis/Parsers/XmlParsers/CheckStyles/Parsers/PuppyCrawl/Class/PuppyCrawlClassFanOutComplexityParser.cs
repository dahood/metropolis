using Metropolis.Extensions;
using Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.Parsers.PuppyCrawl.Class
{
    public class PuppyCrawlClassFanOutComplexityParser : CheckStyleBaseParser, ICheckStylesClassParser
    {
        public override string Source => PuppyCrawlSources.ClassFanOutComplexity;
        
        public void Parse(Domain.Class type, CheckStylesItem item)
        {
            type.ClassFanOutComplexity= IntParser.Match(item.Message).Value.AsInt();
        }
    }
}