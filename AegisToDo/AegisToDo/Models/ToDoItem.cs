using System;
using System.ComponentModel.DataAnnotations;

namespace AegisToDo.Models
{
    public class ToDoItem
    {
        public int ItemId { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Title is too long")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Details { get; set; }

        [Display(Name = "Due Date")]
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }

        [Display(Name = "Mark Done")]
        public bool IsDone { get; set; }

        public bool IsOverDue { get; set; }
    }
}