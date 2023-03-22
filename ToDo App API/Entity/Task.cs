using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo_App_API.Entity
{
    [Table("task")]
    public class Task
    {
        [Key, Required]
        public int Id { get; set; }

        [Required, MaxLength(250)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        [Required, MaxLength(2000)]
        public string Category { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public int AuthorId { get; set; }
    }
}
