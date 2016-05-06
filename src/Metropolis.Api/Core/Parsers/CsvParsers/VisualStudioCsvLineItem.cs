using CsvHelper.Configuration;

namespace Metropolis.Parsers.CsvParsers
{
    public class VisualStudioCsvLineItem
    {
        public string Scope { get; set; }
        public string Project { get; set; }
        public string Namespace { get; set; }
        public string Type { get; set; }
        public string Member { get; set; }
        public int MaintainabilityIndex { get; set; }
        public int CyclomaticComplexity { get; set; }
        public int DepthOfInheritance { get; set; }
        public int ClassCoupling { get; set; }
        public int LinesOfCode { get; set; }
    }

    public sealed class VisualStudioCsvLineItemClassMap : CsvClassMap<VisualStudioCsvLineItem>
    { 


        public VisualStudioCsvLineItemClassMap()
        {
            Map(m => m.Scope);
            Map(m => m.Project);
            Map(m => m.Namespace);
            Map(m => m.Type);
            Map(m => m.Member);
            Map(m => m.MaintainabilityIndex).Name("Maintainability Index").Default(-1);
            Map(m => m.CyclomaticComplexity).Name("Cyclomatic Complexity");
            Map(m => m.DepthOfInheritance).Name("Depth of Inheritance").Default(-1);
            Map(m => m.ClassCoupling).Name("Class Coupling");
            Map(m => m.LinesOfCode).Name("Lines of Code");
        }
    }
}