using Metropolis.Api.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Class
{
    public class PuppyCrawlAnonymousInnerClassLenthReader : CheckStyleBaseReader, ICheckStylesClassReader
    {
        public override string Source => PuppyCrawlSources.AnonymousInnerClassLength;
        
        public void Parse(Domain.Instance type, CheckStylesItem item)
        {
            type.AnonymousInnerClassLength = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}