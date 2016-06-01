using System.Collections.Generic;
using FluentAssertions;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;
using NUnit.Framework;

namespace Metropolis.Test.Extensions
{
    [TestFixture]
    public class DictionaryExtensionsTest
    {
        private Dictionary<string, Instance> classDict;

        [SetUp]
        public void Setup()
        {
            classDict = new Dictionary<string, Instance> { { "Hi", new Instance((string)"hi", (string)"ns", CodeBagType.Empty, string.Empty) } };
        }

        [Test]
        public void DoWhenItemFound()
        {
            var found = false;
            classDict.DoWhenItemFound("Hi", m => found = true);
            found.Should().BeTrue();
        }


        [Test]
        public void DoWhenItemFound_NotFound()
        {
            var found = false;
            classDict.DoWhenItemFound("bye", m => found = true);
            found.Should().BeFalse();
        }

        [Test]
        public void FindOrCreate_Found()
        {
            var found = classDict.FindOrCreate("Hi",
                () => { throw new AssertionException("Should not have to create class"); });
            found.Should().NotBeNull();
        }

        [Test]
        public void FindOrCreate_HaveToCreate()
        {
            classDict = new Dictionary<string, Instance>();
            classDict.Should().BeEmpty();
            var found = classDict.FindOrCreate("Hi", () => new Instance((string) "hi", (string) "ns", CodeBagType.Empty, string.Empty));

            classDict.Count.Should().Be(1);
            found.Should().NotBeNull();
        }
    }
}