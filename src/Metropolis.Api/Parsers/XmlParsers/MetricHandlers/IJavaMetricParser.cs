using System.Collections.Generic;
using System.Xml.Linq;
using Metropolis.Api.Domain;

namespace Metropolis.Api.Parsers.XmlParsers.MetricHandlers
{
    public interface IJavaMetricParser
    {
        string Id { get; }
        int Order { get; }
        void Parse(XElement metric, Dictionary<string, Instance> classMap, XNamespace nameSpace);
    }
}
