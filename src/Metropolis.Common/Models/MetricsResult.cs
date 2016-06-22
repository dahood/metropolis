using System.Collections.Generic;

namespace Metropolis.Common.Models
{
    public class MetricsResult
    {
        public ParseType ParseType { get; set; }
        public string MetricsFile { get; set; }

        public static IEnumerable<MetricsResult> Empty()
        {
            return new MetricsResult[0];
        }
    }
}