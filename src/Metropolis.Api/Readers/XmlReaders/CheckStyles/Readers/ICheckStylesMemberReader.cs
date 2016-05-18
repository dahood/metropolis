using Metropolis.Api.Domain;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles.Readers
{
    public interface ICheckStylesMemberReader
    {
        string Source { get; }
        void Read(Member member, CheckStylesItem item);
    }
}