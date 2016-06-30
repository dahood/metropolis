using System;
using System.Collections.Generic;
using System.Linq;
using Metropolis.Api.Extensions;

namespace Metropolis.Api.Domain
{
    public class Instance
    {
        private List<InstanceVersionInfo> meta = new List<InstanceVersionInfo>();
        public List<Duplicate> Duplicates = new List<Duplicate>();
        
        public Instance(CodeBag codeBag, string name, Location path)
        {
            CodeBag = codeBag;
            Name = name;
            PhysicalPath = path;
        }

        [Obsolete("this constructor should be removed and InstanceBuilder used instead")]
        public Instance(string name, string codeBagName, CodeBagType codeBagType, string codeBagPath = "") //TODO: Review if this should be empty or null
        {
            CodeBag = new CodeBag(codeBagName, codeBagType, codeBagPath);
            Name = name;
            QualifiedName = CodeBag == null ? Name : $"{CodeBag.Name}.{Name}";
            PhysicalPath = new Location(codeBagPath);
        }

        [Obsolete("this constructor should be removed and InstanceBuilder used instead")]
        public Instance(string nameSpace, string name, int numberOfMethods, int linesOfCode, int cyclomaticComplexity,
            int depthOfInheritance, int classCoupling) : this(name, nameSpace, CodeBagType.Empty, string.Empty)
        {
            NumberOfMethods = numberOfMethods;
            LinesOfCode = linesOfCode;
            CyclomaticComplexity = cyclomaticComplexity;
            DepthOfInheritance = depthOfInheritance;
            ClassCoupling = classCoupling;
        }

        public CodeBag CodeBag { get; }
        public string Name { get; }
        public string QualifiedName { get; }
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

        public int DuplicateLines => Duplicates.Sum(x => x.LinesOfCode);
        public double DuplicatePercentage => LinesOfCode != 0 ? (double) DuplicateLines / LinesOfCode : 0;

        public List<Member> Members { get; set; } = new List<Member>();

        public IEnumerable<InstanceVersionInfo> Meta
        {
            get { return meta; }
            set { meta = value.ToList(); }
        }

        public int NamespaceDepth()
        {
            //TODO: Move this to CodeBag
            return CodeBag.Name.Split('.').Length;
        }

        public override string ToString()
        {
            return QualifiedName;
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

        public Instance AddMembers(IEnumerable<Member> toadd)
        {
            Members.AddRange(toadd);
            NumberOfMethods = toadd.Count();
            return this;
        }

        public Member GetMemberByName(string name)
        {
            return Members.SingleOrDefault(x => x.Name == name);
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
            PhysicalPath = src.PhysicalPath ?? PhysicalPath;
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

    public class Duplicate
    {
        public int LinesOfCode { get; set; }
        public int LineNumber { get; set; }
    }
}