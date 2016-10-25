using System.Collections.Generic;
using System.Linq;
using Metropolis.Api.Extensions;
using NLog;

namespace Metropolis.Api.Domain
{
    /// <summary>
    /// Any executable code module. Think a java class file or a javascript module, etc.
    /// </summary>
    public class Instance : AbstractFile, IInstance
    {
        private static readonly Logger Journal = LogManager.GetLogger("ProgressJournal");

        public List<Duplicate> Duplicates { get; private set; } = new List<Duplicate>();

        public Instance(CodeBag codeBag, string name, Location path) : base(path)
        {
            CodeBag = codeBag;
            Name = name;
            IsInHeadRevision = FileHeadRevisionStatus.Exists;
        }

        public CodeBag CodeBag { get; }
        public string Name { get; }
        public int NumberOfMethods { get; set; }
        public int LinesOfCode { get; set; }
        public int DepthOfInheritance { get; set; }
        public int CyclomaticComplexity { get; set; }
        public int ClassCoupling { get; set; }
        public int AnonymousInnerClassLength { get; set; }
        public int ClassFanOutComplexity { get; set; }
        public int ClassDataAbstractionCoupling { get; set; }
        public double Toxicity { get; set; }

        public int DuplicateLines => Duplicates.Where(x=>x.Location == PhysicalPath).Sum(x => x.LinesOfCode);
        public double DuplicatePercentage => LinesOfCode != 0 ? (double) DuplicateLines / LinesOfCode : 0;

        public List<Member> Members { get; set; } = new List<Member>();

        public CommitEntry History { get; set; }

        public override string ToString()
        {
            return PhysicalPath.Path;
        }

        private bool Equals(Instance other)
        {
            return Equals(CodeBag, other.CodeBag) && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Instance) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((CodeBag?.GetHashCode() ?? 0)*397) ^ (Name?.GetHashCode() ?? 0);
            }
        }

        public void AddDuplicate(Duplicate dup)
        {
            if (dup.Location == PhysicalPath)
            {
                Duplicates.Add(dup);
            }
            else
                Journal.Info($"{dup.Location} does not match instance {Name}");
        }

        public void AddMembers(IEnumerable<Member> toadd)
        {
            Members.AddRange(toadd);
            NumberOfMethods = toadd.Count();
        }

        public void Apply(Instance src)
        {
            if (!Matches(src)) return;

            LinesOfCode = LinesOfCode.Max(src.LinesOfCode);
            DepthOfInheritance = DepthOfInheritance.Max(src.DepthOfInheritance);
            CyclomaticComplexity = CyclomaticComplexity.Max(src.CyclomaticComplexity);
            ClassCoupling = ClassCoupling.Max(src.ClassCoupling);
            NumberOfMethods = NumberOfMethods.Max(src.NumberOfMethods);
            AnonymousInnerClassLength = AnonymousInnerClassLength.Max(src.AnonymousInnerClassLength);
            ClassFanOutComplexity = ClassFanOutComplexity.Max(src.ClassFanOutComplexity);
            ClassDataAbstractionCoupling = ClassDataAbstractionCoupling.Max(src.ClassDataAbstractionCoupling);

            if (src.Members.HasValues())
            {
                Members = new List<Member>(src.Members);
            }
            if (src.Duplicates.HasValues())
            {
                Duplicates = new List<Duplicate>(src.Duplicates);
            }
        }

        private bool Matches(Instance src)
        {
            return PhysicalPath == src.PhysicalPath;
        }

        public static IInstance CreateEmpty(IInstance original)
        {
            return new Instance(CodeBag.Empty, original.Name, original.PhysicalPath);
        }
    }
}