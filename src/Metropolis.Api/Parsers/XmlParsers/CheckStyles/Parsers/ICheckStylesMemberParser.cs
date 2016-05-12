using Metropolis.Api.Domain;

namespace Metropolis.Api.Parsers.XmlParsers.CheckStyles.Parsers
{
    public interface ICheckStylesMemberParser
    {
        string Source { get; }
        void Parse(Member member, CheckStylesItem item);
    }
}