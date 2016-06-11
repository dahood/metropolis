using FluentAssertions;
using Metropolis.Api.Collection.Steps.AllLanguages;
using Metropolis.Common.Models;
using NUnit.Framework;

namespace Metropolis.Test.Api.Collection.Steps.AllLanguages
{
    [TestFixture]
    public class CpdCollectionStepTest : CollectionBaseTest
    {
        private CpdCollectionStep step;      

        [SetUp]
        public void BeforeEachTest()
        {
            step = new CpdCollectionStep(ParseType.CpdJava);
        }

        [Test]
        public void HasCorrectSettings()
        {
            step.Extension.Should().Be(".csv");
            step.MetricsType.Should().Be("CPD");
            step.ParseType.Should().Be(ParseType.CpdJava);
        }

//        [Test]
//        public void CanParseCommand()
//        {
//            //java -cp {0} net.sourceforge.pmd.cpd.CPD --format csv --language {1} --minimum-tokens {2} --files {3} > {4}
//            //-minimum-tokens 100 --language cs --format csv --files c:\dev\metropolis > results.csv
//            string expected = $@"{NodeModulesPath}sloc '{Args.SourceDirectory}' -d --format csv -> '{Result.MetricsFile}'";
//            var command = step.PrepareCommand(Args, Result);
//
//            command.Should().Be(expected);
//        }
    }
}
