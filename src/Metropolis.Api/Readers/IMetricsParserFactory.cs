using Metropolis.Common.Models;

namespace Metropolis.Api.Readers
{
    public interface IMetricsParserFactory
    {
        IInstanceReader ParserFor(ParseType parseType);
    }
}
