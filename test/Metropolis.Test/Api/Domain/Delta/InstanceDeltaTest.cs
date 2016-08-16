using System;
using System.Linq.Expressions;
using FluentAssertions;
using Metropolis.Api.Domain;
using Metropolis.Api.Domain.Delta;
using Metropolis.Api.Utilities;
using NUnit.Framework;

namespace Metropolis.Test.Api.Domain.Delta
{
    [TestFixture]
    public class InstanceDeltaTest
    {
        const string Name = "Repository";
        readonly Location physicalPath = new Location(@"c:\dev\Repository.cs");

        [Test]
        public void NumberOfMethods()
        {
            AssertDeltaEquals(orig => orig.NumberOfMethods, 2, 2, 0);   //equals
            AssertDeltaEquals(orig => orig.NumberOfMethods, 4, 2, -2);  //original is More
            AssertDeltaEquals(orig => orig.NumberOfMethods, 4, 9, 5);   //newvalue is More
        }

        [Test]
        public void LinesOfCode()
        {
            AssertDeltaEquals(orig => orig.LinesOfCode, 2, 2, 0);   //equals
            AssertDeltaEquals(orig => orig.LinesOfCode, 4, 2, -2);  //original is More
            AssertDeltaEquals(orig => orig.LinesOfCode, 4, 9, 5);   //newvalue is More
        }

        [Test]
        public void DepthOfInheritance()
        {
            AssertDeltaEquals(orig => orig.DepthOfInheritance, 2, 2, 0);   //equals
            AssertDeltaEquals(orig => orig.DepthOfInheritance, 4, 2, -2);  //original is More
            AssertDeltaEquals(orig => orig.DepthOfInheritance, 4, 9, 5);   //newvalue is More
        }

        [Test]
        public void CyclomaticComplexity()
        {
            AssertDeltaEquals(orig => orig.CyclomaticComplexity, 2, 2, 0);   //equals
            AssertDeltaEquals(orig => orig.CyclomaticComplexity, 4, 2, -2);  //original is More
            AssertDeltaEquals(orig => orig.CyclomaticComplexity, 4, 9, 5);   //newvalue is More
        }

        [Test]
        public void ClassCoupling()
        {
            AssertDeltaEquals(orig => orig.ClassCoupling, 2, 2, 0);   //equals
            AssertDeltaEquals(orig => orig.ClassCoupling, 4, 2, -2);  //original is More
            AssertDeltaEquals(orig => orig.ClassCoupling, 4, 9, 5);   //newvalue is More
        }

        [Test]
        public void AnonymousInnerClassLength()
        {
            AssertDeltaEquals(orig => orig.AnonymousInnerClassLength, 2, 2, 0);   //equals
            AssertDeltaEquals(orig => orig.AnonymousInnerClassLength, 4, 2, -2);  //original is More
            AssertDeltaEquals(orig => orig.AnonymousInnerClassLength, 4, 9, 5);   //newvalue is More
        }

        [Test]
        public void ClassFanOutComplexity()
        {
            AssertDeltaEquals(orig => orig.ClassFanOutComplexity, 2, 2, 0);   //equals
            AssertDeltaEquals(orig => orig.ClassFanOutComplexity, 4, 2, -2);  //original is More
            AssertDeltaEquals(orig => orig.ClassFanOutComplexity, 4, 9, 5);   //newvalue is More
        }

        [Test]
        public void ClassDataAbstractionCoupling()
        {
            AssertDeltaEquals(orig => orig.ClassDataAbstractionCoupling, 2, 2, 0);   //equals
            AssertDeltaEquals(orig => orig.ClassDataAbstractionCoupling, 4, 2, -2);  //original is More
            AssertDeltaEquals(orig => orig.ClassDataAbstractionCoupling, 4, 9, 5);   //newvalue is More
        }

        [Test]
        public void Toxicity()
        {
            AssertDeltaEquals(orig => orig.Toxicity, 2, 2, 0);   //equals
            AssertDeltaEquals(orig => orig.Toxicity, 4, 2, -2);  //original is More
            AssertDeltaEquals(orig => orig.Toxicity, 4, 9, 5);   //newvalue is More
        }

        private void AssertDeltaEquals<TReturnValue>(Expression<Func<Instance, TReturnValue>> property, TReturnValue initialValue, TReturnValue newValue, TReturnValue expectedDelta)
        {
            var original = new Instance(CodeBag.Empty, Name, physicalPath);
            var target = new Instance(CodeBag.Empty, Name, physicalPath);
            var info = PropertyExtensions.GetPropertyInfo(original, property);

            property.SetValue(original, initialValue);
            property.SetValue(target, newValue);

            var delta = new InstanceDelta(original, target);
            var deltaValue = delta.GetPropertyInfo(info.Name).GetValue(delta);

            deltaValue.Should().Be(expectedDelta, $"{info.Name} delta is incorrect");
        }
    }
}
