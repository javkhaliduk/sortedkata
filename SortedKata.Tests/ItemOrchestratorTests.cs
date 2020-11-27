using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortedKata.BLL.Implementation;
using SortedKata.BLL.Models;
using System;

namespace SortedKata.Tests
{
    [TestClass]
    public class ItemOrchestratorTests
    {
        [TestMethod]
        public void AddItem_IfArgumentIsNull_ShouldThrowArgumentNullException()
        {
            //arrange
            var orchestrator = new ItemOrchestrator();
            //act
            Func<bool> action = () => { return orchestrator.AddItem(null); };
            //assert
            action.Should().Throw<ArgumentNullException>();
        }
        [TestMethod]
        public void GetItem_IfSKUNullOrEmpty__ShouldThrowArgumentNullException()
        {
            //arrange
            var orchestrator = new ItemOrchestrator();
            //act
            Func<Item> action = () => { return orchestrator.GetItem(null); };
            //assert
            action.Should().Throw<ArgumentNullException>();
        }
        [TestMethod]
        public void GetItem_IfSKUExists__ShouldReturnItem()
        {
            //arrange
            var orchestrator = new ItemOrchestrator();
            var expected = new Item { SKU = "A99", Price = 0.50m };
            orchestrator._listItems.Add(expected);
            //act
            var item= orchestrator.GetItem("A99"); ;
            //assert
            item.Should().NotBeNull();
            item.Should().Equals(expected);
            
        }
    }
}
