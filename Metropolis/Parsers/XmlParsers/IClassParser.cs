using Metropolis.Domain;

namespace Metropolis.Parsers
{
    public interface IClassParser
    {
        CodeBase Parse(string fileName);
    }
}