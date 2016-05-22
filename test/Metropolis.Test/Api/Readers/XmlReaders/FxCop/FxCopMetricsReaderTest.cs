using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;
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

        private readonly Instance expectedClock = new Instance("Metropolis.Api.Utilities", "Clock", 2, 8, 8, 1, 3);
        private readonly Member expectedFreezeMember = new Member("Freeze", 1, 1, 2);
        private readonly Member expectedTodayGetMember = new Member("Today.get", 1, 1, 2);

        private readonly Instance expectedAnalysisServices = new Instance("Metropolis.Api.Services", "AnalysisServices", 2, 12, 4, 1, 15);
        private readonly Member expectedAnalysisServicesMember = new Member("AnalysisServices", 1, 1, 2);
        private readonly Member expectedAnalyzeMember = new Member("Analyze", 9, 2, 12);

        private FxCopMetricsReader reader;

        [SetUp]
        public void SetUp()
        {
            pathToMetricsFile = Path.Combine(Environment.CurrentDirectory, FileName);
            pathToMetricsFile.RemoveFileIfExists();
            File.WriteAllText(pathToMetricsFile, MetricsDataFixture.FxCopMetricsMetricsData);

            reader = new FxCopMetricsReader();
        }

        [TearDown]
        public void TearDown()
        {
            pathToMetricsFile.RemoveFileIfExists();
        }

        [Test]
        public void Can_Read_Metropolis_Metrics_File()
        {
            var codeBase = reader.Parse(pathToMetricsFile);

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
                    .IsEqual(actual.NameSpace, expected.NameSpace, "NameSpace")
                    .IsEqual(actual.Name, expected.Name, "Name")
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
                            .IsEqual(actual.Name, expected.Name, "Name")
                            .IsEqual(actual.CylomaticComplexity, expected.CylomaticComplexity, "CylomaticComplexity")
                            .IsEqual(actual.ClassCoupling, expected.ClassCoupling, "ClassCoupling")
                            .IsEqual(actual.LinesOfCode, expected.LinesOfCode, "LinesOfCode")
                            .Check();
        }
    }
}
