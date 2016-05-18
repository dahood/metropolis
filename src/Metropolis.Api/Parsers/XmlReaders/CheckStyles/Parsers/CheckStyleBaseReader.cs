using System.Text.RegularExpressions;

namespace Metropolis.Api.Parsers.XmlReaders.CheckStyles.Parsers
{
    public abstract class CheckStyleBaseReader
    {
        protected const string IntRegex = @"\d+";
        public abstract string Source { get; }

        protected Regex Parser { get; }
        protected Regex IntParser { get; }

        protected CheckStyleBaseReader()
        {
            IntParser = new Regex(IntRegex, RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        }

        protected CheckStyleBaseReader(string regexPattern) : this()
        {
            Parser = new Regex(regexPattern, RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        }
    }
}