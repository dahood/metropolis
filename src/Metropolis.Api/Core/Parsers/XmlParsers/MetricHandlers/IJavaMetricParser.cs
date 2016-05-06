using System.Collections.Generic;
using System.Xml.Linq;
using Metropolis.Domain;

namespace Metropolis.Parsers.XmlParsers.MetricHandlers
{
    public interface IJavaMetricParser
    {
        string Id { get; }
        int Order { get; }
        void Parse(XElement metric, Dictionary<string, Class> classMap, XNamespace nameSpace);
    }
}
