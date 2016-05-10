using System.Collections.Generic;
using System.Linq;
using Metropolis.Api.Extensions;
using NUnit.Framework;

namespace Metropolis.Test.Extensions
{
    [TestFixture]
    public class LinqExtenstionsTest
    {
        [TestCase(10, 10)]
        [TestCase(5, 20)]
        [TestCase(2, 50)]
        public void Should_Generate_Batches_of(int batchSize, int numberOfBatches)
        {
            var list = new List<int>();
            for (var i = 0; i < batchSize*numberOfBatches; i++)
            {
                list.Add(i);
            }

            var result = list.Batch(batchSize).ToList();
            Assert.AreEqual(numberOfBatches, result.Count);
            foreach (var batch in result)
            {
                Assert.AreEqual(batchSize, batch.Count());
            }
        }

        [TestCase(99, 10, 10)]
        [TestCase(5, 2, 3)]
        [TestCase(15, 10, 2)]
        public void Last_Batch_Should_Have_Remainder(int totalItems, int batchSize, int numberOfBatches)
        {
            var list = new List<int>();
            for (var i = 0; i < totalItems; i++)
            {
                list.Add(i);
            }

            var result = list.Batch(batchSize);
            Assert.AreEqual(numberOfBatches, result.Count());
            Assert.AreEqual(totalItems - batchSize*(numberOfBatches - 1), result.Last().Count());
        }
    }
}