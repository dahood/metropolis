using Metropolis.Api.Extensions;

namespace Metropolis.Api.Parsers.XmlReaders.CheckStyles.Parsers.PuppyCrawl.Member
{
    public class PuppyCrawlNumberOfParametersReader : CheckStyleBaseReader, ICheckStylesMemberParser
    {
        public override string Source => PuppyCrawlSources.NumberOfParameters;

        public PuppyCrawlNumberOfParametersReader() : base("[found|)]")
        {
            
        }
        
        public void Parse(Domain.Member member, CheckStylesItem item)
        {
            var split = Parser.Split(item.Message);
            member.NumberOfParameters = split[split.Length-2].AsInt();
        }
    }
}