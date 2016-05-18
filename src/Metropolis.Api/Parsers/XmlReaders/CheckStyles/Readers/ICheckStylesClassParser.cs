
using Metropolis.Api.Domain;

namespace Metropolis.Api.Parsers.XmlReaders.CheckStyles.Readers
{
    public interface ICheckStylesClassParser
    {
        string Source { get; }
        void Parse(Instance type, CheckStylesItem item);
    }
}
