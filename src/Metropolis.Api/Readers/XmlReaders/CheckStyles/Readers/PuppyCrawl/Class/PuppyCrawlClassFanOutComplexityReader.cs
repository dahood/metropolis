using Metropolis.Api.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Class
{
    public class PuppyCrawlClassFanOutComplexityReader : CheckStyleBaseReader, ICheckStylesClassReader
    {
        public override string Source => PuppyCrawlSources.ClassFanOutComplexity;
        
        public void Read(Domain.Instance type, CheckStylesItem item)
        {
            type.ClassFanOutComplexity= IntParser.Match(item.Message).Value.AsInt();
        }
    }
}