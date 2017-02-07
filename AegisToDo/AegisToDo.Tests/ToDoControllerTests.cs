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

            var result = controller.Create() as PartialViewResult; 

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void AddItemShouldReturnErrorForInvalidModel()
        {
            var mockRepository = new Mock<IToDoItemsRepository>();

            var controller = new ToDoController(mockRepository.Object);

            var actionResult = controller.AddItem(null).Result;

            var viewResult = actionResult as PartialViewResult;

            Assert.IsNull(viewResult);

            var httpResult = actionResult as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, httpResult.StatusCode);
        }

        [TestMethod]
        public void AddItemShouldAddItemAndRedirectRouteToIndex()
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

        [TestMethod]
        public void GetShouldReturnEditPartialView()
        {
            var mockRepository = new Mock<IToDoItemsRepository>();

            var controller = new ToDoController(mockRepository.Object);

            var result = controller.Get(5).Result as PartialViewResult;

            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void GetShouldReturnErrorForInvalidId()
        {
            var mockRepository = new Mock<IToDoItemsRepository>();

            var controller = new ToDoController(mockRepository.Object);

            var actionResult = controller.Get(null).Result;

            var viewResult = actionResult as PartialViewResult;

            Assert.IsNull(viewResult);

            var httpResult = actionResult as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, httpResult.StatusCode);
        }

        [TestMethod]
        public void GetShouldReturnValidModel()
        {
            var mockRepository = new Mock<IToDoItemsRepository>();

            var item = new ToDoItem
            {
                ItemId = 1,
                Title = "Item 1",
                DueDate = DateTime.Now,
                IsDone = false
            };

            mockRepository.Setup(repo => repo.GetItemById(It.IsAny<int>())).ReturnsAsync(item);

            var controller = new ToDoController(mockRepository.Object);

            var result = controller.Get(1).Result as PartialViewResult;

            Assert.IsNotNull(result.Model);

            var model = (ToDoItem)result.Model;

            Assert.AreEqual(item.ItemId, model.ItemId);
            Assert.AreEqual(item.Title, model.Title);
            Assert.AreEqual(item.DueDate, model.DueDate);
        }

        [TestMethod]
        public void UpdateItemShouldReturnErrorForInvalidModel()
        {
            var mockRepository = new Mock<IToDoItemsRepository>();

            var controller = new ToDoController(mockRepository.Object);

            var actionResult = controller.UpdateItem(null).Result;

            var viewResult = actionResult as PartialViewResult;

            Assert.IsNull(viewResult);

            var httpResult = actionResult as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, httpResult.StatusCode);
        }

        [TestMethod]
        public void UpdateItemShouldUpdateExistingItemAndRedirectRouteToIndex()
        {
            var mockRepository = new Mock<IToDoItemsRepository>();

            var item = new ToDoItem
            {
                ItemId = 1,
                Title = "Item1",
                DueDate = DateTime.Now
            };

            mockRepository.Setup(repo => repo.UpdateItem(It.IsAny<ToDoItem>())).ReturnsAsync(item);

            var controller = new ToDoController(mockRepository.Object);

            var result = controller.UpdateItem(item).Result as RedirectToRouteResult;

            mockRepository.Verify(repo => repo.UpdateItem(It.IsAny<ToDoItem>()), Times.Once);
            Assert.AreEqual("Index", result.RouteValues.FirstOrDefault().Value);
        }

        [TestMethod]
        public void UpdateStateShouldReturnErrorForInvalidItemId()
        {
            var mockRepository = new Mock<IToDoItemsRepository>();

            var controller = new ToDoController(mockRepository.Object);

            var actionResult = controller.UpdateState(null).Result;

            var viewResult = actionResult as PartialViewResult;

            Assert.IsNull(viewResult);

            var httpResult = actionResult as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, httpResult.StatusCode);
        }

        [TestMethod]
        public void UpdateStateShouldUpdateStateOfExistingItemAndRedirectRouteToIndex()
        {
            var mockRepository = new Mock<IToDoItemsRepository>();

            var item = new ToDoItem
            {
                ItemId = 1,
                Title = "Item1",
                DueDate = DateTime.Now
            };

            mockRepository.Setup(repo => repo.UpdateItemState(It.IsAny<int>())).ReturnsAsync(item);

            var controller = new ToDoController(mockRepository.Object);

            var result = controller.UpdateState(item.ItemId).Result as RedirectToRouteResult;

            mockRepository.Verify(repo => repo.UpdateItemState(It.IsAny<int>()), Times.Once);
            Assert.AreEqual("Index", result.RouteValues.FirstOrDefault().Value);
        }

        [TestMethod]
        public void DeleteShouldReturnErrorForInvalidItemId()
        {
            var mockRepository = new Mock<IToDoItemsRepository>();

            var controller = new ToDoController(mockRepository.Object);

            var actionResult = controller.Delete(null).Result;

            var viewResult = actionResult as PartialViewResult;

            Assert.IsNull(viewResult);

            var httpResult = actionResult as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, httpResult.StatusCode);
        }

        [TestMethod]
        public void DeleteShouldDeleteExistingItemAndRedirectRouteToIndex()
        {
            var mockRepository = new Mock<IToDoItemsRepository>();

            var controller = new ToDoController(mockRepository.Object);

            var result = controller.Delete(5).Result as RedirectToRouteResult;

            mockRepository.Verify(repo => repo.DeleteItem(It.IsAny<int>()), Times.Once);
            Assert.AreEqual("Index", result.RouteValues.FirstOrDefault().Value);
        }
    }
}
