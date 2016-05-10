using Metropolis.Api.Core.Domain;
using Metropolis.Domain;
using NUnit.Framework;
using Test.Metropolis.Utilities;

namespace Test.Metropolis.Domain
{
    [TestFixture]
    public class ClassTest
    {
        [Test]
        public void ConstructWithValues()
        {
            var cls = new Class("ns", "name");
            Validate.Begin().IsNotNull(cls, "class")
                            .IsEqual(cls.NameSpace, "ns", "namespace")
                            .IsEqual(cls.Name, "name", "name")
                            .IsEqual(cls.QualifiedName, "ns.name", "qualifiedname")
                            .Check();
        }

        [Test]
        public void ConstructWithMembers()
        {
            var mbr = new Member("store", 1, 1, 1);
            var cls = new Class("ns", "name", new [] {mbr});

            Validate.Begin().IsNotNull(cls, "class")
                            .IsEqual(1, cls.Members.Count, "mbr count")
                            .IsEqual(cls.NamespaceDepth(), 1, "ns depth")
                            .Check();

        }

        [Test]
        public void Apply_NoMembersToApply()
        {
            var mbr = new Member("store", 1, 1, 1);
            var cls = new Class("ns", "name", new[] {mbr})
                        {LinesOfCode = 1, DepthOfInheritance = 1, CyclomaticComplexity = 1,
                         ClassCoupling = 1, NumberOfMethods = 1};

            var toApply = new Class(cls.NameSpace, cls.Name, 2, 3, 4, 5, 6);

            cls.Apply(toApply);

            Validate.Begin().IsNotNull(cls, "class")
                            .IsEqual(cls.Members.Count, 1, "mbr count")
                            .IsEqual(cls.NumberOfMethods, 2, "# Methods")
                            .IsEqual(cls.LinesOfCode, 3, "loc")
                            .IsEqual(cls.CyclomaticComplexity, 4, "cyclo")
                            .IsEqual(cls.DepthOfInheritance, 5, "DIT")
                            .IsEqual(cls.ClassCoupling, 6, "class coupling")
                            .Check();
        }

        [Test]
        public void Apply_MembersToApply()
        {
            var mbr = new Member("store", 1, 1, 1);
            var cls = new Class("ns", "name", new[] {mbr})
                        {LinesOfCode = 1, DepthOfInheritance = 3, CyclomaticComplexity = 1,
                         ClassCoupling = 1, NumberOfMethods = 2};

            var toApply = new Class("ns", "name", new[] {mbr})
                        {LinesOfCode = 2, DepthOfInheritance = 2, CyclomaticComplexity = 2,
                         ClassCoupling = 2, NumberOfMethods = 2};
            
            cls.Apply(toApply);

            Validate.Begin().IsNotNull(cls, "class")
                            .IsEqual(cls.Members.Count, 1, "mbr count")
                            .IsEqual(cls.DepthOfInheritance, 3, "DIT")
                            .Check();
        }

        [Test]
        public void Apply_NoMatchOnNamespace()
        {
            var mbr = new Member("store", 1, 1, 1);
            var cls = new Class("ns", "name", new[] {mbr})
                        {LinesOfCode = 1, DepthOfInheritance = 3, CyclomaticComplexity = 1,
                         ClassCoupling = 1, NumberOfMethods = 2};

            var toApply = new Class("ns1", "name", new[] {mbr})
                        {LinesOfCode = 2, DepthOfInheritance = 2, CyclomaticComplexity = 2,
                         ClassCoupling = 2, NumberOfMethods = 2};
            
            cls.Apply(toApply);

            Validate.Begin().IsNotNull(cls, "class")
                            .IsEqual(cls.Members.Count, 1, "mbr count")
                            .IsEqual(cls.NumberOfMethods, 2, "# Methods")
                            .IsEqual(cls.LinesOfCode, 1, "loc")
                            .IsEqual(cls.CyclomaticComplexity, 1, "cyclo")
                            .IsEqual(cls.DepthOfInheritance, 3, "DIT")
                            .IsEqual(cls.ClassCoupling, 1, "class coupling")
                            .Check();
        }

        [Test]
        public void Apply_NoMatchOnName()
        {
            var mbr = new Member("store", 1, 1, 1);
            var cls = new Class("ns", "name", new[] {mbr})
                        {LinesOfCode = 1, DepthOfInheritance = 3, CyclomaticComplexity = 1,
                         ClassCoupling = 1, NumberOfMethods = 2};

            var toApply = new Class("ns", "namexxx", new[] {mbr})
                        {LinesOfCode = 2, DepthOfInheritance = 2, CyclomaticComplexity = 2,
                         ClassCoupling = 2, NumberOfMethods = 2};
            
            cls.Apply(toApply);

            Validate.Begin().IsNotNull(cls, "class")
                            .IsEqual(cls.Members.Count, 1, "mbr count")
                            .IsEqual(cls.NumberOfMethods, 2, "# Methods")
                            .IsEqual(cls.LinesOfCode, 1, "loc")
                            .IsEqual(cls.CyclomaticComplexity, 1, "cyclo")
                            .IsEqual(cls.DepthOfInheritance, 3, "DIT")
                            .IsEqual(cls.ClassCoupling, 1, "class coupling")
                            .Check();
        }
    }
}
