using System.Linq;
using Metropolis.Api.Core.Domain;
using Metropolis.Api.Core.Parsers.CsvParsers;
using Metropolis.Test.Extensions;
using Metropolis.Test.Utilities;
using NUnit.Framework;

namespace Metropolis.Test.Api.Core.Parsers.CsvParsers
{
    [TestFixture]
    public class VisualStudioMetricsParserTest
    {
        [SetUp]
        public void SetUp()
        {
            parser = new VisualStudioMetricsParserDouble();
        }

        private VisualStudioMetricsParserDouble parser;

        private static VisualStudioCsvLineItem MakeType(string type, string ns = "ns", int loc = 0, int cyclo = 0,
            int dit = 0, int cc = 0)
        {
            return new VisualStudioCsvLineItem
            {
                Scope = "Type",
                Namespace = ns,
                Type = type,
                LinesOfCode = loc,
                CyclomaticComplexity = cyclo,
                DepthOfInheritance = dit,
                ClassCoupling = cc
            };
        }

        private static VisualStudioCsvLineItem MakeMember(string member, string type = "Clock", string ns = "ns",
            int loc = 0, int cyclo = 0, int dit = 0, int cc = 0)
        {
            return new VisualStudioCsvLineItem
            {
                Scope = "Member",
                Member = member,
                Namespace = ns,
                Type = type,
                LinesOfCode = loc,
                CyclomaticComplexity = cyclo,
                DepthOfInheritance = dit,
                ClassCoupling = cc
            };
        }

        [Test]
        public void Parse_NoMembers()
        {
            var typ = MakeType("Clock", loc: 1, dit: 2, cyclo: 3, cc: 4);

            var actual = parser.TestParse(new[] {typ});

            Validate.Begin().IsNotNull(actual, "actual")
                .IsEqual(actual.AllInstances.Count, 1, "class Count")
                .Check()
                .IsEqual(actual.LinesOfCode, 1, "loc")
                .IsEqual(actual.NumberOfTypes, 1, "# Types")
                .IsEqual(actual.AverageToxicity(), 0, "avg toxicity")
                .Check();
        }

        [Test]
        public void ParseSingleMember()
        {
            var typ = MakeType("Clock", loc: 1, dit: 2, cyclo: 3, cc: 4);
            var mbr = MakeMember("Today()");

            var actual = parser.TestParse(new[] {typ, mbr});

            Validate.Begin().IsNotNull(actual, "actual").IsEqual(actual.AllInstances.Count, 1, "class Count").Check()
                .IsEqual(actual.LinesOfCode, 1, "loc")
                .IsEqual(actual.NumberOfTypes, 1, "# Types")
                .IsEqual(actual.AverageToxicity(), 0, "avg toxicity")
                .Check();


            var cls = actual.AllInstances.First();
            Validate.Begin()
                .IsNotNull(cls, "cls")
                .IsEqual(cls.Members.Count, 1, "# members")
                .IsTrue(
                    cls.Members[0].ReflectionEquals(
                        new Member("Today()", mbr.LinesOfCode, mbr.ClassCoupling, mbr.ClassCoupling), true),
                    "member equals")
                .Check();
        }
    }

    public class VisualStudioMetricsParserDouble : VisualStudioMetricsParser
    {
        public CodeBase TestParse(VisualStudioCsvLineItem[] lines)
        {
            return ParseLines(lines);
        }
    }
}