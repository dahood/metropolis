﻿using System.Collections.Generic;
using System.Linq;
using Metropolis.Api.Domain;

namespace Metropolis.Api.Readers.CsvReaders
{
    //Data Aquisition
    public class ToxicityReader : CsvInstanceReader<ToxicityCsvLineItem, ToxicityCsvLineItemClassMap>
    {
        public ToxicityReader(bool hasHeaderRecord = true) : base(hasHeaderRecord)
        {
        }

        protected override CodeBase ParseLines(IEnumerable<ToxicityCsvLineItem> lines)
        {
            return new CodeBase(
                new CodeGraph(
                    lines.Select(line => new Instance(line.Type, line.Namespace, CodeBagType.Namespace)
                                        {
                                            NumberOfMethods = line.NumberOfMethods,
                                            LinesOfCode = line.LinesOfCode,
                                            Toxicity = line.Toxicity
                                        })));
        }
    }
}