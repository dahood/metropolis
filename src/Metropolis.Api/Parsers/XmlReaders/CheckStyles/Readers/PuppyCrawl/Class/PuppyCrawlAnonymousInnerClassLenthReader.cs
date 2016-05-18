using Metropolis.Api.Extensions;

namespace Metropolis.Api.Parsers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Class
{
    public class PuppyCrawlAnonymousInnerClassLenthReader : CheckStyleBaseReader, ICheckStylesClassParser
    {
        public override string Source => PuppyCrawlSources.AnonymousInnerClassLength;
        
        public void Parse(Domain.Instance type, CheckStylesItem item)
        {
            type.AnonymousInnerClassLength = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}