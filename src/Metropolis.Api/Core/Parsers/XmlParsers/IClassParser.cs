using Metropolis.Domain;

namespace Metropolis.Parsers.XmlParsers
{
    public interface IClassParser
    {
        CodeBase Parse(string fileName, string sourceBaseDirector = "");
    }
}