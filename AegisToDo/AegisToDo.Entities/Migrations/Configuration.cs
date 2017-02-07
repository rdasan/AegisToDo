namespace AegisToDo.Entities.Migrations
{
    using DatabaseContext;
    using Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class Configuration : DbMigrationsConfiguration<ToDoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ToDoContext context)
        {
            //Test Data - debug test
            var milkItem = new ToDoItem
            {
                ItemId = 1,
                Title = "Buy Milk",
                Details = "Buy 2 gallon of milk from Safeway",
                DueDate = new DateTime(2017, 2, 8),
                IsCompleted = false
            };

            var laundryItem = new ToDoItem
            {
                ItemId = 2,
                Title = "Pick clothes from dry cleaner",
                Details = "Pick all the 4 pieces of suit from laundry",
                DueDate = new DateTime(2017, 1, 29),
                IsCompleted = true
            };

            var libraryItem = new ToDoItem
            {
                ItemId = 3,
                Title = "Renew library books",
                DueDate = new DateTime(2017, 3, 23),
                IsCompleted = false
            };

            var homeworkItem = new ToDoItem
            {
                ItemId = 4,
                Title = "Finish Homework",
                DueDate = new DateTime(2017, 1, 5),
                IsCompleted = false
            };

            context.ToDoItems.AddOrUpdate(milkItem, laundryItem, libraryItem, homeworkItem);

            context.SaveChanges();
        }
    }
}
