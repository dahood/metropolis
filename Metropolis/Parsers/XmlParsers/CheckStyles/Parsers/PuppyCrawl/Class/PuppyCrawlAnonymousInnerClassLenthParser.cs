using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.Parsers.PuppyCrawl.Class
{
    public class PuppyCrawlAnonymousInnerClassLenthParser : CheckStyleBaseParser, ICheckStylesClassParser
    {
        public override string Source => PuppyCrawlSources.AnonymousInnerClassLength;
        
        public void Parse(Domain.Class type, CheckStylesItem item)
        {
            type.AnonymousInnerClassLenth = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}