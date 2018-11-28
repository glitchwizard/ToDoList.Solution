using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoList.Controllers;
using ToDoList.Models;
using System;


namespace ToDoList.Tests
{
    [TestClass]
    public class CategoryTest : IDisposable
    {
        public void Dispose()
        {
            Item.ClearAll();
        }

        [TestMethod]
        public void CategoryConstructor_CreatesInstanceOfCategory_Category()
        {
            // Arrange
            Category newCategory = new Category("test category");
            // Act

            //Assert
            Assert.AreEqual(typeof(Category), newCategory.GetType());
        }
    }
}
