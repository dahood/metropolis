using System.Text.RegularExpressions;
using Metropolis.Domain;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.CheckStylesMemberParsers
{
    public abstract class MemberParserBase : ICheckStylesMemberParser
    {
        protected const string IntRegex = @"\d+";
        public abstract string Source { get; }

        protected Regex Parser { get; }
        protected Regex IntParser { get; }

        protected MemberParserBase()
        {
            IntParser = new Regex(IntRegex, RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        }

        protected MemberParserBase(string regexPattern) : this()
        {
            Parser = new Regex(regexPattern, RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        }

        public abstract void Parse(Member member, CheckStylesItem item);
    }
}