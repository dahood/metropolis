using Metropolis.Api.Domain;

namespace Metropolis.Api.Readers
{
    public interface IInstanceReader
    {
        CodeBase Parse(string fileName);
    }
}