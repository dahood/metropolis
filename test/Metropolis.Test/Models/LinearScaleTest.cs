using Metropolis.Models;
using NUnit.Framework;

namespace Metropolis.Test.Models
{
    [TestFixture]
    public class LinearScaleTest
    {
        [Test]
        public void Should_Scale_In_Linear_Fashion()
        {
            Assert.AreEqual(100, LinearScale.Apply(100, 0, 100));
            Assert.AreEqual(50, LinearScale.Apply(250, 0, 500));
        }
    }
}