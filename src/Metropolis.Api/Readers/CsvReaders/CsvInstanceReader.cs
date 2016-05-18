using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Metropolis.Api.Domain;

namespace Metropolis.Api.Readers.CsvReaders
{
    public abstract class CsvInstanceReader<T, TMapper> : IInstanceReader
    {
        protected bool HasHeaderRecord { get; }

        protected CsvInstanceReader(bool hasHeaderRecord = false)
        {
            HasHeaderRecord = hasHeaderRecord;
        }

        public CodeBase Parse(string fileName)
        {
            using (TextReader reader = File.OpenText(fileName))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.HasHeaderRecord = HasHeaderRecord;
                csv.Configuration.RegisterClassMap(typeof (TMapper));

                try
                {
                    return ParseLines(csv.GetRecords<T>().ToList());
                }
                catch (CsvMissingFieldException fieldMissingException)
                {
                    throw new ApplicationException("Incorrect File Format for " + typeof (T).Name + " Message: " + fieldMissingException.Message);
                }
            }
        }

        protected abstract CodeBase ParseLines(IEnumerable<T> lines);
    }
}