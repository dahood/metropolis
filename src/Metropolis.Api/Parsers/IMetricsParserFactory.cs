using Metropolis.Common.Models;

namespace Metropolis.Api.Parsers
{
    public interface IMetricsParserFactory
    {
        IInstanceReader ParserFor(ParseType parseType);
    }
}
