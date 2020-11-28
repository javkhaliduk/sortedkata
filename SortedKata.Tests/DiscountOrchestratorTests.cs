using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortedKata.BLL.Implementation;
using SortedKata.BLL.Models;
using System.Collections.Generic;

namespace SortedKata.Tests
{
    [TestClass]
    public class DiscountOrchestratorTests
    {
        [TestMethod]
        public void CalculateDiscount_IfItemsNull_ShouldReturnZero()
        {
            //arrange
            var expected = 0.0m;
            var orchestrator = new DiscountOrchestrator();
            //act 
            var actual=orchestrator.CalculateDiscount(null, GetOffer());
            //assert
            actual.Should().Be(expected);
        }
        [TestMethod]
        public void CalculateDiscount_IfItemsEmpty_ShouldReturnZero()
        {
            //arrange
            var expected = 0.0m;
            var orchestrator = new DiscountOrchestrator();
            //act 
            var actual = orchestrator.CalculateDiscount(new List<Item>(), GetOffer());
            //assert
            actual.Should().Be(expected);
        }
        [TestMethod]
        public void CalculateDiscount_IfItemsNotEmptyOrNull_ShouldCalculateDiscount()
        {
            //arrange
            var expected = 0.20M;
            var orchestrator = new DiscountOrchestrator();
            //act 
            var actual = orchestrator.CalculateDiscount(ItemsList(), GetOffer());
            //assert
            actual.Should().Be(expected);
        }
        private ItemOffer GetOffer()
        {
            return new ItemOffer { SKU="A99", OfferPrice=1.30m, Quantity=3 };
        }
        private List<Item> ItemsList()
        {
            return new List<Item>
            {
                  new Item { SKU="A99", Price = 0.50m },
                  new Item { SKU="A99", Price = 0.50m },
                  new Item { SKU="A99", Price = 0.50m },
                  new Item { SKU="A99", Price = 0.50m },
            };
        }
    }
}
