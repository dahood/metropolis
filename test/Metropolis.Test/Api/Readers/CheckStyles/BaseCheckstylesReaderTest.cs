using System;
using System.IO;
using Metropolis.Api.Readers.XmlReaders.CheckStyles;
using Metropolis.Test.TestHelpers;
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
            checkstylesFileName.RemoveFileIfExists();
            File.WriteAllText(checkstylesFileName, CheckStylesFixture);

            Reader = CreateParser();
        }

        [TearDown]
        public void TearDown()
        {
            checkstylesFileName.RemoveFileIfExists();
        }
        
    }
}