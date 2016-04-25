﻿using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Metropolis.Domain;
using Metropolis.Parsers.XmlParxers;
using Metropolis.Utilities;
using NUnit.Framework;

namespace Test.Metropolis.Parsers
{
    [TestFixture]
    public class XmlClassParserTest
    {
        private string fileName;

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

        [Test]
        public void Should_Parse_DepthOfInheritanceTree()
        {
            var result = new XmlClassParser().Parse(fileName);

            var actualClass = AssertResultIsNotNullAndWithOneClass(result);

            actualClass.Name.Should().Be("SqlGeneratorBase");
            actualClass.NameSpace.Should().Be("org.hibernate.hql.internal.ast.tree");
            actualClass.DepthOfInheritance.Should().Be(JavaMetricsHelper.DIT);
        }

        [Test]
        public void Should_Parse_MethodLinesOfCode()
        {
            var result = new XmlClassParser().Parse(fileName);

            var actualClass = AssertResultIsNotNullAndWithOneClass(result);

            actualClass.Members.Count.Should().Be(1);
            actualClass.Members.First().LinesOfCode.Should().Be(JavaMetricsHelper.MLOC);
        }

        [Test]
        public void Should_Parse_CyclomaticComplexity()
        {
            var result = new XmlClassParser().Parse(fileName);

            var actualClass = AssertResultIsNotNullAndWithOneClass(result);

            actualClass.Members.Count.Should().Be(1);
            actualClass.Members.First().CylomaticComplexity.Should().Be(JavaMetricsHelper.VG);
        }

        [Test]
        public void Should_Parse_ClassAttributes_To_Makeup_Lines_of_Code()
        {
            var result = new XmlClassParser().Parse(fileName);

            var actualClass = AssertResultIsNotNullAndWithOneClass(result);
            actualClass.LinesOfCode.Should().Be(JavaMetricsHelper.NOF + JavaMetricsHelper.MLOC); 
        }
         
        private static Class AssertResultIsNotNullAndWithOneClass(CodeBase result)
        {
            result.Should().NotBeNull();
            result.AllClasses.Count.Should().Be(1);

            return result.AllClasses.First();
        }
    }
}
