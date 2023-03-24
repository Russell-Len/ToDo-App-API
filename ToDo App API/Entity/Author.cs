using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo_App_API.Entity
{
    [Table("authors")]
    public class Author
    {
        [Key, Required]
        public int AuthorId { get; set; }

        [Required, RegularExpression(@"^\S*$/")]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

    }
}
