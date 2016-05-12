using Metropolis.Api.Domain;

namespace Metropolis.Api.Parsers
{
    public interface IClassParser
    {
        CodeBase Parse(string fileName);
    }
}