﻿using Metropolis.Api.Domain;
using Metropolis.Test.Fixtures;
using NUnit.Framework;

namespace Metropolis.Test.Api.Core.Domain
{
    [TestFixture]
    public class CodeGraphTest
    {
        [SetUp]
        public void Setup()
        {
            graph = CodeGraphFixture.Metropolis;
        }

        private CodeGraph graph;

        [Test]
        public void Should_Add_New_Class_To_Existing_Children()
        {
            var foo = new Instance("Metropolis", "Foo", 15, 300, 4);
            graph.Apply(foo);
            Assert.Contains(foo, graph.AllInstances);
        }

        [Test]
        public void Should_Add_New_Class_To_Graph()
        {
            var foo = new Instance("Foo.Foo", "Foo", 10, 200, 3);
            graph.Apply(foo);
            Assert.Contains(foo, graph.AllInstances);
        }

        [Test]
        public void Should_Add_Record_To_Meta_Of_Existing_Class()
        {
            var metaUpdated = CodeGraphFixture.CodeBase;
            metaUpdated.AddMeta(new InstanceVersionInfo("CodeBase.cs", "no message just playing around"));

            graph.Apply(metaUpdated);
            Assert.Contains(metaUpdated, graph.AllInstances);
        }
    }
}