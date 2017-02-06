using System.Collections.Generic;
using System.Threading.Tasks;
using AegisToDo.Models;

namespace AegisToDo.Repository
{
    public interface IToDoItemsRepository
    {
        Task<ToDoItem> AddItem(ToDoItem itemToAdd);

        Task DeleteItem(int itemId);

        Task<ToDoItem> GetItemById(int itemId);

        Task<IEnumerable<ToDoItem>> GetItems();

        Task<ToDoItem> UpdateItem(ToDoItem itemToUpdate);

        Task<ToDoItem> UpdateItemState(int itemId);
    }
}