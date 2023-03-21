using System.ComponentModel.DataAnnotations;

namespace ToDo_App_API.Model
{
    public class TaskToEditModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public string Category { get; set; } = string.Empty;
    }
}
