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
                IsOverDue = IsItemOverDue()
            };
        }

        private static bool IsItemOverDue()
        {
            //TODO: Add a Due Date column to the Database Table and check against that
            return false;
        }
    }
}