using System.ComponentModel.DataAnnotations;

namespace AegisToDo.Models
{
    public class AddToDoItemRequest
    {
        [Required]
        [MaxLength(255, ErrorMessage = "The title is too long")]
        public string Title { get; set; }
    }
}