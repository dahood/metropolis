﻿using System;
using System.Collections.Generic;
using FluentAssertions;
using Metropolis.Api.Readers;
using Metropolis.Api.Readers.CsvReaders;
using Metropolis.Api.Readers.XmlReaders.CheckStyles;
using Metropolis.Api.Readers.XmlReaders.FxCop;
using Metropolis.Common.Models;
using NUnit.Framework;

namespace Metropolis.Test.Api.Readers
{
    [TestFixture]
    public class MetricsReaderFactoryTest
    {
        readonly MetricsReaderFactory factory = new MetricsReaderFactory();

        [Test]
        public void PuppCrawler()
        {
            factory.GetReader(ParseType.PuppyCrawler)
                .Should().NotBeNull()
                .And.BeAssignableTo<CheckStylesReader>();
        }

        [Test]
        public void Eslink()
        {
            factory.GetReader(ParseType.EsLint)
                .Should().NotBeNull()
                .And.BeAssignableTo<CheckStylesReader>();
        }

        [Test]
        public void SlocECMA()
        {
            factory.GetReader(ParseType.SlocEcma)
                   .Should().NotBeNull()
                   .And.BeAssignableTo<SlocReader>();
        }

        [Test]
        public void SlocCSharp()
        {
            factory.GetReader(ParseType.SlocCSharp)
                .Should().NotBeNull()
                .And.BeAssignableTo<SlocReader>();
        }

        [Test]
        public void SlocJava()
        {
            factory.GetReader(ParseType.SlocJava)
                .Should().NotBeNull()
                .And.BeAssignableTo<SlocReader>();
        }

        [Test]
        public void SlocFxCop()
        {
            factory.GetReader(ParseType.FxCop)
                .Should().NotBeNull()
                .And.BeAssignableTo<FxCopMetricsReader>();
        }

        [Test]
        public void ShouldThrowExceptionWhenNotMapped()
        {
            var factoryWithMissingMapping = new MetricsReaderFactory(new Dictionary<ParseType, Func<IInstanceReader>>
                                                    {{ParseType.FxCop, () => new FxCopMetricsReader()}});

            Assert.Throws<ApplicationException>(() => factoryWithMissingMapping.GetReader(ParseType.EsLint));
        }
    }
}