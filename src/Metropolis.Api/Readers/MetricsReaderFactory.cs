﻿using System;
using System.Collections.Generic;
using Metropolis.Api.Readers.CsvReaders;
using Metropolis.Api.Readers.XmlReaders.CheckStyles;
using Metropolis.Api.Readers.XmlReaders.FxCop;
using Metropolis.Common.Models;

namespace Metropolis.Api.Readers
{
    public class MetricsReaderFactory : IMetricsReaderFactory
    {
        private static readonly Dictionary<ParseType, Func<IInstanceReader>> MapDefinitions = new Dictionary<ParseType, Func<IInstanceReader>>
        {
            {ParseType.FxCop,  () => new FxCopMetricsReader()},
            {ParseType.PuppyCrawler, () => CheckStylesReader.PuppyCrawlReader},
            {ParseType.EsLint, () => CheckStylesReader.EslintReader},
            {ParseType.SlocEcma, () => new SlocReader(FileInclusion.Js)},
            {ParseType.SlocCSharp, () => new SlocReader(FileInclusion.CSharp)},
            {ParseType.SlocJava, () => new SlocReader(FileInclusion.Java)},
            {ParseType.CpdJava, () => new CpdReader() },
            {ParseType.CpdEcma, () => new CpdReader() },
            {ParseType.CpdCsharp, () => new CpdReader() }
        };

        private readonly Dictionary<ParseType, Func<IInstanceReader>> readerMap;

        public MetricsReaderFactory() : this(MapDefinitions)
        {
            
        }

        public MetricsReaderFactory(Dictionary<ParseType, Func<IInstanceReader>> readerMap)
        {
            this.readerMap = readerMap;
        }


        public IInstanceReader GetReader(ParseType parseType)
        {
            if (!readerMap.ContainsKey(parseType))
                throw new ApplicationException($"{parseType} is not a known metrics parser type");
            return readerMap[parseType]();
        }
    }
}