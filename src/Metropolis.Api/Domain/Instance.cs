using System;
using System.Collections.Generic;
using System.Linq;
using Metropolis.Api.Extensions;
using NLog;

namespace Metropolis.Api.Domain
{
    public class Instance
    {
        private static readonly Logger Journal = LogManager.GetLogger("ProgressJournal");

        private List<InstanceVersionInfo> meta = new List<InstanceVersionInfo>();
        public List<Duplicate> Duplicates { get; private set; } = new List<Duplicate>();

        public Instance(CodeBag codeBag, string name, Location path)
        {
            CodeBag = codeBag;
            Name = name;
            PhysicalPath = path;
        }

        public CodeBag CodeBag { get; }
        public string Name { get; }
        public Location PhysicalPath { get; set; }

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

        public IEnumerable<InstanceVersionInfo> Meta
        {
            get { return meta; }
            set { meta = value.ToList(); }
        }

        public override string ToString()
        {
            return PhysicalPath.Path;
        }

        protected bool Equals(Instance other)
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

        public Instance AddMembers(IEnumerable<Member> toadd)
        {
            Members.AddRange(toadd);
            NumberOfMethods = toadd.Count();
            return this;
        }

        public void AddMeta(IEnumerable<InstanceVersionInfo> toAdd)
        {
            meta.AddRange(toAdd);
        }

        public void AddMeta(params InstanceVersionInfo[] toAdd)
        {
            AddMeta(toAdd.ToList());
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
    }
}