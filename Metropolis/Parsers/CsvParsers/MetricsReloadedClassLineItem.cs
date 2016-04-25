using CsvHelper.Configuration;
using Metropolis.Parsers.CsvParsers.TypeConverters;
using Metropolis.Parsers.CsvParsers.TypeConverters.Checkstyles;
using Metropolis.Parsers.CsvParsers.TypeConverters.MetricsReloaded;

namespace Metropolis.Parsers.CsvParsers
{
    public class MetricsReloadedClassLineItem
    {
        public string Namespace { get; set; }
        public string Type { get; set; }
        public string Member { get; set; }
        public int ClassCoupling { get; set; }
        public int DepthOfInheritance { get; set; }
        public int LinesOfCode { get; set; }
    }

    public class MetricsReloadedClassMap : CsvClassMap<MetricsReloadedClassLineItem>
    {
        //Class,CBO,DIT,LOC
        //"org.jboss.as.appclient.component.ApplicationClientComponentDescription",10,2,32
        public MetricsReloadedClassMap()
        {
            Map(x => x.Namespace).Index(0).TypeConverter<CheckstylesNamespaceConverter>();
            Map(x => x.Type).Index(0).TypeConverter<CheckstylesClassConverter>();
            Map(x => x.ClassCoupling).Index(1).TypeConverter<IntTypeConverter>();
            Map(x => x.DepthOfInheritance).Index(2).TypeConverter<IntTypeConverter>();
            Map(x => x.LinesOfCode).Index(3).TypeConverter<IntTypeConverter>();
        }
    }
}