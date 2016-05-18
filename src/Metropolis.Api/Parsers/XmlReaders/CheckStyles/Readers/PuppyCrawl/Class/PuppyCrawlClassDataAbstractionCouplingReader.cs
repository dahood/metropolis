using Metropolis.Api.Extensions;

namespace Metropolis.Api.Parsers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Class
{
    public class PuppyCrawlClassDataAbstractionCouplingReader : CheckStyleBaseReader, ICheckStylesClassParser
    {
        public override string Source => PuppyCrawlSources.ClassDataAbstractionCoupling;
        
        public void Parse(Domain.Instance type, CheckStylesItem item)
        {
            type.ClassDataAbstractionCoupling = IntParser.Match(item.Message).Value.AsInt();
        }
    }
}