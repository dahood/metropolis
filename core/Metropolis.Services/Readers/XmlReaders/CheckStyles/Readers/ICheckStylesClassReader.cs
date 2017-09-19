
using Metropolis.Api.Domain;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers
{
    public interface ICheckStylesClassReader
    {
        string Source { get; }
        void Read(Instance type, CheckStylesItem item);
    }
}
