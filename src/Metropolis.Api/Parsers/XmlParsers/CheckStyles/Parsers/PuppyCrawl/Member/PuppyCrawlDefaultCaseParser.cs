namespace Metropolis.Api.Parsers.XmlParsers.CheckStyles.Parsers.PuppyCrawl.Member
{
    public class PuppyCrawlDefaultCaseParser : CheckStyleBaseParser, ICheckStylesMemberParser
    {
        public override string Source => PuppyCrawlSources.MissingSwitchDefault;
        
        public void Parse(Domain.Member member, CheckStylesItem item)
        {
            member.MissingDefaultCase += 1;
        }
    }
}