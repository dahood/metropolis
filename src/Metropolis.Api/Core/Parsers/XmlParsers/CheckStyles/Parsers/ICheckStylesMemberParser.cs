using Metropolis.Api.Core.Domain;

namespace Metropolis.Api.Core.Parsers.XmlParsers.CheckStyles.Parsers
{
    public interface ICheckStylesMemberParser
    {
        string Source { get; }
        void Parse(Member member, CheckStylesItem item);
    }
}