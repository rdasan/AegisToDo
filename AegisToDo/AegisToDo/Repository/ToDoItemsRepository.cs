using AegisToDo.Entities.DatabaseContext;
using AegisToDo.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AegisToDo.Repository
{
    public class ToDoItemsRepository : IToDoItemsRepository
    {
        private IToDoContext context;

        public ToDoItemsRepository(IToDoContext todoContext)
        {
            context = todoContext;
        }

        public async Task<IEnumerable<ToDoItem>> GetItems()
        {
            var toDoItemList = new List<ToDoItem>();

            using (context)
            {
                var toDoitems = await context.ToDoItems.ToListAsync();
                toDoitems.ForEach(item => toDoItemList.Add(item.ToToDoItemViewModel()));           
            }

            return toDoItemList.Any() ? toDoItemList : Enumerable.Empty<ToDoItem>();                
        }

        public async Task<ToDoItem> GetItemById(int itemId)
        {
            using (context)
            {
                var item = await context.ToDoItems.FirstOrDefaultAsync(toDoItem => toDoItem.ItemId == itemId);

                return item?.ToToDoItemViewModel();
            }
        }

        public async Task<ToDoItem> AddItem(ToDoItem itemToAdd)
        {
            using (context)
            {
                var item = new Entities.Model.ToDoItem
                {
                    Title = itemToAdd.Title,
                    Details = itemToAdd.Details,
                    DueDate = itemToAdd.DueDate,
                    IsCompleted = false
                };

                context.ToDoItems.Add(item);
                await context.SaveChangesAsync();

                return item?.ToToDoItemViewModel();
            }                 
        }

        public async Task<ToDoItem> UpdateItem(ToDoItem itemToUpdate)
        {
            using (context)
            {
                var existingItem = await context.ToDoItems.FirstOrDefaultAsync(item => item.ItemId == itemToUpdate.ItemId);

                if(existingItem != null)
                {
                    existingItem.Title = itemToUpdate.Title;
                    existingItem.IsCompleted = itemToUpdate.IsDone;
                    existingItem.DueDate = itemToUpdate.DueDate;
                    existingItem.Details = itemToUpdate.Details;
                    await context.SaveChangesAsync();                    
                }

                return existingItem?.ToToDoItemViewModel();
            }            
        }

        public async Task<ToDoItem> UpdateItemState(int itemId)
        {
            using (context)
            {
                var item = await context.ToDoItems
                                        .FirstOrDefaultAsync(toDoItem => toDoItem.ItemId == itemId);

                if (item != null)
                {
                    item.IsCompleted = item.IsCompleted ? false : true;
                    await context.SaveChangesAsync();
                }

                return item?.ToToDoItemViewModel();
            }
        }

        public async Task DeleteItem(int itemId)
        {
            using (context)
            {
                var item = await context.ToDoItems
                                        .FirstOrDefaultAsync(toDoItem => toDoItem.ItemId == itemId);

                if (item != null)
                {
                    context.ToDoItems.Remove(item);
                    await context.SaveChangesAsync();
                }                
            }
        }
    }
}