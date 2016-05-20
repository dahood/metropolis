using Metropolis.Common.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Member
{
    public class PuppyCrawlNumberOfParametersReader : CheckStyleBaseReader, ICheckStylesMemberReader
    {
        public override string Source => PuppyCrawlSources.NumberOfParameters;

        public PuppyCrawlNumberOfParametersReader() : base("[found|)]")
        {
            
        }
        
        public void Read(Domain.Member member, CheckStylesItem item)
        {
            var split = Parser.Split(item.Message);
            member.NumberOfParameters = split[split.Length-2].AsInt();
        }
    }
}