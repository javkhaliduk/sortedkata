using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortedKata.BLL.Implementation;
using SortedKata.BLL.Models;
using System;

namespace SortedKata.Tests
{
    [TestClass]
    public class OfferOrchestratorTests
    {
        [TestMethod]
        public void AddOffer_IfSuccessfull_ShouldReturnTrue()
        {
            //arrange
            var orchestrator = new OfferOrchestrator();
            //act
            var offerAdded=orchestrator.AddOffer(new ItemOffer { SKU = "A99", Quantity = 3, OfferPrice = 1.30m });
            //assert
            offerAdded.Should().BeTrue();
        }
        [TestMethod]
        public void AddOffer_IfArgumentIsNull_ShouldThrowArgumentNullException()
        {
            //arrange
            var orchestrator = new OfferOrchestrator();
            //act
            Func<bool> action = () => { return orchestrator.AddOffer(null); };
            //assert
            action.Should().Throw<ArgumentNullException>();
        }
        [TestMethod]
        public void GetOffer_IfFound_ShouldNotBeNull()
        {
            //arrange
            var orchestrator = new OfferOrchestrator();
            orchestrator.itemOffers.Add(new ItemOffer { SKU = "A99", Quantity = 3, OfferPrice = 1.30m });
            //act
            var offerAdded = orchestrator.GetOffer("A99");
            //assert
            offerAdded.Should().NotBeNull();
        }
        
    }
}
