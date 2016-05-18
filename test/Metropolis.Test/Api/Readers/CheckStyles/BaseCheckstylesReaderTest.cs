using System;
using System.IO;
using Metropolis.Api.Readers.XmlReaders.CheckStyles;
using NUnit.Framework;

namespace Metropolis.Test.Api.Readers.CheckStyles
{
    public abstract class BaseCheckstylesReaderTest
    {
        protected CheckStylesReader Reader;
        protected abstract CheckStylesReader CreateParser();
        protected abstract string FileName { get; }
        protected abstract string CheckStylesFixture { get; }
        private string checkstylesFileName;

        [SetUp]
        public void SetUp()
        {
            checkstylesFileName = $"{Path.Combine(Environment.CurrentDirectory, FileName)}";
            Reader = CreateParser();
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