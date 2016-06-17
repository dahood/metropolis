using System.Collections.Generic;

namespace Metropolis.Api.Readers.CsvReaders
{
    public class CpdLineItem
    {
        public CpdLineItem()
        {
            Occurances = new List<CpdOccurance>();
        }

        public int LinesOfCode { get; set; }
        public List<CpdOccurance> Occurances { get; set; }
    }
}