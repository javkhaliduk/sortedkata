using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SortedKata.BLL.Implementation;
using SortedKata.BLL.Interfaces;
using SortedKata.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SortedKata.Tests
{

    [TestClass]
    public class CheckoutOrchestratorTests
    {
        Checkout checkoutItems;
        Guid checkoutId;
        [TestMethod]
        [TestCategory("Checkout > ScanItem")]
        public void AddItemsToCheckout_IfSkuNullOrEmpty_ShouldThrowArgumentNullException()
        {
            // arrange
            var orchestrator = new CheckoutOrchestrator(new Mock<ItemOrchestrator>().Object, new Mock<IOfferOrchestrator>().Object);
            //act
            Func<bool> action = () => { return orchestrator.ScanItem(null); };
            //assert
            action.Should().Throw<ArgumentNullException>();
        }
        [TestMethod]
        [TestCategory("Checkout > ScanItem")]
        public void AddItemsToCheckout_AddItem_ShouldReturnTrue()
        {
            // arrange
            Mock<IItemOrchestrator> mockItem = new Mock<IItemOrchestrator>();
            mockItem.Setup(mock => mock.GetItem(It.IsAny<string>())).Returns(new Item { SKU = "A99", Price = 0.50m });
            var orchestrator = new CheckoutOrchestrator(mockItem.Object, new Mock<IOfferOrchestrator>().Object);
            //act
            var itemAdded = orchestrator.ScanItem("A99");
            //assert
            itemAdded.Should().BeTrue();
        }
        [TestMethod]
        public void CalculateDiscount_IfSKUEmptyOrNull_ShouldThrowArgumentNullException()
        {
            // arrange
            var orchestrator = new CheckoutOrchestrator(new Mock<ItemOrchestrator>().Object, new Mock<IOfferOrchestrator>().Object);
            //act
            Func<decimal> action = () =>
            {
                return orchestrator.CalculateDiscount(null);
            };

            //assert
            action.Should().Throw<ArgumentNullException>();
        }
        [TestMethod]
        [TestCategory("Checkout > CalculateDiscount")]
        public void CalculateDiscount_IfNoOfferFound_ShouldNotCalculateDiscount()
        {
            // arrange
            var orchestrator = new CheckoutOrchestrator(new Mock<ItemOrchestrator>().Object, new Mock<IOfferOrchestrator>().Object);
            var sku = "B15";
            var expected = 0.0m;
            //act
            var actual = orchestrator.CalculateDiscount(sku);
            //assert
            actual.Should().Be(expected);
        }
        [TestMethod]
        [TestCategory("Checkout > CalculateDiscount")]
        public void CalculateDiscount_IfOfferFound_ShouldCalculateDiscount()
        {
            // arrange
            var offerMock = new Mock<IOfferOrchestrator>();
            offerMock.Setup(mock => mock.GetOffer("B15"))
                .Returns(new ItemOffer { OfferPrice = 0.45m, Quantity = 2, SKU = "B15" });
            var orchestrator = new CheckoutOrchestrator(new Mock<ItemOrchestrator>().Object, offerMock.Object);
            var sku = "B15";
            var expected = 0.45m;
            orchestrator._listCheckout = new List<Checkout>() { checkoutItems };
            //act
            var actual = orchestrator.CalculateDiscount(sku);
            //assert
            actual.Should().Be(expected);
        }
        [TestMethod]
        [TestCategory("Checkout > GetTotalPrice")]
        public void GetTotalPrice_IfOfferExists_ShouldApplyDiscount()
        {
            // arrange
            var offerMock = new Mock<IOfferOrchestrator>();
            offerMock.Setup(mock => mock.GetOffer("B15"))
                .Returns(new ItemOffer { OfferPrice = 0.45m, Quantity = 2, SKU = "B15" });
            offerMock.Setup(mock => mock.GetOffer("A99"))
               .Returns(new ItemOffer { OfferPrice = 1.30m, Quantity = 3, SKU = "A99" });
            var orchestrator = new CheckoutOrchestrator(new Mock<ItemOrchestrator>().Object, offerMock.Object);
            var expected = 4.05m;
            orchestrator._listCheckout = new List<Checkout>() { checkoutItems };
            //act
            var actual = orchestrator.GetTotalPriceWithDiscount(checkoutId);
            //assert

            actual.Should().Be(expected);
        }
        [TestMethod]
        [TestCategory("Checkout > GetTotalPrice")]
        public void GetTotalPrice_IfNoOfferExists_ShouldNotApplyDiscount()
        {
            // arrange
            var orchestrator = new CheckoutOrchestrator(new Mock<ItemOrchestrator>().Object, new Mock<IOfferOrchestrator>().Object);
            var expected = checkoutItems.Items.Sum(p => p.Price);
            orchestrator._listCheckout = new List<Checkout>() { checkoutItems };
            //act
            var actual = orchestrator.GetTotalPriceWithDiscount(checkoutId);
            //assert

            actual.Should().Be(expected);
        }
        [TestMethod]
        [TestCategory("Checkout > GetTotalPrice")]
        public void GetTotalPrice_ShouldReturnTotalPriceWithoutDiscount()
        {
            // arrange
            var orchestrator = new CheckoutOrchestrator(new Mock<ItemOrchestrator>().Object, new Mock<IOfferOrchestrator>().Object);
            var expected = checkoutItems.Items.Sum(p => p.Price);
            orchestrator._listCheckout = new List<Checkout>() { checkoutItems };
            //act
            var actual = orchestrator.GetTotalPrice(checkoutId);
            //assert

            actual.Should().Be(expected);
        }
        [TestInitialize]
        public void Initialize()
        {
            checkoutId = Guid.NewGuid();
            checkoutItems = CheckoutItems();
        }
        [TestCleanup]
        public void CleanUp()
        {
            checkoutItems = null;
        }
        private Checkout CheckoutItems()
        {
            return new Checkout
            {
                Id = checkoutId,
                Items = new List<Item> {
                  new Item { SKU="A99", Price = 0.50m },
                  new Item { SKU="B15", Price = 0.30m },
                  new Item { SKU="C40", Price = 0.60m },
                  new Item { SKU="A99", Price = 0.50m },
                  new Item { SKU="B15", Price = 0.30m },
                  new Item { SKU="A99", Price = 0.50m },
                  new Item { SKU="B15", Price = 0.30m },
                  new Item { SKU="B15", Price = 0.30m },
                  new Item { SKU="A99", Price = 0.50m },
                  new Item { SKU="B15", Price = 0.30m },
                  new Item { SKU="B15", Price = 0.30m },
                  new Item { SKU="B15", Price = 0.30m },
                 }
            };
        }
    }
}
