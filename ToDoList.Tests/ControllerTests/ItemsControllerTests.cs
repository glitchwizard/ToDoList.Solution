using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoList.Controllers;
using ToDoList.Models;
using System;


namespace ToDoList.Tests
{
    [TestClass]
    public class ItemControllerTest : IDisposable
    {
        public void Dispose()
        {
            Item.ClearAll();
        }

        [TestMethod]
        public void DontHaveATestYet()
        {
            // Arrange

            // Act

            //Assert
        }
    }
}
