using Metropolis.Domain;
using Metropolis.Parsers.CsvParsers;
using NUnit.Framework;

namespace Test.Metropolis.Parsers.CsvParsers
{
    [TestFixture]
    public class MetricsReloadedClassParserTest : CsvParsersBaseTest<MetricsReloadedClassParser>
    {
        //Class,CBO,DIT,LOC
        //"org.jboss.as.appclient.component.ApplicationClientComponentDescription",10,2,32
        private const string Heading = "Class,CBO,DIT,LOC";

        [Test]
        public void Should_Parse()
        {
            const string line = "\"org.jboss.as.appclient.component.ApplicationClientComponentDescription\",10,2,32";

            var codeBase = ParseUsingData(new[] {Heading, line});
            
            var expected = new Class("org.jboss.as.appclient.component", "ApplicationClientComponentDescription", 0, 32, 0, 2, 10);
            AssertHasOneClassEqualTo(expected, codeBase);
        }

        [Test]
        public void Should_Parse_When_CBO_Is_NA()
        {
            const string line = "\"org.jboss.as.appclient.component.ApplicationClientComponentDescription\",\"n/a\",2,32";

            var codeBase = ParseUsingData(new[] {Heading, line});
            
            var expected = new Class("org.jboss.as.appclient.component", "ApplicationClientComponentDescription", 0, 32, 0, 2, 0);
            AssertHasOneClassEqualTo(expected, codeBase);
        }

        [Test]public void Should_Parse_When_DIT_Is_NA()
        {
            const string line = "\"org.jboss.as.appclient.component.ApplicationClientComponentDescription\",10,\"n/a\",32";

            var codeBase = ParseUsingData(new[] { Heading, line });

            var expected = new Class("org.jboss.as.appclient.component", "ApplicationClientComponentDescription", 0, 32, 0, 0, 10);
            AssertHasOneClassEqualTo(expected, codeBase);
        }

        [Test]public void Should_Parse_When_LOC_Is_NA()
        {
            const string line = "\"org.jboss.as.appclient.component.ApplicationClientComponentDescription\",10,3,\"n/a\"";

            var codeBase = ParseUsingData(new[] { Heading, line });

            var expected = new Class("org.jboss.as.appclient.component", "ApplicationClientComponentDescription", 0, 0, 0, 3, 10);
            AssertHasOneClassEqualTo(expected, codeBase);
        }
    }
}
