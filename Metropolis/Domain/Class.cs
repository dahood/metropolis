using System.Collections.Generic;
using System.Linq;
using Metropolis.Extensions;

namespace Metropolis.Domain
{
    public class Class
    {
        private List<ClassVersionInfo> meta = new List<ClassVersionInfo>();

        public Class(string nameSpace, string name)
        {
            NameSpace = nameSpace;
            Name = name;
            QualifiedName = string.IsNullOrEmpty(NameSpace) ? Name : $"{NameSpace}.{Name}";
        }

        public Class(string nameSpace, string name, int numberOfMethods, int linesOfCode, int toxicity) : this(nameSpace, name)
        {
            NumberOfMethods = numberOfMethods;
            LinesOfCode = linesOfCode;
            Toxicity = toxicity;
        }

        public Class(string nameSpace, string name, int numberOfMethods, int linesOfCode, int cyclomaticComplexity,
            int depthOfInheritance, int classCoupling) : this(nameSpace, name)
        {
            NumberOfMethods = numberOfMethods;
            LinesOfCode = linesOfCode;
            CyclomaticComplexity = cyclomaticComplexity;
            DepthOfInheritance = depthOfInheritance;
            ClassCoupling = classCoupling;
        }

        public Class(string nameSpace, string name, IEnumerable<Member> members) : this(nameSpace, name)
        {
            ApplyMembers(members);
            members.ForEach(x =>
            {
                LinesOfCode += x.LinesOfCode;
                CyclomaticComplexity += x.CylomaticComplexity;
            });
            NumberOfMethods = members.Count();
        }

        private void ApplyMembers(IEnumerable<Member> toAdd)
        {
            Members = new List<Member>();
            Members.AddRange(toAdd);
        }

        public string NameSpace { get; }
        public string Name { get; }
        public string QualifiedName { get; }
        public int NumberOfMethods { get; set; }
        public int LinesOfCode { get; set; }
        public int DepthOfInheritance { get; set; }
        public int CyclomaticComplexity { get; set; }
        public int ClassCoupling { get; set; }
        public int AnonymousInnerClassLength { get; set; }
        public int ClassFanOutComplexity { get; set; }
        public int ClassDataAbstractionCoupling { get; set; }

        public double Toxicity { get; set; }

        public List<Member> Members { get; set; } = new List<Member>();

        public IEnumerable<ClassVersionInfo> Meta
        {
            get { return meta; }
            set { meta = value.ToList(); }
        }

        public int NamespaceDepth()
        {
            return NameSpace.Split('.').Length;
        }

        public override string ToString()
        {
            return QualifiedName;
        }

        protected bool Equals(Class other)
        {
            return string.Equals(NameSpace, other.NameSpace) && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Class) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((NameSpace?.GetHashCode() ?? 0)*397) ^ (Name?.GetHashCode() ?? 0);
            }
        }

        public void AddMember(IEnumerable<Member> toadd)
        {
            Members.AddRange(toadd);
        }

        public Member GetMemberByName(string name)
        {
            return Members.SingleOrDefault(x => x.Name == name);
        }

        public void AddMeta(IEnumerable<ClassVersionInfo> toAdd)
        {
            meta.AddRange(toAdd);
        }

        public void AddMeta(params ClassVersionInfo[] toAdd)
        {
            AddMeta(toAdd.ToList());
        }

        public void Apply(Class src)
        {
            if (!Matches(src)) return;

            LinesOfCode = LinesOfCode.Max(src.LinesOfCode);
            DepthOfInheritance = DepthOfInheritance.Max(src.DepthOfInheritance);
            CyclomaticComplexity = CyclomaticComplexity.Max(src.CyclomaticComplexity);
            ClassCoupling = ClassCoupling.Max(src.ClassCoupling);
            NumberOfMethods = NumberOfMethods.Max(src.NumberOfMethods);

            if (src.Members.IsNotEmpty())
                ApplyMembers(src.Members);
        }

        private bool Matches(Class src)
        {
            return NameSpace == src.NameSpace & Name == src.Name;
        }
    }
}