using System;
using System.Linq;
using FluentAssertions;
using Metropolis.Api.Core.Domain;
using Metropolis.Test.Extensions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Metropolis.Test.Domain
{
    [TestFixture]
    public class SerializableTests
    {
        [SetUp]
        public void SetUp()
        {
            versionInfo = new SerializableClassVersionInfo
            {
                CommitMessage = "msg",
                FileName = "filename",
                TimeStamp = DateTime.Now
            };

            member = new SerializableMember
            {
                ClassCoupling = 1,
                CylomaticComplexity = 2,
                LinesOfCode = 3,
                MissingDefaultCase = 4,
                Name = "member",
                NoFallthrough = 5,
                NumberOfParameters = 6
            };

            toSerialize = new SerializableClass
            {
                ClassCoupling = 1,
                CyclomaticComplexity = 2,
                DepthOfInheritance = 3,
                LinesOfCode = 4,
                NumberOfMethods = 5,
                Toxicity = 6,
                Name = "MyClass",
                NameSpace = "MyNamespace",
                Meta = new[] {versionInfo},
                Members = new[] {member}
            };
        }

        private SerializableClass toSerialize;
        private SerializableClassVersionInfo versionInfo;
        private SerializableMember member;


        [Test]
        public void CanSerializeProject()
        {
            var project = new Project {Classes = new[] {toSerialize}};

            var json = JsonConvert.SerializeObject(project);
            json.Should().NotBeEmpty();

            var hydrated = JsonConvert.DeserializeObject<Project>(json);
            hydrated.Should().NotBeNull();

            hydrated.Classes.Count().Should().Be(1);
            hydrated.Classes.First().Members.Count().Should().Be(1);

            project.ReflectionEquals(hydrated).Should().BeTrue();
        }
    }
}