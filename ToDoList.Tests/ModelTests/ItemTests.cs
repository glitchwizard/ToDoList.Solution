using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;

namespace ToDoList.Tests
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void GetId_ItemsInstantiateWithAnIdAndGetterReturns_Int()
        {
            //Arrange
            string description = "Walk the dog.";
            Item newItem = new Item(description);

            //Act
            int result = newItem.GetId();

            //Assert
            Assert.AreEqual(1, result);

        }
    }
}