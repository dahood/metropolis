
using Metropolis.Domain;

namespace Metropolis.Parsers.XmlParsers.CheckStyles
{
    public interface ICheckStylesClassParser
    {
        string Source { get; }
        void Parse(Class type, CheckStylesItem item);
    }
}
