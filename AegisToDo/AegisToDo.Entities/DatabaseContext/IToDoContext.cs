using System.Data.Entity;
using System.Threading.Tasks;
using AegisToDo.Entities.Model;
using System;

namespace AegisToDo.Entities.DatabaseContext
{
    public interface IToDoContext : IDisposable
    {
        DbSet<ToDoItem> ToDoItems { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}