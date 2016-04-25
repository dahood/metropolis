using System.Linq;
using FluentAssertions;
using Metropolis.Domain;
using NUnit.Framework;

namespace Test.Metropolis.Domain
{
    [TestFixture]
    public class CodeBaseTest
    {
        private readonly Class storeFront = new Class("shopping", "StoreFront", 1, 1, 1, 1, 1) {Toxicity = 2};
        private CodeGraph graph;
        private CodeBase codeBase;

        [SetUp]
        public void SetUp()
        {
            graph = new CodeGraph(new[] { storeFront });
            codeBase = new CodeBase("shopping.cart", graph);
        }

        [Test]
        public void Construct()
        {
            var cb = new CodeBase(graph);
            cb.AllClasses.Should().Contain(graph.AllClasses);
            cb.LinesOfCode.Should().Be(graph.AllClasses.Sum(x => x.LinesOfCode));
        }

        [Test]
        public void Enrich()
        {
            var cart = new Class("cart", "Cart",2,2,2,2,2) {Toxicity = 1};
            var newGraph = new CodeGraph(new [] {cart});

            codeBase.Enrich(newGraph);

            graph.AllClasses.Count.Should().Be(2);
            graph.AllNamespaces.Should().Contain(new[] {"shopping", "cart"});         
        }

        [Test]
        public void NumberOfTypes()
        {
            var cart = new Class("cart", "Cart", 2, 2, 2, 2, 2) { Toxicity = 1 };
            graph.Apply(cart);
            codeBase.NumberOfTypes.Should().Be(2);
        }

        [Test]
        public void AverageToxicity()
        {
            var cart = new Class("cart", "Cart", 2, 2, 2, 2, 2) {Toxicity = 1};
            graph.Apply(cart);
            codeBase.AverageToxicity().Should().Be(1.5);
        }

        [Test]
        public void LinesOfCode()
        {
            var cart = new Class("cart", "Cart", 2, 2, 2, 2, 2) { Toxicity = 1 };
            graph.Apply(cart);
            codeBase.LinesOfCode.Should().Be(3);
            codeBase.AllClasses.Should().Contain(new[] { storeFront, cart });
        }
        [Test]
        public void AllClasses()
        {
            var cart = new Class("cart", "Cart", 2, 2, 2, 2, 2) { Toxicity = 1 };
            graph.Apply(cart);
            codeBase.AllClasses.Should().Contain(graph.AllClasses);
        }

        [Test]
        public void ByNamespace()
        {
            var cart = new Class("cart", "Cart", 2, 2, 2, 2, 2) { Toxicity = 1 };
            graph.Apply(cart);

            var actual = codeBase.ByNamespace();
            actual.Count.Should().Be(2);
            actual.Keys.Should().Contain(new[] {"shopping", "cart"});

            actual["shopping"].Count().Should().Be(1);
            actual["shopping"].Should().Contain(storeFront);
            actual["cart"].Count().Should().Be(1);
            actual["cart"].Should().Contain(cart);
        }
        
    }
}
