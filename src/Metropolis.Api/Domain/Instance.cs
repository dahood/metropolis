using System;
using System.Collections.Generic;
using System.Linq;
using Metropolis.Api.Extensions;
using Metropolis.Common.Extensions;

namespace Metropolis.Api.Domain
{
    public class Instance
    {
        private List<InstanceVersionInfo> meta = new List<InstanceVersionInfo>();
        public List<Duplicate> Duplicates = new List<Duplicate>();

        public Instance(string name, string codeBagName, CodeBagType codeBagType, string codeBagPath = "") //TODO: Review if this should be empty or null
        {
            CodeBag = new CodeBag(codeBagName, codeBagType, codeBagPath);
            Name = name;
            QualifiedName = CodeBag == null ? Name : $"{CodeBag.Name}.{Name}";
            PhysicalPath = string.Empty;
        }

        public Instance(string nameSpace, string name, int numberOfMethods, int linesOfCode, int toxicity)
            : this(name, nameSpace, CodeBagType.Empty, String.Empty)
        {
            NumberOfMethods = numberOfMethods;
            LinesOfCode = linesOfCode;
            Toxicity = toxicity;
        }

        public Instance(string nameSpace, string name, int numberOfMethods, int linesOfCode, int cyclomaticComplexity,
            int depthOfInheritance, int classCoupling) : this(name, nameSpace, CodeBagType.Empty, String.Empty)
        {
            NumberOfMethods = numberOfMethods;
            LinesOfCode = linesOfCode;
            CyclomaticComplexity = cyclomaticComplexity;
            DepthOfInheritance = depthOfInheritance;
            ClassCoupling = classCoupling;
        }

        public Instance(string nameSpace, string name, IEnumerable<Member> members) : this(name, nameSpace, CodeBagType.Empty, String.Empty)
        {
            ApplyMembers(members);
            Members.ForEach(x =>
            {
                LinesOfCode += x.LinesOfCode;
                CyclomaticComplexity += x.CylomaticComplexity;
            });
            NumberOfMethods = Members.Count;
        }


        private void ApplyMembers(IEnumerable<Member> toAdd)
        {
            Members = new List<Member>(toAdd);
        }
        private void ApplyDuplicates(IEnumerable<Duplicate> toAdd)
        {
            Duplicates = new List<Duplicate>(toAdd);
        }

        public CodeBag CodeBag { get; }
        public string Name { get; }
        public string QualifiedName { get; }
        public string PhysicalPath { get; set; }

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
        public double DuplicatePercentage => LinesOfCode != 0 ? Math.Round((double) DuplicateLines / LinesOfCode * 100,2) : 0;

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
            return string.Equals(CodeBag, other.CodeBag) && string.Equals(Name, other.Name);
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
                ApplyMembers(src.Members);
            if (src.Duplicates.HasValues())
                ApplyDuplicates(src.Duplicates);
        }

        private bool Matches(Instance src)
        {
            return QualifiedName == src.QualifiedName ||
                   (PhysicalPath != string.Empty && PhysicalPath == src.PhysicalPath);
        }
    }

    public class Duplicate
    {
        public int LinesOfCode { get; set; }
        public int LineNumber { get; set; }
    }
}