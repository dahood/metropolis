using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl
{
    public class PuppyCrawlAnonymousInnerClassLenthParser : CheckStyleBaseParser, ICheckStylesClassParser
    {
        public override string Source => PuppyCrawlSources.AnonymousInnerClassLength;
        
        public void Parse(Class type, CheckStylesItem item)
        {
            type.AnonymousInnerClassLenth = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}