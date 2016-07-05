using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Metropolis.Api.Domain;

namespace Metropolis.Api.Readers.CsvReaders
{
    /// <summary>
    ///     Reads Copy-Paste-Detector csv output file
    /// </summary>
    public class CpdReader : IInstanceReader
    {
        private readonly List<Instance> instances = new List<Instance>();

        protected Instance this[string fileName]
        {
            get
            {
                var found = instances.SingleOrDefault(x => x.PhysicalPath == new Location(fileName));
                if (found != null) return found;
                found = InstanceBuilder.Build(fileName);
                instances.Add(found);
                return found;
            }
        }

        public CodeBase Parse(TextReader textReader)
        {
            var items = ReadFile(textReader);

            foreach (var cpdLineItem in items)
            {
                foreach (var occurance in cpdLineItem.Occurances)
                {
                    this[occurance.FileName].Duplicates.Add(new Duplicate(cpdLineItem.LinesOfCode, occurance.LineNumber, new Location(occurance.FileName)));
                }
            }
            return new CodeBase(new CodeGraph(instances));
        }

        private static IEnumerable<CpdLineItem> ReadFile(TextReader textReader)
        {
            //lines,tokens,occurrences
            //47,196,2,
            //  106,C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedBatchThroughputTest.java,
            //  104,C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedThroughputTest.java
            //47,196,4,
            //  106,C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedBatchThroughputTest.java,
            //  104,C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedThroughputTest.java,
            //  106,C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedBatchThroughputTestAA.java,
            //  104,C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedThroughputTestAAA.java
            var items = new List<CpdLineItem>();
            using (textReader)
            {
                var csv = new CsvReader(textReader);
                while (csv.Read())
                {
                    var linesOfCode = csv.GetField<int>(0);
                    var occurances = csv.GetField<int>(2);
                    var lineItem = new CpdLineItem {LinesOfCode = linesOfCode};
                    for (var i = 0; i < occurances; i++)
                    {
                        var index = 3 + i*2;
                        var cpdOccurance = new CpdOccurance
                        {
                            LineNumber = csv.GetField<int>(index), // 3, 5, 7, etc.
                            FileName = csv.GetField<string>(index + 1) // 4, 6, 8, etc
                        };
                        lineItem.Occurances.Add(cpdOccurance);
                    }
                    items.Add(lineItem);
                }
            }
            return items;
        }
    }
}