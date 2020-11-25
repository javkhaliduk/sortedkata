using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortedKata.BLL.Implementation;

namespace SortedKata.Tests
{
    [TestClass]
    public class OfferOrchestratorTests
    {
        public void AddOffer_IfSuccessfull_ShouldReturnTrue()
        {
            //arrange
            var orchestrator = new OfferOrchestrator();
            //act
            var offerAdded=orchestrator.AddOffer(null);
            //assert
            offerAdded.Should().BeTrue();
        }
        public void GetOffer_IfFound_ShouldNotBeNull()
        {
            //arrange
            var orchestrator = new OfferOrchestrator();
            //act
            var offerAdded = orchestrator.GetOffer(null);
            //assert
            offerAdded.Should().NotBeNull();
        }
    }
}
