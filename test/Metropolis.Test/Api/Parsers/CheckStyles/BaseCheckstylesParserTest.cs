using System;
using System.IO;
using Metropolis.Api.Parsers.XmlReaders.CheckStyles;
using NUnit.Framework;

namespace Metropolis.Test.Api.Parsers.CheckStyles
{
    public abstract class BaseCheckstylesParserTest
    {
        protected CheckStylesParser Parser;
        protected abstract CheckStylesParser CreateParser();
        protected abstract string FileName { get; }
        protected abstract string CheckStylesFixture { get; }
        private string checkstylesFileName;

        [SetUp]
        public void SetUp()
        {
            checkstylesFileName = $"{Path.Combine(Environment.CurrentDirectory, FileName)}";
            Parser = CreateParser();
            RemoveFile(checkstylesFileName);
            File.WriteAllText(checkstylesFileName, CheckStylesFixture);
        }

        [TearDown]
        public void TearDown()
        {
            RemoveFile(checkstylesFileName);
        }

        private static void RemoveFile(string fileName)
        {
            if (File.Exists(fileName)) File.Delete(fileName);
        }
    }
}