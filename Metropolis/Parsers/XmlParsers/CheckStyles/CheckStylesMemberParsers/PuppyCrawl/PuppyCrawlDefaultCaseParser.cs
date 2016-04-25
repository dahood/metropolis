using Metropolis.Domain;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl
{
    public class PuppyCrawlDefaultCaseParser : MemberParserBase
    {
        public override string Source => PuppyCrawlSources.MissingSwitchDefault;
        
        public override void Parse(Member member, CheckStylesItem item)
        {
            member.MissingDefaultCase += 1;
        }
    }
}