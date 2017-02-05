using System;
using System.ComponentModel.DataAnnotations;

namespace AegisToDo.Entities.Model
{
    public class ToDoItem
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Details { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }

    }
}
