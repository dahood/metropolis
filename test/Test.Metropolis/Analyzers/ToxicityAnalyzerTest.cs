using Metropolis.Api.Core.Analyzers.Toxicity;
using Metropolis.Api.Core.Domain;
using Metropolis.Domain;
using NUnit.Framework;

namespace Test.Metropolis.Analyzers
{
    [TestFixture]
    public class ToxicityAnalyzerTest 
    {
        private const double Delta = 0.1;

        [Test]
        public void Toxicity_On_ComplexMethod()
        {
            var type = new Class("Metropolis", "Canvas", 1, 1, 1, 1, 1);
            type.AddMember(new[] {new Member("Foo()", 1, 22, 1)});

            Assert.AreEqual(0.7, new CSharpToxicityAnalyzer().CalculateToxicity(type).Toxicity, Delta);
        }

        [Test]
        public void Toxicity_On_Deep_Depth_of_Inheritance()
        {
            var type = new Class("Metropolis", "Canvas", 1, 1, 1, 4, 1);
            Assert.AreEqual(1.6, new CSharpToxicityAnalyzer().CalculateToxicity(type).Toxicity, Delta);
        }

        [Test]
        public void Toxicity_On_Good_Type()
        {
            var type = new Class("Metropolis", "Canvas", 1, 1, 1, 1, 1);
            type.AddMember(new[] {new Member("Foo()", 1, 1, 1)});

            Assert.AreEqual(0, new CSharpToxicityAnalyzer().CalculateToxicity(type).Toxicity);
        }

        [Test]
        public void Toxicity_On_Large_Class_Coupling()
        {
            var type = new Class("Metropolis", "Canvas", 1, 1, 1, 1, 32);
            Assert.AreEqual(0.7, new CSharpToxicityAnalyzer().CalculateToxicity(type).Toxicity, Delta);
        }

        [Test]
        public void Toxicity_On_Large_Lines_Of_Code()
        {
            var type = new Class("Metropolis", "Canvas", 1, 502, 1, 1, 1);
            Assert.AreEqual(0.7, new CSharpToxicityAnalyzer().CalculateToxicity(type).Toxicity, Delta);
        }

        [Test]
        public void Toxicity_On_LargeMethodCount()
        {
            const int setting = 22;
            var type = new Class("Metropolis", "Canvas", 1, 1, 1, 1, 1);
            for (var i = 0; i < setting; i++)
                type.AddMember(new[] {new Member("Foo()", 1, 1, 1)});

            Assert.AreEqual(0.7, new CSharpToxicityAnalyzer().CalculateToxicity(type).Toxicity, Delta);
        }

        [Test]
        public void Toxicity_On_LongMethod()
        {
            var type = new Class("Metropolis", "Canvas", 1, 1, 1, 1, 1);
            type.AddMember(new[] {new Member("Foo()", 32, 1, 1)});

            Assert.AreEqual(0.7, new CSharpToxicityAnalyzer().CalculateToxicity(type).Toxicity, Delta);
        }

        [Test]
        public void Toxicity_On_VERY_ComplexMethod()
        {
            var type = new Class("Metropolis", "Canvas", 1, 1, 1, 1, 1);
            type.AddMember(new[] {new Member("Foo()", 1, 55, 1)});

            Assert.AreEqual(3.55, new CSharpToxicityAnalyzer().CalculateToxicity(type).Toxicity, Delta);
        }

        [Test]
        public void Toxicity_On_VERY_Deep_Depth_of_Inheritance()
        {
            var type = new Class("Metropolis", "Canvas", 1, 1, 1, 14, 1);
            Assert.AreEqual(4, new CSharpToxicityAnalyzer().CalculateToxicity(type).Toxicity, Delta);
        }

        [Test]
        public void Toxicity_On_VERY_Large_Class_Coupling()
        {
            var type = new Class("Metropolis", "Canvas", 1, 1, 1, 1, 250);
            Assert.AreEqual(5.4, new CSharpToxicityAnalyzer().CalculateToxicity(type).Toxicity, Delta);
        }

        [Test]
        public void Toxicity_On_VERY_Large_Lines_Of_Code()
        {
            var type = new Class("Metropolis", "Canvas", 1, 1500, 1, 1, 1);
            Assert.AreEqual(6.9, new CSharpToxicityAnalyzer().CalculateToxicity(type).Toxicity, Delta);
        }

        [Test]
        public void Toxicity_On_VERY_LargeMethodCount()
        {
            const int setting = 80;
            var type = new Class("Metropolis", "Canvas", 1, 1, 1, 1, 1);
            for (var i = 0; i < setting; i++)
                type.AddMember(new[] {new Member("Foo()", 1, 1, 1)});

            Assert.AreEqual(4, new CSharpToxicityAnalyzer().CalculateToxicity(type).Toxicity, Delta);
        }

        [Test]
        public void Toxicity_On_VERY_LongMethod()
        {
            var type = new Class("Metropolis", "Canvas", 1, 1, 1, 1, 1);
            type.AddMember(new[] {new Member("Foo()", 100, 0, 1)});

            Assert.AreEqual(4.2, new CSharpToxicityAnalyzer().CalculateToxicity(type).Toxicity, Delta);
        }
    }
}