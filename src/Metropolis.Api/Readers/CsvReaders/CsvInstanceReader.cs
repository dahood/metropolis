using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Metropolis.Api.Domain;
using NLog;

namespace Metropolis.Api.Readers.CsvReaders
{
    public abstract class CsvInstanceReader<T, TMapper> : IInstanceReader
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger(typeof(T));
        protected bool HasHeaderRecord { get; }

        protected CsvInstanceReader(bool hasHeaderRecord = false)
        {
            HasHeaderRecord = hasHeaderRecord;
        }

        public CodeBase Parse(TextReader textReader)
        {
            using (textReader)
            {
                var csv = new CsvReader(textReader);
                csv.Configuration.HasHeaderRecord = HasHeaderRecord;
                csv.Configuration.RegisterClassMap(typeof (TMapper));

                try
                {
                    return ParseLines(csv.GetRecords<T>().ToList());
                }
                catch (CsvMissingFieldException fieldMissingException)
                {
                    var message = "Incorrect File Format for " + typeof(T).Name + " Message: " + fieldMissingException.Message;
                    Logger.Error(fieldMissingException, message);
                    throw new ApplicationException(message);
                }
            }
        }

        protected abstract CodeBase ParseLines(IEnumerable<T> lines);
    }
}