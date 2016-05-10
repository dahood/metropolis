using System.Collections.Generic;
using System.Xml.Linq;
using Metropolis.Api.Core.Domain;

namespace Metropolis.Api.Core.Parsers.XmlParsers.MetricHandlers
{
    public interface IJavaMetricParser
    {
        string Id { get; }
        int Order { get; }
        void Parse(XElement metric, Dictionary<string, Class> classMap, XNamespace nameSpace);
    }
}
