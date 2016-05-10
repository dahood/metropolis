using Metropolis.Api.Extensions;

namespace Metropolis.Api.Core.Parsers.XmlParsers.CheckStyles.Parsers.PuppyCrawl.Member
{
    public class PuppyCrawlNumberOfParametersParser : CheckStyleBaseParser, ICheckStylesMemberParser
    {
        public override string Source => PuppyCrawlSources.NumberOfParameters;

        public PuppyCrawlNumberOfParametersParser() : base("[found|)]")
        {
            
        }
        
        public void Parse(Domain.Member member, CheckStylesItem item)
        {
            var split = Parser.Split(item.Message);
            member.NumberOfParameters = split[split.Length-2].AsInt();
        }
    }
}