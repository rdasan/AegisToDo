namespace AegisToDo.Entities.Migrations
{
    using Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ToDoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ToDoContext context)
        {
            var milkItem = new ToDoItem
            {
                ItemId = 1,
                Title = "Buy Milk",
                IsCompleted = false
            };

            var laundryItem = new ToDoItem
            {
                ItemId = 2,
                Title = "Pick clothes from dry cleaner",
                IsCompleted = true
            };

            var libraryItem = new ToDoItem
            {
                ItemId = 3,
                Title = "Renew library books",
                IsCompleted = false
            };

            context.ToDoItems.AddOrUpdate(milkItem, laundryItem, libraryItem);

            context.SaveChanges();
        }
    }
}
