namespace Metropolis.Api.Parsers.XmlReaders.CheckStyles.Parsers.PuppyCrawl.Member
{
    public class PuppyCrawlDefaultCaseReader : CheckStyleBaseReader, ICheckStylesMemberParser
    {
        public override string Source => PuppyCrawlSources.MissingSwitchDefault;
        
        public void Parse(Domain.Member member, CheckStylesItem item)
        {
            member.MissingDefaultCase += 1;
        }
    }
}