using System;
using AegisToDo.Models;

namespace AegisToDo.Repository
{
    public static class ModelMapper
    {
        public static ToDoItem ToToDoItemViewModel(this Entities.Model.ToDoItem toDoItem)
        {
            return new ToDoItem
            {
                ItemId = toDoItem.ItemId,
                Title = toDoItem.Title,
                IsDone = toDoItem.IsCompleted,
                Details = toDoItem.Details,
                DueDate = toDoItem.DueDate,
                IsOverDue = toDoItem.IsItemOverDue()
            };
        }

        private static bool IsItemOverDue(this Entities.Model.ToDoItem item)
        {
            if(item.DueDate < DateTime.Now && !item.IsCompleted)
            {
                return true;
            }
            return false;
        }
    }
}