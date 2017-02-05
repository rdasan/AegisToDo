using AegisToDo.Entities.Model;
using System.Data.Entity;

namespace AegisToDo.Entities
{
    public class ToDoContext : DbContext
    {
        public ToDoContext() : base()
        {

        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
