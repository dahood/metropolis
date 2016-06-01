using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CsvHelper.Configuration;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;
using Metropolis.Api.Readers.CsvReaders.TypeConverters;
using Metropolis.Api.Readers.CsvReaders.TypeConverters.Sloc;

namespace Metropolis.Api.Readers.CsvReaders
{
    public enum FileInclusion
    {
        [Description(".js")] Js,

        [Description(".cs")] CSharp,

        [Description(".java")] Java
    }

    public class SourceLinesOfCodeReader : CsvInstanceReader<SourceLinesOfCodeLineItem, SourceLinesOfCodeMap>
    {
        public SourceLinesOfCodeReader(FileInclusion inclusion = FileInclusion.Js) : base(true)
        {
            Inclusion = inclusion;
        }

        public SourceLinesOfCodeReader() : this(FileInclusion.Js)
        {
        }

        private FileInclusion Inclusion { get; }

        protected override CodeBase ParseLines(IEnumerable<SourceLinesOfCodeLineItem> lines)
        {
            var inclusionExtension = Inclusion.GetDescription();
            var inclusionCodeBagType = MapToCodeBag(Inclusion);
            var classes = lines.Where(x => x.Class.EndsWith(inclusionExtension))
                //TODO: Grab physical path!!!!
                .Select(each => new Instance(each.Class, each.Namespace, inclusionCodeBagType, string.Empty) {LinesOfCode = each.SourceLoc})
                .ToList();

            return new CodeBase(new CodeGraph(classes));
        }

        private CodeBagType MapToCodeBag(FileInclusion inclusion)
        {
            switch (inclusion)
            {
                case FileInclusion.CSharp:
                    return CodeBagType.Namespace;
                case FileInclusion.Java:
                    return CodeBagType.Package;
                case FileInclusion.Js:
                    return CodeBagType.Directory;

                default:
                    return CodeBagType.Empty;
            }
        }
    }

    public sealed class SourceLinesOfCodeMap : CsvClassMap<SourceLinesOfCodeLineItem>
    {
        public SourceLinesOfCodeMap()
        {
            Map(x => x.Namespace).Index(0).TypeConverter<SourceLinesOfCodeNamespaceConverter>();
            Map(x => x.Class).Index(0).TypeConverter<SourceLinesOfCodeClassConverter>();
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