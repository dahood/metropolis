using System;
using System.IO;
using System.Reflection;
using Metropolis.Api.Readers.CsvReaders;
using NUnit.Framework;

namespace Metropolis.Test.Api.Readers
{
    [TestFixture]
    public class ToxicityReaderTest
    {
        [Test]
        public void Should_Parse_Line_Into_LineItem()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var path = executingAssembly.CodeBase.Substring(0,
                executingAssembly.CodeBase.IndexOf("metropolis", StringComparison.CurrentCultureIgnoreCase));
            var fileName =
                new Uri(Path.Combine(path, @"metropolis\src\Metropolis\SampleFiles\aspnet-toxicity-input.csv"))
                    .LocalPath;

            var results = new ToxicityReader(true).Parse(fileName);
            Assert.That(results, Is.Not.Null);
            //TODO: assert something :)
        }
    }
}