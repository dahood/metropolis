using Metropolis.Common.Models;

namespace Metropolis.Api.Readers
{
    public interface IMetricsReaderFactory
    {
        IInstanceReader GetReader(ParseType parseType);
    }
}
