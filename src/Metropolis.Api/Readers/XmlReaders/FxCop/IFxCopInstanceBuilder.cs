using System.Xml.Linq;
using Metropolis.Api.Domain;

namespace Metropolis.Api.Readers.XmlReaders.FxCop
{
    public interface IFxCopInstanceBuilder 
    {
        Instance Build(XElement typeElement);
    }
}