using Metropolis.Api.Domain;

namespace Metropolis.Api.Parsers.XmlReaders.CheckStyles.Readers
{
    public interface ICheckStylesMemberParser
    {
        string Source { get; }
        void Parse(Member member, CheckStylesItem item);
    }
}