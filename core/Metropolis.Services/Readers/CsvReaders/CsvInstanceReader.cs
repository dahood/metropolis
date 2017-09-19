using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Metropolis.Api.Domain;
using Microsoft.Extensions.Logging;

namespace Metropolis.Api.Readers.CsvReaders
{
    public abstract class CsvInstanceReader<T, TMapper> : IInstanceReader
    {
        private readonly ILogger Logger; 
        protected bool HasHeaderRecord { get; }

        protected CsvInstanceReader(bool hasHeaderRecord = false)
        {
            HasHeaderRecord = hasHeaderRecord;
            Logger = LogManager.LoggerFactory.CreateLogger<T>();
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
                    Logger.LogError(fieldMissingException, message);
                    throw new ApplicationException(message);
                }
            }
        }

        protected abstract CodeBase ParseLines(IEnumerable<T> lines);
    }
}