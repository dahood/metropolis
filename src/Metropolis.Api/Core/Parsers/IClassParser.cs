using Metropolis.Api.Core.Domain;

namespace Metropolis.Api.Core.Parsers
{
    public interface IClassParser
    {
        CodeBase Parse(string fileName, string sourceBaseDirector = "");
    }
}