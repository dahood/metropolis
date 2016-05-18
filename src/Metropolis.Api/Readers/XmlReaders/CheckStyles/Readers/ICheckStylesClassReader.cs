
using Metropolis.Api.Domain;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers
{
    public interface ICheckStylesClassReader
    {
        string Source { get; }
        void Parse(Instance type, CheckStylesItem item);
    }
}
