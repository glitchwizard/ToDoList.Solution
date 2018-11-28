using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;

namespace ToDoList.Tests
{
    [TestClass]
    public class ItemTests : IDisposable
    {
        public void Dispose()
        {
            Item.ClearAll();
        }

        [TestMethod]
        public void GetId_ItemsInstantiateWithAnIdAndGetterReturns_Int()
        {
            //Arrange
            string description = "Walk the dog.";
            Item newItem = new Item(description);

            //Act
            int result = newItem.GetId();
            Console.WriteLine("This is the result: " + result);
            //Assert
            Assert.AreEqual(1, result);

        }

        [TestMethod]
        public void Find_ReturnsCorrect_Item()
        {
            //Arrange
            string description01 = "Walk the dog";
            string description02 = "Wash the dishes";
            Item newItem1 = new Item(description01);
            Item newItem2 = new Item(description02);
            Console.WriteLine(newItem1.GetId());
            Console.WriteLine(newItem2.GetId());
            //Act
            Item result = Item.Find(2);

            //Assert
            Assert.AreEqual(newItem2, result);
        }
    }
}