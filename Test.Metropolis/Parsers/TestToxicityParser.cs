using System;
using System.IO;
using System.Reflection;
using Metropolis.Parsers.CsvParsers;
using NUnit.Framework;

namespace Test.Metropolis.Parsers
{
    [TestFixture]
    public class TestToxicityParser
    {
        [Test]
        public void Should_Parse_Line_Into_LineItem()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var path = executingAssembly.CodeBase.Substring(0, executingAssembly.CodeBase.IndexOf("metropolis", StringComparison.CurrentCultureIgnoreCase));
            var fileName = new Uri(Path.Combine(path,@"metropolis\Metropolis\SampleFiles\aspnet-toxicity-input.csv")).LocalPath;

            var results = new ToxicityParser(true).Parse(fileName);
            Assert.That(results, Is.Not.Null);
            //TODO: assert something :)
        }
    }
}