using System.Collections.Generic;
using Metropolis.Api.Domain;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles
{
    public interface ICheckStylesClassBuilder
    {
        Instance Build(string key, List<CheckStylesItem> cls);
    }
}