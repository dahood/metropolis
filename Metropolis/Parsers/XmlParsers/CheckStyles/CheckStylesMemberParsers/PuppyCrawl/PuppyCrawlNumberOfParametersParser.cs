using System.Linq;
using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers.PuppyCrawl
{
    public class PuppyCrawlNumberOfParametersParser : MemberParserBase
    {
        public override string Source => "com.puppycrawl.tools.checkstyle.checks.sizes.ParameterNumberCheck";

        public PuppyCrawlNumberOfParametersParser() : base("[found|)]")
        {
            
        }
        
        public override void Parse(Member member, CheckStylesItem item)
        {
            var split = Parser.Split(item.Message);
            member.NumberOfParameters = split[split.Length-2].AsInt();
        }
    }
}