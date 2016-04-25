using System.Text.RegularExpressions;

namespace Metropolis.Parsers.XmlParsers.CheckStyles.Parsers
{
    public abstract class CheckStyleBaseParser
    {
        protected const string IntRegex = @"\d+";
        public abstract string Source { get; }

        protected Regex Parser { get; }
        protected Regex IntParser { get; }

        protected CheckStyleBaseParser()
        {
            IntParser = new Regex(IntRegex, RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        }

        protected CheckStyleBaseParser(string regexPattern) : this()
        {
            Parser = new Regex(regexPattern, RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        }
    }
}