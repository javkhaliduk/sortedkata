using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SortedKata.BLL.Implementation;
using SortedKata.BLL.Interfaces;
using SortedKata.BLL.Models;
using System;
using System.Collections.Generic;

namespace SortedKata.Tests
{
    [TestClass]
    public class CheckoutOrchestratorTests
    {
        [TestMethod]
        public void AddItemsToCheckout_IfSkuNullOrEmpty_ShouldThrowArgumentNullException()
        {
            // arrange
            var orchestrator = new CheckoutOrchestrator(new Mock<ItemOrchestrator>().Object,new Mock<IOfferOrchestrator>().Object);
            //act
            Func<bool> action = ()=> { return orchestrator.ScanItem(null); };
            //assert
            action.Should().Throw<ArgumentNullException>();
        }
        [TestMethod]
        public void AddItemsToCheckout_AddItem_ShouldReturnTrue()
        {
            // arrange
            Mock<IItemOrchestrator> mockItem = new Mock<IItemOrchestrator>();
            mockItem.Setup(mock => mock.GetItem(It.IsAny<string>())).Returns(new Item { SKU = "A99", Price = 0.50m });
            var orchestrator = new CheckoutOrchestrator(mockItem.Object, new Mock<IOfferOrchestrator>().Object);
            //act
            var itemAdded=orchestrator.ScanItem("A99");
            //assert
            itemAdded.Should().BeTrue();
        }
        [TestMethod]
        public void CalculateDiscount_IfSKUEmptyOrNull_ShouldThrowArgumentNullException()
        {
            // arrange
            var orchestrator = new CheckoutOrchestrator(new Mock<ItemOrchestrator>().Object, new Mock<IOfferOrchestrator>().Object);
            //act
            Func<decimal> action = () =>{
                return orchestrator.CalculateDiscount(null);
            };

            //assert
            action.Should().Throw<ArgumentNullException>();
        }

    }
}
