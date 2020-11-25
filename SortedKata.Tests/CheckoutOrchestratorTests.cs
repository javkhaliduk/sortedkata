using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortedKata.BLL.Implementation;
using System;

namespace SortedKata.Tests
{
    [TestClass]
    public class CheckoutOrchestratorTests
    {
        [TestMethod]
        public void AddItemsToCheckout_ShouldReturnTrue()
        {
            // arrange
            var orchestrator = new CheckoutOrchestrator();
            //act
            Func<bool> action = ()=> { return orchestrator.ScanItem(null); };
            //assert
            action.Should().Throw<ArgumentNullException>();
        }
        [TestMethod]
        public void CalculateDiscount_IfSKUEmptyOrNull_ShouldThrowArgumentNullException()
        {
            // arrange
            var orchestrator = new CheckoutOrchestrator();
            //act
            Func<decimal> action = () =>{
                return orchestrator.CalculateDiscount(null);
            };

            //assert
            action.Should().Throw<ArgumentNullException>();
        }
    }
}
