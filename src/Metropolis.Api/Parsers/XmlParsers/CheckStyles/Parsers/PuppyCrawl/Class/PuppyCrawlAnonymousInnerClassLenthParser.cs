using Metropolis.Api.Extensions;

namespace Metropolis.Api.Parsers.XmlParsers.CheckStyles.Parsers.PuppyCrawl.Class
{
    public class PuppyCrawlAnonymousInnerClassLenthParser : CheckStyleBaseParser, ICheckStylesClassParser
    {
        public override string Source => PuppyCrawlSources.AnonymousInnerClassLength;
        
        public void Parse(Domain.Instance type, CheckStylesItem item)
        {
            type.AnonymousInnerClassLength = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}