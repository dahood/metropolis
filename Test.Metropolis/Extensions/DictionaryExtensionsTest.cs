using System.Collections.Generic;
using FluentAssertions;
using Metropolis.Domain;
using Metropolis.Extensions;
using NUnit.Framework;

namespace Test.Metropolis.Extensions
{
    [TestFixture]
    public class DictionaryExtensionsTest
    {
        [Test]
        public void FindOrCreate_Found()
        {
            var classDict = new Dictionary<string, Class> {{"Hi", new Class("ns", "hi")}};
            var found = classDict.FindOrCreate("Hi", () => { throw new AssertionException("Should not have to create class"); });
            found.Should().NotBeNull();
        }

        [Test]
        public void FindOrCreate_HaveToCreate()
        {
            var classDict = new Dictionary<string, Class>();
            classDict.Should().BeEmpty();
            var found = classDict.FindOrCreate("Hi", () => new Class("ns", "hi"));

            classDict.Count.Should().Be(1);
            found.Should().NotBeNull();
        }

        [Test]
        public void DoWhenItemFound()
        {
            var classDict = new Dictionary<string, Class> { { "Hi", new Class("ns", "hi") } };
            var found = false;
            classDict.DoWhenItemFound("Hi", m => found = true);
            found.Should().BeTrue();
        }

        [Test]
        public void DoWhenItemFound_NotFound()
        {
            var classDict = new Dictionary<string, Class> { { "Hi", new Class("ns", "hi") } };
            var found = false;
            classDict.DoWhenItemFound("bye", m => found = true);
            found.Should().BeFalse();
        }
    }
}
