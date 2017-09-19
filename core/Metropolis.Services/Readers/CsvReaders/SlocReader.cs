using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;

namespace Metropolis.Api.Readers.CsvReaders
{
    public enum FileInclusion
    {
        [Description(".js")] Js,

        [Description(".cs")] CSharp,

        [Description(".java")] Java
    }

    public class SlocReader : CsvInstanceReader<SlocLineItem, SlocMap>
    {
        public SlocReader(FileInclusion inclusion = FileInclusion.Js) : base(true)
        {
            Inclusion = inclusion;
        }

        public SlocReader() : this(FileInclusion.Js)
        {
        }

        private FileInclusion Inclusion { get; }

        protected override CodeBase ParseLines(IEnumerable<SlocLineItem> lines)
        {
            var inclusionExtension = Inclusion.GetDescription();
            var inclusionCodeBagType = MapToCodeBag(Inclusion);
            
            var classes = lines.Where(x => x.FileName.EndsWith(inclusionExtension))
                .Select(each => InstanceBuilder.Build(
                    new CodeBag(each.Directory, inclusionCodeBagType, each.Directory), each.FileName, each.PhysicalPath, each.SourceLoc))
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
}