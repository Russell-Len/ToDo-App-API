using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo_App_API.Entity
{
    [Table("authors")]
    public class Author
    {
        [Key, Required]
        public int AuthorId { get; set; }

        [Required, MaxLength(30), MinLength(1)]
        public string Username { get; set; } = string.Empty;

        [Required, MinLength(8)]
        public string Password { get; set; } = string.Empty;

    }
}
