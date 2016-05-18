namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers.PuppyCrawl.Member
{
    public class PuppyCrawlDefaultCaseReader : CheckStyleBaseReader, ICheckStylesMemberReader
    {
        public override string Source => PuppyCrawlSources.MissingSwitchDefault;
        
        public void Parse(Domain.Member member, CheckStylesItem item)
        {
            member.MissingDefaultCase += 1;
        }
    }
}