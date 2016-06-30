using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;
using Metropolis.Api.IO;
using Metropolis.Api.Readers.XmlReaders.FxCop;
using Metropolis.Test.Fixtures;
using Metropolis.Test.TestHelpers;
using Metropolis.Test.Utilities;
using NUnit.Framework;

namespace Metropolis.Test.Api.Readers.XmlReaders.FxCop
{
    [TestFixture]
    public class FxCopMetricsReaderTest
    {
        private const string FileName = "FxCop_Sample_Metrics.xml";
        private string pathToMetricsFile;
        private Instance expectedClock;

        private readonly Member expectedFreezeMember = new Member("Freeze", 1, 1, 2);
        private readonly Member expectedTodayGetMember = new Member("Today.get", 1, 1, 2);

        private Instance expectedAnalysisServices;
        private readonly Member expectedAnalysisServicesMember = new Member("AnalysisServices", 1, 1, 2);
        private readonly Member expectedAnalyzeMember = new Member("Analyze", 9, 2, 12);

        private FxCopMetricsReader reader;
        private TextReader textReader;

        [SetUp]
        public void SetUp()
        {
            expectedClock = InstanceBuilder.Build(new CodeBag("Metropolis.Api.Utilities", CodeBagType.Namespace, @"c:\dev\clock.cs"),
            "Clock", @"C:\foo.cs", 8, 8, 1, 3, new List<Member> { expectedFreezeMember , expectedTodayGetMember });
            expectedAnalysisServices = InstanceBuilder.Build(new CodeBag("Metropolis.Api.Services", CodeBagType.Namespace, @"c:\dev\AnalysisServices.cs"),
            "AnalysisServices", @"C:\foo2.cs",12, 4, 1, 15, new List<Member> { expectedAnalysisServicesMember, expectedAnalysisServicesMember });


            pathToMetricsFile = Path.Combine(Environment.CurrentDirectory, FileName);
            pathToMetricsFile.RemoveFileIfExists();
            File.WriteAllText(pathToMetricsFile, MetricsDataFixture.FxCopMetricsMetricsData);
            textReader = new FileSystem().OpenFileStream(pathToMetricsFile);

            reader = new FxCopMetricsReader();
        }

        [TearDown]
        public void TearDown()
        {
            textReader.Dispose();
            pathToMetricsFile.RemoveFileIfExists();
        }

        [Test]
        public void Can_Read_Metropolis_Metrics_File()
        {
            var codeBase = reader.Parse(textReader);

            codeBase.Should().NotBeNull();
            codeBase.AllInstances.Count.Should().Be(2);

            VerifyInstance(codeBase.AllInstances, expectedClock);
            VerifyMembers(codeBase.AllInstances, expectedClock, expectedFreezeMember, expectedTodayGetMember);

            VerifyInstance(codeBase.AllInstances, expectedAnalysisServices);
            VerifyMembers(codeBase.AllInstances, expectedAnalysisServices, expectedAnalysisServicesMember, expectedAnalyzeMember);
        }

        private static void VerifyInstance(IEnumerable<Instance> allInstances, Instance expected)
        {
            var actual = allInstances.FirstOrDefault(x => x.Name == expected.Name);

            Validate.Begin()
                .IsNotNull(actual, "Actual").Check()
                .IsEqual(actual.CodeBag.Name, expected.CodeBag.Name, "CodeBag")
                .IsEqual(actual.Name, expected.Name, "CodeBag")
                .IsEqual(actual.NumberOfMethods, expected.NumberOfMethods, "NumberOfMethods")
                .IsEqual(actual.LinesOfCode, expected.LinesOfCode, "LinesOfCode")
                .IsEqual(actual.CyclomaticComplexity, expected.CyclomaticComplexity, "CyclomaticComplexity")
                .IsEqual(actual.ClassCoupling, expected.ClassCoupling, "ClassCoupling")
                .IsEqual(actual.DepthOfInheritance, expected.DepthOfInheritance, "DepthOfInheritance")
                .Check();
        }

        private static void VerifyMembers(IEnumerable<Instance> allInstances, Instance expected, params Member[] expectedMembers)
        {
            var actual = allInstances.FirstOrDefault(x => x.Name == expected.Name);
            Validate.Begin().IsNotNull(actual, "Actual").Check()
                .IsEqual(actual.Members.Count, expectedMembers.Length, "MembersCount");
            expectedMembers.ForEach(each => VerifyMember(actual, each));
        }

        private static void VerifyMember(Instance instance, Member expected)
        {
            var actual = instance.Members.FirstOrDefault(x => x.Name == expected.Name);
            Validate.Begin().IsNotNull(actual, "Actual").Check()
                .IsEqual(actual.Name, expected.Name, "CodeBag")
                .IsEqual(actual.CylomaticComplexity, expected.CylomaticComplexity, "CylomaticComplexity")
                .IsEqual(actual.ClassCoupling, expected.ClassCoupling, "ClassCoupling")
                .IsEqual(actual.LinesOfCode, expected.LinesOfCode, "LinesOfCode")
                .Check();
        }
    }
}