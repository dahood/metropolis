using Metropolis.Api.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Class
{
    public class PuppyCrawlClassDataAbstractionCouplingReader : CheckStyleBaseReader, ICheckStylesClassReader
    {
        public override string Source => PuppyCrawlSources.ClassDataAbstractionCoupling;
        
        public void Read(Domain.Instance type, CheckStylesItem item)
        {
            type.ClassDataAbstractionCoupling = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}