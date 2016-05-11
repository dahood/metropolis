using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CsvHelper.Configuration;
using Metropolis.Api.Core.Domain;
using Metropolis.Api.Core.Parsers.CsvParsers.TypeConverters;
using Metropolis.Api.Core.Parsers.CsvParsers.TypeConverters.Sloc;
using Metropolis.Api.Extensions;

namespace Metropolis.Api.Core.Parsers.CsvParsers
{
    public enum FileInclusion
    {
        [Description(".js")]
        Js,

        [Description(".cs")]
        CSharp,

        [Description(".java")]
        Java
    }

    public class SourceLinesOfCodeParser : CsvClassParser<SourceLinesOfCodeLineItem, SourceLinesOfCodeMap>
    {
        public SourceLinesOfCodeParser(FileInclusion inclusion = FileInclusion.Js) : base(true)
        {
            this.Inclusion = inclusion;
        }

        public SourceLinesOfCodeParser() : this(FileInclusion.Js)
        {
        }

        public FileInclusion Inclusion { get; private set; }

        protected override CodeBase ParseLines(IEnumerable<SourceLinesOfCodeLineItem> lines)
        {
            var inclusionExtension = Inclusion.GetDescription();
            var classes = lines.Where(x => x.Class.EndsWith(inclusionExtension)) 
                               .Select(each => new Instance(each.Namespace, each.Class) {LinesOfCode = each.SourceLoc})
                               .ToList();

            return new CodeBase(new CodeGraph(classes));
        }
    }

    public class SourceLinesOfCodeLineItem
    {
//        Path,Physical,Source,Comment,Single-line comment, Block comment,Mixed,Empty
//        angular.js\src\angular.bind.js,12,7,3,3,0,0,2
        public string Namespace { get; set; }
        public string Class { get; set; }
        public int PhysicalLoc { get; set; }
        public int SourceLoc { get; set; }
        public int CommentLoc { get; set; }
        public int SingleLineCommentLoc { get; set; }
        public int BlockCommentLoc { get; set; }
        public int MixedLoc { get; set; }
        public int EmptyLoc { get; set; }
    }

    public class SourceLinesOfCodeMap : CsvClassMap<SourceLinesOfCodeLineItem>
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
