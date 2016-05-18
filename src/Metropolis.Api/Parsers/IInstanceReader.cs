using Metropolis.Api.Domain;

namespace Metropolis.Api.Parsers
{
    public interface IInstanceReader
    {
        CodeBase Parse(string fileName);
    }
}