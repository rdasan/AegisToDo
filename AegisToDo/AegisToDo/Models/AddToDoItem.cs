using System;
using System.ComponentModel.DataAnnotations;

namespace AegisToDo.Models
{
    public class AddToDoItem
    {
        [Required]
        [MaxLength(255, ErrorMessage = "The title is too long")]
        public string Title { get; set; }

        public string Details { get; set; }

        public DateTime DueDate { get; set; }

    }
}