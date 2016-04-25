using CsvHelper.Configuration;
using Metropolis.Parsers.CsvParsers.TypeConverters;
using Metropolis.Parsers.CsvParsers.TypeConverters.MetricsReloaded;

namespace Metropolis.Parsers.CsvParsers
{
    public class MetricsReloadedMethodLineItem
    {
        public string Project { get; set; }
        public string Namespace { get; set; }
        public string Type { get; set; }
        public string Member { get; set; }
        public int CyclomaticComplexity { get; set; }
        public int NumberOfParameters { get; set; }
        public int LinesOfCode { get; set; }
    }
    
    public class MetricsReloadedMethodMap : CsvClassMap<MetricsReloadedMethodLineItem>
    {
        //v(G) = Cylomatic complexity
        //NP = Number of Parameters

        //Method,LOC,NP,v(G)
        //"atg.commerce.order.ShawShoppingCartFormHandler.getCatalogRepository()",7,0,1
        public MetricsReloadedMethodMap()
        {
            Map(x => x.Namespace).Index(0).TypeConverter<CheckstylesMethodNamespaceConverter>();
            Map(x => x.Type).Index(0).TypeConverter<MetricsReloadedTypeConverter>();
            Map(x => x.Member).Index(0).TypeConverter<MetricsReloadedMethodConverter>();
            Map(x => x.LinesOfCode).Index(1).TypeConverter<IntTypeConverter>();
            Map(x => x.NumberOfParameters).Index(2).TypeConverter<IntTypeConverter>();
            Map(x => x.CyclomaticComplexity).Index(3).TypeConverter<IntTypeConverter>();
        }
    }
}