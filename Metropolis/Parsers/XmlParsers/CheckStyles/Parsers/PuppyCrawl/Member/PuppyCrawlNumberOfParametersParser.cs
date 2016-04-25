using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl
{
    public class PuppyCrawlNumberOfParametersParser : CheckStyleBaseParser, ICheckStylesMemberParser
    {
        public override string Source => PuppyCrawlSources.NumberOfParameters;

        public PuppyCrawlNumberOfParametersParser() : base("[found|)]")
        {
            
        }
        
        public void Parse(Member member, CheckStylesItem item)
        {
            var split = Parser.Split(item.Message);
            member.NumberOfParameters = split[split.Length-2].AsInt();
        }
    }
}