using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Metropolis.Api.Domain;
using Metropolis.Api.Utilities;
using NUnit.Framework;

namespace Metropolis.Test.Api.Readers
{
    [TestFixture]
    public class XmlClassParserTest
    {
        [SetUp]
        public void SetUp()
        {
            fileName = Path.Combine(Environment.CurrentDirectory, $"xml {Clock.Now.ToString("yyyy-M-d dddd-HH-mm-ss")}");
            File.Exists(fileName).Should().BeFalse($"{fileName} should not exist");
            File.WriteAllText(fileName, JavaMetricsHelper.GetXml());
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
        }

        private string fileName;

        private static Instance AssertResultIsNotNullAndWithOneClass(CodeBase result)
        {
            result.Should().NotBeNull();
            result.AllInstances.Count.Should().Be(1);

            return result.AllInstances.First();
        }
    }
}
