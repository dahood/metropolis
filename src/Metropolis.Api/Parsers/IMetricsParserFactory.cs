using Metropolis.Common.Models;

namespace Metropolis.Api.Parsers
{
    public interface IMetricsParserFactory
    {
        IClassParser ParserFor(ParseType parseType);
    }
}
