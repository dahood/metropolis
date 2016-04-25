using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl
{
    public class PuppyCrawlClassFanOutComplexityParser : CheckStyleBaseParser, ICheckStylesClassParser
    {
        public override string Source => PuppyCrawlSources.ClassFanOutComplexity;
        
        public void Parse(Class type, CheckStylesItem item)
        {
            type.ClassFanOutComplexity= IntParser.Match(item.Message).Value.AsInt();
        }
    }
}