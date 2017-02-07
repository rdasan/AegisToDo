using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AegisToDo.Controllers;
using AegisToDo.Repository;
using Moq;
using System.Web.Mvc;
using AegisToDo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AegisToDo.Tests
{
    [TestClass]
    public class ToDoControllerTests
    {
        [TestMethod]
        public void IndexShouldReturnValidViewBag()
        {
            var mockRepository = new Mock<IToDoItemsRepository>();

            var controller = new ToDoController(mockRepository.Object);

            var result = controller.Index().Result as ViewResult;

            Assert.IsNotNull(result.ViewBag);

        }

        [TestMethod]
        public void IndexShouldReturnValidModel()
        {
            var mockRepository = new Mock<IToDoItemsRepository>();

            var toDoItemsList = new List<ToDoItem>
            {
                new ToDoItem
                {
                    ItemId = 1,
                    Title = "Task1",
                    DueDate = new DateTime(2017,2,3),
                    IsDone = false
                },
                new ToDoItem
                {
                    ItemId = 2,
                    Title = "Task2",
                    DueDate = new DateTime(2017,4,5),
                    IsDone = true
                }
            };

            mockRepository.Setup(repo => repo.GetItems()).ReturnsAsync(toDoItemsList);

            var controller = new ToDoController(mockRepository.Object);

            var result = controller.Index().Result as ViewResult;

            var model = (List<ToDoItem>)result.ViewData.Model;

            Assert.AreEqual(toDoItemsList.Count, model.Count);
            Assert.AreEqual(toDoItemsList.First().ItemId, model.FirstOrDefault()?.ItemId);
            Assert.AreEqual(toDoItemsList.First().Title, model.FirstOrDefault()?.Title);
        }

        [TestMethod]
        public void CreateShouldReturnCreatePartialView()
        {
            var mockRepository = new Mock<IToDoItemsRepository>();

            var controller = new ToDoController(mockRepository.Object);

            var result = controller.Create() as PartialViewResult; ;

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void AddItemShouldReturnErrorForInvalidModel()
        {
            var mockRepository = new Mock<IToDoItemsRepository>();

            var controller = new ToDoController(mockRepository.Object);

            var result = controller.AddItem(null).Result as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void AddItemShouldRedirectRouteToIndexAfterSuccessfullAddition()
        {
            var mockRepository = new Mock<IToDoItemsRepository>();

            var addedItem = new ToDoItem
            {
                ItemId = 1,
                Title = "Item1",
                DueDate = DateTime.Now
            };

            mockRepository.Setup(repo => repo.AddItem(It.IsAny<ToDoItem>())).ReturnsAsync(addedItem);

            var controller = new ToDoController(mockRepository.Object);

            var result = controller.AddItem(addedItem).Result as RedirectToRouteResult;

            mockRepository.Verify(repo => repo.AddItem(It.IsAny<ToDoItem>()), Times.Once);

            Assert.AreEqual("Index", result.RouteValues.FirstOrDefault().Value);
        }
    }
}
