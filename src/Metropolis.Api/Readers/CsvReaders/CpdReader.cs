using System.IO;
using CsvHelper;
using Metropolis.Api.Domain;

namespace Metropolis.Api.Readers.CsvReaders
{
    /// <summary>
    ///     Reads Copy-Paste-Detector csv output file
    /// </summary>
    public class CpdReader : IInstanceReader
    {
        public CodeBase Parse(TextReader textReader)
        {
            //lines,tokens,occurrences
            //47,196,2,106,C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedBatchThroughputTest.java,104,
            //  C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedThroughputTest.java
            //49,161,2,117,C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\raw\OneToOneRawBatchThroughputTest.java,
            //  115,C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\raw\OneToOneRawThroughputTest.java
            //45,159,2,58,C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\workhandler\OneToThreeReleasingWorkerPoolThroughputTest.java,
            //  72,C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\workhandler\OneToThreeWorkerPoolThroughputTest.java

            using (textReader)
            {
                var csv = new CsvReader(textReader);
                while (csv.Read())
                {
                    var intField = csv.GetField<int>(0);
                    var stringField = csv.GetField<string>(1);
                    var boolField = csv.GetField<bool>("HeaderName");
                }

                var codebase = CodeBase.Empty();
                return codebase;
            }
        }
    }
}