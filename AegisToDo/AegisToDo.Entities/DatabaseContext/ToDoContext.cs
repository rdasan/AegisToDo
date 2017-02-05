using AegisToDo.Entities.Model;
using System.Data.Entity;
using System.Threading.Tasks;

namespace AegisToDo.Entities.DatabaseContext
{
    public class ToDoContext : DbContext, IToDoContext
    {
        public ToDoContext() : base()
        {

        }

        public DbSet<ToDoItem> ToDoItems { get; set; }

        public override Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
