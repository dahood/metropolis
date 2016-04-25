using Metropolis.Domain;
using Metropolis.Parsers.CsvParsers;
using NUnit.Framework;

namespace Test.Metropolis.Parsers.CsvParsers
{
    [TestFixture]
    public class MetricsReloadedMethodParserTest : CsvParsersBaseTest<MetricsReloadedMethodParser>
    {
        //Method,LOC,NP,v(G)
        //"org.jboss.as.appclient.component.ApplicationClientComponentDescription.getCatalogRepository()",7,0,1
        private const string Heading = "/Method,LOC,NP,v(G)";

        [Test]
        public void Can_Parse()
        {
            const string line = "\"org.jboss.as.appclient.component.ApplicationClientComponentDescription.getCatalogRepository()\",7,0,1";

            var codeBase = ParseUsingData(new[] { Heading, line });

            var expected = new Class("org.jboss.as.appclient.component", "ApplicationClientComponentDescription", 1, 7, 1, 0, 0);
            var actual = AssertHasOneClassEqualTo(expected, codeBase);
            
            var expectedMethod = new Member("getCatalogRepository()", 7, 1, 0);
            AssertHasOneMemberEqualTo(actual, expectedMethod);
        }

        [Test]
        public void Can_Parse_When_LOC_Is_NA()
        {
            const string line = "\"org.jboss.as.appclient.component.ApplicationClientComponentDescription.getCatalogRepository()\",\"n/a\",0,1";

            var codeBase = ParseUsingData(new[] { Heading, line });

            var expected = new Class("org.jboss.as.appclient.component", "ApplicationClientComponentDescription", 1, 0, 1, 0, 0);
            var actual = AssertHasOneClassEqualTo(expected, codeBase);

            var expectedMethod = new Member("getCatalogRepository()", 0, 1, 0);
            AssertHasOneMemberEqualTo(actual, expectedMethod);
        }

        [Test]
        public void Can_Parse_When_VG_Is_NA()
        {
            const string line = "\"org.jboss.as.appclient.component.ApplicationClientComponentDescription.getCatalogRepository()\",7,0,\"n/a\"";

            var codeBase = ParseUsingData(new[] { Heading, line });

            var expected = new Class("org.jboss.as.appclient.component", "ApplicationClientComponentDescription", 1, 7, 0, 0, 0);
            var actual = AssertHasOneClassEqualTo(expected, codeBase);

            var expectedMethod = new Member("getCatalogRepository()", 7, 0, 0);
            AssertHasOneMemberEqualTo(actual, expectedMethod);
        }
    }
}