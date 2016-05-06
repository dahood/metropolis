using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Metropolis.Api.Core.Domain;
using Metropolis.Api.Core.Parsers.XmlParsers;

namespace Metropolis.Api.Core.Parsers.CsvParsers
{
    public abstract class CsvClassParser<T, TMapper> : IClassParser
    {
        protected bool HasHeaderRecord { get; set; }

        protected CsvClassParser(bool hasHeaderRecord = false)
        {
            this.HasHeaderRecord = hasHeaderRecord;
        }

        public CodeBase Parse(string fileName, string sourceBaseDirectory)
        {
            using (TextReader reader = File.OpenText(fileName))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.HasHeaderRecord = HasHeaderRecord;
                csv.Configuration.RegisterClassMap(typeof (TMapper));

                try
                {
                    return ParseLines(csv.GetRecords<T>().ToList(), sourceBaseDirectory);
                }
                catch (CsvMissingFieldException fieldMissingException)
                {
                    throw new ApplicationException("Incorrect File Format for " + typeof (T).Name + " Message: " + fieldMissingException.Message);
                }
            }
        }

        protected abstract CodeBase ParseLines(IEnumerable<T> lines, string sourceBaseDirectory);
    }
}