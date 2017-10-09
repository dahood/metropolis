using System.Collections.Generic;
using ArchiMetrics.Analysis.Common.Metrics;

namespace Metropolis.Common.Models
{
    public abstract class BaseMetricsResult
    {
        public ParseType ParseType { get; set; }
    }

    public class MetricsResult : BaseMetricsResult
    {
        public string MetricsFile { get; set; }

        public static IEnumerable<MetricsResult> Empty()
        {
            return new MetricsResult[0];
        }
    }

    public class CSharpMetricsResult : MetricsResult
    {
        public INamespaceMetric Metric { get; }

        public CSharpMetricsResult(INamespaceMetric metrics)
        {
            Metric = metrics;
            ParseType = ParseType.CSharp;
        }
    }
}