
using Metropolis.Api.Core.Domain;

namespace Metropolis.Api.Core.Parsers.XmlParsers.CheckStyles.Parsers
{
    public interface ICheckStylesClassParser
    {
        string Source { get; }
        void Parse(Class type, CheckStylesItem item);
    }
}
