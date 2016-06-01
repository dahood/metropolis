using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Metropolis.Api.Domain;
using NUnit.Framework;

namespace Metropolis.Test.Api.Domain
{
    [TestFixture]
    public class CodeBaseTest
    {
        [SetUp]
        public void SetUp()
        {
            graph = new CodeGraph(new[] {storeFront});
            codeBase = new CodeBase("shopping.cart", graph);
        }

        private readonly Instance storeFront = new Instance("shopping", "StoreFront", 1, 1, 1, 1, 1) {Toxicity = 2};
        private CodeGraph graph;
        private CodeBase codeBase;

        [Test]
        public void AllClasses()
        {
            var cart = new Instance("cart", "Cart", 2, 2, 2, 2, 2) {Toxicity = 1};
            graph.Apply(cart);
            codeBase.AllInstances.Should().Contain(graph.AllInstances);
        }

        [Test]
        public void AverageToxicity()
        {
            var cart = new Instance("cart", "Cart", 2, 2, 2, 2, 2) {Toxicity = 1};
            graph.Apply(cart);
            codeBase.AverageToxicity().Should().Be(1.5);
        }

        [Test]
        public void ByNamespace()
        {
            var cart = new Instance("cart", "Cart", 2, 2, 2, 2, 2) {Toxicity = 1};
            graph.Apply(cart);

            var actual = codeBase.ByNamespace();
            actual.Count.Should().Be(2);
            var shoppingBag = new CodeBag("shopping", CodeBagType.Empty, string.Empty);
            var cartBag = new CodeBag("cart", CodeBagType.Empty, string.Empty);
            actual.Keys.Should().Contain(new List<CodeBag>
                {
                    shoppingBag,
                    cartBag
                });

            actual[shoppingBag].Count().Should().Be(1);
            actual[shoppingBag].Should().Contain(storeFront);
            actual[cartBag].Count().Should().Be(1);
            actual[cartBag].Should().Contain(cart);
        }

        [Test]
        public void Construct()
        {
            var cb = new CodeBase(graph);
            cb.AllInstances.Should().Contain(graph.AllInstances);
            cb.LinesOfCode.Should().Be(graph.AllInstances.Sum(x => x.LinesOfCode));
        }

        [Test]
        public void Enrich()
        {
            var cart = new Instance("cart", "Cart", 2, 2, 2, 2, 2) {Toxicity = 1};
            var newGraph = new CodeGraph(new[] {cart});

            codeBase.Enrich(newGraph);

            graph.AllInstances.Count.Should().Be(2);
            graph.AllNamespaces.Should()
                .Contain(new List<CodeBag>
                {
                    new CodeBag("shopping", CodeBagType.Empty, string.Empty),
                    new CodeBag("cart", CodeBagType.Empty, string.Empty)
                });
        }

        [Test]
        public void LinesOfCode()
        {
            var cart = new Instance("cart", "Cart", 2, 2, 2, 2, 2) {Toxicity = 1};
            graph.Apply(cart);
            codeBase.LinesOfCode.Should().Be(3);
            codeBase.AllInstances.Should().Contain(new[] {storeFront, cart});
        }

        [Test]
        public void NumberOfTypes()
        {
            var cart = new Instance("cart", "Cart", 2, 2, 2, 2, 2) {Toxicity = 1};
            graph.Apply(cart);
            codeBase.NumberOfTypes.Should().Be(2);
        }
    }
}