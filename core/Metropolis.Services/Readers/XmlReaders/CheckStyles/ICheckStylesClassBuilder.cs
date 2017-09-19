using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Metropolis.Api.Domain;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles
{
    public interface ICheckStylesClassBuilder
    {
        Instance Build(string fileFullName, List<CheckStylesItem> cls);
    }
}