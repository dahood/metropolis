using Metropolis.Api.Core.Domain;

namespace Metropolis.Api.Core.Parsers.XmlParsers
{
    public interface IClassParser
    {
        CodeBase Parse(string fileName, string sourceBaseDirector = "");
    }
}