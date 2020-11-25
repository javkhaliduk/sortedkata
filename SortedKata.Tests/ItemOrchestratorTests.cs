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
    }
}
