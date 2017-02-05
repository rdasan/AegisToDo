using System.Data.Entity;
using AegisToDo.Entities.Model;

namespace AegisToDo.Entities.DatabaseContext
{
    public interface IToDoContext
    {
        DbSet<ToDoItem> ToDoItems { get; set; }
    }
}