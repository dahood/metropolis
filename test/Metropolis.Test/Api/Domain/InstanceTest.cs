using Metropolis.Api.Domain;
using Metropolis.Test.Utilities;
using NUnit.Framework;

namespace Metropolis.Test.Api.Domain
{
    [TestFixture]
    public class InstanceTest
    {
        [Test]
        public void ShouldApplyLargestMetricWhenPhysicalPathMatches()
        {
            var mbr = new Member("store", 1, 1, 1);
            var cls = InstanceBuilder.Build(new CodeBag("dev", CodeBagType.Namespace, @"C:\dev"), "name", @"C:\dev\name.cs", 1, 1, 3, 1, new[] { mbr });
            var toApply = InstanceBuilder.Build(new CodeBag("dev", CodeBagType.Namespace, @"C:\dev"), "name", @"C:\dev\name.cs", 2, 2, 2, 2, new[] { mbr });

            cls.Apply(toApply);

            Validate.Begin().IsNotNull(cls, "class")
                .IsEqual(cls.Members.Count, 1, "mbr count")
                .IsEqual(cls.NumberOfMethods, 1, "# Methods")
                .IsEqual(cls.LinesOfCode, 2, "loc")
                .IsEqual(cls.CyclomaticComplexity, 2, "cyclo")
                .IsEqual(cls.DepthOfInheritance, 3, "DIT")
                .IsEqual(cls.ClassCoupling, 2, "class coupling")
                .Check();
        }

        [Test]
        public void ShouldNotApplyWhenPhysicalPathDiffers()
        {
            var mbr = new Member("store", 1, 1, 1);
            var cls = InstanceBuilder.Build(new CodeBag("dev", CodeBagType.Namespace, @"C:\dev"), "name", @"C:\dev\name.cs", 1, 1, 3, 1, new[] {mbr});
            var toApply = InstanceBuilder.Build(new CodeBag("dev", CodeBagType.Namespace, @"C:\dev"), "namexxx", @"C:\dev\namexxx.cs", 2, 2, 2, 2, new[] { mbr });

           cls.Apply(toApply);

            Validate.Begin().IsNotNull(cls, "class")
                .IsEqual(cls.Members.Count, 1, "mbr count")
                .IsEqual(cls.NumberOfMethods, 1, "# Methods")
                .IsEqual(cls.LinesOfCode, 1, "loc")
                .IsEqual(cls.CyclomaticComplexity, 1, "cyclo")
                .IsEqual(cls.DepthOfInheritance, 3, "DIT")
                .IsEqual(cls.ClassCoupling, 1, "class coupling")
                .Check();
        }
    }
}