using CsvHelper.Configuration;

namespace Metropolis.Parsers.CsvParsers
{
    public class ToxicityCsvLineItem
    {
        public string Namespace { get; set; }
        public string Type { get; set; }
        public int CyclomaticComplexity { get; set; }
        public int DepthOfInheritance { get; set; }
        public int ClassCoupling { get; set; }
        public int LinesOfCode { get; set; }
        public int NumberOfMethods { get; set; }
        public int Toxicity { get; set; }
    }

    public sealed class ToxicityCsvLineItemClassMap : CsvClassMap<ToxicityCsvLineItem>
    {
        public ToxicityCsvLineItemClassMap()
        {
            Map(m => m.Namespace);
            Map(m => m.Type);
            Map(m => m.CyclomaticComplexity);
            Map(m => m.DepthOfInheritance);
            Map(m => m.ClassCoupling);
            Map(m => m.LinesOfCode);
            Map(m => m.NumberOfMethods);
            Map(m => m.Toxicity);
        }
    }
}