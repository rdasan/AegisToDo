using System.ComponentModel.DataAnnotations;

namespace AegisToDo.Models
{
    public class ToDoItem
    {
        public int ItemId { get; set; }

        [Required]
        public string Title { get; set; }

        public bool IsDone { get; set; }

        public bool IsOverDue { get; set; }
    }
}