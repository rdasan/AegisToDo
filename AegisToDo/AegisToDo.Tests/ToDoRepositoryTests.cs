using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AegisToDo.Repository;
using Moq;
using AegisToDo.Models;
using System.Collections.Generic;
using System.Linq;
using AegisToDo.Entities.DatabaseContext;
using EntityModel = AegisToDo.Entities.Model;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using AegisToDo.Tests.TestDoubles;

namespace AegisToDo.Tests
{
    [TestClass]
    public class ToDoRepositoryTests
    {
        [TestMethod]
        public void GetItemsReturnsListOfValidToDoItemViewModel()
        {
            var toDoItemEntityList = new List<EntityModel.ToDoItem>
            {
                new EntityModel.ToDoItem
                {
                    ItemId = 1,
                    Title = "Item 1",
                    DueDate = DateTime.Now,
                    IsCompleted = false
                },
                new EntityModel.ToDoItem
                {
                    ItemId = 2,
                    Title = "Item 2",
                    DueDate = DateTime.Now,
                    IsCompleted = true
                }
            };

            var mockToDoDbSet = GetMockedDbSet(toDoItemEntityList);

            var mockContext = new Mock<IToDoContext>();

            mockContext.Setup(context => context.ToDoItems).Returns(mockToDoDbSet);

            var repository = new ToDoItemsRepository(mockContext.Object);

            var result = repository.GetItems().Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(toDoItemEntityList[0].Title, result.FirstOrDefault()?.Title);
            Assert.AreEqual(toDoItemEntityList[0].ItemId, result.FirstOrDefault()?.ItemId);
            Assert.AreEqual(toDoItemEntityList[0].DueDate, result.FirstOrDefault()?.DueDate);
        }

        [TestMethod]
        public void AddItemSavesItemAndReturnsValidModel()
        {
            var itemToAdd = new ToDoItem
            {
                Title = "New Item",
                DueDate = new DateTime(2020, 12, 30),
            };     

            var mockContext = new Mock<IToDoContext>();

            mockContext.Setup(context => context.ToDoItems.Add(It.IsAny<EntityModel.ToDoItem>()))
                .Returns(new EntityModel.ToDoItem());

            var repository = new ToDoItemsRepository(mockContext.Object);

            var result = repository.AddItem(itemToAdd).Result;

            mockContext.Verify(context => context.ToDoItems.Add(It.IsAny<EntityModel.ToDoItem>()), Times.Once);
            mockContext.Verify(context => context.SaveChangesAsync(), Times.Once);
            Assert.AreEqual(itemToAdd.Title, result.Title);
            Assert.AreEqual(itemToAdd.DueDate, result.DueDate);
            Assert.IsFalse(result.IsDone);
            Assert.IsFalse(result.IsOverDue);
        }

        [TestMethod]
        public void AddItemSetsItemAsOverDueIfDueDateIsBeforeToday()
        {
            var itemToAdd = new ToDoItem
            {
                Title = "New Item",
                DueDate = new DateTime(1900, 12, 30),
            };

            var mockContext = new Mock<IToDoContext>();

            mockContext.Setup(context => context.ToDoItems.Add(It.IsAny<EntityModel.ToDoItem>()))
                .Returns(new EntityModel.ToDoItem());

            var repository = new ToDoItemsRepository(mockContext.Object);

            var result = repository.AddItem(itemToAdd).Result;

            mockContext.Verify(context => context.ToDoItems.Add(It.IsAny<EntityModel.ToDoItem>()), Times.Once);
            mockContext.Verify(context => context.SaveChangesAsync(), Times.Once);

            Assert.IsFalse(result.IsDone);
            Assert.IsTrue(result.IsOverDue);
        }

        [TestMethod]
        public void UpdateItemSavesItemAndReturnsValidModel()
        {
            var itemToUpdate = new ToDoItem
            {
                ItemId = 1,
                Title = "Updated Item",
                DueDate = DateTime.Now,
                IsDone = false
            };

            var existingEntityItem = new EntityModel.ToDoItem
            {
                ItemId = itemToUpdate.ItemId,
                Title = "Old Title",
                DueDate = itemToUpdate.DueDate,
                IsCompleted = itemToUpdate.IsDone
            };

            var mockToDoDbSet = GetMockedDbSet(new [] { existingEntityItem });

            var mockContext = new Mock<IToDoContext>();
            
            mockContext.Setup(context => context.ToDoItems).Returns(mockToDoDbSet);

            var repository = new ToDoItemsRepository(mockContext.Object);

            var result = repository.UpdateItem(itemToUpdate).Result;

            mockContext.Verify(context => context.SaveChangesAsync(), Times.Once);
            Assert.AreEqual(itemToUpdate.Title, result.Title);
            Assert.AreEqual(itemToUpdate.DueDate, result.DueDate);
            Assert.AreEqual(itemToUpdate.IsDone, result.IsDone);
        }

        [TestMethod]
        public void UpdateItemStateSetsDoneStateCorrectlyOnItem()
        {
            var existingEntityItem = new EntityModel.ToDoItem
            {
                ItemId = 1,
                Title = "Item 1",
                DueDate = DateTime.Now,
                IsCompleted = false,
            };

            var mockToDoDbSet = GetMockedDbSet(new[] { existingEntityItem });

            var mockContext = new Mock<IToDoContext>();

            mockContext.Setup(context => context.ToDoItems).Returns(mockToDoDbSet);

            var repository = new ToDoItemsRepository(mockContext.Object);

            var result = repository.UpdateItemState(existingEntityItem.ItemId).Result;

            mockContext.Verify(context => context.SaveChangesAsync(), Times.Once);
            Assert.AreNotSame(existingEntityItem.IsCompleted, result.IsDone);
        }

        [TestMethod]
        public void DeleteItemRemovesItem()
        {
            var existingEntityItem = new EntityModel.ToDoItem
            {
                ItemId = 1,
                Title = "Item 1",
                DueDate = DateTime.Now,
                IsCompleted = false,
            };

            var mockToDoDbSet = GetMockedDbSet(new[] { existingEntityItem });

            var mockContext = new Mock<IToDoContext>();

            mockContext.Setup(context => context.ToDoItems).Returns(mockToDoDbSet);

            var repository = new ToDoItemsRepository(mockContext.Object);

            var result = repository.DeleteItem(existingEntityItem.ItemId);

            mockContext.Verify(context => context.SaveChangesAsync(), Times.Once);
        }

        private DbSet<EntityModel.ToDoItem> GetMockedDbSet(IEnumerable<EntityModel.ToDoItem> toDoModel)
        {
            var model = toDoModel.AsQueryable();

            var toDoEntityMock = new Mock<DbSet<EntityModel.ToDoItem>>();

            toDoEntityMock.As<IDbAsyncEnumerable<EntityModel.ToDoItem>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<EntityModel.ToDoItem>(model.GetEnumerator()));

            toDoEntityMock.As<IQueryable<EntityModel.ToDoItem>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<EntityModel.ToDoItem>(model.Provider));

            toDoEntityMock.As<IQueryable<EntityModel.ToDoItem>>().Setup(m => m.Expression).Returns(model.Expression);
            toDoEntityMock.As<IQueryable<EntityModel.ToDoItem>>().Setup(m => m.ElementType).Returns(model.ElementType);
            toDoEntityMock.As<IQueryable<EntityModel.ToDoItem>>().Setup(m => m.GetEnumerator()).Returns(model.GetEnumerator());

            return toDoEntityMock.Object;
        }
    }
}
