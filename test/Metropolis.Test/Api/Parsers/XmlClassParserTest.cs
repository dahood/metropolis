using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Metropolis.Api.Domain;
using Metropolis.Api.Readers.XmlReaders;
using Metropolis.Api.Utilities;
using NUnit.Framework;

namespace Metropolis.Test.Api.Parsers
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

        [Test]
        public void Should_Parse_ClassAttributes_To_Makeup_Lines_of_Code()
        {
            var result = new XmlInstanceReader().Parse(fileName);

            var actualClass = AssertResultIsNotNullAndWithOneClass(result);
            actualClass.LinesOfCode.Should().Be(JavaMetricsHelper.NOF + JavaMetricsHelper.MLOC);
        }

        [Test]
        public void Should_Parse_CyclomaticComplexity()
        {
            var result = new XmlInstanceReader().Parse(fileName);

            var actualClass = AssertResultIsNotNullAndWithOneClass(result);

            actualClass.Members.Count.Should().Be(1);
            actualClass.Members.First().CylomaticComplexity.Should().Be(JavaMetricsHelper.VG);
        }

        [Test]
        public void Should_Parse_DepthOfInheritanceTree()
        {
            var result = new XmlInstanceReader().Parse(fileName);

            var actualClass = AssertResultIsNotNullAndWithOneClass(result);

            actualClass.Name.Should().Be("SqlGeneratorBase");
            actualClass.NameSpace.Should().Be("org.hibernate.hql.internal.ast.tree");
            actualClass.DepthOfInheritance.Should().Be(JavaMetricsHelper.DIT);
        }

        [Test]
        public void Should_Parse_MethodLinesOfCode()
        {
            var result = new XmlInstanceReader().Parse(fileName);

            var actualClass = AssertResultIsNotNullAndWithOneClass(result);

            actualClass.Members.Count.Should().Be(1);
            actualClass.Members.First().LinesOfCode.Should().Be(JavaMetricsHelper.MLOC);
        }
    }
}
