using CsvHelper.Configuration;
using Metropolis.Api.Readers.CsvReaders.TypeConverters;
using Metropolis.Api.Readers.CsvReaders.TypeConverters.Sloc;

namespace Metropolis.Api.Readers.CsvReaders
{
    public sealed class SlocMap : CsvClassMap<SlocLineItem>
    {
        public SlocMap()
        {
            Map(x => x.PhysicalPath).Index(0);
            Map(x => x.Directory).Index(0).TypeConverter<SlocDirectoryConverter>();
            Map(x => x.FileName).Index(0).TypeConverter<SlocFileNameConverter>();
            Map(x => x.PhysicalLoc).Index(1).TypeConverter<IntTypeConverter>();
            Map(x => x.SourceLoc).Index(2).TypeConverter<IntTypeConverter>();
            Map(x => x.CommentLoc).Index(3).TypeConverter<IntTypeConverter>();
            Map(x => x.SingleLineCommentLoc).Index(4).TypeConverter<IntTypeConverter>();
            Map(x => x.BlockCommentLoc).Index(5).TypeConverter<IntTypeConverter>();
            Map(x => x.MixedLoc).Index(6).TypeConverter<IntTypeConverter>();
            Map(x => x.EmptyLoc).Index(7).TypeConverter<IntTypeConverter>();
        }
    }
}